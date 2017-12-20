using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITagManipulator
    {
        Result<IEnumerable<string>> Manipulate(IEnumerable<string> tags);
    }
}