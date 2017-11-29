using System.Drawing;

namespace WindowsFormsApp1
{
    public interface IViewCinfiguration
    {
        Color Color { get; set; }
        FontFamily FontFamily { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        StringFormat StringFormat { get; set; }
    }
}