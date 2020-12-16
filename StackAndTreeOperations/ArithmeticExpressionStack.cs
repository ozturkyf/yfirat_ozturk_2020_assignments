using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAndTreeOperations
{
    public static class ArithmeticExpressionStack
    {
        public static int EvalExp(char[] chr)
        {
            Stack<int> valStk = new Stack<int>();
            Stack<char> opStk = new Stack<char>();

            for (int i = 0; i < chr.Length; i++)
            {
                if (chr[i] == ' ')
                    continue;

                if (chr[i] >= '0' && chr[i] <= '9')
                {
                    StringBuilder sb = new StringBuilder();
                    bool deg = true;
                    for (int j = i; j < chr.Length; j++)
                    {
                        if (!Char.IsNumber(chr[j]))
                        {
                            i = --j;
                            break;
                            deg = false;
                        }
                        sb.Append(chr[j]);
                        if (deg)
                        {
                            i = j;
                        }
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
                        valStk.Push(OperatorAction(opStk.Pop(), valStk.Pop(), valStk.Pop()));
                    }
                    opStk.Pop();
                }
                else if (chr[i] == '+' || chr[i] == '-' || chr[i] == '*' || chr[i] == '/' || chr[i] == '%')
                {

                    while (opStk.Count != 0 && HasPrecedenceStatus(chr[i], opStk.Peek()))
                    {
                        valStk.Push(OperatorAction(opStk.Pop(), valStk.Pop(), valStk.Pop()));
                    }
                    

                    opStk.Push(chr[i]);
                }
            }

            while (opStk.Count != 0)
            {
                valStk.Push(OperatorAction(opStk.Pop(), valStk.Pop(), valStk.Pop()));
            }

            return valStk.Pop();
        }

        public static bool HasPrecedenceStatus(char op1, char op2)
        {

            if (op2 == '(' || op2 == ')')
                return false;
            if ((op1 == '*' || op1 == '/' || op1 == '%') && (op2 == '+' || op2 == '-'))
                return false;
            else
                return true;
        }

        public static int OperatorAction(char op, int b, int a)
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
                    {
                        Console.WriteLine("Sıfıra a bölünmez.");
                        return 0;
                    }
                    return a / b;
                case '%':
                    return a % b;
            }
            return 0;
        }

        public static bool RegEx(char[] val)
        {
            //char[] arr = new char[] { '+', '-', '*', '/', '%','0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] arr2 = new char[] { '+', '-', '*', '/', '%','(',')' };


            foreach (var item in val)
            {
                if (!Char.IsNumber(item) && !arr2.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
