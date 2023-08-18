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
                "Charlie Yang",
                "Jane Doe",
                "Alice Yang",
            };

            List<string> expectedSortedNames = new List<string>
            {
                "Jane Doe",
                "Alice Yang",
                "Charlie Yang",
            };

            // Act
            List<string> sortedNames = nameSorter.SortNames(unsortedNames);

            // Assert
            CollectionAssert.AreEqual(expectedSortedNames, sortedNames);
        }

        
    }
}