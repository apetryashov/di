using System.Collections.Generic;
using System.Linq;

namespace Di
{
    public class AllWordsToLowerCase : ITagManipulator
    {
        public IEnumerable<string> Manipulate(IEnumerable<string> tags)
        {
            return tags.Select(x => x.ToLower());
        }
    }
}