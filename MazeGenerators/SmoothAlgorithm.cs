namespace MazeGenerators
{
    public static class SmoothAlgorithm
    {
        public static Maze Smooth(this Maze result, int iterations = 5, int smoothingThreshold = 4)
        {
            var currentMap = result.GetPathsClone();
            var smoothedMap = result.GetPathsClone();
            for (int i = 0; i < iterations; i++)
            {
                for (var x = 0; x < result.Width; x++)
                {
                    for (var y = 0; y < result.Height; y++)
                    {
                        var surroundingWallsCount = checkWall(currentMap, x - 1, y + 1) + checkWall(currentMap, x, y + 1) + checkWall(currentMap, x + 1, y + 1)
                            + checkWall(currentMap, x - 1, y) + checkWall(currentMap, x, y) + checkWall(currentMap, x + 1, y)
                            + checkWall(currentMap, x - 1, y - 1) + checkWall(currentMap, x, y - 1) + checkWall(currentMap, x + 1, y - 1);
                        smoothedMap[x, y] = surroundingWallsCount > smoothingThreshold ? Tile.EmptyTileId : Tile.MazeTileId;
                    }
                }

                var temp = currentMap;
                smoothedMap = temp;
                currentMap = smoothedMap;
            }
            result.SetPaths(currentMap);
            return result;
        }

        private static int checkWall(Tile[,] map, int x, int y)
        {
            if (x < 0 || y < 0 || x >= map.GetLength(0) || y >= map.GetLength(1)) return 1;
            return (map[x, y] == Tile.WallTileId || map[x, y] == Tile.EmptyTileId) ? 1 : 0;
        }
    }
}
