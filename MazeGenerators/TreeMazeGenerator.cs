namespace MazeGenerators
{
    using System;
    using System.Collections.Generic;

    using MazeGenerators.Utils;
    using MazeGenerators.Utils.DeadendRemover;
    using MazeGenerators.Utils.RegionConnector;

    public class TreeMazeGenerator
    {
        public class Result : IDeadEndRemoverResult, IRegionConnectorResult, ITreeMazeBuilderResult
        {
            /// <summary>
            /// Actual generated maze data
            /// </summary>
            public int?[,] Regions;

            /// <summary>
            /// Junctions between different branches of a tree maze.
            /// Equal to <see cref="Settings.AdditionalPassagesTries"/> number.
            /// </summary>
            public List<Vector2> Junctions { get; set; }

            public int? GetTile(Vector2 pos)
            {
                return this.Regions[pos.X, pos.Y];
            }

            public void SetTile(Vector2 pos, int regionId)
            {
                this.Regions[pos.X, pos.Y] = regionId;
            }

            public void RemoveTile(Vector2 pos)
            {
                this.Regions[pos.X, pos.Y] = null;
            }

            public bool IsInRegion(Vector2 loc)
            {
                return loc.X >= 0 && loc.Y >= 0 && loc.X < this.Regions.GetLength(0) && loc.Y < this.Regions.GetLength(1);
            }
        }

        public class Settings: IDeadEndRemoverSettings, IRegionConnectorSettings, ITreeMazeBuilderSettings
        {
            /// <summary>
            /// Width of the maze. 
            /// Must be odd-sized. 
            /// Left and Right columns will always contains walls.
            /// </summary>
            public int Width { get; set; } = 21;

            /// <summary>
            /// Height of the maze. 
            /// Must be odd-sized. 
            /// Left and Right columns will always contains walls.
            /// </summary>
            public int Height { get; set; } = 21;

            /// <summary>
            /// Random generator. 
            /// You can change it to your system wide random. 
            /// Or set with specified seed to make it more predictable.
            /// </summary>
            public Random Random { get; set; } = new Random();

            /// <summary>
            /// Possible directions when building paths across maze.
            /// Can be <see cref="Utils.Directions.CardinalDirs"/>, <see cref="Utils.Directions.CompassDirs"/> or any custom array of normalized vectors.
            /// </summary>
            public Vector2[] Directions { get; set; } = Utils.Directions.CardinalDirs;

            /// <summary>
            /// Chance to turn direction during tree maze generation between rooms.
            /// The less this value the longer stright paths will be generated.
            /// </summary>
            public int WindingPercent { get; set; } = 100;

            /// <summary>
            /// Specify The number of additional passages to make maze not single-connected.
            /// If <see cref="AdditionalPassagesTries"/> set to 0 and <see cref="RemoveDeadEnds"/> is true - you will get empty maze as without additional passages tree maze is only deadends.
            /// </summary>
            public int AdditionalPassagesTries { get; set; } = 20;

            /// <summary>
            /// Specify if deadends from tree maze generation should be removed.
            /// If <see cref="AdditionalPassagesTries"/> set to 0 and <see cref="RemoveDeadEnds"/> is true - you will get empty maze as without additional passages tree maze is only deadends.
            /// </summary>
            public bool RemoveDeadEnds { get; set; } = true;
        }

        public Result Generate(Settings settings)
        {
            if (settings.Width % 2 == 0 || settings.Height % 2 == 0)
            {
                throw new Exception("The stage must be odd-sized.");
            }

            var result = new Result
            {
                Regions = new int?[settings.Width, settings.Height],
                Junctions = new List<Vector2>()
            };

            TreeMazeBuilderAlgorythm.GrowMaze(result, settings, new Vector2(1, 1), 1);
            RegionConnectorAlgorythm.ConnectRegions(result, settings, 2);
            DeadEndRemoverAlgorythm.RemoveDeadEnds(result, settings);

            return result;
        }
    }
}