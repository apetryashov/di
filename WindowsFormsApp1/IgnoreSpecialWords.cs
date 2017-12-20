using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TagsCloudVisualization
{
    public class IgnoreSpecialWords : ITagManipulator
    {
        private ITextReader Reader { get; }
        private IConfigReader ConfigReader { get; }
        private HashSet<string> SpecialStrings { get; set; }
        public IgnoreSpecialWords( ITextReader reader, IConfigReader configReader)
        {
            this.Reader = reader;
            this.ConfigReader = configReader;
        }

        private Result<None> ReadSpecialStrings()
        {
            SpecialStrings = new HashSet<string>();
            return ConfigReader.GetIgnoreWordsConfiguration()
                .Then(conf =>
                {
                    foreach (var path in conf.Paths)
                    {
                        var result = Reader.Read(path)
                            .Then(words => words.ToList()
                                .ForEach(x => SpecialStrings.Add(x)));
                        if (!result.IsSuccess) return result;
                    }
                    return Result.Ok();
                });
        }
        public Result<IEnumerable<string>> Manipulate(IEnumerable<string> tags)
        {
            if (SpecialStrings == null)
            {
                var result = ReadSpecialStrings();
                if (!result.IsSuccess)
                    return Result.Fail<IEnumerable<string>>(result.Error);
            }
            return Result.Ok(tags.Where(tag => !SpecialStrings.Contains(tag)));
        }
    }
}