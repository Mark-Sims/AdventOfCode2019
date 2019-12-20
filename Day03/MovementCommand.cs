using System;
using System.Collections.Generic;
using System.Text;

namespace Day03
{
    class MovementCommand
    {
        public Direction Direction { get; set; }
        public int Distance { get; set; }

        public MovementCommand(Direction direction, int distance)
        {
            Direction = direction;
            Distance = distance;
        }

        public MovementCommand(string command)
        {
            string dir = command.Substring(0, 1);
            Distance = int.Parse(command.Substring(1));

            switch (dir)
            {
                case "U":
                    Direction = Direction.Up;
                    break;
                case "R":
                    Direction = Direction.Right;
                    break;
                case "D":
                    Direction = Direction.Down;
                    break;
                case "L":
                    Direction = Direction.Left;
                    break;
            }
        }

        public override string ToString()
        {
            string ret = "";
            switch (Direction)
            {
                case Direction.Up:
                    ret += "U";
                    break;
                case Direction.Right:
                    ret += "R";
                    break;
                case Direction.Down:
                    ret += "D";
                    break;
                case Direction.Left:
                    ret += "L";
                    break;
            }

            ret += Distance.ToString();

            return ret;
        }
    }
}
