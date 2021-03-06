﻿using System;
using System.Collections.Generic;

namespace Day13
{
    class IntcodeInterpreter
    {
        private ProgramMemory _program;
        private long _addressPointer;
        private long _relativeBase;
        public bool IsHalted { get; set; }

        private IEnumerator<long> _inputs;
        private bool _interractiveMode;
        private long? _output = null;

        public IntcodeInterpreter(string programString, IEnumerable<long> inputs = null, bool isInterractiveMode = true)
        {
            _program = new ProgramMemory(splitInputLine(programString));
            _addressPointer = 0;
            _relativeBase = 0;
            IsHalted = false;
            _interractiveMode = isInterractiveMode;

            if (inputs != null)
            {
                _inputs = inputs.GetEnumerator();
                if (_interractiveMode)
                {
                    throw new Exception("Interpreter started in interractive mode, but inputs were provided programatically.");
                }
            }
        }

        public void PrepareForExecution(IEnumerable<long> inputs = null)
        {
            if (inputs != null)
            {
                _inputs = inputs.GetEnumerator();
                if (_interractiveMode)
                {
                    throw new Exception("Interpreter running in interractive mode, but inputs were provided programatically.");
                }
            }
            _output = null;
        }

        private void ResolveParameterValues(Instruction instruction)
        {
            if (instruction.Param1.UnresolvedValue != null)
            {
                if (instruction.Param1.Mode == ParameterMode.Position)
                {
                    if (instruction.Param1.IOMode == IOMode.Write)
                    {
                        instruction.Param1.ResolvedValue = (long)instruction.Param1.UnresolvedValue;
                    }
                    else
                    {
                        instruction.Param1.ResolvedValue = _program[(long)instruction.Param1.UnresolvedValue];
                    }
                }
                else if (instruction.Param1.Mode == ParameterMode.Immediate)
                {
                    instruction.Param1.ResolvedValue = (long)instruction.Param1.UnresolvedValue;
                }
                else // Relative parameter mode
                {
                    if (instruction.Param1.IOMode == IOMode.Write)
                    {
                        instruction.Param1.ResolvedValue = (long)instruction.Param1.UnresolvedValue + _relativeBase;
                    }
                    else
                    {
                        instruction.Param1.ResolvedValue = _program[(long)instruction.Param1.UnresolvedValue + _relativeBase];
                    }
                }
            }

            if (instruction.Param2.UnresolvedValue != null)
            {
                if (instruction.Param2.Mode == ParameterMode.Position)
                {
                    if (instruction.Param2.IOMode == IOMode.Write)
                    {
                        instruction.Param2.ResolvedValue = (long)instruction.Param2.UnresolvedValue;
                    }
                    else
                    {
                        instruction.Param2.ResolvedValue = _program[(long)instruction.Param2.UnresolvedValue];
                    }
                }
                else if (instruction.Param2.Mode == ParameterMode.Immediate)
                {
                    instruction.Param2.ResolvedValue = (long)instruction.Param2.UnresolvedValue;
                }
                else // Relative parameter mode
                {
                    if (instruction.Param2.IOMode == IOMode.Write)
                    {
                        instruction.Param2.ResolvedValue = (long)instruction.Param2.UnresolvedValue + _relativeBase;
                    }
                    else
                    {
                        instruction.Param2.ResolvedValue = _program[(long)instruction.Param2.UnresolvedValue + _relativeBase];
                    }
                }
            }

            if (instruction.Param3.UnresolvedValue != null)
            {
                if (instruction.Param3.Mode == ParameterMode.Position)
                {
                    if (instruction.Param3.IOMode == IOMode.Write)
                    {
                        instruction.Param3.ResolvedValue = (long)instruction.Param3.UnresolvedValue;
                    }
                    else
                    {
                        throw new Exception("Param 3 should never be a Read IOMode");
                    }
                }
                else if (instruction.Param3.Mode == ParameterMode.Immediate)
                {
                    throw new Exception("Parameters that an instruction writes to will never be in immediate mode.");
                }
                else // Relative parameter mode
                {
                    if (instruction.Param3.IOMode == IOMode.Write)
                    {
                        instruction.Param3.ResolvedValue = (long)instruction.Param3.UnresolvedValue + _relativeBase;
                    }
                    else
                    {
                        throw new Exception("Param 3 should never be a Read IOMode");
                    }
                }
            }
        }

