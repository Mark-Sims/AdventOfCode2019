namespace Day09
{
    class Parameter
    {
        public long? UnresolvedValue;
        public long ResolvedValue;
        public ParameterMode Mode;
        public IOMode IOMode;
    }

    public enum ParameterMode
    {
        Position,  // Use the parameter value as an address relative to the current address pointer
        Immediate, // Use the literal parameter value
        Relative   // Use the parameter value as an address relative to the "Relative Base" address pointer.
    }

    public enum IOMode
    {
        Read,
        Write
    }
}
