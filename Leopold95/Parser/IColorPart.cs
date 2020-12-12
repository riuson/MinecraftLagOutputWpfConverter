using System.Windows.Media;

namespace Leopold95.Parser
{
    public interface IColorPart : IPart
    {
        Color Color { get; }
    }
}