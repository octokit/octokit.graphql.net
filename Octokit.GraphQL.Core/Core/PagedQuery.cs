using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core.Deserializers;

namespace Octokit.GraphQL.Core
{
    public class PagedQuery<TResult> : ICompiledQuery<TResult>
    {
        public PagedQuery(
            SimpleQuery<TResult> masterQuery,
            IEnumerable<ISubquery> subqueries)
        {
            MasterQuery = masterQuery;
            Subqueries = subqueries.ToList();
        }

        public SimpleQuery<TResult> MasterQuery { get; }
        public IReadOnlyList<ISubquery> Subqueries { get; }

        public IQueryRunner<TResult> Start(IConnection connection, IDictionary<string, object> variables)
        {
            return new Runner(this, connection, variables);
        }

        public string ToString(int indentation)
        {
            return MasterQuery.ToString(indentation);
        }

        IQueryRunner ICompiledQuery.Start(IConnection connection, IDictionary<string, object> variables)
        {
            return Start(connection, variables);
        }

        class Runner : IQueryRunner<TResult>, IPagedQueryContext
        {
            readonly PagedQuery<TResult> parent;
            readonly IConnection connection;
            readonly IDictionary<string, object> variables;
            readonly ResponseDeserializer deserializer = new ResponseDeserializer();
            Dictionary<ISubquery, IQueryRunner> subqueryRunners;
            Dictionary<ISubquery, IList> subqueryResults;

            public Runner(
                PagedQuery<TResult> parent,
                IConnection connection,
                IDictionary<string, object> variables)
            {
                this.parent = parent;
                this.connection = connection;
                this.variables = variables;
            }

            public TResult Result { get; private set; }
            object IQueryRunner.Result => Result;

            public async Task<bool> RunPage()
            {
                if (subqueryRunners == null)
                {
                    subqueryRunners = new Dictionary<ISubquery, IQueryRunner>();
                    subqueryResults = new Dictionary<ISubquery, IList>();

                    // This is the first run, so run the master page.
                    var master = parent.MasterQuery;
                    var data = await connection.Run(master.GetPayload(variables));
                    var json = JObject.Parse(data);

                    json.AddAnnotation(this);
                    Result = deserializer.Deserialize(master.ResultBuilder, json);

                    // Look through each subquery for any results that have a next page.
                    foreach (var subquery in parent.Subqueries)
                    {
                        var pageInfos = subquery.ParentPageInfo(json).ToList();
                        var parentIds = subquery.ParentIds(json).ToList();

                        for (var i = 0; i < pageInfos.Count; ++i)
                        {
                            var pageInfo = pageInfos[i];

                            if ((bool)pageInfo["hasNextPage"] == true)
                            {
                                var id = parentIds[i].ToString();
                                var after = (string)pageInfo["endCursor"];
                                var runner = subquery.Start(connection, id, after, variables);
                                subqueryRunners.Add(subquery, runner);
                            }
                        }

                        //foreach (var pageInfo in pageInfos)
                        //{
                        //    if ((bool)pageInfo["hasNextPage"] == true)
                        //    {
                        //        var id = subquery.ParentIds(json).ToString();
                        //        var after = (string)pageInfo["endCursor"];
                        //        var runner = subquery.Start(connection, id, after, variables);
                        //        subqueryRunners.Add(subquery, runner);
                        //    }
                        //}
                    }
                }
                else
                {
                    // Get the next subquery runner.
                    var item = subqueryRunners.First();
                    var subquery = item.Key;
                    var runner = item.Value;

                    // Run its next page and remove from the active runners if finished.
                    if (!await runner.RunPage())
                    {
                        subqueryRunners.Remove(subquery);
                    }

                    // Copy its results to the final results.
                    var data = (IList)runner.Result;
                    var results = subqueryResults[subquery];
                    foreach (var i in data) results.Add(i);
                }

                return subqueryRunners.Count > 0;
            }

            public void SetQueryResultSink(ISubquery query, IList result)
            {
                subqueryResults[query] = result;
            }
        }
    }
}
