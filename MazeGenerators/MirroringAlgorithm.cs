using MazeGenerators.Utils;

namespace MazeGenerators
{
    public class MirroringAlgorithm
    {
        public static void Mirror(GeneratorResult result, GeneratorSettings settings)
        {
            var oldPath = result.Paths;
            var oldRoomsCount = result.Rooms.Count;
            var oldJunctionsCount = result.Junctions.Count;
            switch (settings.Mirror)
            {
                case GeneratorSettings.MirrorDirection.Horizontal:
                    {
                        settings.Width = settings.Width * 2 - 3;
                        result.Paths = new int[result.Paths.GetLength(0) * 2 - 3, result.Paths.GetLength(1)];
                        for (var x = 0; x < oldPath.GetLength(0) - 1; x++)
                            for (var y = 0; y < oldPath.GetLength(1) - 1; y++)
                            {
                                result.Paths[x, y] = oldPath[x, y];
                                result.Paths[result.Paths.GetLength(0) - x - 1, y] = oldPath[x, y];
                            }
                        for (var i = 0; i < oldRoomsCount; i++)
                        {
                            result.Rooms.Add(new Rectangle(result.Paths.GetLength(0) - result.Rooms[i].X - result.Rooms[i].Width, result.Rooms[i].Y, result.Rooms[i].Width, result.Rooms[i].Height));
                        }
                        for (var i = 0; i < oldJunctionsCount; i++)
                        {
                            result.Junctions.Add(new Vector2(result.Paths.GetLength(0) - result.Junctions[i].X - 1, result.Junctions[i].Y));
                        }

                        break;
                    }
                case GeneratorSettings.MirrorDirection.Vertical:
                    {
                        settings.Height = settings.Height * 2 - 3;
                        result.Paths = new int[result.Paths.GetLength(0), result.Paths.GetLength(1) * 2 - 3];
                        for (var x = 0; x < oldPath.GetLength(0) - 1; x++)
                            for (var y = 0; y < oldPath.GetLength(1) - 1; y++)
                            {
                                result.Paths[x, y] = oldPath[x, y];
                                result.Paths[x, result.Paths.GetLength(1) - y - 1] = oldPath[x, y];
                            }
                        for (var i = 0; i < oldRoomsCount; i++)
                        {
                            result.Rooms.Add(new Rectangle(result.Rooms[i].X, result.Paths.GetLength(1) - result.Rooms[i].Y - result.Rooms[i].Height, result.Rooms[i].Width, result.Rooms[i].Height));
                        }
                        for (var i = 0; i < oldJunctionsCount; i++)
                        {
                            result.Junctions.Add(new Vector2(result.Junctions[i].X, result.Paths.GetLength(1) - result.Junctions[i].Y - 1));
                        }

                        break;
                    }
                case GeneratorSettings.MirrorDirection.Both:
                    {
                        settings.Width = settings.Width * 2 - 3;
                        settings.Height = settings.Height * 2 - 3;
                        result.Paths = new int[result.Paths.GetLength(0) * 2 - 3, result.Paths.GetLength(1) * 2 - 3];
                        for (var x = 0; x < oldPath.GetLength(0) - 1; x++)
                            for (var y = 0; y < oldPath.GetLength(1) - 1; y++)
                            {
                                result.Paths[x, y] = oldPath[x, y];
                                result.Paths[x, result.Paths.GetLength(1) - y - 1] = oldPath[x, y];
                                result.Paths[result.Paths.GetLength(0) - x - 1, y] = oldPath[x, y];
                                result.Paths[result.Paths.GetLength(0) - x - 1, result.Paths.GetLength(1) - y - 1] = oldPath[x, y];
                            }
                        
                        for (var i = 0; i < oldRoomsCount; i++)
                        {
                            result.Rooms.Add(new Rectangle(result.Rooms[i].X, result.Paths.GetLength(1) - result.Rooms[i].Y - result.Rooms[i].Height, result.Rooms[i].Width, result.Rooms[i].Height));
                            result.Rooms.Add(new Rectangle(result.Paths.GetLength(0) - result.Rooms[i].X - result.Rooms[i].Width, result.Rooms[i].Y, result.Rooms[i].Width, result.Rooms[i].Height));
                            result.Rooms.Add(new Rectangle(result.Paths.GetLength(0) - result.Rooms[i].X - result.Rooms[i].Width, result.Paths.GetLength(1) - result.Rooms[i].Y - result.Rooms[i].Height, result.Rooms[i].Width, result.Rooms[i].Height));
                        }

                        for (var i = 0; i < oldJunctionsCount; i++)
                        {
                            result.Junctions.Add(new Vector2(result.Junctions[i].X, result.Paths.GetLength(1) - result.Junctions[i].Y - 1));
                            result.Junctions.Add(new Vector2(result.Paths.GetLength(0) - result.Junctions[i].X - 1, result.Junctions[i].Y));
                            result.Junctions.Add(new Vector2(result.Paths.GetLength(0) - result.Junctions[i].X - 1, result.Paths.GetLength(1) - result.Junctions[i].Y - 1));
                        }

                        break;
                    }
                case GeneratorSettings.MirrorDirection.Rotate:
                    {
                        if (result.Paths.GetLength(0) != result.Paths.GetLength(1))
                        {
                            throw new System.Exception("Can't rotate non-square maze");
                        }

                        settings.Width = settings.Width * 2 - 3;
                        settings.Height = settings.Height * 2 - 3;
                        result.Paths = new int[result.Paths.GetLength(0) * 2 - 3, result.Paths.GetLength(1) * 2 - 3];
                        for (var x = 0; x < oldPath.GetLength(0) - 1; x++)
                            for (var y = 0; y < oldPath.GetLength(1) - 1; y++)
                            {
                                result.Paths[x, y] = oldPath[x, y];
                                result.Paths[result.Paths.GetLength(1) - y - 1, x] = oldPath[x, y];
                                result.Paths[result.Paths.GetLength(0) - x - 1, result.Paths.GetLength(1) - y - 1] = oldPath[x, y];
                                result.Paths[y, result.Paths.GetLength(0) - x - 1] = oldPath[x, y];
                            }

                        for (var i = 0; i < oldRoomsCount; i++)
                        {
                            result.Rooms.Add(new Rectangle(result.Paths.GetLength(1) - result.Rooms[i].Y - result.Rooms[i].Height, result.Rooms[i].X, result.Rooms[i].Height, result.Rooms[i].Width));
                            result.Rooms.Add(new Rectangle(result.Paths.GetLength(0) - result.Rooms[i].X - result.Rooms[i].Width, result.Paths.GetLength(1) - result.Rooms[i].Y - result.Rooms[i].Height, result.Rooms[i].Width, result.Rooms[i].Height));
                            result.Rooms.Add(new Rectangle(result.Rooms[i].Y, result.Paths.GetLength(0) - result.Rooms[i].X - result.Rooms[i].Width, result.Rooms[i].Height, result.Rooms[i].Width));
                        }

                        for (var i = 0; i < oldJunctionsCount; i++)
                        {
                            result.Junctions.Add(new Vector2(result.Paths.GetLength(1) - result.Junctions[i].Y - 1, result.Junctions[i].X));
                            result.Junctions.Add(new Vector2(result.Paths.GetLength(0) - result.Junctions[i].X - 1, result.Paths.GetLength(1) - result.Junctions[i].Y - 1));
                            result.Junctions.Add(new Vector2(result.Junctions[i].Y, result.Paths.GetLength(0) - result.Junctions[i].X - 1));
                        }

                        break;
                    }
            }
        }
    }
}
