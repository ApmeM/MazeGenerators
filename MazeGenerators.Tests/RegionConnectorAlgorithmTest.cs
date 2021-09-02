namespace MazeGenerators.Tests
{
    using System;
    using System.Collections.Generic;
    using MazeGenerators.Common;
    using MazeGenerators.RegionConnector;
    using MazeGenerators.StringParser;
    using MazeGenerators.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class RegionConnectorAlgorithmTest
    {
        public class Result : IRegionConnectorResult, IStringParserResult
        {
            public int?[,] Paths { get; set; }
            public List<Vector2> Junctions { get; set; } = new List<Vector2>();
        }

        public class Settings : IRegionConnectorSettings, IStringParserSettings
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public Vector2[] Directions => Utils.Directions.CardinalDirs;
            public string MazeText { get; set; }
            public int MazeTileId { get; set; } = 1;
            public int JunctionTileId { get; set; } = 2;
            public int AdditionalPassagesTries { get; set; }
            public Random Random => new Random(0);
        }

        [Test]
        public void GenerateConnectors_SingleArea_NothingChanged()
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
            RegionConnectorAlgorithm.GenerateConnectors(result, settings);
            Assert.AreEqual(
"###\r\n" +
"# #\r\n" +
"# #\r\n" +
"# #\r\n" +
"###\r\n", StringParserAlgorithm.Stringify(result, settings));
            Assert.AreEqual(0, result.Junctions.Count);
        }

        [Test]
        public void GenerateConnectors_MultipleAreas_ConnectorGenerated()
        {
            var settings = new Settings
            {
                Width = 3,
                Height = 5,
                MazeText =
                "###\n" +
                "# #\n" +
                "###\n" +
                "# #\n" +
                "###\n"
            };
            var result = new Result();
            CommonAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings);
            RegionConnectorAlgorithm.GenerateConnectors(result, settings);
            Assert.AreEqual(
"###\r\n" +
"# #\r\n" +
"#-#\r\n" +
"# #\r\n" +
"###\r\n", StringParserAlgorithm.Stringify(result, settings));
            Assert.AreEqual(1, result.Junctions.Count);
            Assert.AreEqual(new Vector2(1, 2), result.Junctions[0]);
        }

        [Test]
        public void GenerateConnectors_RandomConnectorsRequested_ConnectorsAdded()
        {
            var settings = new Settings
            {
                Width = 5,
                Height = 5,
                AdditionalPassagesTries = 100,
                MazeText =
                "#####\n" +
                "#   #\n" +
                "#####\n" +
                "#   #\n" +
                "#####\n"
            };

            var result = new Result();
            CommonAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings);
            RegionConnectorAlgorithm.GenerateConnectors(result, settings);
            Assert.AreEqual(
"#####\r\n" +
"#   #\r\n" +
"#-#-#\r\n" +
"#   #\r\n" +
"#####\r\n", StringParserAlgorithm.Stringify(result, settings));
            Assert.AreEqual(2, result.Junctions.Count);
            Assert.AreEqual(new Vector2(3, 2), result.Junctions[0]);
        }
    }
}
