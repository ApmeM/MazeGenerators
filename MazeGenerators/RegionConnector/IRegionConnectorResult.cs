﻿using MazeGenerators.Common;
using MazeGenerators.Utils;
using System.Collections.Generic;

namespace MazeGenerators.RegionConnector
{
    public interface IRegionConnectorResult : ICommonResult
    {
        /// <summary>
        /// Junctions between different branches of a tree maze.
        /// </summary>
        List<Vector2> Junctions { get; }
    }
}
