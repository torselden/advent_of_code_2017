using System;
using System.IO;
using System.Linq;

namespace tor_selden_csharp
{
    class Day19
    {
        static string[] input = File.ReadAllLines(Path.Combine(Program.BasePath, "input19.txt"));

        internal static void AB()
        {
            var grid = ParseGrid();
            TraverseGrid(grid);
        }

        private static void TraverseGrid(char[,] grid)
        {
            string visisted = "";
            (int x, int y) pos = GetStartPos(grid);
            (int dx, int dy) dir = (0, 1);

            while (true)
            {
                char posVal = grid[pos.x, pos.y];

                if (posVal == '+')
                    dir = ChangeDirection(grid, pos.x, pos.y, visisted);
                else if (posVal==' ')
                    break;

                visisted += posVal;

                pos.x += dir.dx;
                pos.y += dir.dy;
            }

            var letters = visisted.Where(x => (x != '+' && x != '|' && x != '-')).ToArray();
            Console.WriteLine(letters); //A
            Console.WriteLine(visisted.Length); //B
        }

        private static (int dx, int dy) ChangeDirection(char[,] grid, int x, int y, string visited)
        {
            var deltax = new[] { 0, 1, 0, -1 };
            var deltay = new[] { -1, 0, 1, 0 };
            int dx = 0;
            int dy = 0;
            char prevPos = visited[visited.Length - 1];

            for (int i = 0; i < 4; i++)
            {
                if (x + deltax[i] < grid.GetLength(0) && (x + deltax[i]) >= 0 && y + deltay[i] < grid.GetLength(0) && (y + deltay[i]) >= 0)
                {
                    char newPos = grid[x + deltax[i], y + deltay[i]];
                    if (newPos != prevPos && newPos != ' ')
                    {
                        dx = deltax[i];
                        dy = deltay[i];
                        break;
                    }
                }
            }

            return (dx, dy);
        }

        private static (int x, int y) GetStartPos(char[,] grid)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if (grid[x, 0] == '|')
                {
                    return (x, 0);
                }
            }
            return (0, 0);
        }

        private static char[,] ParseGrid()
        {
            int xSize = input[0].Length;
            int ySize = input.Count();
            var grid = new char[xSize, ySize];

            for (int x = 0; x < ySize; x++)
            {
                for (int y = 0; y < xSize; y++)
                {
                    grid[x, y] = Convert.ToChar(input[y][x]);
                }
            }
            return grid;
        }
    }
}
