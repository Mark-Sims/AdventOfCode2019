using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = Environment.CurrentDirectory + "//input.txt";

            // Input file should be a single line
            string line = System.IO.File.ReadAllLines(inputFile)[0];

            IntcodeInterpreter discoveryInterpreter = new IntcodeInterpreter(line, isInterractiveMode: false);
            Droid mazeDiscoveryDroid = new Droid(0, 0, discoveryInterpreter);

            var solver = new DroidMazeSolver(line);
            solver.DiscoverMaze(mazeDiscoveryDroid);

            var start = (0, 0);
            var end = solver.OxygenSystemLocation;
            solver.FindShortestPathUsingBFS(start, end);

            solver.PrintMaze();
        }
    }

    class DroidMazeSolver
    {

        private string _program;
        private Dictionary<(int, int), Cell> _knownCells;

        public (int, int) OxygenSystemLocation { get; set; }

        public List<(int, int)> ShortestPath;

        public DroidMazeSolver(string line)
        {
            _program = line;
            _knownCells = new Dictionary<(int, int), Cell>();
            ShortestPath = new List<(int, int)>();
        }

        private bool IsCellKnown(Droid d, Direction dir)
        {
            if (dir == Direction.North)
            {
                return _knownCells.ContainsKey((d.PositionX, d.PositionY + 1));
            }

            if (dir == Direction.South)
            {
                return _knownCells.ContainsKey((d.PositionX, d.PositionY - 1));
            }

            if (dir == Direction.West)
            {
                return _knownCells.ContainsKey((d.PositionX - 1, d.PositionY));
            }

            if (dir == Direction.East)
            {
                return _knownCells.ContainsKey((d.PositionX + 1, d.PositionY));
            }

            throw new Exception(string.Format("Unrecognized direction {0}", dir));
        }

        public void DFS(Droid droid, Direction dirToStep)
        {

            Droid d = new Droid(droid.PositionX, droid.PositionY, droid.Interpreter);
            var output = d.TryStep(dirToStep);
            InterpretStepResponse(dirToStep, output, d);

            if (!IsCellKnown(d, Direction.North))
            {
                DFS(d, Direction.North);
            }

            if (!IsCellKnown(d, Direction.South))
            {
                DFS(d, Direction.South);
            }

            if (!IsCellKnown(d, Direction.West))
            {
                DFS(d, Direction.West);
            }

            if (!IsCellKnown(d, Direction.East))
            {
                DFS(d, Direction.East);
            }
        }

        public void InterpretStepResponse(Direction direction, int response, Droid d)
        {

            if (d.PositionY == -17)
            {

            }
            // Droid hit a wall
            if (response == 0)
            {
                switch (direction)
                {
                    case Direction.North:
                        _knownCells.TryAdd((d.PositionX, d.PositionY + 1), Cell.Obstacle);
                        return;
                    case Direction.South:
                        _knownCells.TryAdd((d.PositionX, d.PositionY - 1), Cell.Obstacle);
                        return;
                    case Direction.West:
                        _knownCells.TryAdd((d.PositionX - 1, d.PositionY), Cell.Obstacle);
                        return;
                    case Direction.East:
                        _knownCells.TryAdd((d.PositionX + 1, d.PositionY), Cell.Obstacle);
                        return;
                }
            }
            else if (response == 1)
            {
                switch (direction)
                {
                    case Direction.North:
                        d.PositionY += 1;
                        _knownCells.TryAdd((d.PositionX, d.PositionY), Cell.Open);
                        return;
                    case Direction.South:
                        d.PositionY -= 1;
                        _knownCells.TryAdd((d.PositionX, d.PositionY), Cell.Open);
                        return;
                    case Direction.West:
                        d.PositionX -= 1;
                        _knownCells.TryAdd((d.PositionX, d.PositionY), Cell.Open);
                        return;
                    case Direction.East:
                        d.PositionX += 1;
                        _knownCells.TryAdd((d.PositionX, d.PositionY), Cell.Open);
                        return;
                }
            }
            else if (response == 2)
            {
                switch (direction)
                {
                    case Direction.North:
                        d.PositionY += 1;
                        _knownCells.TryAdd((d.PositionX, d.PositionY), Cell.OxygenSystem);
                        OxygenSystemLocation = (d.PositionX, d.PositionY);
                        return;
                    case Direction.South:
                        d.PositionY -= 1;
                        _knownCells.TryAdd((d.PositionX, d.PositionY), Cell.OxygenSystem);
                        OxygenSystemLocation = (d.PositionX, d.PositionY);
                        return;
                    case Direction.West:
                        d.PositionX -= 1;
                        _knownCells.TryAdd((d.PositionX, d.PositionY), Cell.OxygenSystem);
                        OxygenSystemLocation = (d.PositionX, d.PositionY);
                        return;
                    case Direction.East:
                        d.PositionX += 1;
                        _knownCells.TryAdd((d.PositionX, d.PositionY), Cell.OxygenSystem);
                        OxygenSystemLocation = (d.PositionX, d.PositionY);
                        return;
                }
            }
        }

        public void PrintMaze()
        {
            var minX = _knownCells.Keys.Select(x => x.Item1).Min();
            var minY = _knownCells.Keys.Select(x => x.Item2).Min();

            var maxX = _knownCells.Keys.Select(x => x.Item1).Max();
            var maxY = _knownCells.Keys.Select(x => x.Item2).Max();

            var width = Math.Abs(minX) + Math.Abs(maxX) + 1;
            var height = Math.Abs(minY) + Math.Abs(maxY) + 1;

            string[][] maze = new string[height][];

            for (int i = 0; i < height; i++)
            {
                maze[i] = new string[width];
            }

            foreach (var cell in _knownCells)
            {
                // The actual cell indices go into negatives, so shift all indices
                // to make them all positive for building out the 2d array.
                maze[cell.Key.Item2 + Math.Abs(minY)][cell.Key.Item1 + Math.Abs(minX)] = RepresentCell(cell.Value);
            }

            // Manually mark the droid's strating position.
            maze[Math.Abs(minY)][Math.Abs(minX)] = RepresentCell(Cell.Droid);

            string RepresentCell(Cell c)
            {
                switch (c)
                {
                    case (Cell.Obstacle):
                        return "▓";
                    case (Cell.Open):
                        return " ";
                    case (Cell.OxygenSystem):
                        return "X";
                    case (Cell.Droid):
                        return "D";
                }

                throw new Exception(string.Format("Unknown cell value: {0}", c.ToString()));
            }

            // Apply the shortest path
            foreach (var cell in ShortestPath)
            {
                maze[cell.Item2 + Math.Abs(minY)][cell.Item1 + Math.Abs(minX)] = ".";
            }

            foreach (var row in maze)
            {
                string printRow = "";
                foreach (var cell in row)
                {
                    // Certain cells will be unknown (and thus null) if the droid was unable to reach the cell
                    // For example, if a cell is surrounded by obstacles, the droid is unable to know what is there.
                    if (cell == null)
                    {
                        printRow += RepresentCell(Cell.Obstacle);
                    }

                    printRow += cell;
                }
                Console.WriteLine(printRow);
            }
        }

        public void DiscoverMaze(Droid d)
        {
            DFS(d, Direction.North);
            DFS(d, Direction.South);
            DFS(d, Direction.West);
            DFS(d, Direction.East);
        }

        public List<(int, int)> FindShortestPathUsingBFS((int, int) start, (int, int) end)
        {
            Dictionary<(int, int), (int, int)> previousCells = SolveBFS((0, 0));


            (int, int) currentCell = (end.Item1, end.Item2);
            while (currentCell != (start.Item1, start.Item2))
            {
                ShortestPath.Add(currentCell);
                currentCell = previousCells[currentCell];
            }

            return ShortestPath;
        }

        public Dictionary<(int, int), (int, int)> SolveBFS((int, int) startCell)
        {
            // Queue of nodes we still need to explore
            Queue<(int, int)> cellsToVisit = new Queue<(int, int)>();
            cellsToVisit.Enqueue(startCell);

            // Keep track of which nodes we've already been to
            HashSet<(int, int)> visited = new HashSet<(int, int)>();

            // A dict mapping from one cell's coords to the coords of it's parent cell in a BFS traversal
            Dictionary<(int, int), (int, int)> prev = new Dictionary<(int, int), (int, int)>();

            while (cellsToVisit.Count > 0)
            {
                (int, int) currentCell = cellsToVisit.Dequeue();
                visited.Add(currentCell);

                // Add any non-obstacle cells to the path we need to travel
                foreach (Direction d in Enum.GetValues(typeof(Direction)))
                {
                    // If we haven't already traversed to this cell, and if it's not an obstacle
                    if (!visited.Contains(ApplyDirection(currentCell.Item1, currentCell.Item2, d))
                        && GetNeighborCell(currentCell.Item1, currentCell.Item2, d) != Cell.Obstacle)
                    {
                        cellsToVisit.Enqueue(ApplyDirection(currentCell.Item1, currentCell.Item2, d));
                        prev.TryAdd(ApplyDirection(currentCell.Item1, currentCell.Item2, d), currentCell);
                    }
                }
            }

            return prev;
        }

        public (int, int) ApplyDirection(int x, int y, Direction d)
        {
            if (d == Direction.North)
            {
                return (x, y + 1);
            }
            else if (d == Direction.South)
            {
                return (x, y - 1);
            }
            else if (d == Direction.West)
            {
                return (x - 1, y);
            }
            else
            {
                return (x + 1, y);
            }
        }

        public Cell GetNeighborCell(int x, int y, Direction d)
        {
            if (!_knownCells.ContainsKey(ApplyDirection(x, y, d)))
            {
                return Cell.Obstacle;
            }
            return _knownCells[ApplyDirection(x, y, d)];
        }
    }
}
