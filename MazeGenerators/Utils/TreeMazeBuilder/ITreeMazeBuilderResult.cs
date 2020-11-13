using System.Collections.Generic;

namespace MazeGenerators.Utils.RegionConnector
{
    public interface ITreeMazeBuilderResult
    {
        void SetTile(Vector2 start, int regionId);
        bool IsInRegion(Vector2 block);
        int? GetTile(Vector2 end);
    }
}
