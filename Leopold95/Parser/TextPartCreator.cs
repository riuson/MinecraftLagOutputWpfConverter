namespace Leopold95.Parser
{
    internal class TextPartCreator : IPartCreator
    {
        public bool CanHandle(string _)
        {
            return true;
        }

        public IPart Create(string part)
        {
            return new TextPart(part);
        }

        public bool Fallback => true;
    }
}