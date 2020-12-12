using System.Windows.Documents;

namespace Leopold95.Parser
{
    internal class TextPart : ITextPart, IContent
    {
        public TextPart(string text)
        {
            Text = text;
        }

        public Span CreateContent()
        {
            return new Span(new Run(Text));
        }

        public string Text { get; }

        public override bool Equals(object? other)
        {
            if (ReferenceEquals(this, other)) return true;

            if (this is null || other is null) return false;

            if (!(other is TextPart tp)) return false;

            return Equals(tp);
        }

        public bool Equals(TextPart other)
        {
            return Text == other.Text;
        }
    }
}