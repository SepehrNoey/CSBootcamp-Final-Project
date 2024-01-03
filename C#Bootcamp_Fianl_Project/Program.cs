using SearchInterface;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace C_Bootcamp_Fianl_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dispHandler = new DisplayHandler();
            dispHandler.InitPrint();
            
            var pluginFolder = @"C:\Users\Lenovo\source\repos\C#Bootcamp_Fianl_Project\C#Bootcamp_Fianl_Project\bin\Debug\plugins";
            var typesToEngines = new Dictionary<string, ISearch>();
            var extHandler = new SearchExtensionHandler(pluginFolder);
            var defaultSearch = extHandler.Load("TextSearch.dll");
            typesToEngines[defaultSearch.Type] = defaultSearch;
            var history = new OrderedDictionary();

            var inpHandler = new InputHandler();

            int input; 
            while (true)
            {
                dispHandler.Menu();
                try
                {
                    input = inpHandler.ReadInt();
                }catch (Exception)
                {
                    dispHandler.InvalidInput();
                    continue;
                }
                
                switch (input)
                {
                    case 1:

                        dispHandler.SearchPrompt();
                        Dictionary<string, string> flagsValue;
                        try
                        {
                            flagsValue = inpHandler.ParseSearchInput(Console.ReadLine().Trim());
                        }catch(Exception e)
                        {
                            dispHandler.Print(e.Message);
                            continue;
                        }
                        
                        var typesStr = flagsValue["t"]; // types
                        var types = inpHandler.SplitTypes(typesStr);
                        var allSupported = true;
                        foreach (var type in types)
                        {
                            if (!typesToEngines.ContainsKey(type))
                            {
                                dispHandler.FileTypeNotSupported(type);
                                allSupported = false;
                                break;
                            }
                        }
                        if (!allSupported)
                            continue;
                        
                        var path = flagsValue["p"]; // path

                        if (!Directory.Exists(path))
                        {
                            dispHandler.InvalidPath(path);
                            continue;
                        }

                        var query = flagsValue["q"]; // query

                        var results = new List<SearchResult>();
                        foreach (var type in types)
                        {
                            results.AddRange(typesToEngines[type].Search(query, path, type));
                        }
                        addToHistory(history, typesStr, path, query, results);

                        if (results.Count == 0)
                        {
                            dispHandler.Print("No results found");
                            continue;
                        }

                        dispHandler.Print("Found results are:");
                        dispHandler.PrintList(results);
                        break;

                    case 2:
                        dispHandler.MngExtPrompt();
                        int command;
                        try
                        {
                            command = inpHandler.ReadInt();
                        }catch(Exception)
                        {
                            dispHandler.InvalidInput();
                            continue;
                        }

                        int extIndex;
                        if (command == 1)
                        {
                            var foundExts = extHandler.FindMatchingExts();
                            if (foundExts.Count == 0)
                            {
                                dispHandler.NoMatchingExtFound();
                                continue;
                            }
                            dispHandler.Print("Found extensions are:");
                            dispHandler.PrintList(foundExts);
                            dispHandler.Print("Enter the number of extension:");
                            try {
                                extIndex = inpHandler.ReadInt() - 1;
                            }catch(Exception)
                            {
                                dispHandler.InvalidInput();
                                continue;
                            }

                            if (extIndex >= foundExts.Count || extIndex < 0)
                            {
                                dispHandler.InvalidInput();
                                continue;
                            }
                            var searcher = extHandler.Load(foundExts[extIndex].Name);
                            typesToEngines[searcher.Type] = searcher;
                            dispHandler.Print($"Loaded extension {foundExts[extIndex].Name}");

                        }
                        else if (command == 2)
                        {
                            var existingExts = typesToEngines.Keys.ToList();
                            if (existingExts.Count == 0)
                            {
                                dispHandler.Print("No currently loaded extension found");
                                continue;
                            }

                            dispHandler.Print("Currently loaded extensions are:");
                            for (int i = 0; i < existingExts.Count; i++)
                            {
                                dispHandler.Print($"{i + 1}. {existingExts[i]}");
                            }
                            
                            dispHandler.Print("Enter the number of extension:");
                            try
                            {
                                extIndex = inpHandler.ReadInt() - 1;
                            }catch(Exception)
                            {
                                dispHandler.InvalidInput();
                                continue;
                            }

                            if (extIndex >= existingExts.Count || extIndex < 0)
                            {
                                dispHandler.InvalidInput();
                                continue;
                            }
                            typesToEngines.Remove(existingExts[extIndex]);
                            dispHandler.Print("Extension removed.");
                        }
                        else
                        {
                            dispHandler.Print("Command not supported");
                            continue;
                        }
                        

                        break;
                    case 3:
                        dispHandler.PrintHistory(history);
                        break;

                    case 4:
                        dispHandler.Print("Exiting...");
                        return;

                    default:
                        dispHandler.InvalidInput();
                        continue;
                }
                
            }

        }


        private static void addToHistory(OrderedDictionary dict, string type,string path, string query, List<SearchResult> results)
        {
            
            var key = $"Search type: {type}\tPath: {path}\tQuery: {query}";
            dict.Add(key, results);
        }


    }
}
