using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core.Deserializers;

namespace Octokit.GraphQL.Core
{
    public class PagedQuery<TResult> : ICompiledQuery<TResult>
    {
        public PagedQuery(
            CompiledQuery<TResult> masterQuery,
            IReadOnlyList<Subquery> subqueries)
        {
            MasterQuery = masterQuery;
            Subqueries = subqueries;
        }

        public CompiledQuery<TResult> MasterQuery { get; }
        public IReadOnlyList<Subquery> Subqueries { get; }

        public IQueryRunner<TResult> Start(IConnection connection, Dictionary<string, object> variables)
        {
            return new Runner(this, connection, variables);
        }

        public string ToString(int indentation)
        {
            return MasterQuery.ToString(indentation);
        }

        IQueryRunner ICompiledQuery.Start(IConnection connection, Dictionary<string, object> variables)
        {
            return Start(connection, variables);
        }

        class Runner : IQueryRunner<TResult>, IPagedQueryContext
        {
            readonly PagedQuery<TResult> parent;
            readonly IConnection connection;
            readonly Dictionary<string, object> variables;
            readonly ResponseDeserializer deserializer = new ResponseDeserializer();
            Dictionary<Subquery, IQueryRunner> subqueryRunners;
            Dictionary<Subquery, IList> subqueryResults;

            public Runner(
                PagedQuery<TResult> parent,
                IConnection connection,
                Dictionary<string, object> variables)
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
                    subqueryRunners = new Dictionary<Subquery, IQueryRunner>();
                    subqueryResults = new Dictionary<Subquery, IList>();

                    // This is the first run, so run the master page.
                    var master = parent.MasterQuery;
                    var data = await connection.Run(master.GetPayload(variables));
                    var json = JObject.Parse(data);

                    json.AddAnnotation(this);
                    Result = deserializer.Deserialize(master.CompiledExpression, json);

                    // Look through each subquery for any results that have a next page.
                    foreach (var subquery in parent.Subqueries)
                    {
                        // TODO: Don't compile every time.
                        var pageInfo = subquery.ParentPageInfo.Compile()(json);

                        if ((bool)pageInfo["hasNextPage"] == true)
                        {
                            subqueryRunners.Add(subquery, subquery.Query.Start(connection, variables));
                        }
                    }
                }
                else
                {
                    var item = subqueryRunners.First();
                    var subquery = item.Key;
                    var runner = item.Value;

                    if (await runner.RunPage() == false)
                    {
                        var list = subqueryResults[subquery];
                        subqueryRunners.Remove(subquery);

                        foreach (var i in (IEnumerable)runner.Result)
                        {
                            list.Add(i);
                        }
                    }
                }

                return subqueryRunners.Count > 0;
            }

            public void SetQueryResultSink(Subquery query, IList result)
            {
                subqueryResults.Add(query, result);
            }
        }
    }
}
