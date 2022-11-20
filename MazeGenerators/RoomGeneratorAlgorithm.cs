using MazeGenerators.Utils;
using System;

namespace MazeGenerators
{
    public class RoomGeneratorAlgorithm
    {
        public static void GenerateRooms(GeneratorResult result, GeneratorSettings settings, int numRoomTries = 100, int targetRoomCount = 100, bool preventOverlappedRooms = true, int minRoomSize = 2, int maxRoomSize = 5, int maxWidthHeightRoomSizeDifference = 5)
        {
            minRoomSize = minRoomSize / 2 * 2 + 1;
            maxRoomSize = (maxRoomSize + 1) / 2 * 2 - 1;
            var roomLength = maxRoomSize - minRoomSize;

            if (roomLength < 0)
            {
                throw new Exception("MaxRoomSize cant be less then MinRoomSize.");
            }

            var roomsGemerated = 0;
            for (var i = 0; i < numRoomTries; i++)
            {
                var width = (settings.Random.Next(roomLength + 1) + minRoomSize) / 2 * 2 + 1;
                var maxDifference = settings.Random.Next(Math.Min(maxWidthHeightRoomSizeDifference, roomLength));
                var height = Math.Min((int)maxRoomSize, Math.Max((int)minRoomSize, (int)((maxDifference - maxDifference / 2 + width) / 2 * 2 + 1)));

                var x = settings.Random.Next((settings.Width - width) / 2) * 2 + 1;
                var y = settings.Random.Next((settings.Height - height) / 2) * 2 + 1;

                var room = new Rectangle(x, y, width, height);

                var overlaps = false;
                if (preventOverlappedRooms)
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
                    continue;

                CustomDrawAlgorithm.AddFillRectangle(result, settings, room, settings.RoomTileId);
                result.Rooms.Add(room);
                roomsGemerated++;
                if (roomsGemerated >= targetRoomCount)
                {
                    break;
                }
            }
        }
    }
}
