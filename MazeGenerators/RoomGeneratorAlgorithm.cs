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

            minRoomSize = minRoomSize / 2 * 2 + 1;
            maxRoomSize = (maxRoomSize + 1) / 2 * 2 - 1;
            var roomLength = maxRoomSize - minRoomSize;

            if (roomLength < 0)
            {
                throw new Exception("MaxRoomSize cant be less then MinRoomSize.");
            }

            var width = (nextRandom(roomLength + 1) + minRoomSize) / 2 * 2 + 1;
            var maxDifference = nextRandom(Math.Min(maxWidthHeightRoomSizeDifference, roomLength));
            var height = Math.Min((int)maxRoomSize, Math.Max((int)minRoomSize, (int)((maxDifference - maxDifference / 2 + width) / 2 * 2 + 1)));

            var x = nextRandom((result.Width - width) / 2) * 2 + 1;
            var y = nextRandom((result.Height - height) / 2) * 2 + 1;

            var room = new Rectangle(x, y, width, height);

            var overlaps = false;
            if (preventOverlap)
            {
                foreach (var other in result.Rooms)
                {
                    if (room.Intersects(other))
                    {
                        overlaps = true;
                        break;
                    }
                }
            }

            if (overlaps)
            {
                return result;
            }

            result.DrawFullRect(room, Tile.MazeTileId);
            result.Rooms.Add(room);

            return result;
        }
    }
}
