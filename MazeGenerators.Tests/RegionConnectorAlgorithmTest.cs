﻿namespace MazeGenerators.Tests
{
    using MazeGenerators;
    using MazeGenerators.Utils;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class RegionConnectorAlgorithmTest
    {
        [Test]
        public void GenerateConnectors_SingleArea_NothingChanged()
        {
            var settings = new GeneratorSettings
            {
                Width = 3,
                Height = 5,
                MazeText =
                "   \n" +
                " . \n" +
                " . \n" +
                " . \n" +
                "   \n"
            };
            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings);
            RegionConnectorAlgorithm.GenerateConnectors(result, settings);
            Assert.AreEqual(
"   \r\n" +
" . \r\n" +
" . \r\n" +
" . \r\n" +
"   \r\n", StringParserAlgorithm.Stringify(result, settings));
            Assert.AreEqual(0, result.Junctions.Count);
        }

        [Test]
        public void GenerateConnectors_MultipleAreas_ConnectorGenerated()
        {
            var settings = new GeneratorSettings
            {
                Width = 3,
                Height = 5,
                MazeText =
                "   \n" +
                " . \n" +
                "   \n" +
                " . \n" +
                "   \n"
            };
            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings);
            RegionConnectorAlgorithm.GenerateConnectors(result, settings);
            Assert.AreEqual(
"   \r\n" +
" . \r\n" +
" - \r\n" +
" . \r\n" +
"   \r\n", StringParserAlgorithm.Stringify(result, settings));
            Assert.AreEqual(1, result.Junctions.Count);
            Assert.AreEqual(new Vector2(1, 2), result.Junctions[0]);
        }

        [Test]
        public void GenerateConnectors_RandomConnectorsRequested_ConnectorsAdded()
        {
            var settings = new GeneratorSettings
            {
                Width = 5,
                Height = 5,
                AdditionalPassagesTries = 100,
                Random = new Random(0),
                MazeText =
                "     \n" +
                " ... \n" +
                "     \n" +
                " ... \n" +
                "     \n"
            };

            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings);
            RegionConnectorAlgorithm.GenerateConnectors(result, settings);
            Assert.AreEqual(
"     \r\n" +
" ... \r\n" +
" - - \r\n" +
" ... \r\n" +
"     \r\n", StringParserAlgorithm.Stringify(result, settings));
            Assert.AreEqual(2, result.Junctions.Count);
            Assert.AreEqual(new Vector2(3, 2), result.Junctions[0]);
        }
    }
}
