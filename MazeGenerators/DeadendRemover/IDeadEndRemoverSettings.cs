using MazeGenerators.Common;
using MazeGenerators.Utils;

namespace MazeGenerators.DeadEndRemover
{
    public interface IDeadEndRemoverSettings : ICommonSettings
    {
        /// <summary>
        /// Possible directions when building paths across maze.
        /// Can be <see cref="Directions.CardinalDirs"/>, <see cref="Directions.CompassDirs"/> or any custom array of normalized vectors.
        /// </summary>
        Vector2[] Directions { get; }
    }
}
