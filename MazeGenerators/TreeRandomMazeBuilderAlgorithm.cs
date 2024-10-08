using System;
using System.Collections.Generic;

namespace MazeGenerators
{
    public static class TreeRandomMazeBuilderAlgorithm
    {
        private static readonly Random r = new Random();
        private static readonly Func<int, int> defaultRandomizer = (max) => r.Next(max);
        private static readonly Vector2[] CompassDirs = {
            new Vector2( 1, 0 ),
            new Vector2( 1, -1 ),
            new Vector2( 0, -1 ),
            new Vector2( -1, -1 ),
            new Vector2( -1, 0 ),
            new Vector2( -1, 1 ),
            new Vector2( 0, 1 ),
            new Vector2( 1, 1 ),
        };

        public static Maze GrowRandomMaze(this Maze result, Func<int, int> nextRandom = null, int windingPercent = 50)
        {
            nextRandom = nextRandom ?? defaultRandomizer;

            // Fill in all of the empty space with mazes.
            for (var x = 0; x < result.Width; x++)
            {
                for (var y = 0; y < result.Height; y++)
                {
                    GrowSingleMazeTree(result, new Vector2(x, y), windingPercent, nextRandom);
                }
            }

            return result;
        }

        private static void GrowSingleMazeTree(Maze result, Vector2 start, int windingPercent, Func<int, int> nextRandom)
        {
            if (!CanCarve(result, start))
            {
                return;
            }

            var cells = new Stack<Vector2>();

            result.SetTile(start, Tile.MazeTileId);

            cells.Push(start);

            Vector2? lastDir = null;

            while (cells.Count > 0)
            {
                var cell = cells.Peek();

                var possibleDirs = new List<Vector2>();

                foreach (var dir in result.Directions)
                {
                    if (CanCarve(result, cell + dir))
                        possibleDirs.Add(dir);
                }

                if (possibleDirs.Count == 0)
                {
                    cells.Pop();
                    lastDir = null;
                    continue;
                }

                Vector2 selectedDir;
                if (lastDir != null && possibleDirs.Contains(lastDir.Value) && nextRandom(100) > windingPercent)
                {
                    selectedDir = lastDir.Value;
                }
                else
                {
                    selectedDir = possibleDirs[nextRandom(possibleDirs.Count)];
                }

                result.SetTile(cell + selectedDir, Tile.MazeTileId);

                cells.Push(cell + selectedDir);
                lastDir = selectedDir;
            }
        }

        private static bool CanCarve(Maze result, Vector2 block)
        {
            // Must end in bounds.
            if (!result.IsInRegion(block))
            {
                return false;
            }

            if (result.GetTile(block) == Tile.MazeTileId)
            {
                return false;
            }

            var x = block.X;
            var y = block.Y;

            int surroundingWallsCount = 0;
            var directions = CompassDirs;
            foreach (var dir2 in directions)
            {
                surroundingWallsCount += checkWall(result, x + dir2.X, y + dir2.Y);
            }

            return surroundingWallsCount >= directions.Length - 2;
        }

        private static int checkWall(Maze result, int x, int y)
        {
            if (x < 0 || y < 0 || x >= result.Width || y >= result.Height) return 1;
            return (result.GetTile(new Vector2(x, y)) == Tile.WallTileId || result.GetTile(new Vector2(x, y)) == Tile.EmptyTileId) ? 1 : 0;
        }
    }
}
