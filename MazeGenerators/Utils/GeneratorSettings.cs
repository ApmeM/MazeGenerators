using System;

namespace MazeGenerators.Utils
{
    public class GeneratorSettings
    {
        public enum MirrorDirection
        {
            Horizontal,
            Vertical,
            Both,
            Rotate
        }

        public int Width { get; set; } = 21;

        public int Height { get; set; } = 21;

        public Random Random { get; set; } = new Random();

        public Vector2[] Directions { get; set; } = DefaultDirections.CardinalDirs;

        public int NumRoomTries { get; set; } = 100;

        public int TargetRoomCount { get; set; } = 4;

        public bool PreventOverlappedRooms { get; set; } = true;

        public int MinRoomSize { get; set; } = 2;

        public int MaxRoomSize { get; set; } = 5;

        public int MaxWidthHeightRoomSizeDifference { get; set; } = 5;

        public int WindingPercent { get; set; } = 0;

        public int AdditionalPassagesTries { get; set; } = 10;

        public int RoomTileId { get; set; } = 1;

        public int MazeTileId { get; set; } = 1;

        public int JunctionTileId { get; set; } = 1;

        public string MazeText { get; set; }

        public MirrorDirection Mirror { get; set; }
    }
}
