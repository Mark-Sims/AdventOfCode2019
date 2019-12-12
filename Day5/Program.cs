using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = Environment.CurrentDirectory + "//input.txt";

            // Input file should be a single line
            string line = System.IO.File.ReadAllLines(inputFile)[0];

            IntcodeInterpreter i = new IntcodeInterpreter(line);
            i.ExecuteProgram();

        }
    }
}
