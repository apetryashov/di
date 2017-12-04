using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITagManipulator
    {
        IEnumerable<string> Manipulate(IEnumerable<string> tags);
    }
}