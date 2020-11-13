namespace MazeGenerators.Utils.DeadendRemover
{
    public interface IDeadEndRemoverSettings
    {
        int Width { get; }
        int Height { get; }
        Vector2[] Directions { get; }
        bool RemoveDeadEnds { get; }
    }
}