        public long? ExecuteProgram()
        {
            _output = null;

            while (true)
            {
                // This string values represents the opcode, as well as the parameter
                // mode (immediate/positional) for the instruction's parameters.
                // We left pad with 0's because if the value is contains fewer
                // parameter modes than the number of values in the instruction,
                // then the parameter modes are assumed to be positional. (AKA, 0).
                string value_0 = _program[_addressPointer].ToString().PadLeft(5, '0');

                var instr = new Instruction
                {
                    // The final 2 digits represent the opcode
                    OpCode = int.Parse(value_0.Substring(3, 2)),

                    // The next 3 digits (read right to left) represent the parameter
                    // modes of the values in this instruction.
                    // Note that the instruction may not necessarily have values for this
                    // many parameter modes. But if that is the case, these parameter modes
                    // will simply be ignored/unused.
                    Param1 = new Parameter { Mode = (ParameterMode)int.Parse(value_0.Substring(2, 1)) },
                    Param2 = new Parameter { Mode = (ParameterMode)int.Parse(value_0.Substring(1, 1)) },
                    Param3 = new Parameter { Mode = (ParameterMode)int.Parse(value_0.Substring(0, 1)) },
                };

                if (instr.OpCode == 1 || instr.OpCode == 2)
                {

                    instr.Param1.UnresolvedValue = _program[_addressPointer + 1];
                    instr.Param2.UnresolvedValue = _program[_addressPointer + 2];
                    instr.Param3.UnresolvedValue = _program[_addressPointer + 3];

                    instr.Param1.IOMode = IOMode.Read;
                    instr.Param2.IOMode = IOMode.Read;
                    instr.Param3.IOMode = IOMode.Write;

                    _addressPointer = ExecuteInstruction(instr, _addressPointer);
                }
                else if (instr.OpCode == 3)
                {
                    instr.Param1.UnresolvedValue = _program[_addressPointer + 1];
                    instr.Param1.IOMode = IOMode.Write;

                    _addressPointer = ExecuteInstruction(instr, _addressPointer);
                }
                else if (instr.OpCode == 4)
                {
                    instr.Param1.UnresolvedValue = _program[_addressPointer + 1];
                    instr.Param1.IOMode = IOMode.Read;

                    _addressPointer = ExecuteInstruction(instr, _addressPointer);

                    // Opcode 4 should now yield the program.
                    if (_output is null)
                    {
                        throw new Exception("Output is null");
                    }
                    return _output;
                }
                else if (instr.OpCode == 5 || instr.OpCode == 6)
                {
                    instr.Param1.UnresolvedValue = _program[_addressPointer + 1];
                    instr.Param2.UnresolvedValue = _program[_addressPointer + 2];

                    instr.Param1.IOMode = IOMode.Read;
                    instr.Param2.IOMode = IOMode.Read;

                    _addressPointer = ExecuteInstruction(instr, _addressPointer);
                }
                else if (instr.OpCode == 7 || instr.OpCode == 8)
                {
                    instr.Param1.UnresolvedValue = _program[_addressPointer + 1];
                    instr.Param2.UnresolvedValue = _program[_addressPointer + 2];
                    instr.Param3.UnresolvedValue = _program[_addressPointer + 3];

                    instr.Param1.IOMode = IOMode.Read;
                    instr.Param2.IOMode = IOMode.Read;
                    instr.Param3.IOMode = IOMode.Write;

                    _addressPointer = ExecuteInstruction(instr, _addressPointer);
                }
                else if (instr.OpCode == 9)
                {
                    instr.Param1.UnresolvedValue = _program[_addressPointer + 1];

                    instr.Param1.IOMode = IOMode.Read;

                    _addressPointer = ExecuteInstruction(instr, _addressPointer);

                }
                else if (instr.OpCode == 99)
                {
                    Console.WriteLine("Halting...");
                    IsHalted = true;
                    return null;
                }
                // This instruction contains a first value that specifies non-zero parameter modes
                else
                {
                    throw new Exception(string.Format("Unsupported instruction with opcode: {0}", instr.OpCode.ToString()));
                }
            }
        }

        public void SetInterractiveMode(bool interractiveMode)
        {
            _interractiveMode = interractiveMode;
        }

        // Return: The new value for the address pointer. This value is dependent on not only which
        // opcode is executed, but also, sometimes on the outcome of that execution.
        private long ExecuteInstruction(Instruction instruction, long _addressPointer)
        {
            ResolveParameterValues(instruction);

            if (instruction.OpCode == 1)
            {
                _program[instruction.Param3.ResolvedValue] = instruction.Param1.ResolvedValue + instruction.Param2.ResolvedValue;
                return _addressPointer + 4;
            }
            else if (instruction.OpCode == 2)
            {
                _program[instruction.Param3.ResolvedValue] = instruction.Param1.ResolvedValue * instruction.Param2.ResolvedValue;
                return _addressPointer + 4;
            }
            else if (instruction.OpCode == 3)
            {
                if (_interractiveMode)
                {
                    Console.Write("Input: ");
                    _program[instruction.Param1.ResolvedValue] = int.Parse(Console.ReadLine());
                }
                else
                {
                    _inputs.MoveNext();
                    _program[instruction.Param1.ResolvedValue] = _inputs.Current;
                }
                return _addressPointer + 2;
            }
            else if (instruction.OpCode == 4)
            {
                if (_interractiveMode)
                {
                    Console.WriteLine(instruction.Param1.ResolvedValue);
                }

                // Always set the output value even if in iterractive mode.
                // It can just be ignored if not wanted.
                _output = instruction.Param1.ResolvedValue;

                return _addressPointer + 2;
            }
            else if (instruction.OpCode == 5 || instruction.OpCode == 6)
            {
                if (instruction.OpCode == 5)
                {
                    if (instruction.Param1.ResolvedValue != 0)
                    {
                        return instruction.Param2.ResolvedValue;
                    }
                    else
                    {
                        return _addressPointer + 3;
                    }
                }
                else
                {
                    if (instruction.Param1.ResolvedValue == 0)
                    {
                        return instruction.Param2.ResolvedValue;
                    }
                    else
                    {
                        return _addressPointer + 3;
                    }
                }


            }
            else if (instruction.OpCode == 7 || instruction.OpCode == 8)
            {
                if (instruction.OpCode == 7)
                {
                    _program[instruction.Param3.ResolvedValue] = instruction.Param1.ResolvedValue < instruction.Param2.ResolvedValue ? 1 : 0;
                }
                else
                {
                    _program[instruction.Param3.ResolvedValue] = instruction.Param1.ResolvedValue == instruction.Param2.ResolvedValue ? 1 : 0;
                }

                return _addressPointer + 4;
            }
            else if (instruction.OpCode == 9)
            {
                _relativeBase += instruction.Param1.ResolvedValue;

                return _addressPointer + 2;
            }
            else
            {
                throw new Exception(string.Format("Cannot execute unknown opcode: {0}", instruction.OpCode.ToString()));
            }
        }

        private static long[] splitInputLine(string intcodesLine)
        {
            return Array.ConvertAll(intcodesLine.Split(","), s => long.Parse(s));
        }
    }
}
