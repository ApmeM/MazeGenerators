using System.Collections.Generic;

namespace MazeGenerators.Utils.RegionConnector
{
    internal class TreeMazeBuilderAlgorythm
    {
        internal static void GrowMaze(ITreeMazeBuilderResult result, ITreeMazeBuilderSettings settings, Vector2 start, int regionId)
        {
            var cells = new List<Vector2>();

            result.SetTile(start, regionId);

            cells.Add(start);

            Vector2? lastDir = null;

            while (cells.Count > 0)
            {
                var cell = cells[cells.Count - 1];

                var unmadeCells = new List<Vector2>();

                foreach (var dir in settings.Directions)
                {
                    if (CanCarve(result, cell, dir))
                        unmadeCells.Add(dir);
                }

                if (unmadeCells.Count != 0)
                {
                    Vector2 dir;
                    if (lastDir != null && unmadeCells.Contains(lastDir.Value) && settings.Random.Next(100) > settings.WindingPercent)
                    {
                        dir = lastDir.Value;
                    }
                    else
                    {
                        dir = unmadeCells[settings.Random.Next(unmadeCells.Count)];
                    }

                    result.SetTile(cell + dir, regionId);
                    result.SetTile(cell + dir * 2, regionId);

                    cells.Add(cell + dir * 2);
                    lastDir = dir;
                }
                else
                {
                    cells.RemoveAt(cells.Count - 1);
                    lastDir = null;
                }
            }
        }


        /// Gets whether or not an opening can be carved from the given starting
        /// [Cell] at [pos] to the adjacent Cell facing [direction]. Returns `true`
        /// if the starting Cell is in bounds and the destination Cell is filled
        /// (or out of bounds).
        private static bool CanCarve(ITreeMazeBuilderResult result, Vector2 pos, Vector2 direction)
        {
            // Must end in bounds.
            var block = pos + direction * 3;
            if (!result.IsInRegion(block))
                return false;

            // Destination must not be open.
            var end = pos + direction * 2;
            return !result.GetTile(end).HasValue;
        }
    }
}
