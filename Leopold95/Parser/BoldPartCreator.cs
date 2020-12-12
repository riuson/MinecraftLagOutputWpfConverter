namespace Leopold95.Parser
{
    internal class BoldPartCreator : IPartCreator
    {
        private readonly IParserOptions _options;

        public BoldPartCreator(IParserOptions options)
        {
            _options = options;
        }

        public bool CanHandle(string part)
        {
            if (!part.StartsWith(_options.CommandCharacter)) return false;

            if (part.Length != 2) return false;

            var ch = part[1];

            return ch == 'l';
        }

        public IPart Create(string _)
        {
            return new BoldPart();
        }

        public bool IsContent => false;
    }
}