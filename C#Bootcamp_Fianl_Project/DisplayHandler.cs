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

        public void Help()
        {
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("                    Welcome to My Basic File Searcher");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("This app contains three main functionalities:");
            Console.WriteLine();
            Console.WriteLine("1. **Search for Files:**");
            Console.WriteLine("   To search for files, use the following format:");
            Console.WriteLine();
            Console.WriteLine("   ```");
            Console.WriteLine("   -t [txt, json, etc] -p \"C:\\Some\\Path\" -q someQuery -c [true|false]");
            Console.WriteLine("   ```");
            Console.WriteLine();
            Console.WriteLine("   - `-t` stands for \"type\" of files to be searched. You can specify one or more types, separated by at least one comma. Examples: `txt`, `txt,json`, `txt, json, xml`.");
            Console.WriteLine("   - `-p` stands for \"path\" of the root directory to start the search. The provided path must be enclosed in double quotes. Example: `-p \"C:\\Some\\Path\"`.");
            Console.WriteLine("   - `-q` stands for \"query\" to be searched. The usage of this parameter depends on the next flag (`-c`):");
            Console.WriteLine("     - If `-c` is set to \"false\", it searches the query in file names.");
            Console.WriteLine("     - If `-c` is set to \"true\", it attempts to find the query in the contents of the files. The query can contain characters like \"*\" as a wildcard for matching files.");
            Console.WriteLine("   - `-c` stands for \"content\", a boolean flag that determines the search approach:");
            Console.WriteLine("     - If set to \"true\", it searches in the contents of the files.");
            Console.WriteLine("     - If set to \"false\", it only searches in the names of the files.");
            Console.WriteLine();
            Console.WriteLine("2. **Manage extensions**");
            Console.WriteLine("   This section is very simple, and you can enter integer inputs to load or unload an extension. If any error occurs, some message will be returned.");
            Console.WriteLine();
            Console.WriteLine("3. **View search history**");
            Console.WriteLine("   This section simply print outs all the query response pairs given until now.");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("                  Made by: Sepehr Noey");
            Console.WriteLine("-------------------------------------------------------");
        }

        public void Menu() { 
            Console.WriteLine("1. Search for files\n2. Manage extensions\n3. View search history\n4. Help\n5. Exit");

        }

        public void InvalidInput()
        {
            Console.WriteLine("Invalid input, try again");
        }

        public void SearchPrompt()
        {
            Console.WriteLine("Enter your query like this: -t txt,json -p \"C:\\Some\\Path\" -q name: Sepehr -c true");

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
            Console.WriteLine("Enter number:\n1. Load extension\n2. Unload extension\n3. Show previous loading errors");
        }

        public void NoMatchingExtFound()
        {
            Console.WriteLine("No matching extension found");
        }


    }
}
