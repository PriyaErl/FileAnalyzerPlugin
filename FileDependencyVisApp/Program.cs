using FileDependencyVisApp.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileDependencyVisApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Loads all plugins
        /// Displays the application UI
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //loads all the plugins from the specified Plugin Directory
            PluginLoader loader = new PluginLoader();
            loader.LoadPlugins();

            //Display Main Application UI
            Application.Run(new MainForm()); 
              
          
        }
    }
}
