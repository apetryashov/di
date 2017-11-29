using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public class WinFormCloudVisualizer : Form , ICloudVisualizer
    {
        private Bitmap drawArea;
        private IViewCinfiguration ViewCinfiguration { get; }

        public WinFormCloudVisualizer(IViewCinfiguration viewCinfiguration)
        {
            ViewCinfiguration = viewCinfiguration;
            Width = ViewCinfiguration.Width;
            Height = ViewCinfiguration.Height;
            var saveButton = new Button
            {
                Width = 100,
                Height = 30,
                Text = "Save",
                Location = new Point(Width/2 - 50,Height - 100)
            };
            saveButton.Click += (sender, args) =>
            {
                using (var folder = new FolderBrowserDialog())
                {
                    DialogResult result = folder.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var folderName = folder.SelectedPath;
                        drawArea.Save($"{folderName}\\cloud.png");
                    }
                }
            };
            Controls.Add(saveButton);
        }

        public void DrawCloud(Cloud cloud)
        {
            var pb = new PictureBox
            {

                Width = this.Width - 100,
                Height = this.Height - 130,
                BorderStyle = BorderStyle.Fixed3D,
                Location = new Point(50,20)
            };
            drawArea = new Bitmap(pb.Size.Width, pb.Size.Height);
            Controls.Add(pb);


            using (var g = Graphics.FromImage(drawArea))
            {
                var brush = new SolidBrush(ViewCinfiguration.Color);
                foreach (var word in cloud.Words)
                {
                    var font = new Font(ViewCinfiguration.FontFamily, word.FontSize, FontStyle.Bold, GraphicsUnit.Point);
                    g.DrawString(word.Value,font,brush, word.Area,ViewCinfiguration.StringFormat);
                }
            }
            pb.Image = drawArea;
            pb.Invalidate();
            Application.Run(this);
        }
    }
}