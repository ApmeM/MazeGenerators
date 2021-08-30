namespace MazeGenerators.Common
{
    public interface ICommonSettings
    {
        /// <summary>
        /// Width of the maze. 
        /// Must be odd-sized. 
        /// Left and Right columns will always contains walls.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Height of the maze. 
        /// Must be odd-sized. 
        /// Left and Right columns will always contains walls.
        /// </summary>
        int Height { get; }
    }
}
