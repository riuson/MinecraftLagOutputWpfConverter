using System.Windows.Documents;

namespace Leopold95.Parser
{
    public interface IContent
    {
        Span CreateContent();
    }
}