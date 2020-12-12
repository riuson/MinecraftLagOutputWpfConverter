using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace Leopold95.Parser
{
    public class Parser
    {
        private const char CommandCharacter = '§';

        public FlowDocument Convert(string consoleOutput)
        {
            return new FlowDocument();
        }

        public IEnumerable<string> Split(string consoleOutput)
        {
            var regexCommand = new Regex(CommandCharacter + ".{1}");
            var regexNewLine = new Regex(@"[\r\n]{1,2}");

            var commandMatches = regexCommand.Matches(consoleOutput);
            var newLineMatches = regexNewLine.Matches(consoleOutput);

            var matches = commandMatches
                .Concat(newLineMatches)
                .OrderBy(x => x.Index);

            var result = new List<string>();

            var index = 0;

            foreach (var match in matches)
            {
                if (match.Index > index)
                    result.Add(consoleOutput.Substring(index, match.Index - index));
                result.Add(consoleOutput.Substring(match.Index, match.Length));
                index = match.Index + match.Length;
            }

            if (index < consoleOutput.Length)
                result.Add(consoleOutput.Substring(index));

            return result;
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