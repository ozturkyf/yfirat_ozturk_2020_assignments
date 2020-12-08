using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAndTreeOperations
{
    public class ArithmeticExpressionTree
    {
        public Node RootTreeNode; //root node of expression tree
        public string expressionString;
        //private Dictionary<string, double> _vars;
        public readonly static char[] PossibleOperands = { '+', '-', '*', '/','%' };
        public Dictionary<string, int> DictionaryVariables = new Dictionary<string, int>(); //variable dictionary declartion

        //getter and setter for root node
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

        //getter and setter for expressionstring
        public string ExpressionString
        {
            get
            {
                return expressionString;
            }
            set
            {
                // On set, clear variable dictionary and recompile tree as well as setting the string.
                expressionString = value;
                DictionaryVariables.Clear();
                RootTreeNode = Compile(expressionString);
            }
        }

        //Compile will use the the input expression string to attemp to create an arithmetic expression
        //Returns either null if input empty or a branch node representative of input expression
        public Node Compile(string inputexpression)
        {
            if (string.IsNullOrEmpty(inputexpression))
            {
                return null;
            }


            //Detects left paranthesis, begins looking for right paranthesis
            if (inputexpression[0] == '(')
            {
                // Counter to keep track of paranthesis
                int parcounter = 0;
                for (int i = 0; i < inputexpression.Length; i++)
                {
                    if (inputexpression[i] == '(')
                    {
                        parcounter++; //Increments at left paranthesis
                    }
                    else if (inputexpression[i] == ')')
                    {
                        parcounter--;//Decrements at right paranthesis

                        if (parcounter == 0)//Counter at zero means left and right paranthesis have matched
                        {
                            if (inputexpression.Length - 1 != i)
                            {
                                break; // if we are not at string end, we continue compilation
                            }
                            else
                            {
                                // if we are at end of expression, we compile between the paranthesis
                                return Compile(inputexpression.Substring(1, inputexpression.Length - 2));
                            }
                        }
                    }
                }
            }

            char[] operArr = PossibleOperands;
            foreach (char operand in operArr)
            {
                // Compile the expression based on the current operation.
                // Only return subtree if non-null.
                Node oNode = Compile(inputexpression, operand);
                if (oNode != null)
                    return oNode;
            }

            // is a leaf node; either a ConstantNode or a VariableNode.
            int dubblenum;
            if (int.TryParse(inputexpression, out dubblenum))
            {
                return new ConstantNode(dubblenum);
            }
            else
            {
                // Initialize the variable in the dictionary when found.
                DictionaryVariables[inputexpression] = 0;
                return new VariableNode(inputexpression);
            }
        }

        //This Compile function serves as the recursive call to compile, we overload it with an operand argument
        //this function will take in an expression and operand to return an a branch node of the expression
        public Node Compile(string expression, char operation)
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
                        parcounter--;//decrements left paranthesis if right sided
                    else
                        parcounter++;//increments left paranthesis if left sided
                }
                else if (expression[i] == ')')
                {
                    if (rightsided)
                        parcounter++;//increments right paranthesis if right sided
                    else
                        parcounter--;//decrements right paranthesis if left sided
                }

                if (parcounter == 0 && expression[i] == operation) // if we are inbetween paranthesis, evaluate expression
                {
                    // Create and return a subtree with the current op (as an OPNode) being the root, and the 
                    // left and right expressions (as their own compiled subtrees) being the Left and Right children.
                    OPNode OPnod = new OPNode(operation);
                    OPnod.L = Compile(expression.Substring(0, i));
                    OPnod.R = Compile(expression.Substring(i + 1));
                    return OPnod;
                }

                if (rightsided)
                {
                    if (i == expression.Length - 1)
                        flag = true; //ends expression reading loop if we have hit the right end of the expression
                    i++;//otherwise keeps reads next part of the expression to the right side
                }
                else
                {
                    if (i == 0)
                        flag = true; //ends expression reading loop if we were at the left end of expression
                    i--; //otherwise reads the next part of the expression on the left
                }
            }
            return null;
        }

        public int Evaluation()
        {
            //RootTreeNode = Compile(expressionString);
            return Evaluate(RootTreeNode);
        }

        //Evaluate function will take an input node argument and evaluate it
        public int Evaluate(Node NodeArg)
        {
            ConstantNode ConstaNode = NodeArg as ConstantNode;
            if (ConstaNode != null)
            {
                return ConstaNode.Value;
            }

            VariableNode VarNode = NodeArg as VariableNode;
            if (VarNode != null)
            {
                return DictionaryVariables[VarNode.Name];
            }
            // If OPNode, recursively evaluate Left and Right subtrees and perform operation on them.
            OPNode OperaNode = NodeArg as OPNode;
            if (OperaNode != null)
            {
                switch (OperaNode.Operand) //determines what to do with expression based on operation read
                {
                    case '+':
                        return Evaluate(OperaNode.L) + Evaluate(OperaNode.R);
                    case '-':
                        return Evaluate(OperaNode.L) - Evaluate(OperaNode.R);
                    case '*':
                        return Evaluate(OperaNode.L) * Evaluate(OperaNode.R);
                    case '/':
                        if (Evaluate(OperaNode.R) == 0)
                        {
                            Console.WriteLine("Sıfıra a bölünmez.");
                            return 0;
                        }
                        return Evaluate(OperaNode.L) / Evaluate(OperaNode.R);
                    case '%':
                        return Evaluate(OperaNode.L) % Evaluate(OperaNode.R);

                }
            }
            return 0;
        }
        //This function is used to set variable values in the expression
        public void SetVariable(Node rootNode, string VariableName, int VarValue)
        {
            DictionaryVariables[VariableName] = VarValue;
        }
        public string[] GetVariables()
        {
            return DictionaryVariables.Keys.ToArray();
        }

    }
    public class Node
    {
        public Node L;
        public Node R;

        public string Evaluate()
        {
            throw new NotImplementedException();
        }
    }

    // Node representation for binary operators within the Expression Tree
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

    // Node representing the variables in the expression 
    public class VariableNode : Node
    {
        public string Name;

        public VariableNode()
        {
        }

        public VariableNode(string name)
        {
            Name = name;
        }
    }

    // Node for the constant numerical values within the expression
    public class ConstantNode : Node
    {
        public int Value;

        public ConstantNode()
        {
        }

        public ConstantNode(int num)
        {
            Value = num;
        }
    }

}
