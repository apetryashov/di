using System.Collections.Generic;

namespace Di
{
    public interface ITagStatisticsGenerator
    {
        IEnumerable<TagStatistic> GetStatistics(IEnumerable<string> allTags);
    }
}