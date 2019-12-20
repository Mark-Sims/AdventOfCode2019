using System;
using System.Collections.Generic;
using System.Linq;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            var rangeStart = 124075;
            var rangeEnd = 580769;

            var counterPart1 = 0;
            var counterPart2 = 0;
            List<List<int>> candidatePasswords = new List<List<int>>();
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
                    //Console.WriteLine("Requirement #1 violated for {0}", candidateString);
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
                    //Console.WriteLine("Requirement #2 violated for {0}", candidateString);
                    continue;
                }

                // Both requirements satisfied, this is a valid password candidate
                counterPart1++;
                candidatePasswords.Add(candidate);
            }

            // Part 2 - For requirement #1, the required series of repeated characters cannot be a part
            // of a larger series of repeated characters. In other words, there must be some character
            // that is repeated exactly 2 times. (Other requirements still apply as well.)
            foreach (var candidate in candidatePasswords)
            {
                Dictionary<int, int> numOfEachDigit = new Dictionary<int, int>();
                foreach (int digit in candidate)
                {
                    if (numOfEachDigit.ContainsKey(digit))
                    {
                        numOfEachDigit[digit]++;
                    }
                    else
                    {
                        numOfEachDigit[digit] = 1;
                    }
                }

                foreach (var numOfThisDigit in numOfEachDigit.Values)
                {
                    if (numOfThisDigit == 2)
                    {
                        //string a = "";
                        //candidate.ForEach(x => a += x.ToString());
                        //Console.WriteLine(a);
                        counterPart2++;

                        // Break so that we don't double count passwords that have multiple double digits
                        // eg. 345566
                        break;
                    }
                }
            }

            Console.WriteLine(counterPart1);
            Console.WriteLine(counterPart2);
        }
    }
}
