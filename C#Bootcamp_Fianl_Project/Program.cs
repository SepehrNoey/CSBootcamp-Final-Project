using SearchInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Bootcamp_Fianl_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Author: Sepehr Noey\n\n");
            Console.WriteLine("Welcome to my implementation of a basic file search.");
            Console.WriteLine("1. Search for files\n2. Manage extensions\n3. View search history");
            var pluginFolder = @"C:\Users\Lenovo\source\repos\C#Bootcamp_Fianl_Project\C#Bootcamp_Fianl_Project\bin\Debug\plugins";
            var typesToEngines = new Dictionary<string, ISearch>();
            var loader = new SearchExtensionLoader(pluginFolder);
            var defaultSearch = loader.Load("TextSearch.dll");
            typesToEngines[defaultSearch.Type] = defaultSearch;

            string input; 
            while (true)
            {
                input = Console.ReadLine().Trim();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Enter type: (like txt, json, ...)");
                        var fileType = Console.ReadLine().Trim().ToUpper();
                        
                        if (!typesToEngines.ContainsKey(fileType))
                        {
                            Console.WriteLine("File type not supported");
                            continue;
                        }

                        Console.WriteLine("Enter the root path:");
                        var path = Console.ReadLine().Trim();

                        if (!Directory.Exists(path))
                        {
                            Console.WriteLine("Path doesn't exist");
                            continue;
                        }

                        Console.WriteLine("Enter query:");
                        var query = Console.ReadLine().Trim();

                        var results = typesToEngines[fileType].Search(query, path, fileType);
                        printResults(results);

                        break;

                    case "2":
                        throw new NotImplementedException();

                        break;
                    case "3":
                        throw new NotImplementedException();
                        
                        break;
                }
                
            }

        }

        private static void printResults(List<SearchResult> results)
        {
            Console.WriteLine("Found results are:");
            foreach (var res in results)
            {
                Console.WriteLine($"{res}\n");
            }
        } 
    }
}
