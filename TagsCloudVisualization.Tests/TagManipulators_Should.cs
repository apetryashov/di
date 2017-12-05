using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class TagManipulators_Should
    {
        private Mock<ITextReader> textReader;
        private Mock<IIgnoreWordsConfiguration> config;
        private IEnumerable<ITagManipulator> manipulators;
        [SetUp]
        public void SetUp()
        {
            textReader = new Mock<ITextReader>();
            this.config = new Mock<IIgnoreWordsConfiguration>();
            this.config.Setup(x => x.Paths).Returns(new[] {String.Empty});
        }

        public IEnumerable<string> Manipulate(IEnumerable<string> tags)
        {
            manipulators = new List<ITagManipulator>
            {
                new AllWordsToLowerCase(),
                new IgnoreSpecialWords(textReader.Object, config.Object)
            };
            foreach (var manipulator in manipulators)
                tags = manipulator.Manipulate(tags);
            return tags;
        }

        [Test]
        public void Manipulate_ToLowerCaseCorrectWork()
        {
            var input = new[] {"A", "b", "c", "D", "e", "F", "G", "H"};
            Manipulate(input).Should()
                .BeEquivalentTo("a", "b", "c", "d", "e", "f", "g", "h");
        }
        [Test]
        public void Manipulate_IgnoreSpecialWordsCorrectWork()
        {
            var input = new[] { "a", "b", "c", "d", "e", "f", "g", "h" };
            textReader.Setup(x => x.Read(It.IsAny<string>()))
                .Returns(new[] {"a", "b", "c", "d"});
            Manipulate(input).Should()
                .BeEquivalentTo("e", "f", "g", "h");
        }

        [Test]
        public void Manipulate_ComplexInteraction()
        {
            var input = new[] { "A", "b", "c", "D", "e", "F", "G", "H" };
            textReader.Setup(x => x.Read(It.IsAny<string>()))
                .Returns(new[] { "a", "b", "c", "d" });
            Manipulate(input).Should()
                .BeEquivalentTo("e", "f", "g", "h");
        }
    }
}