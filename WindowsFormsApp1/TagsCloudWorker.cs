using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class TagsCloudWorker : ICloudWorker
    {
        private ICloudVisualizer Visualizer { get; }
        private ICloudCombiner CloudCombiner { get; }
        public TagsCloudWorker(ICloudVisualizer visualizer, ICloudCombiner cloudCombiner)
        {
            Visualizer = visualizer;
            CloudCombiner = cloudCombiner;
        }

        public void View()
        {
            var cloud = CloudCombiner.GetCloud();
            Visualizer.DrawCloud(cloud);
        }
    }
}