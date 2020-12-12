using System.Linq;

namespace Leopold95.Parser
{
    internal class NewLineResetPartCreator : IPartCreator
    {
        public bool CanHandle(string part)
        {
            var lineEndings = new[]
            {
                "\r\n",
                "\r",
                "\n"
            };

            return lineEndings.Contains(part);
        }

        public IPart Create(string _)
        {
            return new NewLineResetPart();
        }

        public bool IsContent => false;
    }
}