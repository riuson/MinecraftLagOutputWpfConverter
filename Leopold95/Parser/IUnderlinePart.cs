namespace Leopold95.Parser
{
    public interface IUnderlinePart : IPart
    {
        bool Underline { get; }
    }

    class UnderlinePart : IUnderlinePart
    {
        public bool Underline { get; }
    }
}