using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = Environment.CurrentDirectory + "//input.txt";
            string[] lines = System.IO.File.ReadAllLines(inputFile);

            // Part A

            Dictionary<string, int> testPartA = new Dictionary<string, int>
            {
                {"12" , 2},
                {"14" , 2},
                {"1969" , 654},
                {"100756" , 33583},
            };

            foreach (var testCase in testPartA)
            {
                Assert.Equal(testCase.Value, PartA(new string[] { testCase.Key }));
            }

            Console.WriteLine(PartA(lines));

            // Part B

            Dictionary<string, int> testPartB = new Dictionary<string, int>
            {
                {"14" , 2},
                {"1969" , 966},
                {"100756" , 50346},
            };

            foreach (var testCase in testPartB)
            {
                Assert.Equal(testCase.Value, PartB(new string[] { testCase.Key }));
            }

            Console.WriteLine(PartB(lines));
        }

        private static int PartA(string[] lines)
        {

            int totalFuel = 0;

            foreach (var line in lines)
            {
                totalFuel += CalculateFuel(Int32.Parse(line));
            }

            int CalculateFuel(int mass)
            {
                return (mass / 3) - 2;
            }

            return totalFuel;
        }

        private static int PartB(string[] lines)
        {
            int totalFuel = 0;

            foreach (var line in lines)
            {
                totalFuel += CalculateFuelRecursive(Int32.Parse(line));
            }

            int CalculateFuelRecursive(int mass)
            {
                int fuelRequired = (mass / 3) - 2;

                if (fuelRequired <= 0)
                {
                    return 0;
                }

                return fuelRequired + CalculateFuelRecursive(fuelRequired);
            }

            return totalFuel;

        }

    }
}
