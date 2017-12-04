using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITextReader
    {
        string[] Read(string path);
    }
}