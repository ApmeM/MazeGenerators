using MazeGenerators.Common;
using System;

namespace MazeGenerators.RoomGenerator
{
    public interface IRoomGeneratorSettings : ICommonSettings
    {

        /// <summary>
        /// Number of tries to add a room.
        /// Number of rooms will probably be less then this number.
        /// - When <see cref="this.PreventOverlappedRooms"/> is true - some tries will fail.
        /// - When <see cref="this.PreventOverlappedRooms"/> is false - some rooms will contain eachother and will be invisible.
        /// </summary>
        int NumRoomTries { get; }

        /// <summary>
        /// Specify room size.
        /// Size become random from <see cref="MinRoomSize"/> to <see cref="MaxRoomSize"/> and made odd-sized.
        /// With width/height difference not bigger then <see cref="MaxWidthHeightRoomSizeDifference"/>
        /// </summary>
        int MinRoomSize { get; }

        /// <summary>
        /// Specify room size.
        /// Size become random from <see cref="MinRoomSize"/> to <see cref="MaxRoomSize"/> and made odd-sized.
        /// With width/height difference not bigger then <see cref="MaxWidthHeightRoomSizeDifference"/>
        /// </summary>
        int MaxRoomSize { get; }

        /// <summary>
        /// Specify maximum difference between room width and height to prevent long narrow rooms.
        /// Size become random from <see cref="MinRoomSize"/> to <see cref="MaxRoomSize"/> and made odd-sized.
        /// With width/height difference not bigger then <see cref="MaxWidthHeightRoomSizeDifference"/>
        /// </summary>
        int MaxWidthHeightRoomSizeDifference { get; }

        /// <summary>
        /// Specify if rooms overlapping is prevented or not.
        /// </summary>
        bool PreventOverlappedRooms { get; }

        /// <summary>
        /// Specify value that will be used to call SetTile for room.
        /// </summary>
        int RoomTileId { get; }

        /// <summary>
        /// Random generator. 
        /// You can change it to your system wide random. 
        /// Or set with specified seed to make it more predictable.
        /// </summary>
        Random Random { get; }
    }
}
