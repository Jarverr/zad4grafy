using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lab4v3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Podaj tekst:");
            //var text = "";//Console.ReadLine();
            //using StreamReader sr = new StreamReader("text.txt");
            //args[0] = sr.ReadToEnd();
            args[0] = "ababcababcac";
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var trie = new UkkonenTrie<int>(1);
            //var trie2 = new SuffixTrie<int>(1);
            trie.Add(args[0], 1);
            
            Console.WriteLine("tree has been build (in {0})",sw.Elapsed);
           
            string temp = "";
            List<string> myLongestWordEdge = new List<string>();
            //string[] temp = new string[trie._root._edges.Count];
           
            Parallel.ForEach(trie._root._edges,
                (edge) => {
                    Dictionary<char, Edge<int>> keyValuePairConvertedToDictonary = new Dictionary<char, Edge<int>> { { edge.Key, edge.Value } };
                    string myLongestWord = "";
                    TraversePostOrder(keyValuePairConvertedToDictonary, ref myLongestWord, ref temp, args[0]);
                    myLongestWordEdge.Add(myLongestWord);
                }
                );
            temp = string.Empty;
            for (int i = 0; i < myLongestWordEdge.Count; i++)
            {
                if (temp.Length<myLongestWordEdge[i].Length)
                {
                    temp = myLongestWordEdge[i];
                }
            }
            //TraversePostOrder( trie._root._edges, ref myLongestWord, ref temp,args[0]);
            sw.Stop(); 
            Console.WriteLine(temp.Length*2+$"\nTime: {sw.Elapsed}");


            //trie2.Add(text,1);
        }
        public static void TraversePostOrder(IDictionary<char, Edge<int>> edges, ref string theLongestRepreatedPrefix, ref string currentWord, string text)
        {
            if (edges.Count != 0)
            {

                if (theLongestRepreatedPrefix.Length < currentWord.Length)
                {
                    theLongestRepreatedPrefix = checkIsThisStringOkay(  theLongestRepreatedPrefix,  currentWord,  text);
                    

                }
                foreach (var item in edges)
                {
                    currentWord += item.Value.Label;
                    TraversePostOrder(item.Value.Target._edges, ref theLongestRepreatedPrefix, ref currentWord,text);
                    currentWord = currentWord.Substring(0, (currentWord.Length - item.Value.Label.Length));
                }
            }
        }

        private static string checkIsThisStringOkay( string theLongestRepreatedPrefix,  string currentWord, string text)
        {
            string temp = text.Substring(0, text.IndexOf(currentWord)) + text.Substring((text.IndexOf(currentWord) + currentWord.Length));
            if (temp.Contains(currentWord))
            {
                return currentWord;   
            }
            else
            {
                currentWord = currentWord.Substring(0, currentWord.Length - 1);
                checkIsThisStringOkay( theLongestRepreatedPrefix,  currentWord, text);
                return currentWord;
            }
        }
    }
}
