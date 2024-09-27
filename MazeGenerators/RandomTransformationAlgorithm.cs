using System;

namespace MazeGenerators
{
    public static class RandomTransformationAlgorithm
    {
        private static readonly Random r = new Random();
        private static readonly Func<int, int> defaultRandomizer = (max) => r.Next(max);
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
