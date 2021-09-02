namespace MazeGenerators.Tests
{
    using System;
    using System.Collections.Generic;
    using MazeGenerators.Common;
    using MazeGenerators.RoomGenerator;
    using MazeGenerators.StringParser;
    using MazeGenerators.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class RoomGeneratorAlgorithmTest
    {
        public class Result : IRoomGeneratorResult
        {
            public int?[,] Paths { get; set; }
            public List<Rectangle> Rooms { get; set; } = new List<Rectangle>();
        }

        public class Settings : IRoomGeneratorSettings
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public Random Random => new Random(0);
            public int NumRoomTries { get; set; } = 10;
            public int MinRoomSize { get; set; } = 2;
            public int MaxRoomSize { get; set; } = 4;
            public int MaxWidthHeightRoomSizeDifference { get; set; } = 3;
            public bool PreventOverlappedRooms { get; set; }
            public int RoomTileId { get; set; } = 3;
        }


        [Test]
        public void GenerateRooms_RoomSizeValuesInvalid_ThrowsException()
        {
            var settings = new Settings
            {
                Width = 201,
                Height = 201,
                MinRoomSize = 1000,
                MaxRoomSize = 100,
                MaxWidthHeightRoomSizeDifference = 20
            };

            var result = new Result();
            CommonAlgorithm.GenerateField(result, settings);
            Assert.Throws<Exception>(() =>
            {
                RoomGeneratorAlgorithm.GenerateRooms(result, settings);
            });
        }

        [Test]
        public void GenerateRooms_EqualSizeValues_AllRoomsHaveEqualSizes()
        {
            var settings = new Settings
            {
                Width = 201,
                Height = 201,
                MinRoomSize = 11,
                MaxRoomSize = 11,
                MaxWidthHeightRoomSizeDifference = 20
            };

            var result = new Result();
            CommonAlgorithm.GenerateField(result, settings);
            RoomGeneratorAlgorithm.GenerateRooms(result, settings);

            foreach (var r in result.Rooms)
            {
                Assert.AreEqual(r.Width, 11);
                Assert.AreEqual(r.Height, 11);
            }
        }

        [Test]
        public void GenerateRooms_SizeLimit_AllRoomsShouldFitCriteria()
        {
            var settings = new Settings
            {
                Width = 201,
                Height = 201,
                MinRoomSize = 10,
                MaxRoomSize = 100,
                MaxWidthHeightRoomSizeDifference = 20
            };

            var result = new Result();
            CommonAlgorithm.GenerateField(result, settings);
            RoomGeneratorAlgorithm.GenerateRooms(result, settings);

            foreach (var r in result.Rooms)
            {
                Assert.LessOrEqual(r.Width, 100);
                Assert.LessOrEqual(r.Height, 100);
                Assert.GreaterOrEqual(r.Width, 10);
                Assert.GreaterOrEqual(r.Height, 10);
                Assert.LessOrEqual(Math.Abs(r.Height - r.Width), 20);
            }
        }
    }
}
