using System;

namespace MazeGenerators
{
    public static class RandomizeAlgorithm
    {
        private static readonly Random r = new Random();
        private static readonly Func<int, int> defaultRandomizer = (max) => r.Next(max);
        /// <summary>
        /// Fully reset the maze randomly setting tile to Tile.MazeTileId or Tile.EmptyTileId.
        /// mapDestiny is a change 0-100 to get Tile.MazeTileId
        /// if nextRandom is not set System.Random is used.
        /// </summary>
        /// <returns></returns>
        public static Maze Randomize(this Maze result, Func<int, int> nextRandom = null, int mapDensity = 50)
        {
            nextRandom = nextRandom ?? defaultRandomizer;

            for (var x = 0; x < result.Width; x++)
            {
                for (var y = 0; y < result.Height; y++)
                {
                    result.SetTile(new Vector2(x, y), nextRandom(100) > mapDensity ? Tile.MazeTileId : Tile.EmptyTileId);
                }
            }
            result.Junctions.Clear();
            result.Rooms.Clear();
            return result;
        }
    }
}
