using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class ArchimedeanCircularCloudLayouter : ICircularCloudLayouter
    {
        private Point center;
        private List<Rectangle> cloudRectangles;
        public ArchimedeanCircularCloudLayouter(Point center)
        {
            this.center = center;
            cloudRectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size size)
        {
            var w = size.Width;
            var h = size.Height;
            if (w <= 0 || h <= 0)
                throw new ArgumentException();
            foreach (var point in ArchomedianPoints())
            {
                var rect = new Rectangle(new Point(point.X - w / 2, point.Y - h / 2), size);
                if (cloudRectangles.Any(x => x.IntersectsWith(rect))) continue;
                cloudRectangles.Add(rect);
                return rect;
            }
            return Rectangle.Empty;
        }

        private IEnumerable<Point> ArchomedianPoints()
        {
            var angleInDegrees = 0;
            while (true)
            {
                var angleInRadians = angleInDegrees * Math.PI / 180;
                var radius = 0.5 * angleInRadians;
                yield return new Point(
                    x: (int)(center.X + radius * Math.Cos(angleInRadians)),
                    y: (int)(center.Y + radius * Math.Sin(angleInRadians)));
                angleInDegrees++;
            }
        }
    }
}