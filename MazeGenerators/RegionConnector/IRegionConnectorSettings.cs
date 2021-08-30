using MazeGenerators.Common;
using MazeGenerators.Utils;
using System;

namespace MazeGenerators.RegionConnector
{
    public interface IRegionConnectorSettings : ICommonSettings
    {
        /// <summary>
        /// Possible directions when building paths across maze.
        /// Can be <see cref="Directions.CardinalDirs"/>, <see cref="Directions.CompassDirs"/> or any custom array of normalized vectors.
        /// </summary>
        Vector2[] Directions { get; }

        /// <summary>
        /// Specify The number of additional passages to make maze not single-connected.
        ///  Increasing this leads to more loosely connected maze.
        /// </summary>
        int AdditionalPassagesTries { get; }


        /// <summary>
        /// Specify value that will be used to call SetTile for junctions.
        /// </summary>
        int JunctionTileId { get; }

        /// <summary>
        /// Random generator. 
        /// You can change it to your system wide random. 
        /// Or set with specified seed to make it more predictable.
        /// </summary>
        Random Random { get; }
    }
}
