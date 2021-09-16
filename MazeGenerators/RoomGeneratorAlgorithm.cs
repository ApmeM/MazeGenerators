using MazeGenerators.Utils;
using System;

namespace MazeGenerators
{
    public class RoomGeneratorAlgorithm
    {
        public static void GenerateRooms(GeneratorResult result, GeneratorSettings settings)
        {
            var roomsGemerated = 0;
            for (var i = 0; i < settings.NumRoomTries; i++)
            {
                var minRoomSize = settings.MinRoomSize / 2 * 2 + 1;
                var maxRoomSize = (settings.MaxRoomSize + 1) / 2 * 2 - 1;
                var roomLength = maxRoomSize - minRoomSize;

                if (roomLength < 0)
                {
                    throw new Exception("MaxRoomSize cant be less then MinRoomSize.");
                }

                var width = (settings.Random.Next(roomLength + 1) + minRoomSize) / 2 * 2 + 1;
                var maxDifference = settings.Random.Next(Math.Min(settings.MaxWidthHeightRoomSizeDifference, roomLength));
                var height = Math.Min(maxRoomSize, Math.Max(minRoomSize, (maxDifference - maxDifference / 2 + width) / 2 * 2 + 1));

                var x = settings.Random.Next((settings.Width - width) / 2) * 2 + 1;
                var y = settings.Random.Next((settings.Height - height) / 2) * 2 + 1;

                var room = new Rectangle(x, y, width, height);

                var overlaps = false;
                if (settings.PreventOverlappedRooms)
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

                AddRoom(result, settings, room);
                roomsGemerated++;
                if (roomsGemerated >= settings.TargetRoomCount)
                {
                    break;
                }
            }
        }

        public static void AddRoom(GeneratorResult result, GeneratorSettings settings, Rectangle room)
        {
            for (var x = room.X; x < room.X + room.Width; x++)
                for (var y = room.Y; y < room.Y + room.Height; y++)
                {
                    CommonAlgorithm.SetTile(result, new Vector2(x, y), settings.RoomTileId);
                }

            result.Rooms.Add(room);
        }
    }
}
