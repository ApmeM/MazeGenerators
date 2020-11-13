namespace MazeGenerators.Utils.DeadendRemover
{
    public interface IDeadEndRemoverResult
    {
        int? GetTile(Vector2 pos);
        bool IsInRegion(Vector2 vector2);
        void RemoveTile(Vector2 pos);
    }
}
