using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day9
{
    class ProgramMemory
    {
        private List<int> _physicalMemory;
        Dictionary<int, int> _virtualMemory;

        public ProgramMemory(int[] memory)
        {
            _physicalMemory = memory.ToList();
            _virtualMemory = new Dictionary<int, int>();
        }

        public int this[int i]
        {
            get
            {
                if (i < 0)
                {
                    throw new Exception("Negative memory addresses are not supported.");
                }

                if (i < _physicalMemory.Count)
                {
                    return _physicalMemory[i];
                }
                else if (_virtualMemory.ContainsKey(i))
                {
                    return _virtualMemory[i];
                }
                else
                {
                    return 0;
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
                    _physicalMemory[i] = value;
                }
                else
                {
                    _virtualMemory[i] = value;
                }
            }
        }
    }
}
