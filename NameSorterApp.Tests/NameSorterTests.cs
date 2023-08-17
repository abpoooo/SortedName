using NUnit.Framework;
using NameSorterApp;

namespace NameSorterApp.Tests
{
    [TestFixture]
    public class NameSorterTests
    {
        [Test]
        public void TestSorting()
        {
            // Arrange
            INameSorter nameSorter = new NameSorter();
            List<string> unsortedNames = new List<string>
            {
                // ... add unsorted names here ...
                "Charlie Yang",
                "Jane Doe",
                "Alice Yang",
            };

            List<string> expectedSortedNames = new List<string>
            {
                // ... add expected sorted names here ...
                "Jane Doe",
                "Alice Yang",
                "Charlie Yang",
            };

            // Act
            List<string> sortedNames = nameSorter.SortNames(unsortedNames);

            // Assert
            CollectionAssert.AreEqual(expectedSortedNames, sortedNames);
        }

        // Add more test methods for edge cases or specific scenarios
    }
}