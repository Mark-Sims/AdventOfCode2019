using System.Collections.Generic;

namespace Day11
{
    class Robot
    {
        public Direction Facing { get; set; }
        public int XLocation { get; set; }
        public int YLocation { get; set; }

        // 0 = Black
        // 1 = White
        Dictionary<(int, int), int> PanelColors;

        public Robot()
        {
            Facing = Direction.North;
            XLocation = 0;
            YLocation = 0;

            PanelColors = new Dictionary<(int, int), int>();
            PanelColors.Add((XLocation, YLocation), 0);
        }

        public void RotateClockwise()
        {
            if (Facing == Direction.West)
            {
                Facing = Direction.North;
            }
            else
            {
                Facing += 1;
            }
        }

        public void RotateCounterclockwise()
        {
            if (Facing == Direction.North)
            {
                Facing = Direction.West;
            }
            else
            {
                Facing -= 1;
            }
        }

        public void Step()
        {
            if (Facing == Direction.North)
            {
                YLocation += 1;
            }
            else if (Facing == Direction.East)
            {
                XLocation += 1;
            }
            else if (Facing == Direction.South)
            {
                YLocation -= 1;
            }
            else if (Facing == Direction.West)
            {
                XLocation -= 1;
            }
        }

        public void PaintPanel(int color)
        {
            PanelColors[(XLocation, YLocation)] = color;
        }

        public void MarkPanelAsTraversed()
        {
            PanelColors[(XLocation, YLocation)] = GetColorOfCurrentPanel();
        }

        public int GetColorOfCurrentPanel()
        {
            if (PanelColors.ContainsKey((XLocation, YLocation)))
            {
                return PanelColors[(XLocation, YLocation)];
            }
            // If the panel hasn't been traversed yet, the color is assumed to be 0
            return 0;
        }

        public int GetNumberOfPanelsPainted()
        {
            return PanelColors.Count;
        }
    }

    enum Direction
    {
        North,
        East,
        South,
        West
    }
}
