using MazeGenerators.Common;
using MazeGenerators.Utils;
using System.Collections.Generic;

namespace MazeGenerators.RoomGenerator
{
    public interface IRoomGeneratorResult : ICommonResult
    {
        /// <summary>
        /// List of generated rooms.
        /// Output number depends on <see cref="IRoomGeneratorSettings.NumRoomTries"/>
        /// </summary>
        List<Rectangle> Rooms { get; }
    }
}
