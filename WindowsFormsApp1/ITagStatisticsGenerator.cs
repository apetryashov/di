using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITagStatisticsGenerator
    {
        IEnumerable<TagStatistic> GetStatistics(IEnumerable<string> allTags);
    }
}