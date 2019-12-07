using System;
using System.Collections.Generic;
using System.Drawing;
using Xunit;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = Environment.CurrentDirectory + "//input.txt";

            // Input file should be a single line
            string[] wires = System.IO.File.ReadAllLines(inputFile);
            List<MovementCommand> wire1 = ParseInputCommandLine(wires[0]);
            List<MovementCommand> wire2 = ParseInputCommandLine(wires[1]);

            GridDimensions dimensions = CalculateTotalRequiredGridSize(new List<List<MovementCommand>> { wire1, wire2 });

            Console.WriteLine(dimensions);

            // Offset the starting position into the grid since we're not starting at an edge.
            int startingX = Math.Abs(dimensions.MinX);
            int startingY = Math.Abs(dimensions.MinY);

            GridManager gridManager = new GridManager(dimensions.DimensionX, dimensions.DimensionY, startingX, startingY);

            TraceWire(gridManager, new Point(startingX, startingY), wire1, 1);
            TraceWire(gridManager, new Point(startingX, startingY), wire2, 2);
        }

        public static void TraceWire(GridManager gridManager, Point startingPoint, List<MovementCommand> wire, int wireNumber)
        {
            foreach (MovementCommand command in wire)
            {
                Console.WriteLine("Tracing {0}", command);
                switch (command.Direction)
                {
                    case Direction.Up:
                        startingPoint = gridManager.WalkUp(startingPoint, command.Distance, wireNumber);
                        break;
                    case Direction.Right:
                        startingPoint = gridManager.WalkRight(startingPoint, command.Distance, wireNumber);
                        break;
                    case Direction.Down:
                        startingPoint = gridManager.WalkDown(startingPoint, command.Distance, wireNumber);
                        break;
                    case Direction.Left:
                        startingPoint = gridManager.WalkLeft(startingPoint, command.Distance, wireNumber);
                        break;
                }
            }
        }

        public static GridDimensions CalculateTotalRequiredGridSize(List<List<MovementCommand>> wires)
        {
            List<MovementCommand> wire1 = wires[0];
            List<MovementCommand> wire2 = wires[1];

            GridDimensions requiredGridWire1 = CalculateRequiredGridSizeForWire(wire1);
            GridDimensions requiredGridWire2 = CalculateRequiredGridSizeForWire(wire2);

            //Console.WriteLine(requiredGridWire1);
            //Console.WriteLine(requiredGridWire2);

            GridDimensions totalGrid = new GridDimensions()
            {
                MinX = Math.Min(requiredGridWire1.MinX, requiredGridWire2.MinX),
                MaxX = Math.Max(requiredGridWire1.MaxX, requiredGridWire2.MaxX),
                MinY = Math.Min(requiredGridWire1.MinY, requiredGridWire2.MinY),
                MaxY = Math.Max(requiredGridWire1.MaxY, requiredGridWire2.MaxY),
            };

            //Console.WriteLine(minX);
            //Console.WriteLine(maxX);
            //Console.WriteLine(minY);
            //Console.WriteLine(maxY);

            totalGrid.DimensionX = Math.Abs(totalGrid.MinX) + totalGrid.MaxX + 1;
            totalGrid.DimensionY = Math.Abs(totalGrid.MinY) + totalGrid.MaxY + 1;

            return totalGrid;
        }

        public static List<MovementCommand> ParseInputCommandLine(string movementCommands)
        {
            List<MovementCommand> parsedCommands = new List<MovementCommand>();
            foreach (string command in movementCommands.Split(","))
            {
                parsedCommands.Add(new MovementCommand(command));
            }
            return parsedCommands;
        }

        public static GridDimensions CalculateRequiredGridSizeForWire(List<MovementCommand> wireMovements)
        {
            //
            //     ^
            //     |
            //     |   // Positive directions
            //     |
            //     +--------->
            //

            GridDimensions grid = new GridDimensions();

            int currentX, currentY;
            currentX = currentY = 0;

            foreach (MovementCommand command in wireMovements)
            {
                switch (command.Direction)
                {
                    case Direction.Up:
                        currentY += command.Distance;
                        grid.MaxY = Math.Max(grid.MaxY, currentY);
                        break;
                    case Direction.Right:
                        currentX += command.Distance;
                        grid.MaxX = Math.Max(grid.MaxX, currentX);
                        break;
                    case Direction.Down:
                        currentY -= command.Distance;
                        grid.MinY = Math.Min(grid.MinY, currentY);
                        break;
                    case Direction.Left:
                        currentX -= command.Distance;
                        grid.MinX = Math.Min(grid.MinX, currentX);
                        break;
                }
            }
            return grid;
        }
    }

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }



}
