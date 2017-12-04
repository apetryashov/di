using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CloudCombiner : ICloudCombiner
    {
        private IEnumerable<ITagManipulator> TagFilters { get; }
        private ITagStatisticsGenerator StatisticsGenerator { get; }
        private ITextReader TextReader { get; }
        private ICloudConfiguration Configuration { get; }
        private ICircularCloudLayouter CloudLayouter { get; }
        public CloudCombiner(ICloudConfiguration configuration, 
            ICircularCloudLayouter cloudLayouter,
            ITextReader textReader, 
            IEnumerable<ITagManipulator> tagFilters,
            ITagStatisticsGenerator statisticsGenerator)
        {
            Configuration = configuration;
            CloudLayouter = cloudLayouter;
            TextReader = textReader;
            TagFilters = tagFilters;
            StatisticsGenerator = statisticsGenerator;
        }

        public Cloud GetCloud()
        {
            IEnumerable<string> words = TextReader.Read(Configuration.Path);

            foreach (var filter in TagFilters)
                words = filter.Manipulate(words);

            var statisic = StatisticsGenerator.GetStatistics(words)
                .OrderByDescending( x=> x.Coefficient)
                .Take(Configuration.NumberOfWordsInTheCloud)
                .ToArray();

            var allWords = new List<Word>();

            var dFont = Configuration.MaxFontSize - Configuration.MinFontSize;
            var minStat = statisic.Min(x => x.Coefficient);
            var maxStat = statisic.Max(x => x.Coefficient);
            var dCoef = maxStat - minStat;
            var d = dFont / dCoef;

            foreach (var tagStat in statisic)
            {
                //однозначное отображение коэффициаета на интервал шрифтов
                var fontSize = Configuration.MinFontSize + tagStat.Coefficient * d;
                var width = fontSize * tagStat.Value.Length;
                var height = fontSize;
                var rect = CloudLayouter.PutNextRectangle(new Size((int)(width), (int)(height)));
                allWords.Add(new Word(tagStat.Value, (int)fontSize, rect));
            }
            return new Cloud(allWords);
        }
    }
}