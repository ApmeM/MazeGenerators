using System;

namespace MazeGenerators.Utils
{
    public class GeneratorSettings
    {

        public int Width { get; set; } = 21;

        public int Height { get; set; } = 21;

        public Random Random { get; set; } = new Random();

        public Vector2[] Directions { get; set; } = DefaultDirections.CardinalDirs;

        public int EmptyTileId { get; set; } = 0;

        public int WallTileId { get; set; } = 1;

        public int MazeTileId { get; set; } = 2;

        public int RoomTileId { get; set; } = 3;

        public int JunctionTileId { get; set; } = 4;
    }
}
