using System.Collections.Generic;

namespace Di
{
    public interface ITextReader
    {
        string[] Read(string path);
    }
}