using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace rexer
{
    class Lexer
    {
        string Source { get; }
        int LineNo { get; set; }
        int Col { get; set; }
        int Pos { get; set; }

        List<Lexelet> Lexelets { get; }

        public Lexer(string source)
        {
            this.Source = source;
            this.Lexelets = new();

            this.LineNo = 1;
            this.Col = 1;

            this.RegisterLexelet(@"^\Z", TokenType.EOF);

            // NEWLINE
            this.RegisterLexelet(@"^(\r\n|\n)", TokenType.NEWLINE);

            // WHITESPACE
            this.RegisterLexelet(@"^( |\t)+", isIgnored: true);
            // SINGLE-LINE COMMENT
            this.RegisterLexelet(@"^//.*(\r\n|\n)", isIgnored: true);
            // MULTI-LINE COMMENT
            // TODO
            this.RegisterLexelet("^", isIgnored: true);

            this.RegisterLexelet(@"^==", TokenType.EQEQ);
            this.RegisterLexelet(@"^->", TokenType.OPEN_ARROW);
            this.RegisterLexelet(@"^<-", TokenType.CLOSE_ARROW);

            this.RegisterLexelet(@"^\(", TokenType.LPAREN);
            this.RegisterLexelet(@"^\)", TokenType.RPAREN);
            this.RegisterLexelet(@"^>", TokenType.GT);
            this.RegisterLexelet(@"^=", TokenType.EQ);
            this.RegisterLexelet(@"^;", TokenType.SEMICOLON);
            this.RegisterLexelet(@"^:", TokenType.COLON);
            this.RegisterLexelet(@"^\?", TokenType.CLOSE_ARROW);

            this.RegisterLexelet(@"if", TokenType.IF);

            this.RegisterLexelet(@"^[_a-zA-Z][_a-zA-Z0-9]*", TokenType.IDEN);
            this.RegisterLexelet(@"^\d*\.?\d+", TokenType.NUMBER);
        }

        void RegisterLexelet(string pattern, TokenType kind)
            => this.Lexelets.Add(new(pattern, kind));

        void RegisterLexelet(string pattern, bool isIgnored)
            => this.Lexelets.Add(new(pattern, isIgnored));

        void Abort(string message)
        {
            Console.WriteLine($"[ERROR] {message}");
            Environment.Exit(1);
        }

        void Advance(int length, bool isNewline = false)
        {
            this.Pos += length;
            this.Col += length;

            if (!isNewline)
                return;

            this.LineNo += 1;
            this.Col = 1;
        }

        public Token GetToken()
        {
            Match match;
            var lexelet = this.FindLexelet(this.Source.Substring(this.Pos), out match);

            while (lexelet.IsIgnored || lexelet.Kind == TokenType.NEWLINE)
            {
                this.Advance(match.Length, lexelet.Kind == TokenType.NEWLINE);
                lexelet = this.FindLexelet(this.Source.Substring(this.Pos), out match);
            }

            Token token = new(match.Value, lexelet.Kind, this.LineNo, this.Col);
            this.Advance(match.Length, false);
            return token;
        }

        Lexelet FindLexelet(string input, out Match match)
        {
            foreach (var lexelet in this.Lexelets)
            {
                match = lexelet.MatchDefinition(input);

                if (match != null)
                    return lexelet;
            }

            var unknownChar = this.Source[this.Pos];
            this.Abort($"Unknown character `{unknownChar}` (U+{(int)unknownChar}) at {this.LineNo}:{this.Col}");
            match = null;
            return null;
        }
    }
}
