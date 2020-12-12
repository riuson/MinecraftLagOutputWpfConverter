using System.Windows.Media;

namespace Leopold95.Parser
{
    internal class ColorPart : IColorPart
    {
        public ColorPart(Color color)
        {
            Color = color;
        }

        public Color Color { get; }
    }
}