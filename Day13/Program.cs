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

            // This section creates the game level from the puzzle input
            while (!interpreter.IsHalted)
            {
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

            game.InitializeMap();

            // Now we will run the program again - with a fresh copy of the program memory
            // and this time provide it a different input value (2) to actually execute the game
            var modifiedProgram = line.ToCharArray();
            modifiedProgram[0] = '2';
            IntcodeInterpreter gameProgram = new IntcodeInterpreter(new string(modifiedProgram), isInterractiveMode: false);

            List<long> inputs = new List<long>();
            int drawingCounter = 1;
            while (!gameProgram.IsHalted)
            {
                gameProgram.PrepareForExecution(inputs);
                var output = gameProgram.ExecuteProgram();
                if (output == null)
                {
                    break;
                }

                int x = (int)output;
                int y = (int)gameProgram.ExecuteProgram();
                int tileId = (int)gameProgram.ExecuteProgram();


                // It takes 4 iterations of the loop to re-draw the game after single game tick.
                // 1.) Draw the old ball location blank
                // 2.) Draw the ball at the new location
                // 3.) Draw the old paddle location blank
                // 4.) Draw the paddle at the new location

                // Note that on the tick when the ball is destroying a block, the ball remains stationary
                // for that tick, and instead of using 2 game ticks to redraw the ball's old, and new locations,
                // we use one game tick to draw the old block location as blank, and the second game tick to
                // output the latest score. Therefore the game should always be rendered once every four game ticks.
                if (drawingCounter % 4 == 0)
                {
                    game.PrintGame();
                }

                if (x == -1 && y == 0)
                {
                    Console.SetCursorPosition(0, game.MaxY + 2);
                    Console.WriteLine("Score: {0}", tileId);
                }
                else
                {
                    game.UpdateTile(x, y, tileId);
                }

                // Program the game to play itself.
                inputs.Clear();
                if (game.LastPaddleX < game.LastBallX)
                {
                    inputs.Add(1);
                }
                else if (game.LastPaddleX > game.LastBallX)
                {
                    inputs.Add(-1);
                }
                else
                {
                    inputs.Add(0);
                }

                drawingCounter += 1;
            }

        }
    }
}
