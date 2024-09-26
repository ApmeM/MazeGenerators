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
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                {
                    this.SetTile(new Vector2(x, y), Tile.EmptyTileId);
                }
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

        public bool IsInRegion(Vector2 loc)
        {
            return loc.X >= 0 && loc.Y >= 0 && loc.X < Paths.GetLength(0) && loc.Y < Paths.GetLength(1);
        }
    }
}
