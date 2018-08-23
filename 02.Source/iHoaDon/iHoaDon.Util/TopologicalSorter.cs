using System;
using System.Collections.Generic;

namespace iHoaDon.Util
{
    /// <summary>
    /// Perform a topological sort on a hypothetical Directed Acyclic Graph (DAG).
    /// </summary>
    public static class TopologicalSorter
    {
        /// <summary>
        /// Note: WARNING: This will throw a StackOverflowException if the graph contains any circular dependency, so use this only when you can be ABSOLUTELY sure about your input
        /// Perform a topological sort on a hypothetical Directed Acyclic Graph (DAG).
        /// Implemented according to the depth-first-search-based algorithm here: http://en.wikipedia.org/wiki/Topological_sorting
        /// TODO:detect circular dependencies similar to the algorithm here: http://www.patrickdewane.com/2009/03/topological-sort.html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="getRefs">The get refs.</param>
        /// <returns></returns>
        public static IEnumerable<T> Sort<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getRefs)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (getRefs == null)
            {
                throw new ArgumentNullException("getRefs");
            }

            //set of visited nodes
            var visited = new HashSet<T>();
            var result = new List<T>();

            foreach (var node in source)
            {
                Visit(node, getRefs, visited, result);
            }
            return result;
        }

        private static void Visit<T>(T node, Func<T, IEnumerable<T>> getRefs, ISet<T> visited, ICollection<T> result)
        {
            if (!visited.Contains(node))
            {
                visited.Add(node);
                var dependencies = getRefs(node);
                foreach (var refNode in dependencies)
                {
                    Visit(refNode, getRefs, visited, result);
                }
                result.Add(node);
            }
        }
    }
}