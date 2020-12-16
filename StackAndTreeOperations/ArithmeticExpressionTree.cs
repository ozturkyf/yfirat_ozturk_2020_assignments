using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAndTreeOperations
{
    public class ArithmeticExpressionTree
    {
        public Node RootTreeNode;
        public string expressionString;
        public readonly static char[] PossibleOperands = { '+', '-', '*', '/', '%' };

        //Root Node field
        public Node RootTN
        {
            set
            {
                this.RootTreeNode = value;
            }
            get
            {
                return this.RootTreeNode;
            }
        }

        //Hesapalanacak İfade
        public string ExpressionString
        {
            get
            {
                return expressionString;
            }
            set
            {
                //ağacı yeniden derleme. Diziyi yeniden oluşturma
                expressionString = value;
                RootTreeNode = NodeOlustur(expressionString);
            }
        }

        public Node NodeOlustur(string inputexpression)
        {
            if (string.IsNullOrEmpty(inputexpression))
            {
                return null;
            }


            //Sol parantez varsa sağ parantezi arar
            if (inputexpression[0] == '(')
            {
                //parantez takip sayacı
                int parcounter = 0;
                for (int i = 0; i < inputexpression.Length; i++)
                {
                    if (inputexpression[i] == '(')
                    {
                        parcounter++; //sol parantezdeki sayac artışı
                    }
                    else if (inputexpression[i] == ')')
                    {
                        parcounter--;//sağ parantezdeki sayaç eksiltmesi.

                        if (parcounter == 0)//sol ve sağ parantezlerin eşit olduğu anlamına gelir.
                        {
                            if (inputexpression.Length - 1 != i)
                            {
                                break; // cümlenin sonu değilse devam.
                            }
                            else
                            {
                                // ifade sonunda parantezler içi derleniyor.
                                return NodeOlustur(inputexpression.Substring(1, inputexpression.Length - 2));
                            }
                        }
                    }
                }
            }

            char[] operArr = PossibleOperands;
            foreach (char operand in operArr)
            {
                //node boş değilse alttaki dalları geri döndürür.
                Node oNode = NodeOlustur(inputexpression, operand);
                if (oNode != null)
                    return oNode;
            }

            return new ValNode(int.Parse(inputexpression));

        }

        // ifade ve operatör alır.
        public Node NodeOlustur(string expression, char operation)
        {
            bool flag = false;
            int i = expression.Length - 1;
            int parcounter = 0;
            bool rightsided = false;

            while (!flag)
            {
                if (expression[i] == '(')
                {
                    if (rightsided)
                        parcounter--;
                    else
                        parcounter++;
                }
                else if (expression[i] == ')')
                {
                    if (rightsided)
                        parcounter++;
                    else
                        parcounter--;
                }

                if (parcounter == 0 && expression[i] == operation) 
                {
                    // sağ ve sol childlar oluşur.
                    OPNode OPnod = new OPNode(operation);
                    OPnod.L = NodeOlustur(expression.Substring(0, i));
                    OPnod.R = NodeOlustur(expression.Substring(i + 1));
                    return OPnod;
                }

                if (rightsided)
                {
                    if (i == expression.Length - 1)
                        flag = true; //ifadenin sağında işlem biter
                    i++;//değilse ifadenin sağ tarafını okur.
                }
                else
                {
                    if (i == 0)
                        flag = true; //ifadenin solundaysak okuma döngüsü biter
                    i--; //aksi durumda ifadenin diğer tarafı okunur
                }
            }
            return null;
        }

        public int Hesaplama()
        {
            RootTreeNode = NodeOlustur(expressionString);
            return Hesapla(RootTreeNode);
        }

        public int Hesapla(Node NodeArg)
        {
            ValNode ConstaNode = NodeArg as ValNode;
            if (ConstaNode != null)
            {
                return ConstaNode.Value;
            }

            //sağ ve sol node ları alarak işlem gerçekleştirir
            OPNode OperaNode = NodeArg as OPNode;
            if (OperaNode != null)
            {
                switch (OperaNode.Operand) //operatöre göre yapılacak işlem.
                {
                    case '+':
                        return Hesapla(OperaNode.L) + Hesapla(OperaNode.R);
                    case '-':
                        return Hesapla(OperaNode.L) - Hesapla(OperaNode.R);
                    case '*':
                        return Hesapla(OperaNode.L) * Hesapla(OperaNode.R);
                    case '/':
                        if (Hesapla(OperaNode.R) == 0)
                        {
                            Console.WriteLine("Sıfıra a bölünmez.");
                            return 0;
                        }
                        return Hesapla(OperaNode.L) / Hesapla(OperaNode.R);
                    case '%':
                        return Hesapla(OperaNode.L) % Hesapla(OperaNode.R);

                }
            }
            return 0;
        }
    }


    public class Node
    {
        public Node L;
        public Node R;
    }

    public class OPNode : Node
    {
        public char Operand;

        public OPNode()
        {
        }

        public OPNode(char op)
        {
            Operand = op;
        }
    }

    public class ValNode : Node
    {
        public int Value;

        public ValNode()
        {
        }

        public ValNode(int num)
        {
            Value = num;
        }
    }

}
