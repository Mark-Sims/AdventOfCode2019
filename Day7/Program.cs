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

            int maxOutput = int.MinValue;
            int ampEMostRecentOutput = int.MinValue;

            for (int a = 5; a < 10; a++)
            {
                for (int b = 5; b < 10; b++)
                {
                    for (int c = 5; c < 10; c++)
                    {
                        for (int d = 5; d < 10; d++)
                        {
                            for (int e = 5; e < 10; e++)
                            {
                                HashSet<int> phaseSettings = new HashSet<int> { a, b, c, d, e };
                                // This means at least one of the phase settings was a repeat.
                                // Each phase setting can be used exactly once, so no repeats.
                                if (phaseSettings.Count != 5)
                                {
                                    continue;
                                }

                                List<int> outputAmpA;
                                List<int> outputAmpB;
                                List<int> outputAmpC;
                                List<int> outputAmpD;
                                List<int> outputAmpE;

                                IntcodeInterpreter ampA = new IntcodeInterpreter(line);
                                IntcodeInterpreter ampB = new IntcodeInterpreter(line);
                                IntcodeInterpreter ampC = new IntcodeInterpreter(line);
                                IntcodeInterpreter ampD = new IntcodeInterpreter(line);
                                IntcodeInterpreter ampE = new IntcodeInterpreter(line);

                                // Begin initial iteration
                                ampA.PrepareForExecution(new List<int> { a, 0 });
                                outputAmpA = ampA.ExecuteProgram();

                                ampB.PrepareForExecution(new List<int> { b, outputAmpA[0] });
                                outputAmpB = ampB.ExecuteProgram();

                                ampC.PrepareForExecution(new List<int> { c, outputAmpB[0] });
                                outputAmpC = ampC.ExecuteProgram();

                                ampD.PrepareForExecution(new List<int> { d, outputAmpC[0] });
                                outputAmpD = ampD.ExecuteProgram();

                                ampE.PrepareForExecution(new List<int> { e, outputAmpD[0] });
                                outputAmpE = ampE.ExecuteProgram();

                                while (!ampE.IsHalted)
                                {
                                    //Console.WriteLine("Feedback");
                                    if (outputAmpE.Count == 1)
                                    {
                                        ampA.PrepareForExecution(new List<int> { outputAmpE[0] });
                                        outputAmpA = ampA.ExecuteProgram();
                                    }

                                    if (outputAmpA.Count == 1)
                                    {
                                        ampB.PrepareForExecution(new List<int> { outputAmpA[0] });
                                        outputAmpB = ampB.ExecuteProgram();
                                    }

                                    if (outputAmpB.Count == 1)
                                    {
                                        ampC.PrepareForExecution(new List<int> { outputAmpB[0] });
                                        outputAmpC = ampC.ExecuteProgram();
                                    }

                                    if (outputAmpC.Count == 1)
                                    {
                                        ampD.PrepareForExecution(new List<int> { outputAmpC[0] });
                                        outputAmpD = ampD.ExecuteProgram();
                                    }

                                    if (outputAmpD.Count == 1)
                                    {
                                        ampE.PrepareForExecution(new List<int> { outputAmpD[0] });
                                        outputAmpE = ampE.ExecuteProgram();
                                    }

                                    // On the final iteration, ampE will halt, without an output value.
                                    // Use the previous output value to send to the thrusters.
                                    if (outputAmpE.Count == 1)
                                    {
                                        ampEMostRecentOutput = outputAmpE[0];
                                    }

                                    if (ampEMostRecentOutput > maxOutput)
                                    {
                                        maxOutput = outputAmpE[0];
                                        Console.WriteLine("Max output value is " + maxOutput);
                                        Console.WriteLine("From phase settings: {0}, {1}, {2}, {3}, {4}", a, b, c, d, e);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
