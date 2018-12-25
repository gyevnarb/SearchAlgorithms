// https://projectbalint.com/en/page/uninformed-search.html
// PathSearchState.cs
//
// State representing a grid for searching
// Copyright (c) 2018 Balint Gyevnar
using UninformedSearch.Search;

namespace UninformedSearch.Problem
{
    /// <summary>
    /// Describes the grid world of the path search problem
    /// </summary>
    class PathSearchState : IState
    {
        /// <summary>
        /// Width of the grid
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of the grid
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The grid world
        /// <para/>
        /// -1: Unvisited cell
        ///  0: Visited cell
        ///  1: Current cell
        ///  2: Obstacle
        /// </summary>
        public int[,] Grid { get; set; }

        public (int x, int y) Position { get; set; }

        /// <summary>
        /// Initialise null state
        /// </summary>
        public PathSearchState()
        {
            Width = 0;
            Height = 0;
            Grid = null;
            Position = (0, 0);
        }

        /// <summary>
        /// Initialise empty grid
        /// </summary>
        /// <param name="w">Width of grid</param>
        /// <param name="h">Height of grid</param>
        /// <param name="x">Row of starting position</param>
        /// <param name="y">Column of starting position</param>
        public PathSearchState(int w, int h, int x = 0, int y = 0)
        {
            Width = w;
            Height = h;

            Grid = new int[w, h];
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    Grid[i, j] = -1;
            Grid[x, y] = 1;

            Position = (x, y);
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Width; i++)
            {
                s += "|";
                for (int j = 0; j < Height; j++)
                {
                    int val = Grid[i, j];
                    switch (val)
                    {
                        case -1:
                            s += " ";
                            break;
                        case 2:
                            s += "X";
                            break;
                        default:
                            s += val.ToString();
                            break;
                    }
                    s += "|";
                }
                s += "\n";
            }
            return s;
        }

        public object Clone()
        {
            PathSearchState s = new PathSearchState(Width, Height, Position.x, Position.y)
            {
                Grid = (int[,])Grid.Clone(),
                Position = (Position.x, Position.y)
            };
            return s;
        }

        public override bool Equals(object obj)
        {
            if (obj is PathSearchState)
            {
                PathSearchState s = obj as PathSearchState;
                bool items_equal = true;
                for (int i = 0; i < Width; i++)
                    for (int j = 0; j < Height; j++)
                        items_equal = items_equal && s.Grid[i, j] == Grid[i, j];

                return s.Position.x == Position.x &&
                    s.Position.y == Position.y &&
                    s.Height == Height &&
                    s.Width == Width && items_equal;
    
            }
            return false;
        }

        public override int GetHashCode()
        {
            int prime = 37;
            return prime * (Position.GetHashCode() +
                prime * (Height.GetHashCode() + Width.GetHashCode() +
                prime * Grid.GetHashCode()));
        }
    }
}
