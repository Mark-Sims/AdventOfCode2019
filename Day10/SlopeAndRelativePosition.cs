namespace Day10
{
    class SlopeAndRelativePosition
    {
        public double Slope { get; set; }
        public bool IsAbove { get; set; }

        public override int GetHashCode()
        {
            return this.Slope.GetHashCode() ^ this.IsAbove.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            SlopeAndRelativePosition x = obj as SlopeAndRelativePosition;
            return x != null && x.Slope.Equals(this.Slope) && x.IsAbove.Equals(this.IsAbove);
        }
    }
}
