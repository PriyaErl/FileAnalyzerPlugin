
namespace FileDependencyVisApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnMarkdownAnalyzer = new System.Windows.Forms.Button();
            this.panelPluginArea = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnMarkdownAnalyzer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelPluginArea);
            this.splitContainer1.Size = new System.Drawing.Size(1540, 846);
            this.splitContainer1.SplitterDistance = 173;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnMarkdownAnalyzer
            // 
            this.btnMarkdownAnalyzer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMarkdownAnalyzer.Location = new System.Drawing.Point(13, 13);
            this.btnMarkdownAnalyzer.Margin = new System.Windows.Forms.Padding(4);
            this.btnMarkdownAnalyzer.Name = "btnMarkdownAnalyzer";
            this.btnMarkdownAnalyzer.Size = new System.Drawing.Size(134, 94);
            this.btnMarkdownAnalyzer.TabIndex = 0;
            this.btnMarkdownAnalyzer.Text = "Markdown File Analyzer";
            this.btnMarkdownAnalyzer.UseVisualStyleBackColor = true;
            this.btnMarkdownAnalyzer.Click += new System.EventHandler(this.btnMarkdownAnalyzer_Click);
            // 
            // panelPluginArea
            // 
            this.panelPluginArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPluginArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelPluginArea.Location = new System.Drawing.Point(0, 0);
            this.panelPluginArea.Margin = new System.Windows.Forms.Padding(4);
            this.panelPluginArea.Name = "panelPluginArea";
            this.panelPluginArea.Size = new System.Drawing.Size(1362, 846);
            this.panelPluginArea.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 846);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Dependency Visualizer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnMarkdownAnalyzer;
        private System.Windows.Forms.Panel panelPluginArea;
    }
}

