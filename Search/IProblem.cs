// https://projectbalint.com/en/page/uninformed-search.html
// IProblem.cs
//
// Interface for representing arbitrary search problems
// Copyright (c) 2018 Balint Gyevnar

using System.Collections.Generic;

namespace UninformedSearch.Search
{
    /// <summary>
    /// An arbitrary search problem
    /// </summary>
    interface IProblem
    {
        /// <summary>
        /// The initial node of the search
        /// </summary>
        Node InitialNode { get; }

        /// <summary>
        /// Apply given action in the given state
        /// </summary>
        /// <param name="s">Original state</param>
        /// <param name="a">Action to perform</param>
        /// <returns>New state with the action performed</returns>
        IState Result(IState s, IAction a);

        /// <summary>
        /// Cost to perform an action from a given state
        /// </summary>
        /// <param name="s">Original state</param>
        /// <param name="a">Action to perform</param>
        /// <returns>The cost of performing the action in the given state</returns>
        int StepCost(IState s, IAction a);

        /// <summary>
        /// Check if given node is a goal state
        /// </summary>
        /// <param name="n">Node to check</param>
        /// <returns>True if and only if the node is a goal state</returns>
        bool GoalTest(Node n);

        /// <summary>
        /// Calculate the legal action that can performed from a given node
        /// </summary>
        /// <param name="n">Original node</param>
        /// <returns>List of legal actions</returns>
        List<IAction> LegalActions(Node n);
    }
}
