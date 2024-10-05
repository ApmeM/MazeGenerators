namespace MazeGenerators
{
    public static class MirroringAlgorithm
    {
        public enum MirrorDirection
        {
            Horizontal,
            Vertical,
            Both,
            Rotate
        }

        public static Maze Mirror(this Maze result, MirrorDirection mirror)
        {
            var oldJunctionsCount = result.Junctions.Count;
            switch (mirror)
            {
                case MirrorDirection.Horizontal:
                    {
                        var newPath = new Tile[result.Width * 2 - 3, result.Height];
                        for (var x = 0; x < result.Width - 1; x++)
                            for (var y = 0; y < result.Height; y++)
                            {
                                newPath[x, y] = result.GetTile(new Vector2(x, y));
                                newPath[newPath.GetLength(0) - x - 1, y] = result.GetTile(new Vector2(x, y));
                            }
                        result.SetPaths(newPath);
                        for (var i = 0; i < oldJunctionsCount; i++)
                        {
                            result.Junctions.Add(new Vector2(result.Width - result.Junctions[i].X - 1, result.Junctions[i].Y));
                        }

                        break;
                    }
                case MirrorDirection.Vertical:
                    {
                        var newPath = new Tile[result.Width, result.Height * 2 - 3];
                        for (var x = 0; x < result.Width; x++)
                            for (var y = 0; y < result.Height - 1; y++)
                            {
                                newPath[x, y] = result.GetTile(new Vector2(x,y));
                                newPath[x, newPath.GetLength(1) - y - 1] = result.GetTile(new Vector2(x,y));
                            }
                        result.SetPaths(newPath);
                        for (var i = 0; i < oldJunctionsCount; i++)
                        {
                            result.Junctions.Add(new Vector2(result.Junctions[i].X, result.Height - result.Junctions[i].Y - 1));
                        }

                        break;
                    }
                case MirrorDirection.Both:
                    {
                        var newPath = new Tile[result.Width * 2 - 3, result.Height * 2 - 3];
                        for (var x = 0; x < result.Width - 1; x++)
                            for (var y = 0; y < result.Height - 1; y++)
                            {
                                newPath[x, y] = result.GetTile(new Vector2(x,y));
                                newPath[x, newPath.GetLength(1) - y - 1] = result.GetTile(new Vector2(x,y));
                                newPath[newPath.GetLength(0) - x - 1, y] = result.GetTile(new Vector2(x,y));
                                newPath[newPath.GetLength(0) - x - 1, newPath.GetLength(1) - y - 1] = result.GetTile(new Vector2(x,y));
                            }
                        result.SetPaths(newPath);
                        for (var i = 0; i < oldJunctionsCount; i++)
                        {
                            result.Junctions.Add(new Vector2(result.Junctions[i].X, result.Height - result.Junctions[i].Y - 1));
                            result.Junctions.Add(new Vector2(result.Width - result.Junctions[i].X - 1, result.Junctions[i].Y));
                            result.Junctions.Add(new Vector2(result.Width - result.Junctions[i].X - 1, result.Height - result.Junctions[i].Y - 1));
                        }

                        break;
                    }
                case MirrorDirection.Rotate:
                    {
                        if (result.Width != result.Height)
                        {
                            throw new System.Exception("Can't rotate non-square maze");
                        }

                        var newPath = new Tile[result.Width * 2 - 3, result.Height * 2 - 3];
                        for (var x = 0; x < result.Width - 1; x++)
                            for (var y = 0; y < result.Height - 1; y++)
                            {
                                newPath[x, y] = result.GetTile(new Vector2(x,y));
                                newPath[newPath.GetLength(1) - y - 1, x] = result.GetTile(new Vector2(x,y));
                                newPath[newPath.GetLength(0) - x - 1, newPath.GetLength(1) - y - 1] = result.GetTile(new Vector2(x,y));
                                newPath[y, newPath.GetLength(0) - x - 1] = result.GetTile(new Vector2(x,y));
                            }
                        result.SetPaths(newPath);
                        for (var i = 0; i < oldJunctionsCount; i++)
                        {
                            result.Junctions.Add(new Vector2(result.Height - result.Junctions[i].Y - 1, result.Junctions[i].X));
                            result.Junctions.Add(new Vector2(result.Width - result.Junctions[i].X - 1, result.Height - result.Junctions[i].Y - 1));
                            result.Junctions.Add(new Vector2(result.Junctions[i].Y, result.Width - result.Junctions[i].X - 1));
                        }

                        break;
                    }
            }
            return result;
        }
    }
}
