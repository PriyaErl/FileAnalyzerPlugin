
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MDFileAnalyzerPlugin
{
    
    internal class MDFileVisualizer
    {
        private List<MarkDownFile> myMdFiles;
        private PictureBox myPictureBox;
        private Size myImageSize;
        private bool IsMouseDown = false;
        private MarkDownFile myActiveMDFile;
        private Graphics myGraph;       
        private bool isShowReference = false;
        private bool isShowLinks = false;
        public delegate void hdnSelectionChanged(FileInfo selectedMarkdownFile);
        public event hdnSelectionChanged SelectionChanged;
        
        public MDFileVisualizer(List<FileInfo> files, PictureBox picturebox)
        {
            myPictureBox = picturebox;
            myPictureBox.MouseClick += MyPictureBox_MouseClick;
            myPictureBox.MouseDown += MyPictureBox_MouseDown;
            myPictureBox.MouseUp += MyPictureBox_MouseUp;
            myPictureBox.MouseMove += MyPictureBox_MouseMove;

            myImageSize = new Size(picturebox.Width, picturebox.Height);
            myMdFiles = new List<MarkDownFile>();
            int fileIndex = 0;
            foreach (var file in files)
            {
                myMdFiles.Add(new MarkDownFile(file, fileIndex++, myPictureBox.Size));
            }
            myActiveMDFile = null;
        }


        public void CreateFilesGraph()
        {
            if (myMdFiles == null || myMdFiles.Count <= 0) return;

            var colors = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Cyan, Color.DarkMagenta, Color.Olive };
            var bitmap = new Bitmap(myImageSize.Width, myImageSize.Height);
            myGraph = Graphics.FromImage(bitmap);
            myGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rectangle = new Rectangle(0, 0, myImageSize.Width, myImageSize.Height);
            myGraph.FillRectangle(Brushes.White, rectangle);

            var index = 0;
            var penColor = Color.Black;
            foreach (var file in myMdFiles)
            {
                penColor = colors[index % colors.Length];
                index++;


                if (isShowLinks)
                {
                    CreateLine(myGraph, penColor, file);
                }

                if (!isShowLinks && (myActiveMDFile != null) && (file.Name == myActiveMDFile.Name))
                {
                    CreateLine(myGraph, penColor, file);
                }

                var shadowBonds = new Rectangle(new Point(file.Node.Location.X + 2, file.Node.Location.Y + 2), file.Node.Size);
                myGraph.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(50, 0, 0, 0)), 3), shadowBonds);
                myGraph.FillRectangle(new SolidBrush(Color.FromArgb(100, penColor)), file.Node);
                myGraph.DrawRectangle(new Pen(penColor, 1), file.Node);
                DrawString(myGraph, file.Name, file.Node);

                if (isShowReference)
                {
                    DisplayReferencesInfo(myGraph, file);
                }
            }
            myPictureBox.Image = bitmap;
        }

        public bool ShowReferences()
        {
            isShowReference = !isShowReference;
            CreateFilesGraph();
            return isShowReference;
        }

        public bool ShowLinks()
        {
            isShowLinks = !isShowLinks;
            CreateFilesGraph();
            return isShowLinks;
        }
       
        private void DisplayReferencesInfo(Graphics g, MarkDownFile mdFile)
        {
            if (mdFile == null) return;
            var rect = new Rectangle(mdFile.Node.X, mdFile.Node.Y + (mdFile.Node.Height), mdFile.Node.Width, mdFile.GetLinkedFilesBoxHeight());
            g.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.LightYellow)), rect);
            g.DrawRectangle(new Pen(Brushes.Orange, 1), rect);
            DrawString(g, mdFile.GetLinkedFilesName(), rect, StringAlignment.Near, 8);// new Rectangle(10, 50, myPictureBox.Width - 20, 100));
        }

        private void DrawString(Graphics g, string text, Rectangle bonds, StringAlignment lineAlignment = StringAlignment.Center, int size = 12)
        {
            var stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = lineAlignment
            };
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawString(text, new Font("Calibri", size), Brushes.Black, bonds, stringFormat);
        }

        public void Sort()
        {
            if (myMdFiles == null || myMdFiles.Count == 0) return;
            int i = 0;
            var rearrangedFiles = new List<MarkDownFile>();
            rearrangedFiles.AddRange(myMdFiles);
            var sorter = new SortMdFiles();
            rearrangedFiles.Sort(sorter);
            myMdFiles.Clear();
            foreach (var file in rearrangedFiles)
            {
                file.FileIndex = i;
                i++;
                myMdFiles.Add(file);
            }

            myActiveMDFile = myMdFiles[0];

            //Recreate File GraphView again after sorting
            CreateFilesGraph();
        }

        internal class SortMdFiles : IComparer<MarkDownFile>
        {
            public int Compare(MarkDownFile x, MarkDownFile y)
            {
                if (x.LinkedFiles.Count > y.LinkedFiles.Count) return -1;
                if (x.LinkedFiles.Count < y.LinkedFiles.Count) return 1;
                return 0;
            }
        }

        private void CreateLine(Graphics g, Color penColor, MarkDownFile file)
        {
            foreach (var xFile in file.LinkedFiles)
            {
                var linkedFile = myMdFiles.FirstOrDefault(x => x.Name == xFile);
                if (linkedFile == null) continue;
                var pen = new Pen(new SolidBrush(Color.FromArgb(100, penColor)), 2);

                var xRef = 0;
                if (isShowReference)
                {
                    xRef += file.GetLinkedFilesBoxHeight();              }

                    var endPoint = new Point(file.LineEndPoint.X, file.LineEndPoint.Y + xRef);
                    g.DrawLine(pen, endPoint, linkedFile.LineStartPoint);
                    CreateArrow(g, new Pen(penColor, 1), file.LineEndPoint, linkedFile.LineStartPoint);
            }
        }

        private void CreateArrow(Graphics g, Pen pen, Point pointA, Point pointB)
        {
            var endY = pointB.Y;
            var endX = pointB.X;
            var startY = pointA.Y;
            var startX = pointA.X;


            float angle = (float)Math.Atan2(endY - startY, endX - startX);
            float arrowAngle = (float)(Math.PI / 6); // 30 degrees
            float arrowLength = 8;

            PointF arrowPoint1 = new PointF(endX - arrowLength * (float)Math.Cos(angle + arrowAngle),
                                            endY - arrowLength * (float)Math.Sin(angle + arrowAngle));

            PointF arrowPoint2 = new PointF(endX - arrowLength * (float)Math.Cos(angle - arrowAngle),
                                            endY - arrowLength * (float)Math.Sin(angle - arrowAngle));

            g.DrawLine(pen, endX, endY, arrowPoint1.X, arrowPoint1.Y);
            g.DrawLine(pen, endX, endY, arrowPoint2.X, arrowPoint2.Y);
        }

        int pixelSkip = 0;
        private void MyPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            pixelSkip++;
            if (pixelSkip < 2) return;
            else pixelSkip = 0;

            if (IsMouseDown && myActiveMDFile != null)
            {
                myActiveMDFile.CalculateNodeSize(e.Location);
                CreateFilesGraph();
            }
        }

        private void MyPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
            myActiveMDFile = null;
        }

        private void MyPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == false)
            {
                myActiveMDFile = myMdFiles.FirstOrDefault(x => x.Node.Contains(e.Location));
                IsMouseDown = true;
                CreateFilesGraph();
            }
        }
        private void MyPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            var mdFile = myMdFiles.FirstOrDefault(x => x.Node.Contains(e.Location));
            if (mdFile != null)
            {
                if (SelectionChanged != null)
                {
                    SelectionChanged(mdFile.myFileInfo);
                }
            }
                      
        }      

    }
}
