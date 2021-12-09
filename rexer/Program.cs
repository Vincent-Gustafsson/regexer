using System;

namespace rexer
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = System.IO.File.ReadAllText("test.sigma");
            Lexer lexer = new(source);

            Token tok = lexer.GetToken();
            while (tok.Kind != TokenType.EOF)
            {
                Console.WriteLine($"`{tok.Text}` {tok.Kind} -> {tok.LineNo}:{tok.Col}");
                tok = lexer.GetToken();
            }
        }
    }
}
