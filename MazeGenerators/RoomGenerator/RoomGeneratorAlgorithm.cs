using MazeGenerators.Common;
using MazeGenerators.Utils;
using System;

namespace MazeGenerators.RoomGenerator
{
    public class RoomGeneratorAlgorithm
    {
        public static void GenerateRooms(IRoomGeneratorResult result, IRoomGeneratorSettings settings)
        {
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

                result.Rooms.Add(room);

                for (var i1 = x; i1 < x + width; i1++)
                    for (var j1 = y; j1 < y + height; j1++)
                    {
                        var pos = new Vector2(i1, j1);
                        CommonAlgorithm.SetTile(result, pos, settings.RoomTileId);
                    }
            }
        }
    }
}
