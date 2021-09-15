using System.Collections.Generic;

namespace MazeGenerators.Utils
{
    public class GeneratorResult
    {
        public int[,] Paths { get; set; }
        public List<Vector2> Junctions { get; } = new List<Vector2>();
        public List<Rectangle> Rooms { get; } = new List<Rectangle>();
    }
}
