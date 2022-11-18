using MazeGenerators.Utils;

namespace MazeGenerators
{
    public class CustomDrawAlgorithm
    {
        public static void AddFillRectangle(GeneratorResult result, GeneratorSettings settings, Rectangle room, int tileId)
        {
            for (var x = room.X; x < room.X + room.Width; x++)
                for (var y = room.Y; y < room.Y + room.Height; y++)
                {
                    result.SetTile(new Vector2(x, y), tileId);
                }
        }

        public static void AddRectangle(GeneratorResult result, GeneratorSettings settings, Rectangle room, int tileId)
        {
            for (var x = room.X; x < room.X + room.Width; x++)
            {
                result.SetTile(new Vector2(x, room.Y), tileId);
                result.SetTile(new Vector2(x, room.Y + room.Height - 1), tileId);
            }
            for (var y = room.Y; y < room.Y + room.Height; y++)
            {
                result.SetTile(new Vector2(room.X, y), tileId);
                result.SetTile(new Vector2(room.X + room.Width - 1, y), tileId);
            }
        }

        public static void AddPoint(GeneratorResult result, GeneratorSettings settings, Vector2 point, int tileId)
        {
            result.SetTile(point, tileId);
        }
    }
}
