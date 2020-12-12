using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Leopold95.Parser
{
    public class Parser
    {
        public FlowDocument Convert(string consoleOutput)
        {
            return new FlowDocument();
        }

        public IEnumerable<string> Split(string consoleOutput)
        {
            throw new NotImplementedException();
        }

        public IPart ConvertPart(string part)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPart> ConvertParts(IEnumerable<string> parts)
        {
            throw new NotImplementedException();
        }
    }
}