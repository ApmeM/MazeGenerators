namespace MazeGenerators.Tests
{
    using System;
    using System.Collections.Generic;
    using MazeGenerators.Common;
    using MazeGenerators.DeadEndRemover;
    using MazeGenerators.StringParser;
    using MazeGenerators.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class DeadendRemoverAlgorithmTest
    {
        public class Result : IDeadEndRemoverResult, IStringParserResult
        {
            public int?[,] Paths { get; set; }
            public List<Vector2> Junctions { get; set; } = new List<Vector2>();
        }

        public class Settings : IDeadEndRemoverSettings, IStringParserSettings
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public Vector2[] Directions => Utils.Directions.CardinalDirs;
            public string MazeText { get; set; }
            public int MazeTileId { get; set; } = 1;
            public int JunctionTileId { get; set; } = 2;
        }

        [Test]
        public void RemoveDeadEnds_LinearMaze_AllRemoved()
        {
            var settings = new Settings
            {
                Width = 3,
                Height = 5,
                MazeText = 
                "###\n" +
                "# #\n" +
                "# #\n" +
                "# #\n" +
                "###\n"
            };
            var result = new Result();
            CommonAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings);
            DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings);
            Assert.AreEqual(
"###\r\n" +
"###\r\n" + 
"###\r\n" + 
"###\r\n" + 
"###\r\n", StringParserAlgorithm.Stringify(result, settings));
        }

        [Test]
        public void RemoveDeadEnds_NoDeadEnd_NothingChanged()
        {
            var settings = new Settings
            {
                Width = 5,
                Height = 5,
                MazeText =
                "#####\n" +
                "#   #\n" +
                "# # #\n" +
                "#   #\n" +
                "#####\n"
            };
            var result = new Result();
            CommonAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings);
            DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings);
            Assert.AreEqual(
"#####\r\n" +
"#   #\r\n" +
"# # #\r\n" +
"#   #\r\n" +
"#####\r\n", StringParserAlgorithm.Stringify(result, settings));
        }

        [Test]
        public void RemoveDeadEnds_MixedMaze_OnlyDeadendsRemoved()
        {
            var settings = new Settings
            {
                Width = 5,
                Height = 5,
                MazeText =
                "#####\n" +
                "#   #\n" +
                "#  ##\n" +
                "#   #\n" +
                "#####\n"
            };
            var result = new Result();
            CommonAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings);
            DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings);
            Assert.AreEqual(
"#####\r\n" +
"#  ##\r\n" +
"#  ##\r\n" +
"#  ##\r\n" +
"#####\r\n", StringParserAlgorithm.Stringify(result, settings));
        }
    }
}
