using System.Collections.Generic;

namespace MazeGenerators.Utils
{
    public class GeneratorResult
    {
        public int[,] Paths { get; set; }
        public List<Vector2> Junctions { get; } = new List<Vector2>();
        public List<Rectangle> Rooms { get; } = new List<Rectangle>();
        
        public int GetTile(Vector2 pos)
        {
            return Paths[pos.X, pos.Y];
        }

        public void SetTile(Vector2 pos, int tileId)
        {
            Paths[pos.X, pos.Y] = tileId;
        }

        public bool IsInRegion(Vector2 loc)
        {
            return loc.X >= 0 && loc.Y >= 0 && loc.X < Paths.GetLength(0) && loc.Y < Paths.GetLength(1);
        }
    }
}
