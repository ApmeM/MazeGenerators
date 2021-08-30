MazeGenerators
==========

MazeGenerators is a set of helper classes to generate different types of mazes.

Examples
==========

Here are a few examples of mazes that can be generated:
Maze that contains rooms.

![](https://github.com/ApmeM/MazeGenerators/raw/main/Images/RoomMazeGenerator.png)

Regular tree maze.

![](https://github.com/ApmeM/MazeGenerators/raw/main/Images/TreeMazeGenerator.png)

Usage
==========

First define a class for settigns and results.
Those classes should implement different settings and results interfaces depending on what type of algorithms are required.

```
        public class Result : IRoomGeneratorResult, IDeadEndRemoverResult, IRegionConnectorResult, ITreeMazeBuilderResult
        {
            public int?[,] Paths { get; set; }
            public List<Vector2> Junctions { get; } = new List<Vector2>();
            public List<Rectangle> Rooms { get; } = new List<Rectangle>();
        }

        public class Settings : IRoomGeneratorSettings, IDeadEndRemoverSettings, IRegionConnectorSettings, ITreeMazeBuilderSettings
        {
            public int Width { get; set; } = 21;
            public int Height { get; set; } = 21;
            public Random Random { get; set; } = new Random();
            public Vector2[] Directions { get; set; } = Utils.Directions.CardinalDirs;
            public int NumRoomTries { get; set; } = 100;
            public bool PreventOverlappedRooms { get; set; } = true;
            public int MinRoomSize { get; set; } = 2;
            public int MaxRoomSize { get; set; } = 5;
            public int MaxWidthHeightRoomSizeDifference { get; set; } = 5;
            public int WindingPercent { get; set; } = 0;
            public int AdditionalPassagesTries { get; set; } = 10;
            public bool RemoveDeadEnds { get; set; } = true;
            public int RoomTileId { get; set; } = 1;
            public int MazeTileId { get; set; } = 1;
            public int JunctionTileId { get; set; } = 1;
        }
```

Then call algorithms itself in a required order:
```
        public Result Generate(Settings settings)
        {
            var result = new Result();
            CommonAlgorithm.GenerateField(result, settings); // Create field and check input parameters for correctness.
            RoomGeneratorAlgorithm.GenerateRooms(result, settings); // Generate unconnected rooms
            TreeMazeBuilderAlgorithm.GrowMaze(result, settings); // Generate unconnected maze paths in free spaces
            RegionConnectorAlgorithm.GenerateConnectors(result, settings); // Generate connectors between rooms and paths
            DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings); // Remove dead ends - path tiles that have less then two paths connected
            return result;
        }
```

Credits
==========

- [**Rooms and mazes**](https://journal.stuffwithstuff.com/2014/12/21/rooms-and-mazes/) - How to generate maze with rooms
- [**Pixel dungeion**](https://github.com/watabou/pixel-dungeon) - Images in example
