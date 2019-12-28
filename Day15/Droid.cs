using System;
using System.Collections.Generic;
using System.Text;

namespace Day15
{
    class Droid
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public IntcodeInterpreter Interpreter;

        public Droid(int x, int y, IntcodeInterpreter i)
        {
            PositionX = x;
            PositionY = y;
            Interpreter = new IntcodeInterpreter(i);
        }

        public void Step(int direction)
        {
            switch (direction)
            {
                case (int)Direction.North:
                    PositionY += 1;
                    return;
                case (int)Direction.South:
                    PositionY -= 1;
                    return;
                case (int)Direction.West:
                    PositionX -= 1;
                    return;
                case (int)Direction.East:
                    PositionX += 1;
                    return;
            }
        }

        public int TryStep(Direction d)
        {
            Interpreter.PrepareForExecution(new List<long> { (long)d });
            return (int)Interpreter.ExecuteProgram();
        }
    }

    enum Direction
    {
        North = 1,
        South = 2,
        West = 3,
        East = 4
    }

    enum Cell
    {
        Unknown = 1,
        Obstacle = 2,
        Open = 3,
        OxygenSystem = 4,
        Droid = 5
    }
}
