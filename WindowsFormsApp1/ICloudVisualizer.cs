using System;

namespace TagsCloudVisualization
{
    public interface ICloudVisualizer
    {
        void ShowError(string errorMessage);
        void DrawCloud(Cloud cloud);
    }
}