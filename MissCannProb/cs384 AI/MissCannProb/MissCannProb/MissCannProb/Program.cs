using System;
using System.Collections.Generic;
using System.IO;
//using System.Collections.Generic;
//using System.Text;

namespace MissCannProb
{
    /* 4 missionaries and 4 cannibals are on the same side of a river with one boat with a max capaxity of 3. 
     * they need to get everyone to the other side of the river. The cannibals cannot outnumber the missionaries
     * at any point or they will get eaten. There has to be a person in the boat each time it crosses the river. 
     * 
     * HW1 cs 384
     * David Erickson
     * Spring 2018
     */
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Missionary and Cannibal Problem.\nSelect search mode \n\t0)Depth First \n\t1)Width First \n\t2)Exit");
            var key = Console.ReadKey();
            bool valid = false;
            int i_key = 2;
            i_key = System.Convert.ToInt32(key.KeyChar.ToString());
            if (i_key < 0 || i_key > 3)
            {
                System.Environment.Exit(1);
            }


            Program prog = new Program();
            if (i_key == 0)
            {
                prog.DFS(new Node(new Operators(4, 4, 1)));
            }
            if (i_key == 1)
            {
                prog.BFS(new Node(new Operators(4, 4, 1)));
            }
            if (i_key == 2)
            {
                System.Environment.Exit(1);
            }
        }

        List<Operators> History = new List<Operators>();//Keep track of reocurring nodes
        Operators[] Operators = new Operators[]//possible moves
        {
            //(lM, lC, B)
        new Operators(1,0,1),
        new Operators(2,0,1),
        new Operators(3,0,1),
        new Operators(0,1,1),
        new Operators(0,2,1),
        new Operators(0,3,1),
        new Operators(1,1,1),
        new Operators(2,1,1),
        new Operators(1,2,1)
        };

        public bool TryMove(Operators current, Operators toApply, bool substract)
        {
            if (substract)
            {
                //check for enough C, M, that C !> M, and boat is on the correct side
                if (current.c - toApply.c < 0 || current.m - toApply.m < 0 || current.b - toApply.b < 0 || (current.c - toApply.c) > (current.m - toApply.m))// || (4 - current.m) < (4-current.c))
                {
                    return false;
                }
                else return true;
            }
            else
            {
                //check for enough C, M, that C !> M, and boat is on the correct side
                if (current.c + toApply.c > 4 || current.m + toApply.m > 4 || current.b + toApply.b > 1 || (current.c + toApply.c) > (current.m + toApply.m))// || (4 - current.m) < (4 - current.c))
                {
                    return false;
                }
                else return true;
            }
        }
        public void DFS(Node n)
        {
            Stack<Node> stack = new Stack<Node>();
            List<Node> validPath = new List<Node>();
            int pathCost = 0;
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
                    if (curNode.State.Equals(new Operators(0, 0, 0)))//meets goal state
                    {
                        Console.WriteLine("/nOptimal Path");
                        foreach(var path in History)
                        {
                            Console.Write("<" + path.m.ToString() + " " + path.c.ToString() + " " + path.b.ToString() + "> ");
                        }
                        Console.WriteLine("\nOptimal Path Cost: " + History.Count);
                        Console.WriteLine("Explored Nodes");
                        foreach(var visited in stack)
                        {
                            Console.Write("<" + visited.State.m.ToString() + " " + visited.State.c.ToString() + " " + visited.State.b.ToString() + "> ");
                        }
                        Console.WriteLine("\nNumber of Explored Nodes " + stack.Count);
                        Console.WriteLine("Solution found.");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        if (curNode.State.b == 0) //Boat is across the river
                        {
                            for (int x = 0; x < 9; x++)
                            {
                                if (TryMove(curNode.State, Operators[x], false))
                                {
                                    stack.Push(new Node(new Operators(curNode.State.m + Operators[x].m, curNode.State.c + Operators[x].c, curNode.State.b + Operators[x].b)));
                                    validPath.Add(new Node(new Operators(curNode.State.m + Operators[x].m, curNode.State.c + Operators[x].c, curNode.State.b + Operators[x].b)));
                                }
                            }
                            pathCost++;
                        }
                        else //Boat == 1
                        {
                            for (int x = 0; x < 9; x++)
                            {
                                if (TryMove(curNode.State, Operators[x], true))
                                {
                                    stack.Push(new Node(new Operators(curNode.State.m - Operators[x].m, curNode.State.c - Operators[x].c, curNode.State.b - Operators[x].b)));
                                    validPath.Add(new Node(new Operators(curNode.State.m - Operators[x].m, curNode.State.c - Operators[x].c, curNode.State.b - Operators[x].b)));
                                    
                                }
                            }
                            pathCost++;
                        }
                    }
                }
            }
            Console.WriteLine("\nNo solution found.");
            Console.ReadKey();
            return;
        }

        public void BFS(Node n)
        {//same as DFS, but using queue instead of stack
            var queue = new Queue<Node>();
            
            queue.Enqueue(n);
            while (queue.Count > 0)
            {

                Node curNode = queue.Dequeue();
                if (History.Contains(curNode.State))
                {

                }
                else
                {
                    History.Add(curNode.State);
                    if (curNode.State.Equals(new Operators(0, 0, 0)))
                    {
                        Console.WriteLine("/nOptimal Path");
                        foreach (var path in History)
                        {
                            Console.Write("<" + path.m.ToString() + " " + path.c.ToString() + " " + path.b.ToString() + "> ");
                        }
                        Console.WriteLine("\nOptimal Path Cost: " + History.Count);
                        Console.WriteLine("Explored Nodes");
                        foreach (var visited in queue)
                        {
                            Console.Write("<" + visited.State.m.ToString() + " " + visited.State.c.ToString() + " " + visited.State.b.ToString() + "> ");
                        }
                        Console.WriteLine("\nNumber of Explored Nodes " + queue.Count);
                        Console.WriteLine("Solution found.");
                        Console.ReadKey();
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
                                    queue.Enqueue(new Node(new Operators(curNode.State.m + Operators[x].m, curNode.State.c + Operators[x].c, curNode.State.b + Operators[x].b)));
                                }
                            }
                        }
                        else //Boat == 1
                        {
                            for (int x = 0; x < 5; x++)
                            {
                                if (TryMove(curNode.State, Operators[x], true))
                                {
                                    queue.Enqueue(new Node(new Operators(curNode.State.m - Operators[x].m, curNode.State.c - Operators[x].c, curNode.State.b - Operators[x].b)));
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
        public Operators State;
        public Node(Operators st)
        {
            State = st;
        }
    }
    public class Operators
    {
        public int m;
        public int c;
        public int b;
        public Operators(int M, int C, int B)
        {
            m = M;
            c = C;
            b = B;
        }
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;

            Operators p = obj as Operators;
            if ((System.Object)p == null)
                return false;

            return (m == p.m) && (c == p.c) && (b == p.b);
        }
    }
    //@~David Erickson
}