using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TagStatisticsGenerator : ITagStatisticsGenerator
    {
        public IEnumerable<TagStatistic> GetStatistics(IEnumerable<string> allTags)
        {
            return allTags.GroupBy(x => x)
                .Select(x => new TagStatistic(x.Key, x.Count()));
        }
    }

}