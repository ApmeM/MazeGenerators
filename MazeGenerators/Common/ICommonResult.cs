namespace MazeGenerators.Common
{
    public interface ICommonResult
    {
        /// <summary>
        /// Actual generated maze data
        /// </summary>
        int?[,] Paths { get; set; }
    }
}
