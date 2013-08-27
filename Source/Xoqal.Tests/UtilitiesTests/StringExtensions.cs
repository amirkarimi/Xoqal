namespace Xoqal.Tests.UtilitiesTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Xoqal.Utilities.Extensions;

    [TestClass]
    public class StringExtensions
    {
        [TestMethod]
        public void RemoveIfStarsWithWorksWithEmptyStrings()
        {
            string.Empty.RemoveIfStartsWith(string.Empty);
        }

        [TestMethod]
        public void RemoveIfStartsWithReturnsCorrectResults()
        {
            // Arrange
            var source1 = "AppleTest";
            var source2 = "Test";
            var text = "Apple";

            // Act
            var source1Result = source1.RemoveIfStartsWith(text);
            var source2Result = source2.RemoveIfStartsWith(text);

            // Assert
            Assert.AreEqual("Test", source1Result);
            Assert.AreEqual("Test", source2Result);
        }

        [TestMethod]
        public void RemoveIfEndsWithWorksWithEmptyStrings()
        {
            string.Empty.RemoveIfEndsWith(string.Empty);
        }

        [TestMethod]
        public void RemoveIfEndsWithReturnsCorrectResults()
        {
            // Arrange
            var source1 = "TestApple";
            var source2 = "Test";
            var text = "Apple";            

            // Act
            var source1Result = source1.RemoveIfEndsWith(text);
            var source2Result = source2.RemoveIfEndsWith(text);

            // Assert
            Assert.AreEqual("Test", source1Result);
            Assert.AreEqual("Test", source2Result);
        }
    }
}
