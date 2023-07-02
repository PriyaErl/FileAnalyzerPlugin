using FileDependencyVisApp;
using FileDependencyVisApp.Contract;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace FileDependencyVisApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //Loads the specified plugin named MDFileAnalyzerPlugin 
        private void btnMarkdownAnalyzer_Click(object sender, EventArgs e)
        {
            string pluginName = "MDFileAnalyzerPlugin";

            //Trigger execution of MDFileAnalyzer Plugin
            IPluginBase plugin = PluginLoader.Plugins.Where(p => p.PluginName == pluginName).FirstOrDefault();
            if (plugin != null)
            {
                //If the plugin is found, execute it
                plugin.Execute(panelPluginArea);
            }
            
        }

       
    }
}

