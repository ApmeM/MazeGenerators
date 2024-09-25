namespace MazeGenerators
{
    public static class WallSurroundingAlgorithm
    {
        public static Maze BuildWalls(this Maze result)
        {
            // Fill in all of the empty space with mazes.
            for (var x = 0; x < result.Width; x++)
            {
                for (var y = 0; y < result.Height; y++)
                {
                    var pos = new Vector2(x, y);
                    if (result.GetTile(pos) == Tile.WallTileId ||
                        result.GetTile(pos) == Tile.EmptyTileId)
                        continue;

                    foreach (var dir in DefaultDirections.CompassDirs)
                    {
                        var newPos = pos + dir;
                        if (!result.IsInRegion(newPos))
                        {
                            continue;
                        }

                        if (result.GetTile(newPos) == Tile.EmptyTileId)
                        {
                            result.SetTile(newPos, Tile.WallTileId);
                        }
                    }
                }
            }
            
            return result;
        }
    }
}
