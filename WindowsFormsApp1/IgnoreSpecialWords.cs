using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class IgnoreSpecialWords : ITagManipulator
    {
        private ITextReader Reader { get; }
        private IIgnoreWordsConfiguration IgnoreWordsConfiguration { get; }
        private HashSet<string> SpecialStrings { get; set; }
        public IgnoreSpecialWords( ITextReader reader, IIgnoreWordsConfiguration ignoreWordsConfiguration)
        {
            this.Reader = reader;
            this.IgnoreWordsConfiguration = ignoreWordsConfiguration;
        }

        private void ReadSpecialStrings()
        {
            SpecialStrings = new HashSet<string>();

            foreach (var path in IgnoreWordsConfiguration.Paths)
            {
                foreach (var ignoreWord in Reader.Read(path))
                {
                    SpecialStrings.Add(ignoreWord);
                }
            }
        }
        public IEnumerable<string> Manipulate(IEnumerable<string> tags)
        {
            if(SpecialStrings == null) ReadSpecialStrings();

            return tags.Where(tag => !SpecialStrings.Contains(tag));
        }
    }
}