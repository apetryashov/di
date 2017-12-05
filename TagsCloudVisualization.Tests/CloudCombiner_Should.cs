using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Autofac;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class CloudCombiner_Should
    {
        private ContainerBuilder builder;
        private Point center;
        private Mock<ICloudConfiguration> cloudConfiguration;
        private Mock<ITextReader> textReader;
        private Mock<ITagStatisticsGenerator> statisticGenerator;
        [SetUp]
        public void SetUp()
        {
            builder = new ContainerBuilder();

            cloudConfiguration = new Mock<ICloudConfiguration>();
            cloudConfiguration.Setup(x => x.MinFontSize).Returns(10);
            cloudConfiguration.Setup(x => x.MaxFontSize).Returns(30);

            textReader = new Mock<ITextReader>();
            statisticGenerator = new Mock<ITagStatisticsGenerator>();

            builder.RegisterType<CloudCombiner>().As<ICloudCombiner>();
            builder.RegisterType<ArchimedeanCircularCloudLayouter>().As<ICircularCloudLayouter>();
            builder.Register(x => new Point());
        }

        

        public ICloudCombiner GetCombiner()
        {
            builder.RegisterInstance(cloudConfiguration.Object)
                .As<ICloudConfiguration>();
            builder.RegisterInstance(textReader.Object)
                .As<ITextReader>();
            builder.RegisterInstance(statisticGenerator.Object)
                .As<ITagStatisticsGenerator>();
            var container = builder.Build();
            center = container.Resolve<Point>();
            return container.Resolve<ICloudCombiner>();
        }

        [TestCase(10)]
        [TestCase(50)]
        [TestCase(100)]
        public void GetCloud_AllWordsFormACircle(int wordsCount)
        {
            cloudConfiguration.Setup(x => x.NumberOfWordsInTheCloud)
                .Returns(wordsCount);
            var statistic = new List<TagStatistic>();
            for (var i = 0; i < wordsCount; i++)
                statistic.Add(new TagStatistic(i.ToString(),i));

            statisticGenerator.Setup(x => x.GetStatistics(It.IsAny<IEnumerable<string>>()))
                .Returns(statistic);

            var cloud = GetCombiner().GetCloud();
            AllWordsFormACircle(cloud);
        }

        [TestCase(10,10,20)]
        [TestCase(10,10,10)]
        [TestCase(100,10,20)]
        [TestCase(100,10,10)]
        [TestCase(100,10,15)]
        public void GetCloud_SingleValuedDisplayOfWordSizes(int wordsCount, int min, int max)
        {

            cloudConfiguration.Setup(x => x.MinFontSize)
                .Returns(min);
            cloudConfiguration.Setup(x => x.MaxFontSize)
                .Returns(max);
            cloudConfiguration.Setup(x => x.NumberOfWordsInTheCloud)
                .Returns(wordsCount);

            var statistic = new List<TagStatistic>();
            for (var i = 0; i < wordsCount; i++)
                statistic.Add(new TagStatistic(i.ToString(), i));

            statisticGenerator.Setup(x => x.GetStatistics(It.IsAny<IEnumerable<string>>()))
                .Returns(statistic);

            var cloud = GetCombiner().GetCloud();

            cloud.Words.Count().Should().Be(wordsCount);
            cloud.Words.Min(x => x.FontSize).Should().Be(min);
            cloud.Words.Max(x => x.FontSize).Should().Be(max);
        }

        private void AllWordsFormACircle(Cloud cloud)
        {
            var rectangles = cloud.Words.Select(x => x.Area).ToList();

            var totalArea = rectangles.Select(x => x.Width * x.Height).Sum();

            var currentCircularRadius = 1.3 * Math.Sqrt(totalArea / Math.PI);

            rectangles.Select(ToBorderPoints).SelectMany(x => x).Distinct()
                .Select(x => new Point(center.X - x.X, center.Y - x.Y))
                .Where(x => x.X * x.X + x.Y * x.Y > currentCircularRadius * currentCircularRadius)
                .Should()
                .HaveCount(0);
        }

        public static IEnumerable<Point> ToBorderPoints(Rectangle rect)
        {
            var x = rect.Location.X;
            var y = rect.Location.Y;
            var w = rect.Width;
            var h = rect.Height;
            return new[]
            {
                new Point(x,y),
                new Point(x + w, y + h),
                new Point(x + w, y),
                new Point(x, y + h),
            };
        }
    }
}
