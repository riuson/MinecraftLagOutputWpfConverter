namespace Leopold95.Parser
{
    internal class UnderlinePart : IUnderlinePart
    {
        public override bool Equals(object? other)
        {
            if (ReferenceEquals(this, other)) return true;

            if (this is null || other is null) return false;

            return other is UnderlinePart _;
        }
    }
}