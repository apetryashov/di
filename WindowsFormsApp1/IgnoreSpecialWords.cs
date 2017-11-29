using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1
{
    public class IgnoreSpecialWords : ITagFilter
    {
        private ITextReader Reader { get; }
        private IgnoreWordsFiles IgnoreWords { get; }
        private HashSet<string> SpecialStrings { get; set; }
        public IgnoreSpecialWords( ITextReader reader, IgnoreWordsFiles ignoreWords)
        {
            this.Reader = reader;
            this.IgnoreWords = ignoreWords;
        }

        private void ReadSpecialStrings()
        {
            SpecialStrings = new HashSet<string>();

            foreach (var path in IgnoreWords.Paths)
            {
                foreach (var ignoreWord in Reader.Read(path))
                {
                    SpecialStrings.Add(ignoreWord);
                }
            }
        }
        public IEnumerable<string> Filter(IEnumerable<string> tags)
        {
            if(SpecialStrings == null) ReadSpecialStrings();

            return tags.Where(tag => !SpecialStrings.Contains(tag));
        }
    }

    public class IgnoreWordsFiles
    {
        public string[] Paths;

    }

}