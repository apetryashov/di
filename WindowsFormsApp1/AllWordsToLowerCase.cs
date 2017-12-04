using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class AllWordsToLowerCase : ITagManipulator
    {
        public IEnumerable<string> Manipulate(IEnumerable<string> tags)
        {
            return tags.Select(x => x.ToLower());
        }
    }
}