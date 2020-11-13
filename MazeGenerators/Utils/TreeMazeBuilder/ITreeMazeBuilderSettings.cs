using System;

namespace MazeGenerators.Utils.RegionConnector
{
    public interface ITreeMazeBuilderSettings
    {
        Vector2[] Directions { get; }
        Random Random { get; }
        int WindingPercent { get; }
    }
}
