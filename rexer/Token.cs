using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rexer
{
    enum TokenType
    {
        EQ,
        SPACE,
        SEMICOLON,
        CLOSE_ARROW,
        EOF,
        EQEQ,
        NEWLINE,
        COLON,
        IDEN,
        NUMBER,
        IF,
        OPEN_ARROW,
        LPAREN,
        RPAREN,
        GT,
        FOR,
        PLUS,
        ASTERISK,
        SLASH,
        MINUS,
        WHITESPACE
    }
    class Token
    {
        public Token(char text, TokenType kind, int lineno, int col)
        {
            Text = text.ToString();
            Kind = kind;
            LineNo = lineno;
            Col = col;
        }
        public Token(string text, TokenType kind, int lineno, int col)
        {
            Text = text;
            Kind = kind;
            LineNo = lineno;
            Col = col;
        }

        public string Text { get; }
        public TokenType Kind { get; }
        public int LineNo { get; }
        public int Col { get; }

        public bool CheckType(TokenType check) => this.Kind == check;
    }
}
