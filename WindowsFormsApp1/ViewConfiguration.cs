using System.Drawing;

namespace TagsCloudVisualization
{
    public class ViewConfiguration : IViewConfiguration
    {
        public Color Color { get; set; } 
        public FontFamily FontFamily { get; set; } 
        public int Width { get; set; } 
        public int Height { get; set; }
        public StringFormat StringFormat { get; set; } = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            FormatFlags = StringFormatFlags.NoClip
        };
    }
}