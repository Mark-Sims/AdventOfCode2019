using System;
using System.Collections.Generic;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = Environment.CurrentDirectory + "//input.txt";

            // Input file should be a single line
            string line = System.IO.File.ReadAllLines(inputFile)[0];

            IntcodeInterpreter interpreter = new IntcodeInterpreter(line);

            List<long> programOutput;

            Robot robot = new Robot();
            while (!interpreter.IsHalted)
            {
                robot.MarkPanelAsTraversed();
                interpreter.PrepareForExecution(new List<long> { robot.GetColorOfCurrentPanel() });

                // Calculate what color to paint the panel
                programOutput = interpreter.ExecuteProgram();

                if (interpreter.IsHalted)
                {
                    Console.WriteLine("Robot painted a total of {0} panels.", robot.GetNumberOfPanelsPainted());
                    break;
                }
                robot.PaintPanel((int)programOutput[0]);

                // Calculate what movement to make
                programOutput = interpreter.ExecuteProgram();

                if (programOutput[0] == 0)
                {
                    robot.RotateCounterclockwise();
                }
                else if (programOutput[0] == 1)
                {
                    robot.RotateClockwise();
                }

                robot.Step();
            }

            robot.PrettyPrintPanels();
        }
    }
}
