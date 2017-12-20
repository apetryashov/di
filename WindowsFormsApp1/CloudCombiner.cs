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
        private IConfigReader ConfigReader { get; }
        private ICircularCloudLayouter CloudLayouter { get; }
        public CloudCombiner(IConfigReader configReader, 
            ICircularCloudLayouter cloudLayouter,
            ITextReader textReader, 
            IEnumerable<ITagManipulator> tagFilters,
            ITagStatisticsGenerator statisticsGenerator)
        {
            ConfigReader = configReader;
            CloudLayouter = cloudLayouter;
            TextReader = textReader;
            TagFilters = tagFilters;
            StatisticsGenerator = statisticsGenerator;
        }

        public Result<IEnumerable<string>> FilterWords(IEnumerable<string> words)
        {
            foreach (var filter in TagFilters)
            {
                var result = filter.Manipulate(words)
                    .Then(x => words = x);
                if (!result.IsSuccess) return Result.Fail<IEnumerable<string>>(result.Error);
            }
            return Result.Ok(words);
        }

        public Rectangle GetWordArea(TagStatistic tagStat, double fontSize)
        {
            //однозначное отображение коэффициаета на интервал шрифтов
            var width = fontSize * tagStat.Value.Length;
            var height = fontSize;
            var size = new Size((int)(width), (int)height);
            return CloudLayouter.PutNextRectangle(size);
        }
        private Cloud GetCloud(TagStatistic[] statisic, ICloudConfiguration config)
        {
            var dFont = config.MaxFontSize - config.MinFontSize;
            var minStat = statisic.Min(x => x.Coefficient);
            var maxStat = statisic.Max(x => x.Coefficient);
            var dCoef = maxStat - minStat;
            var d = dFont / dCoef;
            var allWords = statisic.Select(tagStat =>
            {
                var fontSize = config.MinFontSize + tagStat.Coefficient * d;
                return new Word(tagStat.Value, (int)fontSize, GetWordArea(tagStat,fontSize));
            });
            return new Cloud(allWords);
        }

        public Result<Cloud> GetCloud()
        {
            var configResult = ConfigReader.GetCloudConfiguration();
            if (!configResult.IsSuccess)
                return Result.Fail<Cloud>(configResult.Error);
            var config = configResult.Value;
            return TextReader.Read(config.Path)
                .Then(FilterWords)
                .Then(words => StatisticsGenerator.GetStatistics(words)
                    .OrderByDescending(x => x.Coefficient)
                    .Take(config.NumberOfWordsInTheCloud)
                    .ToArray())
                .Then(s => GetCloud(s,config));

        }
    }
}