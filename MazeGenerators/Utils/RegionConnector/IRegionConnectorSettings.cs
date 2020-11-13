using System;

namespace MazeGenerators.Utils.RegionConnector
{
    public interface IRegionConnectorSettings
    {
        int Width { get; }
        int Height { get; }
        Vector2[] Directions { get; }
        bool RemoveDeadEnds { get; }
        int AdditionalPassages { get; }
        Random Random { get; }
    }
}
