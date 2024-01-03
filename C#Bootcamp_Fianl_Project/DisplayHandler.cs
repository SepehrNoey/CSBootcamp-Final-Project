using SearchInterface;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace C_Bootcamp_Fianl_Project
{
    internal class DisplayHandler
    {

        public void InitPrint()
        {
            Console.WriteLine("Author: Sepehr Noey");
            Console.WriteLine("Welcome to my implementation of a basic file search.\n\n");
        }

        public void Menu() { 
            Console.WriteLine("1. Search for files\n2. Manage extensions\n3. View search history\n4. Exit");

        }

        public void InvalidInput()
        {
            Console.WriteLine("Invalid input, try again");
        }

        public void SearchPrompt()
        {
            Console.WriteLine("Enter your query like this: -t txt,json -p \"C:\\Some\\Path\" -q *bootcamp.txt");

        }

        public void Print(string msg)
        {
            Console.WriteLine(msg);
        }

        public void PrintList(List<SearchResult> results)
        {
            for (int i = 0; i < results.Count; i++)
            {
                Console.WriteLine($"{i + 1}.\n{results[i]}\n");
            }
        }

        public void PrintHistory(OrderedDictionary histDict)
        {
            var count = 0;
            foreach (object key in histDict.Keys)
            {
                count++;
                Console.WriteLine($"{count}. {key}");
                List<SearchResult> values = (List<SearchResult>)histDict[key];
                values.ForEach(item => Console.WriteLine($"{item}\n"));
                Console.WriteLine("**** **** ****\n");
            }
        }

        public void InvalidPath(string path)
        {
            Console.WriteLine($"Path {path} doesn't exist");
        }

        public void FileTypeNotSupported(string type)
        {
            Console.WriteLine($"File type {type} not supported");
        }

        public void MngExtPrompt()
        {
            Console.WriteLine("Enter number:\n1. Load extension\n2. Delete extension");
        }

        public void NoMatchingExtFound()
        {
            Console.WriteLine("No matching extension found");
        }


    }
}
