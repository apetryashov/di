using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITextReader
    {
        Result<string[]> Read(string path);
    }
}