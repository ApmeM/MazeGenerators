namespace MazeGenerators
{
    public static class ResetAlgorithm
    {
        /// <summary>
        /// Resets all the tiles in a maze to empty tile. 
        /// If newWidth or newHeight is more then zero - also resizes the maze with new values.
        /// Negative newWidth/newHeight values means no changes.
        /// </summary>
        public static Maze Reset(this Maze result, int newWidth = -1, int newHeight = -1)
        {
            newWidth = newWidth > 0 ? newWidth : result.Width;
            newHeight = newHeight > 0 ? newHeight : result.Height;

            if (result.Width != newWidth || result.Height != newHeight)
            {
                result.Init(newWidth, newHeight);
            }

            result.DrawFullRect(new Rectangle(0, 0, result.Width, result.Height), Tile.EmptyTileId);
            result.Junctions.Clear();
            return result;
        }
    }
}
