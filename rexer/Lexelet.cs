using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace rexer
{
    class Lexelet
    {
        public Regex Matcher { get; }
        public TokenType Kind { get; }
        public bool IsIgnored { get; }

        public Lexelet(string pattern, bool isIgnored)
        {
            this.Matcher = new(pattern, RegexOptions.Compiled);
            IsIgnored = isIgnored;
        }

        public Lexelet(string pattern, TokenType kind, bool isIgnored = false)
        {
            this.Matcher = new(pattern, RegexOptions.Compiled);
            this.Kind = kind;
            IsIgnored = isIgnored;
        }

        public Match MatchDefinition(string input)
        {
            var matched = this.Matcher.Match(input);
            if (matched.Success)
            {
                return matched;
            }
            return null;
        }
    }
}
