
using System.Drawing;
using System.Windows.Forms;

namespace MDFileAnalyzerPlugin
{
    class MDFileGraphView : Panel
    {
        public Panel toolPanel;
        public Button btnLoadFiles;
        public Button btnShowReferences;
        public Button btnShowLinks;
        public PictureBox pictureBox;        

        public MDFileGraphView(Panel hostPanel)
        {
            //Intialize all Ui Components
            InitializeComponents();

            //Create plugin Area based on the hosting area
            Init(hostPanel);
        }

        //Load UI Controls in host panel
        private void Init(Panel hostPanel)
        {
            hostPanel.Controls.Clear();

            base.Width = hostPanel.Width;
            base.Height = hostPanel.Height;

            base.Controls.Add(toolPanel);
            base.Controls.Add(pictureBox);                    

            toolPanel.Controls.Add(btnLoadFiles);
            toolPanel.Controls.Add(btnShowReferences);
            toolPanel.Controls.Add(btnShowLinks);
            
            //Adding plugin controls into host panel
            hostPanel.Controls.Add(this);
        }

        //Initialize UI Controls
        private void InitializeComponents()
        {
            var buttonWidth = 140;

            toolPanel = new Panel();
            toolPanel.Width = base.Width;
            toolPanel.Height = 32;
            toolPanel.Top = 0;
            toolPanel.Left = 0;
            toolPanel.Dock = DockStyle.Top;

            btnLoadFiles = new Button();
            btnLoadFiles.Width = buttonWidth;
            btnLoadFiles.Height = 28;
            btnLoadFiles.Left = ((buttonWidth + 5) * 0) + 10;
            btnLoadFiles.Top = 2;
            btnLoadFiles.Text = "Load Files ";
            
            btnShowReferences = new Button();
            btnShowReferences.Width = buttonWidth;
            btnShowReferences.Height = 28;
            btnShowReferences.Left = ((buttonWidth + 5) * 1) + 10;
            btnShowReferences.Top = 2;
            btnShowReferences.Text = "Show References";
            
            btnShowLinks = new Button();
            btnShowLinks.Width = buttonWidth;
            btnShowLinks.Height = 28;
            btnShowLinks.Left = ((buttonWidth + 5) * 2) + 10;
            btnShowLinks.Top = 2;
            btnShowLinks.Text = "Show Links";
            
            pictureBox = new PictureBox();
            pictureBox.Width = base.Width;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.BackColor = Color.White;           
        }

    }
}
