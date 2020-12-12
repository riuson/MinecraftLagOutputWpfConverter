using System.Windows;
using System.Windows.Documents;

namespace Leopold95.Parser
{
    internal class BoldPart : IBoldPart, ISetModifiers
    {
        public void SetModifiers(Span span)
        {
            span.FontWeight = FontWeights.Bold;
        }

        public override bool Equals(object? other)
        {
            if (ReferenceEquals(this, other)) return true;

            if (this is null || other is null) return false;

            return other is BoldPart _;
        }
    }
}