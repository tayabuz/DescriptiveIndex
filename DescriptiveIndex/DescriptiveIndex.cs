using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DescriptiveIndex
{
    public class DescriptiveIndex
    {
        private Dictionary<string, int[]> IndexDictionary;
        private const string WRITE_PATH = @"C:\Users\User\Downloads\DescriptiveIndex.txt";
        private static string CurrentPath { get; set; }

        public void AddIndex(string s, int[] arr)
        {
            IndexDictionary.Add(s, arr);
        }

        public void AddIndexByString(string s)
        {
            if (CheckStringForAdd(s))
            {
                var word = s.Substring(0, s.IndexOf(" "));
                s = s.Remove(0, s.IndexOf(" "));
                string[] tokens = s.Split(',');
                int[] convertedItems = Array.ConvertAll(tokens, int.Parse);
                AddIndex(word, convertedItems);
            }
            else { throw new FormatException("Uncorrect string format"); }
        }

        public void DeleteElement(string word)
        {
            IndexDictionary.Remove(word);
        }

        public DescriptiveIndex()
        {
            IndexDictionary = new Dictionary<string, int[]>();
            CurrentPath = SetPathOfFile();
        }

        public void PrintDescriptiveIndex()
        {
            foreach (KeyValuePair<string, int[]> kvp in IndexDictionary)
            {
                Console.WriteLine("Word = {0}, Pages = {1}", kvp.Key, string.Join(",", kvp.Value));
            }
        }

        public void PrintResultForSearch(string word)
        {
            if (IndexDictionary.ContainsKey(word))
            {
                Console.WriteLine(string.Join(" ", IndexDictionary[word]));
            }
            else { Console.WriteLine("Word not found"); }
        }

        private static string SetPathOfFile()
        {
            FileInfo fi = new FileInfo(WRITE_PATH);
            if (!fi.Exists)
            {
                FileStream fs = fi.Create();
                fs.Close();
                return WRITE_PATH;
            }
            else
            {
                int number = 1;
                string path = @"C:\Users\User\Downloads\DescriptiveIndex" + number + ".txt";
                FileInfo fi1 = new FileInfo(path);
                while (fi1.Exists)
                {
                    number++;
                    path = @"C:\Users\User\Downloads\DescriptiveIndex" + number + ".txt";
                    fi1 = new FileInfo(path);
                }
                FileStream fs = fi1.Create();
                fs.Close();
                return path;
            }
        }

        public void PushToFile()
        {
            string data = IndexDictionary.Aggregate(new StringBuilder(),
                (sb, kvp) => sb.AppendFormat(kvp.Key + " " + string.Join(",", kvp.Value) + "\n")).ToString().Trim();
            try
            {
                using (StreamWriter sw = new StreamWriter(CurrentPath, true, Encoding.Default))
                {
                    sw.WriteLine(data);
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                if (e is IOException || e is ObjectDisposedException)
                {
                    Console.WriteLine("Exception in PushToFile " + e.Message);
                }
                throw;
            }
        }

        public void GetDescriptiveIndexByFile(string path)
        {
            if (File.Exists(path))
            {
                string[] data = File.ReadAllLines(path, Encoding.Default);
                foreach (var s in data)
                {
                   if (String.IsNullOrEmpty(s)) { AddIndexByString(s);}
                }
            }
            else
            {
                throw new FileNotFoundException("Invalid path: File not found");
            }
        }

        private bool CheckStringForAdd(string s)
        {
            return Regex.IsMatch(s, @"^[a-zA-Z]+\s[0-9,]+$");
        }
    }
}

