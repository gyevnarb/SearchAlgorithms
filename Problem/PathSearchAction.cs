// https://projectbalint.com/en/page/uninformed-search.html
// PathSearchAction.cs
//
// An action representing a displacement of one step on the grid
// Copyright (c) 2018 Balint Gyevnar

using UninformedSearch.Search;

namespace UninformedSearch.Problem
{
    /// <summary>
    /// Represent displacement of a single step on the grid
    /// </summary>
    class PathSearchAction : IAction
    {
        /// <summary>
        /// The step to take
        /// </summary>
        public (int u, int v) Displacement { get; set; }

        /// <summary>
        /// Initialise new action
        /// </summary>
        /// <param name="x">Step size in X-direction</param>
        /// <param name="y">Step size in Y-direction</param>
        public PathSearchAction(int x, int y) => Displacement = (x, y);
    }
}
