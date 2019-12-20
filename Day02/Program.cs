using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = Environment.CurrentDirectory + "//input.txt";

            // Input file should be a single line
            string line = System.IO.File.ReadAllLines(inputFile)[0];

            // Part A

            Dictionary<string, int[]> testPartA = new Dictionary<string, int[]>
            {
                {"1,0,0,0,99" , new int[] {2,0,0,0,99 } },
                {"2,3,0,3,99" , new int[] {2,3,0,6,99 } },
                {"2,4,4,5,99,0" , new int[] {2,4,4,5,99,9801 } },
                {"1,1,1,4,99,5,6,0,99" , new int[] {30,1,1,4,2,5,6,0,99} }
            };

            foreach (var testCase in testPartA)
            {
                Assert.Equal(testCase.Value, PartA(testCase.Key));
                // Console.WriteLine(string.Join(",", PartA(testCase.Key)));
            }

            Console.WriteLine(string.Join(",", PartA(line)));

            // Part B

            // Brute Force
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (PartB(line, i, j) == 19690720)
                    {
                        Console.WriteLine("noun: " + i + " verb: " + j);
                    }
                }

            }

        }

        private static int[] splitInputLine(string intcodesLine)
        {
            return Array.ConvertAll(intcodesLine.Split(","), s => int.Parse(s));
        }

        private static int[] PartA(string intcodesString)
        {
            int[] intcodes = splitInputLine(intcodesString);

            for (int address = 0; address < intcodes.Length; address += 4)
            {
                int opcode = intcodes[address];

                if (opcode == 99)
                {
                    // Console.WriteLine("Breaking");
                    break;
                }
                else if (opcode == 1)
                {
                    int param1 = intcodes[address + 1];
                    int param2 = intcodes[address + 2];
                    int param3 = intcodes[address + 3];
                    intcodes[param3] = intcodes[param1] + intcodes[param2];
                    // Console.WriteLine("Index: " + address + " Opcode: " + opcode + " Purview: [" + intcodes[address] + ", " + intcodes[address + 1] + ", " + intcodes[address + 2] + ", " + intcodes[address + 3] + "]");
                    // Console.WriteLine("Addition, updating address " + intcodes[address + 3] + " to " + intcodes[param1] + intcodes[param2]);
                }
                else if (opcode == 2)
                {
                    int param1 = intcodes[address + 1];
                    int param2 = intcodes[address + 2];
                    int param3 = intcodes[address + 3];
                    intcodes[param3] = intcodes[param1] * intcodes[param2];
                    // Console.WriteLine("Index: " + address + " Opcode: " + opcode + " Purview: [" + intcodes[address] + ", " + intcodes[address + 1] + ", " + intcodes[address + 2] + ", " + intcodes[address + 3] + "]");
                    // Console.WriteLine("Multiplying, updating address " + intcodes[address + 3] + " to " + intcodes[param1] + intcodes[param2]);
                }
            }

            return intcodes;
        }

        private static int PartB(string intcodesString, int noun, int verb)
        {
            int[] intcodes = splitInputLine(intcodesString);
            intcodes[1] = noun;
            intcodes[2] = verb;

            for (int address = 0; address < intcodes.Length; address += 4)
            {
                int opcode = intcodes[address];

                if (opcode == 99)
                {
                    // Console.WriteLine("Breaking");
                    break;
                }
                else if (opcode == 1)
                {
                    int param1 = intcodes[address + 1];
                    int param2 = intcodes[address + 2];
                    int param3 = intcodes[address + 3];
                    intcodes[param3] = intcodes[param1] + intcodes[param2];
                    // Console.WriteLine("Index: " + address + " Opcode: " + opcode + " Purview: [" + intcodes[address] + ", " + intcodes[address + 1] + ", " + intcodes[address + 2] + ", " + intcodes[address + 3] + "]");
                    // Console.WriteLine("Addition, updating address " + intcodes[address + 3] + " to " + intcodes[param1] + intcodes[param2]);
                }
                else if (opcode == 2)
                {
                    int param1 = intcodes[address + 1];
                    int param2 = intcodes[address + 2];
                    int param3 = intcodes[address + 3];
                    intcodes[param3] = intcodes[param1] * intcodes[param2];
                    // Console.WriteLine("Index: " + address + " Opcode: " + opcode + " Purview: [" + intcodes[address] + ", " + intcodes[address + 1] + ", " + intcodes[address + 2] + ", " + intcodes[address + 3] + "]");
                    // Console.WriteLine("Multiplying, updating address " + intcodes[address + 3] + " to " + intcodes[param1] + intcodes[param2]);
                }
            }

            return intcodes[0];
        }
    }
}
