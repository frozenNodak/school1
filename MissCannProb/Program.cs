using System;
using System.Collections.Generic;
//using System.Collections.Generic;
//using System.Text;

namespace MissCannProb
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Missionary and Cannibal Problem.\nSelect search mode \n\t0)Depth First \n\t1)Width First \n\t2)Interactive Deepening \n\t3)Exit");
            int input = 0;
            while (input != -1)
            {
                try
                {
                    input = System.Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid Entry. Please choose search mode. \n\t0)Depth First \n\t1)Width First \n\t2)Interactive Deepening \n\t3)Exit ");
                }
            }
            Program prog = new Program();
            if(input == 0)
            {
                prog.DFS(new Node(new Vector4(4, 4, 1)));
            }
            if(input == 1)
            {

            }
            if(input == 2)
            {

            }
            if (input == 3)
            {
                System.Environment.Exit(1);
            }
        }

        List<Vector4> History = new List<Vector4>();//Keep track of reocurring nodes
        Vector4[] Operators = new Vector4[]//possible moves
        {
        new Vector4(1,0,1),
        new Vector4(2,0,1),
        new Vector4(3,0,1),
        new Vector4(0,1,1),
        new Vector4(0,2,1),
        new Vector4(0,3,1),
        new Vector4(1,1,1),
        new Vector4(2,1,1),
        new Vector4(1,2,1)
        };

        public bool TryMove(Vector4 current, Vector4 toApply, bool substract)
        {
            if (substract)
            {
                if (current.c - toApply.c < 0 || current.m - toApply.m < 0 || current.b - toApply.b < 0 || (current.c - toApply.c) > (current.m - toApply.m))
                {
                    return false;
                }
                else return true;
            }
            else
            {
                if (current.c + toApply.c > 4 || current.m + toApply.m > 4 || current.b + toApply.b > 1 || (current.c + toApply.c) > (current.m + toApply.m))
                {
                    return false;
                }
                else return true;
            }
        }
        public void DFS(Node n)
        {
            Stack<Node> stack = new Stack<Node>();
            stack.Push(n);
            while (stack.Count > 0)
            {

                Node curNode = stack.Pop();
                if (History.Contains(curNode.State))
                {

                }
                else
                {
                    History.Add(curNode.State);
                    if (curNode.State.Equals(new Vector4(0, 0, 0)))
                    {
                        Console.WriteLine("Solution found.");
                        
                        return;
                    }
                    else
                    {
                        if (curNode.State.b == 0) //Boat is across the river
                        {
                            for (int x = 0; x < 5; x++)
                            {
                                if (TryMove(curNode.State, Operators[x], false))
                                {
                                    stack.Push(new Node(new Vector4(curNode.State.m + Operators[x].m, curNode.State.c + Operators[x].c, curNode.State.b + Operators[x].b)));
                                }
                            }
                        }
                        else //Boat == 1
                        {
                            for (int x = 0; x < 5; x++)
                            {
                                if (TryMove(curNode.State, Operators[x], true))
                                {
                                    stack.Push(new Node(new Vector4(curNode.State.m - Operators[x].m, curNode.State.c - Operators[x].c, curNode.State.b - Operators[x].b)));
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("No solution found.");
            return;
        }
    }
    public class Node
    {
        public Vector4 State;
        public Node(Vector4 st)
        {
            State = st;
        }
    }
    public class Vector4
    {
        public int m;
        public int c;
        public int b;
        public Vector4(int M, int C, int B)
        {
            m = M;
            c = C;
            b = B;
        }
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;

            Vector4 p = obj as Vector4;
            if ((System.Object)p == null)
                return false;

            return (m == p.m) && (c == p.c) && (b == p.b);
        }
    }
}
