using MazeGenerators.Common;

namespace MazeGenerators.StringParser
{
    public interface IStringParserSettings : ICommonSettings
    {
        /// <summary>
        /// Maze string presentation in format
        /// </summary>
        string MazeText { get; }

        /// <summary>
        /// Specify value that will be used to call SetTile for paths.
        /// </summary>
        int MazeTileId { get; set; }

        /// <summary>
        /// Specify value that will be used to call SetTile for junctions.
        /// </summary>
        int JunctionTileId { get; set; }
    }
}
