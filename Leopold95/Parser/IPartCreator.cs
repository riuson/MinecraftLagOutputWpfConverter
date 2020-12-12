namespace Leopold95.Parser
{
    public interface IPartCreator
    {
        bool IsContent { get; }
        bool CanHandle(string part);
        IPart Create(string part);
    }
}