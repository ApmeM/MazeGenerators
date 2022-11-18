namespace MazeGenerators.Tests
{
    using MazeGenerators.Utils;
    using NUnit.Framework;
    using MazeGenerators;
    using System;

    [TestFixture]
    public class MirroringAlgorithmTest
    {
        [Test]
        public void Mirror_Horizontal_Mirrored()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
            };
            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings,
"#######\n" +
"# #####\n" +
"# ### #\n" +
"# #   #\n" +
"# #-# #\n" +
"#   ###\n" +
"#######\n");
            CustomDrawAlgorithm.AddFillRectangle(result, settings, new Rectangle(3, 1, 3, 1), settings.RoomTileId);
            result.Rooms.Add(new Rectangle(3, 1, 3, 1));
            MirroringAlgorithm.Mirror(result, settings, MirroringAlgorithm.MirrorDirection.Horizontal);

            Assert.AreEqual(
"###########\n" +
"# #.....# #\n" +
"# ### ### #\n" +
"# #     # #\n" +
"# #-# #-# #\n" +
"#   ###   #\n" +
"###########\n", StringParserAlgorithm.Stringify(result, settings));
            Assert.AreEqual(11, settings.Width);
            Assert.AreEqual(7, settings.Height);
            Assert.AreEqual(new Vector2(3, 4), result.Junctions[0]);
            Assert.AreEqual(new Vector2(7, 4), result.Junctions[1]);
            Assert.AreEqual(new Rectangle(3, 1, 3, 1), result.Rooms[0]);
            Assert.AreEqual(new Rectangle(5, 1, 3, 1), result.Rooms[1]);
        }

        [Test]
        public void Mirror_Vertical_Mirrored()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
            };
            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings,
"#######\n" +
"#.#####\n" +
"# ### #\n" +
"# #   #\n" +
"# #-# #\n" +
"#   ###\n" +
"#######\n");
            CustomDrawAlgorithm.AddFillRectangle(result, settings, new Rectangle(3, 1, 3, 1), settings.RoomTileId);
            result.Rooms.Add(new Rectangle(3, 1, 3, 1));
            MirroringAlgorithm.Mirror(result, settings, MirroringAlgorithm.MirrorDirection.Vertical);

            Assert.AreEqual(
"#######\n" +
"#.#...#\n" +
"# ### #\n" +
"# #   #\n" +
"# #-# #\n" +
"#   ###\n" +
"# #-# #\n" +
"# #   #\n" +
"# ### #\n" +
"#.#...#\n" +
"#######\n", StringParserAlgorithm.Stringify(result, settings));
            Assert.AreEqual(7, settings.Width);
            Assert.AreEqual(11, settings.Height);
            Assert.AreEqual(new Vector2(3, 4), result.Junctions[0]);
            Assert.AreEqual(new Vector2(3, 6), result.Junctions[1]);
            Assert.AreEqual(new Rectangle(3, 1, 3, 1), result.Rooms[0]);
            Assert.AreEqual(new Rectangle(3, 9, 3, 1), result.Rooms[1]);
        }

        [Test]
        public void Mirror_Both_Mirrored()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
            };
            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings,
"#######\n" +
"#.#####\n" +
"# ### #\n" +
"# #   #\n" +
"# #-# #\n" +
"#   ###\n" +
"#######\n");
            CustomDrawAlgorithm.AddFillRectangle(result, settings, new Rectangle(3, 1, 3, 1), settings.RoomTileId);
            result.Rooms.Add(new Rectangle(3, 1, 3, 1));
            MirroringAlgorithm.Mirror(result, settings, MirroringAlgorithm.MirrorDirection.Both);

            Assert.AreEqual(
"###########\n" +
"#.#.....#.#\n" +
"# ### ### #\n" +
"# #     # #\n" +
"# #-# #-# #\n" +
"#   ###   #\n" +
"# #-# #-# #\n" +
"# #     # #\n" +
"# ### ### #\n" +
"#.#.....#.#\n" +
"###########\n", StringParserAlgorithm.Stringify(result, settings));
            Assert.AreEqual(11, settings.Width);
            Assert.AreEqual(11, settings.Height);
            Assert.AreEqual(new Vector2(3, 4), result.Junctions[0]);
            Assert.AreEqual(new Vector2(3, 6), result.Junctions[1]);
            Assert.AreEqual(new Vector2(7, 4), result.Junctions[2]);
            Assert.AreEqual(new Vector2(7, 6), result.Junctions[3]);
            Assert.AreEqual(new Rectangle(3, 1, 3, 1), result.Rooms[0]);
            Assert.AreEqual(new Rectangle(3, 9, 3, 1), result.Rooms[1]);
            Assert.AreEqual(new Rectangle(5, 1, 3, 1), result.Rooms[2]);
            Assert.AreEqual(new Rectangle(5, 9, 3, 1), result.Rooms[3]);
        }

        [Test]
        public void Mirror_Rotate_Mirrored()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
            };
            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings,
