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
            ArithmeticExpressionTree ExpTree = new ArithmeticExpressionTree();//initializing tree
            int resultStack = 0;
            int resultTree = 0;
            while (true)
            {

                Console.Write("Hesaplanacak String Değer Giriniz: ");
                string line = Console.ReadLine();
                line = line.Replace(" ", "");
                char[] chr = line.ToCharArray();
                if (!ArithmeticExpressionStack.RegEx(chr))
                {
                    Console.WriteLine("Hesaplama yapılabilecek değer girmediniz!!");
                    continue;
                }

                if (string.IsNullOrEmpty(line))
                {
                    Console.WriteLine("Boş değer giremezsiniz!!");
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                resultStack = ArithmeticExpressionStack.EvalExp(chr);
                Console.WriteLine("Stack ile hesaplama sonucu: {0} = {1}", line, resultStack);
                ExpTree.ExpressionString = line;
                resultTree = ExpTree.Evaluation();
                Console.WriteLine("Tree ile hesaplama sonucu: {0} = {1}", line, resultTree);
                Console.ResetColor();
            }
        }
     }
}
