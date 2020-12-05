using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAndTreeOperations
{
    class Program
    {
        public static void Main(string[] args)
        {
            int value = 0;
            while (true)
            {

                Console.Write("Hesaplanacak Değer Giriniz: ");
                string line = Console.ReadLine();
                 if (string.IsNullOrEmpty(line))
                    break;
                value = EvalExp(line);

                Console.WriteLine("{0}={1}", line, value);

            }
        }

        public static int EvalExp(String exp)
        {

            char[] chr = exp.ToCharArray();

            Stack<int> valStk = new Stack<int>();
            Stack<char> opStk = new Stack<char>();

            for (int i = 0; i < exp.Length; i++)
            {
                if (chr[i] == ' ')
                    continue;

                if (chr[i] >= '0' && chr[i] <= '9')
                {
                    StringBuilder sb = new StringBuilder();

                    for (int j = i; j < chr.Length; j++)
                    {
                        if (!Char.IsNumber(chr[j]))
                        {
                            i = --j;
                            break;
                        }
                        sb.Append(chr[j]);
                    }
                    valStk.Push(int.Parse(sb.ToString()));
                }
                else if (chr[i] == '(')
                {
                    opStk.Push(chr[i]);
                }
                else if (chr[i] == ')')
                {
                    while (opStk.Peek() != '(')
                    {
                        valStk.Push(ApplyOp(opStk.Pop(), valStk.Pop(), valStk.Pop()));
                    }
                    opStk.Pop();
                }
                else if (chr[i] == '+' || chr[i] == '-' || chr[i] == '*' || chr[i] == '/' || chr[i] == '%')
                {

                    while (opStk.Count != 0 && HasPrecedence(chr[i], opStk.Peek()))
                    {
                        valStk.Push(ApplyOp(opStk.Pop(), valStk.Pop(), valStk.Pop()));
                    }

                    opStk.Push(chr[i]);
                }
            }

            while (opStk.Count != 0)
            {
                valStk.Push(ApplyOp(opStk.Pop(), valStk.Pop(), valStk.Pop()));
            }

            return valStk.Pop();
        }

        public static bool HasPrecedence(char op1, char op2)
        {
            if (op2 == '(' || op2 == ')')
                return false;
            if ((op1 == '*' || op1 == '/') && (op2 == '%' || op2 == '+' || op2 == '-'))
                return false;
            else
                return true;
        }

        public static int ApplyOp(char op, int b, int a)
        {
            switch (op)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    if (b == 0)
                        throw new Exception("0 a bölünemez.");
                    return a / b;
                case '%':
                    return a % b;
            }
            return 0;
        }
    }
}
