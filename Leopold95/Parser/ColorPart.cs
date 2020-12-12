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

        public override bool Equals(object? other)
        {
            if (ReferenceEquals(this, other)) return true;

            if (this is null || other is null) return false;

            if (!(other is ColorPart cp)) return false;

            return Equals(cp);
        }

        public bool Equals(ColorPart other)
        {
            return Color == other.Color;
        }
    }
}