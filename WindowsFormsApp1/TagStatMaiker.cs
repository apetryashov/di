using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1
{
    public class TagStatMaiker : ITagStatMaiker
    {
        public IEnumerable<TagStatistic> GetStatistic(IEnumerable<string> allTags)
        {
            return allTags.GroupBy(x => x)
                .Select(x => new TagStatistic(x.Key, x.Count()));
        }
    }

}