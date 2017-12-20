using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization
{

    public class WinFormCloudVisualizer : Form , ICloudVisualizer
    {
        public IViewConfiguration Configuration { get; }
        private Bitmap drawArea;

        public WinFormCloudVisualizer(IConfigReader confReader)
        {
            var confResult = confReader.GetViewConfiguration();
            if (!confResult.IsSuccess)
            { 
                ShowError(confResult.Error);
                return;
            }
            Configuration = confResult.Value;
            Width = Configuration.Width;
            Height = Configuration.Height;
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

        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var workArea = new Rectangle(new Point(0,0),new Size(Width,Height));
                foreach (var word in cloud.Words)
                {
                    var font = new Font(Configuration.FontFamily, word.FontSize, FontStyle.Bold, GraphicsUnit.Point);
                    var brush = new SolidBrush(Configuration.Color);

                    if (!workArea.Contains(word.Area))
                    {
                        ShowError("The word does not enter the specified boundaries");
                        return;
                    }
                    g.DrawString(word.Value,font, brush, word.Area,Configuration.StringFormat);
                }
            }
            pb.Image = drawArea;
            pb.Invalidate();
            Application.Run(this);
        }
    }
}