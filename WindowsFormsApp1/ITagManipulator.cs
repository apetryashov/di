using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public interface ITagManipulator
    {
        IEnumerable<string> Manipulate(IEnumerable<string> tags);
    }
}