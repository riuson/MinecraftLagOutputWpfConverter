namespace Leopold95.Parser
{
    internal class UnderlinePartCreator : IPartCreator
    {
        private readonly IParserOptions _options;

        public UnderlinePartCreator(IParserOptions options)
        {
            _options = options;
        }

        public bool CanHandle(string part)
        {
            if (!part.StartsWith(_options.CommandCharacter)) return false;

            if (part.Length != 2) return false;

            var ch = part[1];

            return ch == 'n';
        }

        public IPart Create(string _)
        {
            return new UnderlinePart();
        }

        public bool IsContent => false;
    }
}