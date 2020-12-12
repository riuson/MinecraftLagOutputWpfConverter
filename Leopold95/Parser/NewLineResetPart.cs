namespace Leopold95.Parser
{
    internal class NewLineResetPart : INewLineResetPart, IResetModifiers
    {
        public override bool Equals(object? other)
        {
            if (ReferenceEquals(this, other)) return true;

            if (this is null || other is null) return false;

            return other is NewLineResetPart _;
        }
    }
}