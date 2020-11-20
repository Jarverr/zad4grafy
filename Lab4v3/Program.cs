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
            //Console.WriteLine(args[0].Length);
            args[0] = "ababcababcac";
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var trie = new UkkonenTrie<int>(1);
            //var trie2 = new SuffixTrie<int>(1);
            trie.Add(args[0], 1);

            Console.WriteLine("tree has been build (in {0})", sw.Elapsed);

            string temp = "";
            List<string> myLongestWordEdge = new List<string>();
            //string[] temp = new string[trie._root._edges.Count];
            //try
            //{
            //Parallel.ForEach(trie._root._edges,
            //(edge) =>
            //{
            //    Dictionary<char, Edge<int>> keyValuePairConvertedToDictonary = new Dictionary<char, Edge<int>> { { edge.Key, edge.Value } };
            //    string myLongestWord = "";
            //    TraversePostOrder(keyValuePairConvertedToDictonary, ref myLongestWord, ref temp, args[0]);
            //    myLongestWordEdge.Add(myLongestWord);
            //}
            //);
            //}
            //catch (AggregateException ae)
            //{
            //    var ignoredExceptions = new List<Exception>();
            //    // This is where you can choose which exceptions to handle.
            //    foreach (var ex in ae.Flatten().InnerExceptions)
            //    {
            //        if (ex is ArgumentException)
            //            Console.WriteLine(ex.Message);
            //        else
            //            ignoredExceptions.Add(ex);
            //    }
            //    if (ignoredExceptions.Count > 0) throw new AggregateException(ignoredExceptions);
            //}

            temp = string.Empty;
            for (int i = 0; i < myLongestWordEdge.Count; i++)
            {
                if (temp.Length < myLongestWordEdge[i].Length)
                {
                    temp = myLongestWordEdge[i];
                }
            }
            string myLongestWord = "";
            foreach (var item in trie._root._edges)
            {
                temp = item.Value.Label;
                TraversePostOrder(item, ref myLongestWord, ref temp, args[0]);
                break;

            }
            sw.Stop();
            Console.WriteLine("String: " + myLongestWord + " ; " + myLongestWord.Length * 2 + $"\nTime: {sw.Elapsed}");


            //trie2.Add(text,1);
        }
        static string toRewrite = "";
        static bool isOkay = false;
        public static void TraversePostOrder(KeyValuePair<char, Edge<int>> edges, ref string theLongestRepreatedPrefix, ref string currentWord, string text)
        {
            if (edges.Value.Target._edges.Count != 0)
            {
                if (theLongestRepreatedPrefix.Length < currentWord.Length)
                {
                    (isOkay, toRewrite) = getTheLongestRepeatedString(theLongestRepreatedPrefix, currentWord, text);
                    if (isOkay)
                    {
                        theLongestRepreatedPrefix = toRewrite;
                    }

                }
                foreach (var item in edges.Value.Target._edges)
                {
                    currentWord += item.Value.Label;
                    TraversePostOrder(item, ref theLongestRepreatedPrefix, ref currentWord, text);
                    currentWord = currentWord.Substring(0, (currentWord.Length - item.Value.Label.Length));
                    break;
                }
            }
        }

        private static (bool, string) getTheLongestRepeatedString(string theLongestRepreatedPrefix, string currentWord, string text)
        {
            string temp = "";
            if (text.IndexOf(currentWord) > -1)
            {
                temp = text.Substring(0, text.IndexOf(currentWord)) + text.Substring((text.IndexOf(currentWord) + currentWord.Length));
            }
            else
            {
                return (false, currentWord);
            }
            if (temp.Contains(currentWord) && text.Substring((text.IndexOf(currentWord) + currentWord.Length), currentWord.Length) == currentWord)
            {
                if (currentWord.Length > 0)
                    return (true, currentWord);
                else
                    return (false, currentWord);
            }
            else
            {
                if (currentWord.Length > 0)
                {
                    currentWord = currentWord.Substring(0, currentWord.Length - 1);
                    (isOkay, currentWord) = getTheLongestRepeatedString(theLongestRepreatedPrefix, currentWord, text);
                }

                return (isOkay, currentWord);
            }
        }
    }
}
