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

Fill in GeneratorSettings data with required values.

```
        // Call different maze generator algorithms in a required order:
        public GeneratorResult Generate(GeneratorSettings settings)
        {
            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            RoomGeneratorAlgorithm.GenerateRooms(result, settings, 0, 4, true, 2, 5, 5);
            TreeMazeBuilderAlgorithm.GrowMaze(result, settings, 0);
            RegionConnectorAlgorithm.GenerateConnectors(result, settings, 0);
            DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings);
            WallSurroundingAlgorithm.BuildWalls(result, settings);
            return result;
        }

        // Or usimg fluent syntax
        public GeneratorResult GenerateFluent(GeneratorSettings settings)
        {
            
            return Fluent
                .Build(settings)
                .GenerateField()
                .GenerateRooms(0, 4, true, 2, 5, 5)
                .GrowMaze(0)
                .GenerateConnectors(0)
                .RemoveDeadEnds()
                .BuildWalls()
                .result;
        }
```

Algorithms
==========

CustomDrawAlgorithm - Add custom shapes of any type (fillrect, rectngle, point)

DeadEndRemoverAlgorithm - Remove dead ends - path tiles that have less then two paths connected

FieldGeneratorAlgorithm - Create field and check input parameters for correctness.

MirroringAlgorithm - Mirror existing maze Horizontally/Vertically/Both or rotate it around center

RegionConnectorAlgorithm - Generate connectors between rooms and paths

RoomGeneratorAlgorithm - Generate unconnected rooms

StringParserAlgorithm - Print and parse maze to/from string

TreeMazeBuilderAlgorithm - Generate unconnected maze paths in free spaces

WallSurroundingAlgorithm - Build walls around all passages, junctions and rooms

Credits
==========

- [**Rooms and mazes**](https://journal.stuffwithstuff.com/2014/12/21/rooms-and-mazes/) - How to generate maze with rooms
- [**Pixel dungeion**](https://github.com/watabou/pixel-dungeon) - Images in example
