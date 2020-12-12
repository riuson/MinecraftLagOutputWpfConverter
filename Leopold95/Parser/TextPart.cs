namespace Leopold95.Parser
{
    internal class TextPart : ITextPart
    {
        public TextPart(string text)
        {
            Text = text;
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