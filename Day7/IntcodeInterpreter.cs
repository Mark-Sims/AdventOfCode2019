using System;
using System.Collections.Generic;
using System.Text;

namespace Day5
{
    class IntcodeInterpreter
    {
        private int[] _program;
        private IEnumerator<int> _inputs;
        private bool _interractiveMode = true;
        private List<int> _outputs;

        public IntcodeInterpreter(string programString, IEnumerable<int> inputs = null)
        {
            _program = splitInputLine(programString);
            if (inputs != null)
            {
                _inputs = inputs.GetEnumerator();
                _interractiveMode = false;
            }
            _outputs = new List<int>();
        }


        public void PrepareForExecution(IEnumerable<int> inputs = null)
        {
            if (inputs != null)
            {
                _inputs = inputs.GetEnumerator();
                _interractiveMode = false;
            }
        }

        public List<int> ExecuteProgram()
        {
            for (int addressPointer = 0; addressPointer < _program.Length;)
            {
                // This string values represents the opcode, as well as the parameter
                // mode (immediate/positional) for the instruction's parameters.
                // We left pad with 0's because if the value is contains fewer
                // parameter modes than the number of values in the instruction,
                // then the parameter modes are assumed to be positional. (AKA, 0).
                string value_0 = _program[addressPointer].ToString().PadLeft(5, '0');

                var instr = new Instruction
                {
                    // The final 2 digits represent the opcode
                    OpCode = int.Parse(value_0.Substring(3, 2)),

                    // The next 3 digits (read right to left) represent the parameter
                    // modes of the values in this instruction.
                    // Note that the instruction may not necessarily have values for this
                    // many parameter modes. But if that is the case, these parameter modes
                    // will simply be ignored/unused.
                    Param1Mode = int.Parse(value_0.Substring(2, 1)) == 0 ? ParameterMode.Position : ParameterMode.Immediate,
                    Param2Mode = int.Parse(value_0.Substring(1, 1)) == 0 ? ParameterMode.Position : ParameterMode.Immediate,

                };

                if (instr.OpCode == 1 || instr.OpCode == 2)
                {

                    instr.InstructionValue1 = _program[addressPointer + 1];
                    instr.InstructionValue2 = _program[addressPointer + 2];
                    instr.InstructionValue3 = _program[addressPointer + 3];

                    addressPointer = ExecuteInstruction(instr, addressPointer);
                }
                else if (instr.OpCode == 3 || instr.OpCode == 4)
                {
                    instr.InstructionValue1 = _program[addressPointer + 1];

                    addressPointer = ExecuteInstruction(instr, addressPointer);
                }
                else if (instr.OpCode == 5 || instr.OpCode == 6)
                {
                    instr.InstructionValue1 = _program[addressPointer + 1];
                    instr.InstructionValue2 = _program[addressPointer + 2];

                    addressPointer = ExecuteInstruction(instr, addressPointer);
                }
                else if (instr.OpCode == 7 || instr.OpCode == 8)
                {
                    instr.InstructionValue1 = _program[addressPointer + 1];
                    instr.InstructionValue2 = _program[addressPointer + 2];
                    instr.InstructionValue3 = _program[addressPointer + 3];

                    addressPointer = ExecuteInstruction(instr, addressPointer);
                }
                else if (instr.OpCode == 99)
                {
                    //Console.WriteLine("Breaking...");
                    return _outputs;
                }
                // This instruction contains a first value that specifies non-zero parameter modes
                else
                {
                    throw new Exception(string.Format("Unsupported instruction with opcode: {0}", instr.OpCode.ToString()));
                }
            }

            return _outputs;
        }

