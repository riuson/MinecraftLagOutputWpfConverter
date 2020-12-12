using System.Windows.Media;

namespace Leopold95.Parser
{
    internal class ColorPartCreator : IPartCreator
    {
        private static readonly Color[] Colors =
        {
            System.Windows.Media.Colors.Black,
            System.Windows.Media.Colors.DarkBlue,
            System.Windows.Media.Colors.DarkGreen,
            System.Windows.Media.Colors.DarkCyan,
            System.Windows.Media.Colors.DarkRed,
            System.Windows.Media.Colors.DarkMagenta,
            System.Windows.Media.Colors.Olive,
            System.Windows.Media.Colors.Gray,
            System.Windows.Media.Colors.DarkGray,
            System.Windows.Media.Colors.Blue,
            System.Windows.Media.Colors.Green,
            System.Windows.Media.Colors.Cyan,
            System.Windows.Media.Colors.Red,
            System.Windows.Media.Colors.Magenta,
            System.Windows.Media.Colors.Yellow,
            System.Windows.Media.Colors.White
        };

        private readonly IParserOptions _options;

        public ColorPartCreator(IParserOptions options)
        {
            _options = options;
        }

        public bool CanHandle(string part)
        {
            if (!part.StartsWith(_options.CommandCharacter)) return false;

            if (part.Length != 2) return false;

            var ch = part[1];

            if (ch >= '0' && ch <= '9') return true;
            if (ch >= 'a' && ch <= 'f') return true;

            return false;
        }

        public IPart Create(string part)
        {
            var ch = part[1];

            if (ch >= '0' && ch <= '9') return new ColorPart(Colors[ch - '0']);

            return new ColorPart(Colors[10 + ch - 'a']);
        }

        public bool IsContent => false;
    }
}