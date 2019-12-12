using System;
using System.Collections.Generic;
using System.Text;

namespace Day5
{
    class IntcodeInterpreter
    {
        private int[] program;

        public IntcodeInterpreter(string programString)
        {
            program = splitInputLine(programString);
        }

        public void ExecuteProgram()
        {
            for (int addressPointer = 0; addressPointer < program.Length;)
            {
                // This string values represents the opcode, as well as the parameter
                // mode (immediate/positional) for the instruction's parameters.
                // We left pad with 0's because if the value is contains fewer
                // parameter modes than the number of values in the instruction,
                // then the parameter modes are assumed to be positional. (AKA, 0).
                string value_0 = program[addressPointer].ToString().PadLeft(5, '0');

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

                    instr.InstructionValue1 = program[addressPointer + 1];
                    instr.InstructionValue2 = program[addressPointer + 2];
                    instr.InstructionValue3 = program[addressPointer + 3];

                    ExecuteInstruction(instr);
                    addressPointer += 4;
                }
                else if (instr.OpCode == 3)
                {
                    instr.InstructionValue1 = program[addressPointer + 1];

                    ExecuteInstruction(instr);
                    addressPointer += 2;
                }
                else if (instr.OpCode == 4)
                {
                    instr.InstructionValue1 = program[addressPointer + 1];

                    ExecuteInstruction(instr);
                    addressPointer += 2;
                }
                else if (instr.OpCode == 99)
                {
                    return;
                }
                // This instruction contains a first value that specifies non-zero parameter modes
                else
                {
                    throw new Exception(string.Format("Unsupported instruction with opcode: {0}", instr.OpCode.ToString()));
                }
            }

        }

        private void ExecuteInstruction(Instruction instruction)
        {
            if (instruction.OpCode == 1)
            {
                int param1;
                if (instruction.Param1Mode == ParameterMode.Position)
                {
                    param1 = program[(int)instruction.InstructionValue1];
                }
                else
                {
                    param1 = (int)instruction.InstructionValue1;
                }

                int param2;
                if (instruction.Param2Mode == ParameterMode.Position)
                {
                    param2 = program[(int)instruction.InstructionValue2];
                }
                else
                {
                    param2 = (int)instruction.InstructionValue2;
                }

                program[(int)instruction.InstructionValue3] = param1 + param2;
            }
            else if (instruction.OpCode == 2)
            {
                int param1;
                if (instruction.Param1Mode == ParameterMode.Position)
                {
                    param1 = program[(int)instruction.InstructionValue1];
                }
                else
                {
                    param1 = (int)instruction.InstructionValue1;
                }

                int param2;
                if (instruction.Param2Mode == ParameterMode.Position)
                {
                    param2 = program[(int)instruction.InstructionValue2];
                }
                else
                {
                    param2 = (int)instruction.InstructionValue2;
                }

                program[(int)instruction.InstructionValue3] = param1 * param2;
            }
            else if (instruction.OpCode == 3)
            {
                Console.Write("Input: ");
                program[(int)instruction.InstructionValue1] = int.Parse(Console.ReadLine());
            }
            else if (instruction.OpCode == 4)
            {
                int param1;
                if (instruction.Param1Mode == ParameterMode.Position)
                {
                    param1 = program[(int)instruction.InstructionValue1];
                }
                else
                {
                    param1 = (int)instruction.InstructionValue1;
                }

                Console.WriteLine(param1);
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
