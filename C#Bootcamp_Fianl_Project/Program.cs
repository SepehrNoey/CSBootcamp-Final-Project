using SearchInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            var handler = new SearchExtensionHandler(pluginFolder);
            var defaultSearch = handler.Load("TextSearch.dll");
            typesToEngines[defaultSearch.Type] = defaultSearch;

            int input; 
            while (true)
            {
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }catch(Exception ex)
                {
                    Console.WriteLine("Invalid input, try again");
                    continue;
                }
                
                switch (input)
                {
                    case 1:
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
                        Console.WriteLine("Found results are:");
                        printResults(results);

                        break;

                    case 2:
                        Console.WriteLine("Enter number:\n1. Load extension\n2. Delete extension");
                        int command = int.Parse(Console.ReadLine().Trim());
                        int extIndex;
                        if (command == 1)
                        {
                            var foundExts = handler.FindMatchingExts();
                            if (foundExts.Count == 0)
                            {
                                Console.WriteLine("No matching extension found");
                                continue;
                            }
                            Console.WriteLine("Found extensions are:");
                            printResults(foundExts);

                            Console.WriteLine("Enter the number of extension:");
                            extIndex = int.Parse(Console.ReadLine().Trim()) - 1;

                            if (extIndex >= foundExts.Count || extIndex < 0)
                            {
                                Console.WriteLine("Given number is invalid");
                                continue;
                            }
                            var searcher = handler.Load(foundExts[extIndex].Name);
                            typesToEngines[searcher.Type] = searcher;
                            Console.WriteLine($"Loaded extension {foundExts[extIndex].Name}");

                        }
                        else if (command == 2)
                        {
                            var existingExts = typesToEngines.Keys.ToList();
                            if (existingExts.Count == 0)
                            {
                                Console.WriteLine("No currently loaded extension found");
                                continue;
                            }

                            Console.WriteLine("Currently loaded extensions are:");
                            for (int i = 0; i < existingExts.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {existingExts[i]}");
                            }
                            
                            Console.WriteLine("Enter the number of extension:");
                            extIndex = int.Parse((Console.ReadLine().Trim()).Trim()) - 1;

                            if (extIndex >= existingExts.Count || extIndex < 0)
                            {
                                Console.WriteLine("Given number is invalid");
                                continue;
                            }
                            typesToEngines.Remove(existingExts[extIndex]);
                            Console.WriteLine("Extension removed.");
                        }
                        else
                        {
                            Console.WriteLine("Command not supported");
                            continue;
                        }
                        

                        break;
                    case 3:
                        throw new NotImplementedException();
                        
                        break;
                }
                
            }

        }

        private static void printResults(List<SearchResult> results)
        {
            for (int i = 0; i < results.Count; i++)
            {
                Console.WriteLine($"{i + 1}.\n{results[i]}\n");
            }
        } 
    }
}
