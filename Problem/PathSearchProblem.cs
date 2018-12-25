// https://projectbalint.com/en/page/uninformed-search.html
// PathSearchProblem.cs
//
// The problem definition for the path searching problem
// Copyright (c) 2018 Balint Gyevnar

using System.Text;
using System.Collections.Generic;
using UninformedSearch.Search;

namespace UninformedSearch.Problem
{
    /// <summary>
    /// Problem definition for path search problem
    /// </summary>
    class PathSearchProblem : IProblem
    {
        private readonly int W;
        private readonly int H;
        private int[,] starting_grid = null;

        /// <summary>
        /// The coordinates of the goal
        /// </summary>
        public (int x, int y) GoalPosition { get; private set; }

        /// <summary>
        /// The coordinates of the start
        /// </summary>
        public (int x, int y) StartPosition { get; private set; }

        /// <summary>
        /// Initialise a new problem instance
        /// </summary>
        /// <param name="gx">Row of goal</param>
        /// <param name="gy">Column of goal</param>
        /// <param name="sx">Row of start</param>
        /// <param name="sy">Column of start</param>
        /// <param name="w">Width of grid</param>
        /// <param name="h">Height of grid</param>
        /// <param name="g">Initial grid arrangement</param>
        public PathSearchProblem(int sx = 0, int sy = 0, int gx = 0, int gy = 0, int w = 10, int h = 10, int[,] g = null)
        {
            StartPosition = (sx, sy);
            GoalPosition = (gx, gy);
            W = w;
            H = h;
            starting_grid = g;
        }

        /// <summary>
        /// Initial node with new or prespecified state and location at starting position
        /// </summary>
        public Node InitialNode {
            get { 
                PathSearchState s = new PathSearchState(W, H);
                if (starting_grid != null)
                    s.Grid = starting_grid;

                Node n = new Node()
                {
                    PathCost = 0,
                    State = s,
                    Parent = null,
                    Action = null
                };
                return n;
            }
        }

        /// <summary>
        /// Check whether goal has been reached
        /// </summary>
        /// <param name="n">Node to check</param>
        /// <returns>True if current position is a goal state</returns>
        public bool GoalTest(Node n)
        {
            PathSearchState current = (PathSearchState)n.State;
            return current.Grid[GoalPosition.x, GoalPosition.y] == 1;
        }

        /// <summary>
        /// All legal moves available from current node
        /// </summary>
        /// <param name="n">The current node</param>
        /// <returns>List of legal actions to take from current node</returns>
        public List<IAction> LegalActions(Node n)
        {
            List<IAction> actions = new List<IAction>();
            PathSearchState s = (PathSearchState)n.State;
            int x = s.Position.x, y = s.Position.y;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int u = x + i;
                    int v = y + j;

                    if (u < 0 || u >= W || v < 0 || v >= H)
                        continue;
                    else if (s.Grid[u, v] == 2)
                        continue;
                    else
                        if (s.Grid[x + i, y + j] == -1)
                            actions.Add(new PathSearchAction(i, j));
                }
            }
            return actions;
        }

        /// <summary>
        /// Apply an action to a given state
        /// </summary>
        /// <param name="s">Starting state</param>
        /// <param name="a">Action to apply</param>
        /// <returns>New state with the action applied</returns>
        public IState Result(IState s, IAction a)
        {
            PathSearchState current = (PathSearchState)s.Clone();
            PathSearchAction action = (PathSearchAction)a;
            int x = current.Position.x;
            int y = current.Position.y;
            int u = action.Displacement.u;
            int v = action.Displacement.v;
            current.Grid[x, y] = 0;
            current.Grid[x + u, y + v] = 1;
            current.Position = (x + u, y + v);
            return current;
        }

        /// <summary>
        /// Get step cost from current state with given action
        /// </summary>
        /// <param name="s">Current state</param>
        /// <param name="a">Action to perform</param>
        /// <returns>Cost to perform action from current state</returns>
        public int StepCost(IState s, IAction a)
        {
            return 1;
        }

        /// <summary>
        /// Generate useful description of problem
        /// </summary>
        /// <returns>String containing useful description of the problem</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Width: " + W);
            sb.AppendLine("Height: " + H);
            sb.AppendLine("Start x: " + StartPosition.x);
            sb.AppendLine("Start y: " + StartPosition.y);
            sb.AppendLine("Goal x: " + GoalPosition.x);
            sb.AppendLine("Goal y: " + GoalPosition.y);
            sb.AppendLine(InitialNode.State.ToString());

            return sb.ToString();
        }
    }
}
