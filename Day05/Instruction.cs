using System;
using System.Collections.Generic;
using System.Text;

namespace Day05
{
    class Instruction
    {
        public int OpCode { get; set; }
        public ParameterMode Param1Mode { get; set; }
        public ParameterMode Param2Mode { get; set; }

        // Per problem spec:
        // Parameters that an instruction writes to will never be in immediate mode.
        //public ParameterMode Param3Mode { get; set; }

        public int? InstructionValue1 { get; set; }
        public int? InstructionValue2 { get; set; }
        public int? InstructionValue3 { get; set; }
    }

    public enum ParameterMode
    {
        Position,
        Immediate
    }
}
