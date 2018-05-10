using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public override string ToString() => ToString(2);
 
        public string ToString(int indentation)
        {
            return MasterQuery.ToString(indentation);
        }

        IQueryRunner ICompiledQuery.Start(IConnection connection, IDictionary<string, object> variables)
        {
            return Start(connection, variables);
        }

        class Runner : IQueryRunner<TResult>, ISubqueryRunner
        {
            readonly PagedQuery<TResult> owner;
            readonly IConnection connection;
            readonly IDictionary<string, object> variables;
            readonly ResponseDeserializer deserializer = new ResponseDeserializer();
            Dictionary<ISubquery, IQueryRunner> subqueryRunners;
            Dictionary<ISubquery, List<IList>> subqueryResultSinks;

            public Runner(
                PagedQuery<TResult> owner,
                IConnection connection,
                IDictionary<string, object> variables)
            {
                this.owner = owner;
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
                    subqueryResultSinks = new Dictionary<ISubquery, List<IList>>();

                    // This is the first run, so run the master page.
                    var master = owner.MasterQuery;
                    var data = await connection.Run(master.GetPayload(variables));
                    var json = deserializer.Deserialize(data);

                    json.AddAnnotation(this);
                    Result = deserializer.Deserialize(master.ResultBuilder, json);

                    // Look through each subquery for any results that have a next page.
                    foreach (var subquery in owner.Subqueries)
                    {
                        var pageInfos = subquery.ParentPageInfo(json).ToList();
                        var parentIds = subquery.ParentIds(json).ToList();
                        var sinks = subqueryResultSinks[subquery];

                        for (var i = 0; i < pageInfos.Count; ++i)
                        {
                            var pageInfo = pageInfos[i];

                            if ((bool)pageInfo["hasNextPage"] == true)
                            {
                                var id = parentIds[i].ToString();
                                var after = (string)pageInfo["endCursor"];
                                var runner = subquery.Start(connection, id, after, variables, sinks[i]);
                                subqueryRunners.Add(subquery, runner);
                            }
                        }
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
                }

                return subqueryRunners.Count > 0;
            }

            public void SetQueryResultSink(ISubquery query, IList result)
            {
                if (!subqueryResultSinks.TryGetValue(query, out var value))
                {
                    value = new List<IList>();
                    subqueryResultSinks.Add(query, value);
                }

                value.Add(result);
            }
        }
    }
}
