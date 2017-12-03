using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Di
{

    public class WinFormCloudVisualizer : Form , ICloudVisualizer
    {
        private Bitmap drawArea;


        public WinFormCloudVisualizer()
        {
            Width = 600;
            Height = 600;
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
                var sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                sf.FormatFlags = StringFormatFlags.NoClip;
                foreach (var word in cloud.Words)
                {
                    var font = new Font("Arial", word.FontSize, FontStyle.Bold, GraphicsUnit.Point);
                    g.DrawString(word.Value,font,Brushes.Red, word.Area,sf);
                }
            }
            pb.Image = drawArea;
            pb.Invalidate();
            Application.Run(this);
        }
    }
}