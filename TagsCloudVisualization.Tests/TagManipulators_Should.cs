using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class TagManipulators_Should
    {
        private ITextReader textReader;
        private IEnumerable<ITagManipulator> manipulators;
        [SetUp]
        public void SetUp()
        {
            textReader = A.Fake<ITextReader>();
            var config = A.Fake<IIgnoreWordsConfiguration>();
            A.CallTo(() => config.Paths)
                .Returns(new[] {String.Empty});
            manipulators = new List<ITagManipulator>
            {
                new AllWordsToLowerCase(),
                new IgnoreSpecialWords(textReader, config)
            };
        }

        public IEnumerable<string> Manipulate(IEnumerable<string> tags)
        {
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
            A.CallTo(() => textReader.Read(A<string>.Ignored))
                .Returns(new[] { "a", "b", "c", "d" });
            Manipulate(input).Should()
                .BeEquivalentTo("e", "f", "g", "h");
        }

        [Test]
        public void Manipulate_ComplexInteraction()
        {
            var input = new[] { "A", "b", "c", "D", "e", "F", "G", "H" };
            A.CallTo(() => textReader.Read(A<string>.Ignored))
                .Returns(new[] { "a", "b", "c", "d" });
            Manipulate(input).Should()
                .BeEquivalentTo("e", "f", "g", "h");
        }
    }
}