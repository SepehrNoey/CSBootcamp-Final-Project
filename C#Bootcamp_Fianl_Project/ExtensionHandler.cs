using SearchInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace C_Bootcamp_Fianl_Project
{
    internal class ExtensionHandler
    {
        public string PluginFolder { get; set; }

        public ExtensionHandler(string pluginFolder) { 
            this.PluginFolder = pluginFolder;
        }

        public ISearch LoadSearchExt(string name)
        {
            var files = Directory.GetFiles(PluginFolder, name);
            if (files.Length == 0)
                throw new Exception("Extension not found");
            else if (files.Length > 1)
                throw new Exception("More than one extension with given name found");

            var asm = Assembly.LoadFrom(files[0]);
            Console.WriteLine($"Loading extension {asm.GetName()}");

            var types = asm.GetTypes().Where(t => typeof(ISearch).IsAssignableFrom(t)).ToList();
            if (types.Count == 0)
                throw new Exception("No assignable extension found");

            types = asm.GetTypes().Where(t => typeof(ISearch).IsAssignableFrom(t) &&
            Attribute.IsDefined(t, typeof(CSBootcampAttribute))).ToList();
            if (types.Count == 0)
                throw new Exception($"No extension with attribute {typeof(CSBootcampAttribute).Name} found");
            return (ISearch)Activator.CreateInstance(types[0]);
            
        }

        public List<SearchResult> FindAllExts()
        {
            var files = Directory.GetFiles(PluginFolder, "*.dll", SearchOption.AllDirectories);
            var results = new List<SearchResult>();
            foreach (var file in files)
            {
                var asm = Assembly.LoadFrom(file);
                var types = asm.GetTypes().ToList();
                if (types.Count > 0)
                {
                    results.Add(new SearchResult(Path.GetFileName(file), file));
                }
            }
            return results;
        }

    }
}
