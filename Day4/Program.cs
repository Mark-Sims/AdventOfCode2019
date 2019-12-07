using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var rangeStart = 124075;
            var rangeEnd = 580769;

            var counter = 0;
            for (var i = rangeStart; rangeStart < rangeEnd; rangeStart++)
            {

                string candidateString = rangeStart.ToString();
                List<int> candidate = new List<int>();
                foreach (char c in candidateString)
                {
                    candidate.Add(int.Parse(c.ToString()));
                }

                // Requirement #1 - Two adjacent digits must be the same
                bool requirementSatisfied = false;

                for (var digit = 0; digit < candidate.Count() - 1; digit++)
                {
                    if (candidate[digit] == candidate[digit + 1])
                    {
                        requirementSatisfied = true;
                        break;
                    }
                }

                if (!requirementSatisfied)
                {
                    Console.WriteLine("Requirement #1 violated for {0}", candidateString);
                    continue;
                }

                // Requirement #2 - Digits only increase
                bool requirementViolated = false;
                for (var digit = 0; digit < candidate.Count() - 1; digit++)
                {
                    if (candidate[digit] > candidate[digit + 1])
                    {
                        requirementViolated = true;
                        break;
                    }
                }

                if (requirementViolated)
                {
                    Console.WriteLine("Requirement #2 violated for {0}", candidateString);
                    continue;
                }

                // Both requirements satisfied, this is a valid password candidate
                counter++;
            }

            Console.WriteLine(counter);
        }
    }
}
