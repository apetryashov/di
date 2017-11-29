using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public interface ITagStatMaiker
    {
        IEnumerable<TagStatistic> GetStatistic(IEnumerable<string> allTags);
    }
}