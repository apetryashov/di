using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class AllWordsToLowerCase : ITagManipulator
    {
        public Result<IEnumerable<string>> Manipulate(IEnumerable<string> tags)
        {
            return Result.Of(() => tags.Select(x => x.ToLower()));
        }
    }
}