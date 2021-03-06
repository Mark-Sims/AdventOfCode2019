﻿using System;

namespace Day09
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = Environment.CurrentDirectory + "//input.txt";

            // Input file should be a single line
            string line = System.IO.File.ReadAllLines(inputFile)[0];

            IntcodeInterpreter interpreter = new IntcodeInterpreter(line);
            while (!interpreter.IsHalted)
            {
                interpreter.ExecuteProgram();
            }
        }
    }
}
