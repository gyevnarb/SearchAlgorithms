// https://projectbalint.com/en/page/uninformed-search.html
// Program.cs
//
// Run a sample search
// Copyright (c) 2018 Balint Gyevnar

using System;
using UninformedSearch.Problem;
using UninformedSearch.Search;

namespace UninformedSearch
{
    /// <summary>
    /// Run a sample search
    /// </summary>
    class Program
    {
        static readonly SearchType[] SEARCH_TYPES = { SearchType.BFS, SearchType.DFS, SearchType.DLS, SearchType.IDS, SearchType.UCS };
        static Random r = new Random();

        /// <summary>
        /// Run a random test search with obstacles
        /// </summary>
        static void RunSearchRandomGrid()
        {

            int w = r.Next(5, 25);
            int h = r.Next(5, 25);

            int sx = r.Next(0, w / 2);
            int sy = r.Next(0, h / 2);

            int gx = r.Next(0, w);
            int gy = r.Next(0, h);

            SearchType type = SEARCH_TYPES[r.Next(0, SEARCH_TYPES.Length)];

            int[,] rgrid = GenerateGridWithObstacles(sx, sy, gx, gy, w, h);

            // Create a new search problem
            PathSearchProblem problem = new PathSearchProblem(sx, sy, gx, gy, w, h, rgrid);
            
            // Display problem information
            Console.WriteLine(problem.ToString());
            Console.WriteLine("Search method: " + type.ToString());

            // Run search
            var search = new UninformedSearchAlgorithm(type);
            var goal = search.Search(problem, r.Next(1, 15));

            //Print resulting steps and cost
            if (goal == Node.FAILURE)
            {
                Console.WriteLine("No path found");
            }
            else if (goal == Node.CUTOFF)
            {
                Console.WriteLine("Cutoff occured");
            }
            else
            {
                Console.WriteLine(goal.State.ToString());
                Console.WriteLine("Goal cost: " + goal.PathCost);
            }
        }

        static int[,] GenerateGridWithObstacles(int sx, int sy, int gx, int gy, int w, int h)
        {
            //Create new grid and set all items to unvisited
            int[,] grid = new int[w, h];
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    grid[i, j] = -1;

            //TODO: Replace arbitrary choice for number of obstacles
            int num_obstacles = 5 + w % h;

            //Randomly select grid points as obstacles
            for (int i = 0; i < num_obstacles; i++)
            {
                int x = r.Next(0, w);
                int y = r.Next(0, h);

                if ((x == sx && y == sy) || (x == gy && y == gy))
                    continue;

                grid[x, y] = 2;
            }

            return grid;
        }

        static void Main(string[] args)
        {
            RunSearchRandomGrid();

            Console.ReadLine();
        }
    }
}
