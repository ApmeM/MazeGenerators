﻿namespace MazeGenerators.Tests
{
    using NUnit.Framework;
    using MazeGenerators;

    [TestFixture]
    public class MirroringAlgorithmTest
    {
        [Test]
        public void Mirror_Horizontal_Mirrored()
        {
            var result = new Maze(7, 7).Parse(
"#######\n" +
"# #####\n" +
"# ### #\n" +
"# #   #\n" +
"# #-# #\n" +
"#   ###\n" +
"#######\n");
            result.DrawFullRect(new Rectangle(3, 1, 3, 1), Tile.MazeTileId);
            MirroringAlgorithm.Mirror(result, MirroringAlgorithm.MirrorDirection.Horizontal);

            Assert.AreEqual(
"###########\n" +
"# #.....# #\n" +
"# ### ### #\n" +
"# #     # #\n" +
"# #-# #-# #\n" +
"#   ###   #\n" +
"###########\n", result.Stringify());
            Assert.AreEqual(11, result.Width);
            Assert.AreEqual(7, result.Height);
            Assert.AreEqual(new Vector2(3, 4), result.Junctions[0]);
            Assert.AreEqual(new Vector2(7, 4), result.Junctions[1]);
        }

        [Test]
        public void Mirror_Vertical_Mirrored()
        {
            var result = new Maze(7, 7).Parse(
"#######\n" +
"#.#####\n" +
"# ### #\n" +
"# #   #\n" +
"# #-# #\n" +
"#   ###\n" +
"#######\n");
            result.DrawFullRect(new Rectangle(3, 1, 3, 1), Tile.MazeTileId);
            result.Mirror(MirroringAlgorithm.MirrorDirection.Vertical);

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
"#######\n", result.Stringify());
            Assert.AreEqual(7, result.Width);
            Assert.AreEqual(11, result.Height);
            Assert.AreEqual(new Vector2(3, 4), result.Junctions[0]);
            Assert.AreEqual(new Vector2(3, 6), result.Junctions[1]);
        }

        [Test]
        public void Mirror_Both_Mirrored()
        {
            var result = new Maze(7, 7).Parse(
"#######\n" +
"#.#####\n" +
"# ### #\n" +
"# #   #\n" +
"# #-# #\n" +
"#   ###\n" +
"#######\n");
            result.DrawFullRect(new Rectangle(3, 1, 3, 1), Tile.MazeTileId);
            result.Mirror(MirroringAlgorithm.MirrorDirection.Both);

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
"###########\n", result.Stringify());
            Assert.AreEqual(11, result.Width);
            Assert.AreEqual(11, result.Height);
            Assert.AreEqual(new Vector2(3, 4), result.Junctions[0]);
            Assert.AreEqual(new Vector2(3, 6), result.Junctions[1]);
            Assert.AreEqual(new Vector2(7, 4), result.Junctions[2]);
            Assert.AreEqual(new Vector2(7, 6), result.Junctions[3]);
        }

        [Test]
        public void Mirror_Rotate_Mirrored()
        {
            var result = new Maze(7, 7).Parse(
"#######\n" +
"#.#####\n" +
"# ### #\n" +
"# #   #\n" +
"# #-# #\n" +
"#   ###\n" +
"#######\n");
            result.DrawFullRect(new Rectangle(3, 1, 3, 1), Tile.MazeTileId);
            result.Mirror(MirroringAlgorithm.MirrorDirection.Rotate);

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
"###########\n", result.Stringify());
            Assert.AreEqual(11, result.Width);
            Assert.AreEqual(11, result.Height);
            Assert.AreEqual(new Vector2(3, 4), result.Junctions[0]);
            Assert.AreEqual(new Vector2(6, 3), result.Junctions[1]);
            Assert.AreEqual(new Vector2(7, 6), result.Junctions[2]);
            Assert.AreEqual(new Vector2(4, 7), result.Junctions[3]);
        }

        [Test]
        public void Mirror_FullMazeIsRoom_Mirrored()
        {
            var result = new Maze(5, 5);
            result.DrawFullRect(new Rectangle(1, 1, 3, 3), Tile.MazeTileId);
            MirroringAlgorithm.Mirror(result, MirroringAlgorithm.MirrorDirection.Rotate);

            Assert.AreEqual(
"       \n" +
" ..... \n" +
" ..... \n" +
" ..... \n" +
" ..... \n" +
" ..... \n" +
"       \n", result.Stringify());
            Assert.AreEqual(7, result.Width);
            Assert.AreEqual(7, result.Height);
            Assert.AreEqual(0, result.Junctions.Count);
        }


        [Test]
        public void Mirror_EvenSized_Mirrored()
        {
            var result = new Maze(6, 6).Parse(
"######\n" +
"#    #\n" +
"# ####\n" +
"# ####\n" +
"# ####\n" +
"# ####\n" +
"######\n");
            MirroringAlgorithm.Mirror(result, MirroringAlgorithm.MirrorDirection.Rotate);

            Assert.AreEqual(
"#########\n" +
"#       #\n" +
"# ##### #\n" +
"# ##### #\n" +
"# ##### #\n" +
"# ##### #\n" +
"# ##### #\n" +
"#       #\n" +
"#########\n", result.Stringify());
            Assert.AreEqual(9, result.Width);
            Assert.AreEqual(9, result.Height);
        }
    }
}
