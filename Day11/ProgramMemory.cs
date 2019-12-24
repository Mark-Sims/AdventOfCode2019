using System;
using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    class ProgramMemory
    {
        private List<long> _physicalMemory;
        Dictionary<long, long> _virtualMemory;

        public ProgramMemory(long[] memory)
        {
            _physicalMemory = memory.ToList();

            // Virtual memory is represented by a dict mapping address -> value at that address
            _virtualMemory = new Dictionary<long, long>();
        }

        public long this[long i]
        {
            get
            {
                if (i < 0)
                {
                    throw new Exception("Negative memory addresses are not supported.");
                }

                if (i < _physicalMemory.Count)
                {
                    // Hopefully the initial size of the program doesn't overflow int.MaxValue
                    return _physicalMemory[(int)i];
                }
                else if (_virtualMemory.ContainsKey(i))
                {
                    return _virtualMemory[i];
                }
                else
                {
                    return 0L;
                }
            }
            set
            {
                if (i < 0)
                {
                    throw new Exception("Negative memory addresses are not supported.");
                }

                if (i < _physicalMemory.Count)
                {
                    // Hopefully the initial size of the program doesn't overflow int.MaxValue
                    _physicalMemory[(int)i] = value;
                }
                else
                {
                    _virtualMemory[i] = value;
                }
            }
        }
    }
}
