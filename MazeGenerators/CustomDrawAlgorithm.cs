namespace MazeGenerators
{
    public static class CustomDrawAlgorithm
    {
        public static Maze AddFillRectangle(this Maze result, Rectangle rect, Tile tileId)
        {
            for (var x = rect.X; x < rect.X + rect.Width; x++)
                for (var y = rect.Y; y < rect.Y + rect.Height; y++)
                {
                    result.SetTile(new Vector2(x, y), tileId);
                }
            return result;
        }

        public static Maze AddRectangle(this Maze result, Rectangle rect, Tile tileId)
        {
            for (var x = rect.X; x < rect.X + rect.Width; x++)
            {
                result.SetTile(new Vector2(x, rect.Y), tileId);
                result.SetTile(new Vector2(x, rect.Y + rect.Height - 1), tileId);
            }
            for (var y = rect.Y; y < rect.Y + rect.Height; y++)
            {
                result.SetTile(new Vector2(rect.X, y), tileId);
                result.SetTile(new Vector2(rect.X + rect.Width - 1, y), tileId);
            }
            
            return result;
        }
    }
}
