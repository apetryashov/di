using System.Drawing;

namespace TagsCloudVisualization
{
    public class DefaultViewConfiguration : IViewConfiguration
    {
        public Color Color { get; set; } = Color.Red;
        public FontFamily FontFamily { get; set; } = FontFamily.GenericSerif;
        public int Width { get; set; } = 500;
        public int Height { get; set; } = 500;
        public StringFormat StringFormat { get; set; } = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            FormatFlags = StringFormatFlags.NoClip
        };
    }
}