        // Return: The new value for the address pointer. This value is dependent on not only which
        // opcode is executed, but also, sometimes on the outcome of that execution.
        private int ExecuteInstruction(Instruction instruction, int addressPointer)
        {
            if (instruction.OpCode == 1)
            {
                int param1;
                if (instruction.Param1Mode == ParameterMode.Position)
                {
                    param1 = _program[(int)instruction.InstructionValue1];
                }
                else
                {
                    param1 = (int)instruction.InstructionValue1;
                }

                int param2;
                if (instruction.Param2Mode == ParameterMode.Position)
                {
                    param2 = _program[(int)instruction.InstructionValue2];
                }
                else
                {
                    param2 = (int)instruction.InstructionValue2;
                }

                _program[(int)instruction.InstructionValue3] = param1 + param2;
                return addressPointer + 4;
            }
            else if (instruction.OpCode == 2)
            {
                int param1;
                if (instruction.Param1Mode == ParameterMode.Position)
                {
                    param1 = _program[(int)instruction.InstructionValue1];
                }
                else
                {
                    param1 = (int)instruction.InstructionValue1;
                }

                int param2;
                if (instruction.Param2Mode == ParameterMode.Position)
                {
                    param2 = _program[(int)instruction.InstructionValue2];
                }
                else
                {
                    param2 = (int)instruction.InstructionValue2;
                }

                _program[(int)instruction.InstructionValue3] = param1 * param2;
                return addressPointer + 4;
            }
            else if (instruction.OpCode == 3)
            {
                if (_interractiveMode)
                {
                    Console.Write("Input: ");
                    _program[(int)instruction.InstructionValue1] = int.Parse(Console.ReadLine());
                }
                else
                {
                    _inputs.MoveNext();
                    //Console.WriteLine("Non-interractive mode input: " + _inputs.Current.ToString());
                    _program[(int)instruction.InstructionValue1] = _inputs.Current;
                }
                return addressPointer + 2;
            }
            else if (instruction.OpCode == 4)
            {
                int param1;
                if (instruction.Param1Mode == ParameterMode.Position)
                {
                    param1 = _program[(int)instruction.InstructionValue1];
                }
                else
                {
                    param1 = (int)instruction.InstructionValue1;
                }

                if (_interractiveMode)
                {
                    Console.WriteLine(param1);
                }
                else
                {
                    _outputs.Add(param1);
                }
                return addressPointer + 2;
            }
            else if (instruction.OpCode == 5 || instruction.OpCode == 6)
            {
                int param1;
                if (instruction.Param1Mode == ParameterMode.Position)
                {
                    param1 = _program[(int)instruction.InstructionValue1];
                }
                else
                {
                    param1 = (int)instruction.InstructionValue1;
                }

                int param2;
                if (instruction.Param2Mode == ParameterMode.Position)
                {
                    param2 = _program[(int)instruction.InstructionValue2];
                }
                else
                {
                    param2 = (int)instruction.InstructionValue2;
                }

                if (instruction.OpCode == 5)
                {
                    if (param1 != 0)
                    {
                        return param2;
                    }
                    else
                    {
                        return addressPointer + 3;
                    }
                }
                else
                {
                    if (param1 == 0)
                    {
                        return param2;
                    }
                    else
                    {
                        return addressPointer + 3;
                    }
                }


            }
            else if (instruction.OpCode == 7 || instruction.OpCode == 8)
            {
                int param1;
                if (instruction.Param1Mode == ParameterMode.Position)
                {
                    param1 = _program[(int)instruction.InstructionValue1];
                }
                else
                {
                    param1 = (int)instruction.InstructionValue1;
                }

                int param2;
                if (instruction.Param2Mode == ParameterMode.Position)
                {
                    param2 = _program[(int)instruction.InstructionValue2];
                }
                else
                {
                    param2 = (int)instruction.InstructionValue2;
                }

                if (instruction.OpCode == 7)
                {
                    _program[(int)instruction.InstructionValue3] = param1 < param2 ? 1 : 0;
                }
                else
                {
                    _program[(int)instruction.InstructionValue3] = param1 == param2 ? 1 : 0;
                }

                return addressPointer + 4;
            }
            else
            {
                throw new Exception(string.Format("Cannot execute unknown opcode: {0}", instruction.OpCode.ToString()));
            }
        }

        private static int[] splitInputLine(string intcodesLine)
        {
            return Array.ConvertAll(intcodesLine.Split(","), s => int.Parse(s));
        }
    }
}
