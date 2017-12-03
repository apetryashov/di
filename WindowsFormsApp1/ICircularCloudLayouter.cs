using System.Drawing;

namespace Di
{
    public interface ICircularCloudLayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}