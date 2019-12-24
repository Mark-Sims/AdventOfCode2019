using System;
using System.Collections.Generic;

namespace Day11
{
    class Robot
    {
        public Direction Facing { get; set; }
        public int XLocation { get; set; }
        public int YLocation { get; set; }

        public int MaxX { get; set; }
        public int MinX { get; set; }
        public int MaxY { get; set; }
        public int MinY { get; set; }

        // 0 = Black
        // 1 = White
        Dictionary<(int, int), int> PanelColors;

        public Robot()
        {
            Facing = Direction.North;
            XLocation = 0;
            YLocation = 0;

            PanelColors = new Dictionary<(int, int), int>();
            PanelColors.Add((XLocation, YLocation), 1);


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

            CalculateMaxes();
        }

        private void CalculateMaxes()
        {
            MaxX = Math.Max(MaxX, XLocation);
            MinX = Math.Min(MinX, XLocation);
            MaxY = Math.Max(MaxY, YLocation);
            MinY = Math.Min(MinY, YLocation);
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

        public void PrettyPrintPanels()
        {
            int width = MaxX - MinX + 1;
            int height = MaxY - MinY + 1;

            var grid = new int[height][];
            for (int j = 0; j < height; j++)
            {
                grid[j] = new int[width];
            }

            foreach (var panel in PanelColors)
            {
                // I just happen to know that the robot only travels East and South from the origin.
                // So that's the positive X direction, and negative Y direction, therefore we need
                // to negate the Y coords when indexing into the array.
                grid[panel.Key.Item2 * -1][panel.Key.Item1] = panel.Value;
            }

            foreach (var row in grid)
            {
                string rowString = "";
                foreach (var panelColor in row)
                {
                    if (panelColor == 0)
                    {
                        rowString += " ";
                    }
                    else
                    {
                        rowString += "#";
                    }
                }
                Console.WriteLine(rowString);
            }
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
