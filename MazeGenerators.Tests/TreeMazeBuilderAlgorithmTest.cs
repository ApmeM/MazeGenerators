namespace MazeGenerators.Tests
{
    using System;
    using System.Collections.Generic;
    using MazeGenerators.Common;
    using MazeGenerators.TreeMazeBuilder;
    using MazeGenerators.StringParser;
    using MazeGenerators.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class TreeMazeBuilderAlgorithmTest
    {
        public class Result : ITreeMazeBuilderResult, IStringParserResult
        {
            public int?[,] Paths { get; set; }

            public List<Vector2> Junctions { get; set; } = new List<Vector2>();
        }

        public class Settings : ITreeMazeBuilderSettings, IStringParserSettings
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public Vector2[] Directions => Utils.Directions.CardinalDirs;
            public int MazeTileId { get; set; } = 1;
            public Random Random => new Random(0);
            public int WindingPercent { get; set; }

            public string MazeText { get; set; }

            public int JunctionTileId { get; set; }
        }

        [Test]
        public void GrowMaze_NoWinding_StrightLine()
        {
            var settings = new Settings
            {
                Width = 7,
                Height = 7
            };
            var result = new Result();
            CommonAlgorithm.GenerateField(result, settings);
            TreeMazeBuilderAlgorithm.GrowMaze(result, settings);
            Assert.AreEqual(
"#######\r\n" +
"# #   #\r\n" +
"# # # #\r\n" +
"# # # #\r\n" +
"# ### #\r\n" +
"#     #\r\n" +
"#######\r\n", StringParserAlgorithm.Stringify(result, settings));
        }
    }
}







