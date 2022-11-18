﻿namespace MazeGenerators.Tests
{
    using System;
    using MazeGenerators;
    using MazeGenerators.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class RoomGeneratorAlgorithmTest
    {
        [Test]
        public void GenerateRooms_RoomSizeValuesInvalid_ThrowsException()
        {
            var settings = new GeneratorSettings
            {
                Width = 201,
                Height = 201,
            };

            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            Assert.Throws<Exception>(() =>
            {
                RoomGeneratorAlgorithm.GenerateRooms(result, settings, 100, 4, true, 1000, 100, 20);
            });
        }

        [Test]
        public void GenerateRooms_EqualSizeValues_AllRoomsHaveEqualSizes()
        {
            var settings = new GeneratorSettings
            {
                Width = 201,
                Height = 201,
            };

            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            RoomGeneratorAlgorithm.GenerateRooms(result, settings, 100, 4, true, 11, 11, 20);

            foreach (var r in result.Rooms)
            {
                Assert.AreEqual(r.Width, 11);
                Assert.AreEqual(r.Height, 11);
            }
        }

        [Test]
        public void GenerateRooms_SizeLimit_AllRoomsShouldFitCriteria()
        {
            var settings = new GeneratorSettings
            {
                Width = 201,
                Height = 201,
            };

            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            RoomGeneratorAlgorithm.GenerateRooms(result, settings, 100, 4, true, 10, 100, 20);

            foreach (var r in result.Rooms)
            {
                Assert.LessOrEqual(r.Width, 100);
                Assert.LessOrEqual(r.Height, 100);
                Assert.GreaterOrEqual(r.Width, 10);
                Assert.GreaterOrEqual(r.Height, 10);
                Assert.LessOrEqual(Math.Abs(r.Height - r.Width), 20);
            }
        }

        [Test]
        public void GenerateRooms_LimitNumberOfGeneratedRooms_CreatedRequiredNumber()
        {
            var settings = new GeneratorSettings
            {
                Width = 201,
                Height = 201,
            };

            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            RoomGeneratorAlgorithm.GenerateRooms(result, settings, 100, 2, true, 10, 11, 20);

            Assert.AreEqual(2, result.Rooms.Count);
        }

        [Test]
        public void AddRoom_OutOfRange_Exception()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
            };

            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            Assert.Throws<IndexOutOfRangeException>(() => RoomGeneratorAlgorithm.AddRoom(result, settings, new Rectangle(4, 4, 8, 8)));
        }

        [Test]
        public void AddRoom_ValidValues_RoomAndPathAdded()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
            };

            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            RoomGeneratorAlgorithm.AddRoom(result, settings, new Rectangle(2, 2, 2, 2));

            Assert.AreEqual(1, result.Rooms.Count);
            Assert.AreEqual(new Rectangle(2, 2, 2, 2), result.Rooms[0]);
            Assert.AreEqual(
"       \n" +
"       \n" +
"  ..   \n" +
"  ..   \n" +
"       \n" +
"       \n" +
"       \n", StringParserAlgorithm.Stringify(result, settings));
        }
    }
}
