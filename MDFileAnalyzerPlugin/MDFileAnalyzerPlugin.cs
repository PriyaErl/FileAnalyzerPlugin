using System.Collections.Generic;
using FileDependencyVisApp.Contract;
using System.Windows.Forms;
using System.IO;

namespace MDFileAnalyzerPlugin
{
    public class MDFileAnalyzerPlugin : IPluginBase
    {
        internal MDFileVisualizer myVisualizer { get; private set; }
        internal MDFileGraphView myGraphView { get; private set; }
        internal Panel myHostPanel ;

        public string PluginName
        {
            get { return "MDFileAnalyzerPlugin"; }
        }

        public string PluginDescription
        {
            get { return "This plugin is for analyzing MD files and displaying their dependency"; }
        }

        //Host plugin in host panel area
        public void Execute(object hostView)
        {
            myHostPanel = (Panel)hostView;            
            Init(myHostPanel);      
        }       

        //Initialize UI components and Load it in hostpanel
        private void Init(Panel panel)
        {
            //Create and show Plugin View 
            myGraphView = new MDFileGraphView(panel);

            //Subscribe GraphView UI Control Events with Event Handlers
            myGraphView.btnLoadFiles.MouseClick += btnLoadFiles_MouseClick;
            myGraphView.btnShowReferences.MouseClick += btnShowReferences_MouseClick;
            myGraphView.btnShowLinks.MouseClick += BtnShowLinks_MouseClick;
        }
        //Read Md files from selected folder and load them in GraphView
        private void btnLoadFiles_MouseClick(object sender, MouseEventArgs e)
        {
            //Show open file dialog to select MD files folder
            List<FileInfo> files = BrowseFiles();
            if (files == null) return;

            myVisualizer = new MDFileVisualizer(files, myGraphView.pictureBox);
            myVisualizer.CreateFilesGraph();           
            myVisualizer.Sort();           
        }

        private void btnShowReferences_MouseClick(object sender, MouseEventArgs e)
        {
            if (myVisualizer != null) 
            {
                myVisualizer.ShowReferences();
            }          

        }

        private void BtnShowLinks_MouseClick(object sender, MouseEventArgs e)
        {
            if (myVisualizer != null)
            {
                myVisualizer.ShowLinks();
            }
                
        }

        private List<FileInfo> BrowseFiles()
        {
           var files = new List<FileInfo>();
           var ofd = new FolderBrowserDialog();
             if (ofd.ShowDialog() == DialogResult.OK)
             {
                 var dir = new DirectoryInfo(ofd.SelectedPath);
                if (dir.Exists)
                {
                    foreach (var file in dir.GetFiles("*.md", SearchOption.AllDirectories))
                    {
                        files.Add(file);
                    }
                }

                return files;
             }
            //return files;
            return null;
        }      
    }
}


