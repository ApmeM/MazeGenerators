using MazeGenerators.Common;
using MazeGenerators.Utils;
using System;

namespace MazeGenerators.TreeMazeBuilder
{
    public interface ITreeMazeBuilderSettings : ICommonSettings
    {
        /// <summary>
        /// Possible directions when building paths across maze.
        /// Can be <see cref="Directions.CardinalDirs"/>, <see cref="Directions.CompassDirs"/> or any custom array of normalized vectors.
        /// </summary>
        Vector2[] Directions { get; }

        /// <summary>
        /// Chance to turn direction during tree maze generation between rooms.
        /// The less this value the longer stright paths will be generated.
        /// </summary>
        int WindingPercent { get; }

        /// <summary>
        /// Specify value that will be used to call SetTile for paths.
        /// </summary>
        int MazeTileId { get; }

        /// <summary>
        /// Random generator. 
        /// You can change it to your system wide random. 
        /// Or set with specified seed to make it more predictable.
        /// </summary>
        Random Random { get; }
    }
}