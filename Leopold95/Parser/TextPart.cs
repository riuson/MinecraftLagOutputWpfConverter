namespace Leopold95.Parser
{
    internal class TextPart : ITextPart
    {
        public TextPart(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}