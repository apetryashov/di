using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TxtTextReader : ITextReader
    {
        public string[] Read(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}