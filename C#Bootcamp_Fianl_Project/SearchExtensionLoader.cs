using SearchInterface;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace C_Bootcamp_Fianl_Project
{
    internal class SearchExtensionLoader
    {
        public string PluginFolder { get; set; }

        public SearchExtensionLoader(string pluginFolder) { 
            this.PluginFolder = pluginFolder;
        }

        public ISearch Load(string name)
        {
            var files = Directory.GetFiles(PluginFolder, name);
            if (files.Length == 0)
                throw new Exception("Extension not found");
            else if (files.Length > 1)
                throw new Exception("More than one extension with given name found");

            var asm = Assembly.LoadFrom(files[0]);
            Console.WriteLine($"Loading extension {asm.GetName()}");

            var types = asm.GetTypes().Where(t => typeof(ISearch).IsAssignableFrom(t)).ToList();
            return (ISearch)Activator.CreateInstance(types[0]);
            
        }
    }
}
