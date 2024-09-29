using System.Collections.Generic;

namespace MazeGenerators
{
    public class Maze
    {
        public Tile[,] Paths { get; set; }
        public int Width => this.Paths.GetLength(0);
        public int Height => this.Paths.GetLength(1);
        public List<Vector2> Junctions { get; } = new List<Vector2>();
        public List<Rectangle> Rooms { get; } = new List<Rectangle>();

        public Maze(int width, int height)
        {
            this.Paths = new Tile[width, height];
        }

        public Tile GetTile(Vector2 pos)
        {
            return Paths[pos.X, pos.Y];
        }

        public Maze SetTile(Vector2 pos, Tile tileId)
        {
            Paths[pos.X, pos.Y] = tileId;
            return this;
        }

        public Maze DrawFullRect(Rectangle rect, Tile tileId)
        {
            var result = this;
            for (var x = rect.X; x < rect.X + rect.Width; x++)
                for (var y = rect.Y; y < rect.Y + rect.Height; y++)
                {
                    result.SetTile(new Vector2(x, y), tileId);
                }
            return result;
        }

        public Maze DrawRect(Rectangle rect, Tile tileId)
        {
            var result = this;
            for (var x = rect.X; x < rect.X + rect.Width; x++)
            {
                result.SetTile(new Vector2(x, rect.Y), tileId);
                result.SetTile(new Vector2(x, rect.Y + rect.Height - 1), tileId);
            }
            for (var y = rect.Y; y < rect.Y + rect.Height; y++)
            {
                result.SetTile(new Vector2(rect.X, y), tileId);
                result.SetTile(new Vector2(rect.X + rect.Width - 1, y), tileId);
            }
            
            return result;
        }

        public bool IsInRegion(Vector2 loc)
        {
            return loc.X >= 0 && loc.Y >= 0 && loc.X < Width && loc.Y < Height;
        }
    }
}
