﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace Leopold95.Parser
{
    public class Parser
    {
        private readonly IParserOptions _options;
        private readonly IEnumerable<IPartCreator> _partCreators;

        public Parser()
        {
            _options = new ParserOptions();
            _partCreators = new IPartCreator[]
            {
                new BoldPartCreator(_options),
                new ColorPartCreator(_options),
                new NewLineResetPartCreator(),
                new UnderlinePartCreator(_options),
                new TextPartCreator()
            };
        }

        public Paragraph Convert(string consoleOutput)
        {
            var stringParts = Split(consoleOutput);
            var parsedParts = ConvertParts(stringParts);

            var spans = new List<Span>();
            var modifiers = new List<ISetModifiers>();

            foreach (var parsedPart in parsedParts)
            {
                if (parsedPart is ISetModifiers sm) modifiers.Add(sm);

                if (parsedPart is IResetModifiers _)
                {
                    modifiers.Clear();
                    spans.Add(new Span(new LineBreak()));
                }

                if (parsedPart is IContent c)
                {
                    var spanContent = c.CreateContent();
                    modifiers.ForEach(sm => sm.SetModifiers(spanContent));
                    spans.Add(spanContent);
                }
            }

            var paragraph = new Paragraph();
            paragraph.Inlines.AddRange(spans);
            return paragraph;
        }

        internal IEnumerable<string> Split(string consoleOutput)
        {
            var regexCommand = new Regex(_options.CommandCharacter + ".{1}");
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

        internal IPart ConvertPart(string part)
        {
            foreach (var partCreator in _partCreators.Where(x => !x.IsContent))
                if (partCreator.CanHandle(part))
                    return partCreator.Create(part);

            foreach (var partCreator in _partCreators.Where(x => x.IsContent))
                if (partCreator.CanHandle(part))
                    return partCreator.Create(part);

            throw new ParserException($"Failed to parse part '{part}'");
        }

        internal IEnumerable<IPart> ConvertParts(IEnumerable<string> parts)
        {
            return parts.Select(ConvertPart);
        }
    }
}