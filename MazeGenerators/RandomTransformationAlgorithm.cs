using System;

namespace MazeGenerators
{
    public static class RandomTransformationAlgorithm
    {
        public static Maze Randomize(this Maze result, Func<int, int> nextRandom, int mapDensity)
        {
            for (var x = 0; x < result.Width; x++)
            {
                for (var y = 0; y < result.Height; y++)
                {
                    result.SetTile(new Vector2(x, y), nextRandom(100) > mapDensity ? Tile.MazeTileId : Tile.EmptyTileId);
                }
            }

            return result;
        }
    }
}
