using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = Environment.CurrentDirectory + "//input.txt";

            // Input file should be a single line
            string line = System.IO.File.ReadAllLines(inputFile)[0];

            IntcodeInterpreter interpreter = new IntcodeInterpreter(line, isInterractiveMode: false);

            List<long> programOutput;

            IntcodeArcadeGame game = new IntcodeArcadeGame();

            while (!interpreter.IsHalted)
            {
                // Calculate what color to paint the panel
                var output = interpreter.ExecuteProgram();

                // The final execution of the program will halt and ouput a
                // null value that shouldn't be used as a game tile value.
                if (output == null)
                {
                    break;
                }
                int x = (int)output;
                int y = (int)interpreter.ExecuteProgram();
                int tileId = (int)interpreter.ExecuteProgram();

                game.AddTile(x, y, tileId);
            }

            Console.WriteLine("Found {0} block tiles.", game.Tiles.Where(x => x.Item3 == 2).ToList().Count);
        }
    }
}