"#######\n" +
"#.#####\n" +
"# ### #\n" +
"# #   #\n" +
"# #-# #\n" +
"#   ###\n" +
"#######\n");
            CustomDrawAlgorithm.AddFillRectangle(result, settings, new Rectangle(3, 1, 3, 1), settings.RoomTileId);
            result.Rooms.Add(new Rectangle(3, 1, 3, 1));
            MirroringAlgorithm.Mirror(result, settings, MirroringAlgorithm.MirrorDirection.Rotate);

            Assert.AreEqual(
"###########\n" +
"#.#...   .#\n" +
"# ### #####\n" +
"# #   - #.#\n" +
"# #-# # #.#\n" +
"#.   #   .#\n" +
"#.# # #-# #\n" +
"#.# -   # #\n" +
"##### ### #\n" +
"#.   ...#.#\n" +
"###########\n", StringParserAlgorithm.Stringify(result, settings));
            Assert.AreEqual(11, settings.Width);
            Assert.AreEqual(11, settings.Height);
            Assert.AreEqual(new Vector2(3, 4), result.Junctions[0]);
            Assert.AreEqual(new Vector2(6, 3), result.Junctions[1]);
            Assert.AreEqual(new Vector2(7, 6), result.Junctions[2]);
            Assert.AreEqual(new Vector2(4, 7), result.Junctions[3]);
            Assert.AreEqual(new Rectangle(3, 1, 3, 1), result.Rooms[0]);
            Assert.AreEqual(new Rectangle(9, 3, 1, 3), result.Rooms[1]);
            Assert.AreEqual(new Rectangle(5, 9, 3, 1), result.Rooms[2]);
            Assert.AreEqual(new Rectangle(1, 5, 1, 3), result.Rooms[3]);
        }

        [Test]
        public void Mirror_FullMazeIsRoom_Mirrored()
        {
            var settings = new GeneratorSettings
            {
                Width = 5,
                Height = 5,
            };
            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            CustomDrawAlgorithm.AddFillRectangle(result, settings, new Rectangle(1, 1, 3, 3), settings.RoomTileId);
            result.Rooms.Add(new Rectangle(1, 1, 3, 3));
            MirroringAlgorithm.Mirror(result, settings, MirroringAlgorithm.MirrorDirection.Rotate);

            Assert.AreEqual(
"       \n" +
" ..... \n" +
" ..... \n" +
" ..... \n" +
" ..... \n" +
" ..... \n" +
"       \n", StringParserAlgorithm.Stringify(result, settings));
            Assert.AreEqual(7, settings.Width);
            Assert.AreEqual(7, settings.Height);
            Assert.AreEqual(0, result.Junctions.Count);
            Assert.AreEqual(4, result.Rooms.Count);
            Assert.AreEqual(new Rectangle(1, 1, 3, 3), result.Rooms[0]);
            Assert.AreEqual(new Rectangle(3, 1, 3, 3), result.Rooms[1]);
            Assert.AreEqual(new Rectangle(3, 3, 3, 3), result.Rooms[2]);
            Assert.AreEqual(new Rectangle(1, 3, 3, 3), result.Rooms[3]);
        }

        
        [Test]
        public void Mirror_EvenSized_Mirrored()
        {
            var settings = new GeneratorSettings
            {
                Width = 6,
                Height = 6,
            };
            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings,
"######\n" +
"#    #\n" +
"# ####\n" +
"# ####\n" +
"# ####\n" +
"# ####\n" +
"######\n");
            MirroringAlgorithm.Mirror(result, settings, MirroringAlgorithm.MirrorDirection.Rotate);

            Assert.AreEqual(
"#########\n" +
"#       #\n" +
"# ##### #\n" +
"# ##### #\n" +
"# ##### #\n" +
"# ##### #\n" +
"# ##### #\n" +
"#       #\n" +
"#########\n", StringParserAlgorithm.Stringify(result, settings));
            Assert.AreEqual(9, settings.Width);
            Assert.AreEqual(9, settings.Height);
        }
    }
}
