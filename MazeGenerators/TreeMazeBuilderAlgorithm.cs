using MazeGenerators.Utils;
using System;
using System.Collections.Generic;

namespace MazeGenerators
{
    public class TreeMazeBuilderAlgorithm
    {
        public static void GrowMaze(GeneratorResult result, GeneratorSettings settings, int windingPercent = 50)
        {
            if (settings.Width % 2 == 0 || settings.Height % 2 == 0)
            {
                throw new Exception("The map must be odd-sized.");
            }

            // Fill in all of the empty space with mazes.
            for (var x = 1; x < settings.Width; x += 2)
            {
                for (var y = 1; y < settings.Height; y += 2)
                {
                    var pos = new Vector2(x, y);
                    if (result.GetTile(pos) != settings.EmptyTileId)
                        continue;
                    GrowSingleMazeTree(result, settings, pos, windingPercent);
                }
            }
        }

        private static void GrowSingleMazeTree(GeneratorResult result, GeneratorSettings settings, Vector2 start, int windingPercent)
        {
            var cells = new Stack<Vector2>();

            result.SetTile(start, settings.MazeTileId);

            cells.Push(start);

            Vector2? lastDir = null;

            while (cells.Count > 0)
            {
                var cell = cells.Peek();

                var unmadeCells = new List<Vector2>();

                foreach (var dir in settings.Directions)
                {
                    if (CanCarve(result, settings, cell, dir))
                        unmadeCells.Add(dir);
                }

                if (unmadeCells.Count != 0)
                {
                    Vector2 dir;
                    if (lastDir != null && unmadeCells.Contains(lastDir.Value) && settings.Random.Next(100) > windingPercent)
                    {
                        dir = lastDir.Value;
                    }
                    else
                    {
                        dir = unmadeCells[settings.Random.Next(unmadeCells.Count)];
                    }

                    result.SetTile(cell + dir, settings.MazeTileId);
                    result.SetTile(cell + dir * 2, settings.MazeTileId);

                    cells.Push(cell + dir * 2);
                    lastDir = dir;
                }
                else
                {
                    cells.Pop();
                    lastDir = null;
                }
            }
        }


        /// Gets whether or not an opening can be carved from the given starting
        /// [Cell] at [pos] to the adjacent Cell facing [direction]. Returns `true`
        /// if the starting Cell is in bounds and the destination Cell is filled
        /// (or out of bounds).
        private static bool CanCarve(GeneratorResult result, GeneratorSettings settings, Vector2 pos, Vector2 direction)
        {
            // Must end in bounds.
            var block = pos + direction * 3;
            if (!result.IsInRegion(block))
                return false;

            // Destination must not be open.
            var end = pos + direction * 2;
            return result.GetTile(end) == settings.EmptyTileId;
        }
    }
}
