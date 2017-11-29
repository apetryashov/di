using System.Drawing;

namespace WindowsFormsApp1
{
    public class DefaultViewCinfiguration : IViewCinfiguration
    {
        public Color Color { get; } = Color.Red;
        public FontFamily FontFamily { get; } = FontFamily.GenericSerif;
        public int Width { get; } = 500;
        public int Height { get; } = 500;
        public StringFormat StringFormat { get; } = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            FormatFlags = StringFormatFlags.NoClip
        };
    }
}