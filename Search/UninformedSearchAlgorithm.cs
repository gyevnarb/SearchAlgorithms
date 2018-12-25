// https://projectbalint.com/en/page/uninformed-search.html
// UninformedSearchAlgorithms.cs
//
// Implementations of several uninformed search algorithms
// Copyright (c) 2018 Balint Gyevnar

using System;
using System.Collections.Generic;
using Priority_Queue;

namespace UninformedSearch.Search
{
    enum SearchType
    {
        BFS,
        DFS,
        UCS,
        DLS,
        IDS
    }

    /// <summary>
    /// Implementations of several well-known uninformed search algorithms
    /// </summary>
    class UninformedSearchAlgorithm
    {
        private SearchType type = SearchType.BFS;

        /// <summary>
        /// Initialise a standard BFS search
        /// </summary>
        public UninformedSearchAlgorithm() => type = SearchType.BFS;

        /// <summary>
        /// Initialise a search with a specific search method
        /// </summary>
        /// <param name="_type">SearchType specifing the search method</param>
        public UninformedSearchAlgorithm(SearchType _type) => type = _type;

        /// <summary>
        /// Run uninformed search
        /// </summary>
        /// <param name="problem">The problem definition</param>
        /// <param name="max_depth">Optional parameter. Max depth limit of DLS</param>
        /// <returns>A Node with a goal state</returns>
        public Node Search(IProblem problem, int max_depth = 0)
        {
            switch (type)
            {
                case SearchType.BFS: return BreadthFirstSearch(problem);
                case SearchType.DFS: return DepthFirstSearch(problem);
                case SearchType.UCS: return UniformCostSearch(problem);
                case SearchType.DLS: return DepthLimitedSearch(problem, max_depth);
                case SearchType.IDS: return IterativeDeepeningSearch(problem);
                default: throw new Exception($"Unknown search type: {type.ToString()}");
            }
        }

        /*
         * The following methods are standard implementations of the respective 
         * search algorithms based on the textbook by Russel and Norvig.
         */

        private Node BreadthFirstSearch(IProblem problem)
        {
            HashSet<IState> visited = new HashSet<IState>();
            Queue<Node> frontier = new Queue<Node>();
            frontier.Enqueue(problem.InitialNode);
            visited.Add(problem.InitialNode.State);

            while (true)
            {
                if (frontier.Count == 0)
                    return Node.FAILURE;

                Node current = frontier.Dequeue();
                visited.Add(current.State);
                if (problem.GoalTest(current))
                    return current;

                foreach (IAction action in problem.LegalActions(current))
                {
                    Node child = current.ChildNode(problem, action);
                    if (!visited.Contains(child.State) && !frontier.Contains(child))
                    {
                        if (problem.GoalTest(child))
                            return child;

                        frontier.Enqueue(child);
                    }
                }
            }
        }

        private Node DepthFirstSearch(IProblem problem)
        {
            HashSet<IState> visited = new HashSet<IState>();
            Stack<Node> frontier = new Stack<Node>();
            frontier.Push(problem.InitialNode);
            visited.Add(problem.InitialNode.State);

            while (true)
            {
                if (frontier.Count == 0)
                    return Node.FAILURE;

                Node current = frontier.Pop();
                visited.Add(current.State);
                if (problem.GoalTest(current))
                    return current;

                foreach (IAction action in problem.LegalActions(current))
                {
                    Node child = current.ChildNode(problem, action);
                    if (!visited.Contains(child.State) && !frontier.Contains(child))
                    {
                        if (problem.GoalTest(child))
                            return child;

                        frontier.Push(child);
                    }
                }
            }
        }

        /*
         * Note: this method uses an external implementation of a PriorityQueue
         * by BlueRaja called OptimizedPriorityQueue
         */

        private Node UniformCostSearch(IProblem problem)
        {
            HashSet<IState> visited = new HashSet<IState>();
            SimplePriorityQueue<Node> frontier = new SimplePriorityQueue<Node>();
            frontier.Enqueue(problem.InitialNode, problem.InitialNode.PathCost);

            while (true)
            {
                if (frontier.Count == 0)
                    return Node.FAILURE;

                Node current = frontier.Dequeue();
                visited.Add(current.State);
                if (problem.GoalTest(current))
                    return current;

                foreach (IAction action in problem.LegalActions(current))
                {
                    Node child = current.ChildNode(problem, action);
                    if (!visited.Contains(child.State) && !frontier.Contains(child))
                        frontier.Enqueue(child, child.PathCost);
                    else if (frontier.Contains(child) && frontier.GetPriority(child) > child.PathCost)
                        frontier.UpdatePriority(child, child.PathCost);
                }
            }
        }

        private Node DepthLimitedSearch(IProblem problem, int max_depth)
        {
            return RecursiveDLS(problem.InitialNode, problem, max_depth);
        }

        private Node RecursiveDLS(Node node, IProblem problem, int limit)
        {
            if (problem.GoalTest(node))
            {
                return node;
            }
            else if (limit == 0)
            {
                return Node.CUTOFF;
            }
            else
            {
                bool cutoff_occurred = false;
                foreach (IAction action in problem.LegalActions(node))
                {
                    Node child = node.ChildNode(problem, action);
                    Node result = RecursiveDLS(child, problem, limit - 1);
                    if (result == Node.CUTOFF)
                        cutoff_occurred = true;
                    else if (result != Node.FAILURE)
                        return result;
                }

                if (cutoff_occurred)
                    return Node.CUTOFF;
                else
                    return Node.FAILURE;
            }

        }

        private Node IterativeDeepeningSearch(IProblem problem)
        {
            int depth = 1;
            int max_depth = 1024;
            while (depth < max_depth)
            {
                Node result = DepthLimitedSearch(problem, depth);
                if (result != Node.CUTOFF)
                    return result;
                depth++;
            }
            return Node.FAILURE;
        }
    }
}
