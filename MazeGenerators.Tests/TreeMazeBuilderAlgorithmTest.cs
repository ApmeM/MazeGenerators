﻿namespace MazeGenerators.Tests
{
    using MazeGenerators.Utils;
    using NUnit.Framework;
    using MazeGenerators;
    using System;

    [TestFixture]
    public class TreeMazeBuilderAlgorithmTest
    {
        [Test]
        public void GrowMaze_NoWinding_StrightLine()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
                Random = new Random(0),
            };
            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            TreeMazeBuilderAlgorithm.GrowMaze(result, settings, 0);
            Assert.AreEqual(
"       \n" +
" . ... \n" +
" . . . \n" +
" . . . \n" +
" .   . \n" +
" ..... \n" +
"       \n", StringParserAlgorithm.Stringify(result, settings));
        }

        [Test]
        public void GrowMaze_WindingAlways_TreeMaze()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
                Random = new Random(0)
            };
            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            TreeMazeBuilderAlgorithm.GrowMaze(result, settings, 100);
            Assert.AreEqual(
"       \n" +
" . ... \n" +
" .   . \n" +
" . ... \n" +
" . . . \n" +
" ... . \n" +
"       \n", StringParserAlgorithm.Stringify(result, settings));
        }

    }
}
