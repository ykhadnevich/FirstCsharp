using System;
using System.Globalization;
using System.Net;
using FirstCsharp;

while (true)
{
    Console.Write(">");
    string input = Console.ReadLine();
    Parser parser = new Parser(input);
    var tokens = parser.Tokenizer();
    var rpn = parser.Sort(tokens);
    Console.WriteLine(parser.InterpretRpn(rpn));

}



