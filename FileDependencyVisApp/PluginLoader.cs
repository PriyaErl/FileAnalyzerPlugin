using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using FileDependencyVisApp.Contract;
using System.IO;


namespace FileDependencyVisApp
{
    internal class PluginLoader
    {
        //The folder name which contains the plugin DLLs
        public const string FolderName = "C:\\PluginsFolder";
        
        public static List<IPluginBase> Plugins { get; set; }

       //Load all plugins from the specified directory
        public void LoadPlugins()
        {
            Plugins = new List<IPluginBase>();

            //Load the DLLs from the Plugins directory
            if (Directory.Exists(FolderName))
            {
                string[] files = Directory.GetFiles(FolderName);
                foreach (string file in files)
                {
                    if (file.EndsWith(".dll"))
                    {
                        Assembly.LoadFile(Path.GetFullPath(file));
                    }
                }
            }

            Type interfaceType = typeof(IPluginBase);            
            Type[] types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
            .ToArray();
            foreach (Type type in types)
            {
                //Create a new instance of all found types
                Plugins.Add((IPluginBase)Activator.CreateInstance(type));
            }
        }
    }

}

