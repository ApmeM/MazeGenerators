namespace MazeGenerators
{
    public static class RemoveDeadEndsAlgorithm
    {
        public static Maze RemoveDeadEnds(this Maze result)
        {
            for (var x = 0; x < result.Width; x++)
                for (var y = 0; y < result.Height; y++)
                {
                    var pos = new Vector2(x, y);
                    if (result.GetTile(pos) != Tile.MazeTileId)
                    {
                        continue;
                    }

                    // If it only has one exit, it's a dead end.
                    int exits;
                    do
                    {
                        exits = 0;
                        var lastExitPosition = new Vector2(0, 0);
                        foreach (var dir in result.Directions)
                        {
                            if (!result.IsInRegion(pos + dir))
                            {
                                continue;
                            }

                            if (result.GetTile(pos + dir) != Tile.MazeTileId)
                            {
                                continue;
                            }

                            exits++;
                            lastExitPosition = pos + dir;
                        }

                        if (exits == 0)
                        {
                            result.SetTile(pos, Tile.EmptyTileId);
                        }
                        else if (exits == 1)
                        {
                            result.SetTile(pos, Tile.EmptyTileId);
                            pos = lastExitPosition;
                        }
                    } while (exits == 1);
                }
            
            return result;
        }
    }
}
