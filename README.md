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

Call different maze generator algorithms in a required order:

```
        public GeneratorResult Generate(GeneratorSettings settings)
        {
            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            RoomGeneratorAlgorithm.GenerateRooms(result, settings);
            TreeMazeBuilderAlgorithm.GrowMaze(result, settings);
            RegionConnectorAlgorithm.GenerateConnectors(result, settings);
            DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings);
            return result;
        }
```

Algorithms
==========

CommonAlgorithm - Create field and check input parameters for correctness.

RoomGeneratorAlgorithm - Generate unconnected rooms

TreeMazeBuilderAlgorithm - Generate unconnected maze paths in free spaces

RegionConnectorAlgorithm - Generate connectors between rooms and paths

DeadEndRemoverAlgorithm - Remove dead ends - path tiles that have less then two paths connected

StringParserAlgorithm - Print and parse maze to/from string

MirroringAlgorithm - Mirror existing maze Horizontally/Vertically/Both or rotate it around center

Credits
==========

- [**Rooms and mazes**](https://journal.stuffwithstuff.com/2014/12/21/rooms-and-mazes/) - How to generate maze with rooms
- [**Pixel dungeion**](https://github.com/watabou/pixel-dungeon) - Images in example
