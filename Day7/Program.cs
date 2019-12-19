using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = Environment.CurrentDirectory + "//input.txt";

            // Input file should be a single line
            string line = System.IO.File.ReadAllLines(inputFile)[0];

            var maxOutput = int.MinValue;

            for (int a = 0; a < 5; a++)
            {
                for (int b = 0; b < 5; b++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        for (int d = 0; d < 5; d++)
                        {
                            for (int e = 0; e < 5; e++)
                            {
                                HashSet<int> phaseSettings = new HashSet<int> { a, b, c, d, e };
                                // This means at least one of the phase settings was a repeat.
                                // Each phase setting can be used exactly once, so no repeats.
                                if (phaseSettings.Count != 5)
                                {
                                    continue;
                                }

                                // Begin initial iteration
                                IntcodeInterpreter ampA = new IntcodeInterpreter(line, new List<int> { a, 0 });
                                var outputAmpA = ampA.ExecuteProgram();

                                IntcodeInterpreter ampB = new IntcodeInterpreter(line, new List<int> { b, outputAmpA[0] });
                                var outputAmpB = ampB.ExecuteProgram();

                                IntcodeInterpreter ampC = new IntcodeInterpreter(line, new List<int> { c, outputAmpB[0] });
                                var outputAmpC = ampC.ExecuteProgram();

                                IntcodeInterpreter ampD = new IntcodeInterpreter(line, new List<int> { d, outputAmpC[0] });
                                var outputAmpD = ampD.ExecuteProgram();

                                IntcodeInterpreter ampE = new IntcodeInterpreter(line, new List<int> { e, outputAmpD[0] });
                                var outputAmpE = ampE.ExecuteProgram();

                                if (outputAmpE[0] > maxOutput)
                                {
                                    maxOutput = outputAmpE[0];
                                    Console.WriteLine("Max output value is " + maxOutput);
                                    Console.WriteLine("From settings: {0}, {1}, {2}, {3}, {4}", a, b, c, d, e);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
