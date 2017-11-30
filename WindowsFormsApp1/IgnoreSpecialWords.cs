using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1
{
    public class IgnoreSpecialWords : ITagManipulator
    {
        private ITextReader Reader { get; }
        private IIgnoreWordsConfiguration IgnoreWords { get; }
        private HashSet<string> SpecialStrings { get; set; }
        public IgnoreSpecialWords( ITextReader reader, IIgnoreWordsConfiguration ignoreWords)
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
        public IEnumerable<string> Manipulate(IEnumerable<string> tags)
        {
            if(SpecialStrings == null) ReadSpecialStrings();

            return tags.Where(tag => !SpecialStrings.Contains(tag));
        }
    }
}