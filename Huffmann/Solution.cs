using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffmann
{
    public class Decoding
    {
        public void Decode(String s, Node root)
        {
            Node p = root;
            for ( int i =0; i<s.Length; i++)
            {
                if (s[i] == '0')
                    p = p.left;
                else
                    p = p.right;

                if (p.data != (char)0)
                {
                    Console.Write(p.data);
                    p = root;
                }
            }
        }
    }
    public class Node : IComparable
    {
        public int frequency; // the frequency of this tree
        public char data;
        public Node left, right;
        public Node(int freq)
        {
            frequency = freq;
        }
        public int CompareTo(Object node)
        {
            if (node == null)
                return -1;

            Node node1 = (Node)node;

            return frequency - node1.frequency;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class HuffmanLeaf : Node
    {
        public HuffmanLeaf(int freq, char val) : base ( freq )
        {
            data = val;
        }
    }

    public class HuffmanNode : Node
    { 
        public HuffmanNode(Node l, Node r) : base (l.frequency + r.frequency)
        {
            left = l;
            right = r;
        }
    }

    public class Solution
    {
        //  input is an array of frequencies, indexed by character code
        public static Node BuildTree(int[] charFreqs)
        {
            Minheap<Node> trees = new Minheap<Node>(256);
            //  initially, we have a forest of leaves
            //  one for each non-empty character
            for (int i = 0; i < charFreqs.Count(); i++)
            {
                if (charFreqs[i] > 0)
                {
                    trees.Insert(new HuffmanLeaf(charFreqs[i], (char)i));
                }

            }

            //  loop until there is only one tree left
            while ((trees.Count() > 1))
            {
                //  two trees with least frequency
                Node a = trees.Remove();
                Node b = trees.Remove();
                //  put into new node and re-insert into queue
                trees.Insert(new HuffmanNode(a, b));
            }

            return trees.Remove();
        }

        public static Dictionary<char, String> mapA = new Dictionary<char, string>();

        public static void printCodes(Node tree, StringBuilder prefix)
        {
            if ((tree is HuffmanLeaf))
            {
                HuffmanLeaf leaf = ((HuffmanLeaf)(tree));
                //  print out character, frequency, and code for this leaf (which is just the prefix)
                // System.out.println(leaf.data + "\t" + leaf.frequency + "\t" + prefix);
                mapA.Add(leaf.data, prefix.ToString());
            }
            else if ((tree is HuffmanNode))
            {
                HuffmanNode node = ((HuffmanNode)(tree));
                //  traverse left
                prefix.Append('0');
                Solution.printCodes(node.left, prefix);
                prefix.Remove(prefix.Length - 1,1);
                //  traverse right
                prefix.Append('1');
                Solution.printCodes(node.right, prefix);
                prefix.Remove(prefix.Length - 1,1);
            }

        }


        public static void TestHuffmanCode(String test)
        {

            //  we will assume that all our characters will have
            //  code less than 256, for simplicity
            int[] charFreqs = new int[256];
            //  read each character and record the frequencies
            foreach (char c in test.ToCharArray())
            {
                charFreqs[c]++;
            }

            //  build tree
            Node tree = Solution.BuildTree(charFreqs);

            //  print out results
            StringBuilder sb = new StringBuilder();
            Solution.printCodes(tree, sb);

            StringBuilder s = new StringBuilder();

            for (int i = 0; i < test.Length; i++)
            {
                char c = test[i];
                s.Append(mapA[c]);
            }

            // System.out.println(s);
            Decoding d = new Decoding();
            d.Decode(s.ToString(), tree);
        }
    }
}
