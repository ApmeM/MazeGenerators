using System;

namespace MazeGenerators
{
    public static class RoomGeneratorAlgorithm
    {
        private static readonly Random r = new Random();
        private static readonly Func<int, int> defaultRandomizer = (max) => r.Next(max);

        /// <summary>
        /// Tries to add a room with both sizes between minRoomSize and maxRoomSize and difference between width and height is not more then maxWidthHeightRoomSizeDifference
        /// With preventOverlap rooms will not intersect.
        /// If there is no way to add a room with specified parameters - woom wil not be added.
        /// Room is represented as an entry in 'Rooms' list and updated tiles on a maze map.
        /// if nextRandom is not set System.Random is used.
        /// </summary>
        public static Maze TryAddRoom(this Maze result, Func<int, int> nextRandom = null, bool preventOverlap = true, int minRoomSize = 2, int maxRoomSize = 5, int maxWidthHeightRoomSizeDifference = 5)
        {
            nextRandom = nextRandom ?? defaultRandomizer;

            if (maxRoomSize < minRoomSize)
            {
                throw new Exception("MaxRoomSize cant be less then MinRoomSize.");
            }

            var width = nextRandom(maxRoomSize - minRoomSize) + minRoomSize;
            
            var minHeightSize = Math.Max(width - maxWidthHeightRoomSizeDifference, minRoomSize);
            var maxHeightSize = Math.Min(width + maxWidthHeightRoomSizeDifference, maxRoomSize);

            var height = nextRandom(maxHeightSize - minHeightSize) + minHeightSize;

            var room = new Rectangle(
                nextRandom(result.Width - width), 
                nextRandom(result.Height - height), 
                width, 
                height);

            if (preventOverlap)
            {
                for (var x = room.X; x < room.X + room.Width; x++)
                    for (var y = room.Y; y < room.Y + room.Height; y++)
                    {
                        if (result.GetTile(new Vector2(x, y)) == Tile.MazeTileId)
                        {
                            return result;
                        }
                    }
            }

            result.DrawFullRect(room, Tile.MazeTileId);

            return result;
        }
    }
}
