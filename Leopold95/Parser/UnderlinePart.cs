using System.Windows;
using System.Windows.Documents;

namespace Leopold95.Parser
{
    internal class UnderlinePart : IUnderlinePart, ISetModifiers
    {
        public void SetModifiers(Span span)
        {
            span.TextDecorations.Add(TextDecorations.Underline);
        }

        public override bool Equals(object? other)
        {
            if (ReferenceEquals(this, other)) return true;

            if (this is null || other is null) return false;

            return other is UnderlinePart _;
        }
    }
}