using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TxtTextReader : ITextReader
    {
        public Result<string[]> Read(string path)
        {
            return Result.Of(() => File.ReadAllLines(path));
        }
    }
}