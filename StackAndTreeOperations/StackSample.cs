using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAndTreeOperations
{
    class StackSample
    {
        public static int CallStck()
        {
            int value = 0;
            while (true)
            {

                Console.Write("Enter expression (ENTER to exit): ");
                string line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                    break;

                char[] exp = line.ToCharArray();
                value = EvalExp(exp);

                //Console.WriteLine("{0}={1}", line, value);
                
            }
            return value;
        }

        public static int EvalExp(char[] chr)
        {
            Stack<int> valStk = new Stack<int>();
            Stack<char> opStk = new Stack<char>();

            opStk.Push('('); // Implicit opening parenthesis

            int pos = 0;
            while (pos <= chr.Length)
            {
                if (pos == chr.Length || chr[pos] == ')')
                {
                    ProcessClosingParenthesis(valStk, opStk);
                    pos++;
                }
                else if (chr[pos] >= '0' && chr[pos] <= '9')
                {
                    pos = ProcessInputNumber(chr, pos, valStk);
                }
                else
                {
                    ProcessInputOperator(chr[pos], valStk, opStk);
                    pos++;
                }
            }

            return valStk.Pop(); // Result remains on values stacks
        }

        public static void ProcessClosingParenthesis(Stack<int> vStack, Stack<char> opStack)
        {
            while (opStack.Peek() != '(')
                ExecuteOperation(vStack, opStack);

            opStack.Pop(); // Remove the opening parenthesis
        }

        public static int ProcessInputNumber(char[] exp, int pos, Stack<int> vStack)
        {
            int value = 0;
            while (pos < exp.Length &&
                    exp[pos] >= '0' && exp[pos] <= '9')
                value = 10 * value + (int)(exp[pos++] - '0');

            vStack.Push(value);

            return pos;
        }

        public static void ProcessInputOperator(char op, Stack<int> vStack, Stack<char> opStack)
        {
            while (opStack.Count > 0 && OperatorCausesEvaluation(op, opStack.Peek()))
                ExecuteOperation(vStack, opStack);

            opStack.Push(op);
        }

        public static bool OperatorCausesEvaluation(char op, char prevOp)
        {

            bool evaluate = false;

            switch (op)
            {
                case '+':
                case '-':
                    evaluate = (prevOp != '(');
                    break;
                case '*':
                case ' ':
                    evaluate = (prevOp == '*' || prevOp == ' ');
                    break;
                case ')':
                    evaluate = true;
                    break;
            }

            return evaluate;

        }

        public static void ExecuteOperation(Stack<int> vStack, Stack<char> opStack)
        {

            int rightOperand = vStack.Pop();
            int leftOperand = vStack.Pop();
            char op = opStack.Pop();

            int result = 0;
            switch (op)
            {
                case '+':
                    result = leftOperand + rightOperand;
                    break;
                case '-':
                    result = leftOperand - rightOperand;
                    break;
                case '*':
                    result = leftOperand * rightOperand;
                    break;
                case '/':
                    result = leftOperand / rightOperand;
                    break;
            }

            vStack.Push(result);
        }
    }
}
