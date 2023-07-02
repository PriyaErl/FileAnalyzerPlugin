using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MDFileAnalyzerPlugin
{
    internal class MarkDownFile
    {
        public string Name { get; set; }
        public FileInfo myFileInfo { get; set; }
        public List<string> LinkedFiles { get; set; }
        public Rectangle Node { get; set; }
        public Point LineStartPoint { get; set; }
        public Point LineEndPoint { get; set; }

        private int myFileIndex;
        private int myWidth = 200;
        private int myHeight = 30;
        private Size myImageSize;


        public MarkDownFile(FileInfo fileInfo, int fileIndex, Size imageSize)
        {
            myImageSize = imageSize;
            myFileIndex = fileIndex;
            Name = fileInfo.Name;
            myFileInfo = fileInfo;
            LinkedFiles = new List<string>();

            if (myFileInfo.Exists)
            {
                var content = File.ReadAllLines(fileInfo.FullName);

                foreach (var str in content)
                {
                    if (str.Contains(".md"))
                    {
                        var matches = Regex.Matches(str, @"(?<=\()[0-z \#\!\$\%\^\&\*\.]{1,}(?=)");
                        foreach (Match match in matches)
                        {
                            var name = match.Value;

                            LinkedFiles.Add(name);
                        }
                    }
                }
            }

            CalculateNodeSize(fileIndex, imageSize);
        }

        //Reads the linked files information and returns it in a string
        public string GetLinkedFilesName()
        {
            var linkedFilesStr= new StringBuilder();
            if (LinkedFiles == null || LinkedFiles.Count == 0) return "";

            foreach (var reff in LinkedFiles)
            {
                linkedFilesStr.Append(reff + ", ");
            }
            string linkedFileNames = linkedFilesStr.ToString();
            var indexOfLsatComma = linkedFileNames.LastIndexOf(',');

            if (indexOfLsatComma > 0)
            {
                linkedFileNames = linkedFileNames.Remove(indexOfLsatComma, 1);
            }
            return linkedFileNames;
        }

        //Finds and returns the height of the node based on the information of the linked files
        public int GetLinkedFilesBoxHeight()
        {
            string linkedFilesName = GetLinkedFilesName();
            if (linkedFilesName.Length == 0) return 0;
            return ((linkedFilesName.Length / 14) + 2) * 9;
        }

        //Srno uniquely identifies MD Files        
        public int FileIndex
        {
            get
            {
                return myFileIndex;
            }
            set
            {
                myFileIndex = value;
                CalculateNodeSize(myFileIndex, myImageSize);
            }
        }

        //Calculate Node size based on MDFile index and imagesize        
        private void CalculateNodeSize(int fileindex, Size imageSize)
        {
            var nodesInRow = (imageSize.Width - myWidth) / (myWidth + (myWidth / 3));
            if (nodesInRow == 0) nodesInRow = 1;

            var left = 0;
            if (fileindex == 0)
            {
                left = (imageSize.Width / 2) - (myWidth);
            }
            else
            {
                left = ((fileindex % nodesInRow)) * (myWidth + (myWidth / 4));
                left += myWidth;
            }

            var top = 0;
            if (fileindex == 0)
            {
                top = 1;
            }
            else
            {
                top = 1 + ((fileindex - 1) / nodesInRow);
                top *= (myHeight * 3);
            }

            top += 100;
            CreateNode(left, top);
        }


        //Calculate Node size based on MDFile location 
        public void CalculateNodeSize(Point location)
        {
            CreateNode(location.X, location.Y);
        }

        //Calculate Node size based on MDFile imagesize   
        public void CalculateNodeSize(Size imageSize)
        {
            CalculateNodeSize(myFileIndex, imageSize);
        }

        //Create Node
        private void CreateNode(int left, int top)
        {
            Node = new Rectangle(left, top, myWidth, myHeight);
            LineStartPoint = new Point(left + (myWidth / 2), top);
            LineEndPoint = new Point(left + (myWidth / 2), top + myHeight);
        }
    }
}
