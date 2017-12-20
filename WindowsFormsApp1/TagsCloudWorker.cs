using System.Collections.Generic;
using System.Diagnostics;
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
            CloudCombiner.GetCloud()
                .Then(Visualizer.DrawCloud)
                .OnFail(Visualizer.ShowError);
        }
    }
}