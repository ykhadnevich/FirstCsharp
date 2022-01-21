using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCsharp
{
    public enum TokenType
    { 
        Number, Plus, Minus, Divide, Multiply, Power, ParenthesesL, ParenthesesR
    }
    public class Token
    {
        public TokenType Type { get; set; }
        public double Value { get; set; }
        public Token (TokenType type, double value = 0) { Type = type; Value = value; }
        public override string ToString()
            
        {
            if (Type == TokenType.Number)
                return $"({Type}:{Value})";
            return $"({Type})";
        }
    }
    internal class Parser
    {
        public string Text { get; set; }
        public int Index { get; set; }
        public char Current { get; set; }
        public Parser(string text)
        {
            Text = text.Replace('.', ',');
            Index = -1;
            Advance();
        }
        public void Advance()
        {
            Index++;
            if (Index < Text.Length )
                Current = Text[Index];
        }

        public Queue<Token> Tokenizer()
        {
            Queue<Token> tokens = new Queue<Token>();
            while (Index < Text.Length)
            {   

                if (char.IsDigit(Current))
                {
                    tokens.Enqueue(CreateNumber());
                    continue;
                }
                else if (Current == '+')
                {
                    tokens.Enqueue(new Token(TokenType.Plus));   
                }
                else if (Current == '-')
                {
                    tokens.Enqueue(new Token(TokenType.Minus));
                }
                else if (Current == '/')
                {
                    tokens.Enqueue(new Token(TokenType.Divide));
                }
                else if (Current == '*')
                {
                    tokens.Enqueue(new Token(TokenType.Multiply));
                }
                else if (Current == '^')
                {
                    tokens.Enqueue(new Token(TokenType.Power));
                }
                else if (Current == '(')
                {
                    tokens.Enqueue(new Token(TokenType.ParenthesesL));
                }
                else if (Current == ')')
                {
                    tokens.Enqueue(new Token(TokenType.ParenthesesR));
                }
                Advance();


            }
            
            return tokens;
        }

        private Token CreateNumber()
        {
            string buffer = "";
            while (Index < Text.Length && (char.IsDigit(Current) || Current == ','))
            {
                buffer += Current;
                Advance();
            }
            return new Token(TokenType.Number, double.Parse(buffer));
        }
        public Queue<Token> Sort(Queue<Token> tokens)
        {
            Queue<Token> output = new Queue<Token>();
            Stack<Token> operators = new Stack<Token>();
            Token op;

            Token token;
            while (tokens.Count > 0)
            {
                token = tokens.Dequeue();

                if (token.Type == TokenType.Number)
                {
                    output.Enqueue(token);
                }
                else if (token.Type == TokenType.Plus ||
                         token.Type == TokenType.Minus ||
                         token.Type == TokenType.Multiply ||
                         token.Type == TokenType.Divide ||
                         token.Type == TokenType.Power) 
                {
                    if (operators.Count > 0)
                    {
                        op = operators.Peek();
                        if((op.Type == TokenType.Multiply ||
                         op.Type == TokenType.Divide) && (token.Type != TokenType.Multiply ||
                         token.Type != TokenType.Divide))
                            output.Enqueue(operators.Pop());
                    }
                    operators.Push(token);
                }
                else if (token.Type == TokenType.ParenthesesL)
                {
                    operators.Push(token);
                }
                else if (token.Type == TokenType.ParenthesesR)
                {
                    while (operators.Count > 0)
                    {
                        op = operators.Pop();
                        if (op.Type == TokenType.ParenthesesL) break;
                        output.Enqueue(op);
                    }
                }
            }

            while (operators.Count > 0)
            {
                op = operators.Pop();
                if (op.Type == TokenType.ParenthesesL) throw new ArgumentException("Parenthesis is not closed!");
                output.Enqueue(op);
            }

            return output;
        }
        public double InterpretRpn(Queue<Token> tokens)
        {
            Stack<double> numbers = new();

            Token token;
            while (tokens.Count > 0)
            {
                token = tokens.Dequeue();

                if (token.Type == TokenType.Number) numbers.Push(token.Value);
                else
                {
                    double num2 = numbers.Pop();
                    double num1 = numbers.Pop();

                    switch (token.Type)
                    {
                        case TokenType.Plus:
                            numbers.Push(num1 + num2);
                            break;
                        case TokenType.Minus:
                            numbers.Push(num1 - num2);
                            break;
                        case TokenType.Multiply:
                            numbers.Push(num1 * num2);
                            break;
                        case TokenType.Divide:
                            numbers.Push(num1 / num2);
                            break;
                        case TokenType.Power:
                            numbers.Push((int)Math.Pow(num1, num2));
                            break;
                    }
                }
            }

            return numbers.Pop();
        }
    }
}
