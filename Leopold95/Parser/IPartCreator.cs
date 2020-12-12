namespace Leopold95.Parser
{
    public interface IPartCreator
    {
        bool Fallback { get; }
        bool CanHandle(string part);
        IPart Create(string part);
    }
}