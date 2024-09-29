MazeGenerators
==========

MazeGenerators is a set of helper classes to generate different types of mazes.

Examples
==========

Dynamic example can be found here: https://apmem.github.io/MazeGenerators/Godot.html

Here are a few examples of mazes that can be generated:

Maze that contains rooms.

![](https://github.com/ApmeM/MazeGenerators/raw/main/Images/RoomMazeGenerator.png)

Regular tree maze.

![](https://github.com/ApmeM/MazeGenerators/raw/main/Images/TreeMazeGenerator.png)

Game of life generator.

![](https://github.com/ApmeM/MazeGenerators/raw/main/Images/GameOfLifeGnerator.png)

Usage
==========

Fill in GeneratorSettings data with required values.

```
    var maze = new Maze(10,10)
        .TryAddRoom(true, 2, 5, 5)
        .TryAddRoom(true, 2, 5, 5)
        .TryAddRoom(true, 2, 5, 5)
        .TryAddRoom(true, 2, 5, 5)
        .GrowMaze()
        .GenerateConnectors()
        .RemoveDeadEnds()
        .BuildWalls();
```

Algorithms
==========

CustomDrawAlgorithm - Add custom shapes of any type (fillrect, rectngle, point)

DeadEndRemoverAlgorithm - Remove dead ends - path tiles that have less then two paths connected

FieldGeneratorAlgorithm - Create field and check input parameters for correctness.

LifeGameAlgorithm - Generator based on "game of life" to create

MirroringAlgorithm - Mirror existing maze Horizontally/Vertically/Both or rotate it around center

RegionConnectorAlgorithm - Generate connectors between rooms and paths through empty tiles (EmptyTileId)

RoomGeneratorAlgorithm - Generate unconnected rooms

StringParserAlgorithm - Print and parse maze to/from string

TreeMazeBuilderAlgorithm - Generate unconnected maze paths in free spaces

WallSurroundingAlgorithm - Build walls around all passages, junctions and rooms

Credits
==========

- [**Rooms and mazes**](https://journal.stuffwithstuff.com/2014/12/21/rooms-and-mazes/) - How to generate maze with rooms
- [**Pixel dungeion**](https://github.com/watabou/pixel-dungeon) - Images in demo
