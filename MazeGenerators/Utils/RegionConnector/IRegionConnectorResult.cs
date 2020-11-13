using System.Collections.Generic;

namespace MazeGenerators.Utils.RegionConnector
{
    public interface IRegionConnectorResult
    {
        List<Vector2> Junctions { get; }

        int? GetTile(Vector2 pos);
        bool IsInRegion(Vector2 vector2);
        void SetTile(Vector2 pos, int connectorId);
    }
}
