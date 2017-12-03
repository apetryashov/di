using System.Collections.Generic;

namespace Di
{
    public interface ITagManipulator
    {
        IEnumerable<string> Manipulate(IEnumerable<string> tags);
    }
}