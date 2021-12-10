using System;

namespace rexer
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = System.IO.File.ReadAllText("test.sigma");
            Lexer lexer = new(source);

            var tokens = lexer.Tokenize();
            foreach (var token in tokens)
            {
                Console.WriteLine($"{token.Kind} at {token.LineNo}:{token.Col}");
            }
        }
    }
}
