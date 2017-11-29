using System.Drawing;

namespace WindowsFormsApp1
{
    public interface IViewCinfiguration
    {
        Color Color { get; }
        FontFamily FontFamily { get; }
        int Width { get; }
        int Height { get; }
        StringFormat StringFormat { get; }
    }
}