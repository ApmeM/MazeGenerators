namespace MazeGenerators
{
    public static class CleanupAlgoritm
    {
        public static Maze Clear(this Maze result, Tile tileType = Tile.EmptyTileId)
        {
            for (var x = 0; x < result.Width; x++)
            {
                for (var y = 0; y < result.Height; y++)
                {
                    result.SetTile(new Vector2(x, y), tileType);
                }
            }
            result.Junctions.Clear();
            result.Rooms.Clear();

            return result;
        }
    }
}
