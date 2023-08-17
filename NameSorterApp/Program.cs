using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NameSorterApp
{

    /// <summary>
    /// Provides functionality to sort names based on last name and given names.
    /// </summary>


    //create interface following SOLID principle of OOP for inheritance in the following main function, with INameSorter
    public interface INameSorter
    {
        // the interface name of SortNames
        List<string> SortNames(List<string> names);
    }

//the SortName with two main functions of LastName Comparator and GivenName Comparator with IComparer which is similar to Coparator in Java
    public class LastNameComparer : IComparer<string>
    {
        public int Compare(string name1, string name2)
        {
            return GetLastName(name1).CompareTo(GetLastName(name2));
        }

// encapsulation of GetLastNames, similar with GetGivenNames
        private string GetLastName(string name)
        {
            return name.Split().Last();
        }
    }


//test build action
public class GivenNamesComparer : IComparer<string>
    {
        public int Compare(string name1, string name2)
        {
            var givenNames1 = GetGivenNames(name1);
            var givenNames2 = GetGivenNames(name2);

            for (int i = 0; i < Math.Min(givenNames1.Length, givenNames2.Length); i++)
            {
                int comparisonResult = string.Compare(givenNames1[i], givenNames2[i], StringComparison.Ordinal);
                if (comparisonResult != 0)
                    return comparisonResult;
            }

            return givenNames1.Length.CompareTo(givenNames2.Length);
        }

        private string[] GetGivenNames(string name)
        {
            // we may have up to 3 given names
            string[] parts = name.Split();
            return parts.Length > 1 ? parts.Skip(1).Take(3).ToArray() : new string[0];
        }
    }


    public class NameSorter : INameSorter
    {
        /// <summary>
        /// Sorts the provided list of names based on last name and given names.
        /// </summary>
        /// <param name="names">The list of names to be sorted.</param>
        /// <returns>The sorted list of names.</returns>
        public List<string> SortNames(List<string> names)
        {
            // the sorted names are ordered by last name then by given names
            return names.OrderBy(name => name, new LastNameComparer())
                        .ThenBy(name => name, new GivenNamesComparer())
                        .ToList();
        }
    }

    class Program
    {
        // Main method
        static void Main()
        {
            // ... Sorting implementation ...
            List<string> unsortedNames = File.ReadAllLines("unsorted-names-list.txt").ToList();
            INameSorter nameSorter = new NameSorter();
            List<string> sortedNames = nameSorter.SortNames(unsortedNames);

            foreach (string name in sortedNames)
            {
                Console.WriteLine(name);
            }

            File.WriteAllLines("sorted-names-list.txt", sortedNames);
        }

        // Unit Test
        static void RunUnitTest()
        {
            var lastNameComparer = new LastNameComparer();
            var givenNamesComparer = new GivenNamesComparer();

            //Test lastNameComparer
            Console.WriteLine("Testing LastNameComparer:");
            Console.WriteLine(lastNameComparer.Compare("Doe Yang", "Smith Alice")); // Output: 1 (Doe > Smith)
            Console.WriteLine(lastNameComparer.Compare("Alice Smith", "Alice Johnson")); // Output: 1 (Smith > Johnson)
            Console.WriteLine(lastNameComparer.Compare("John Yang", "Jane Yang")); // Output: 1 (John > Jane)
            Console.WriteLine(lastNameComparer.Compare("Charlie Yang", "Yang Charlie")); // Output: -1 (Charlie < Yang)
            Console.WriteLine(lastNameComparer.Compare("Ryan Huang", "Ryan Huang")); // Output: 0 (Equal)

            // Test GivenNamesComparer
            Console.WriteLine("\nTesting GivenNamesComparer:");
            Console.WriteLine(givenNamesComparer.Compare("John Yang", "John Adam Yang")); // Output: 0 (Both have "John Yang")
            Console.WriteLine(givenNamesComparer.Compare("Alice Smith", "Alice Anne Smith")); // Output: 0 (Both have "Alice Smith")
            Console.WriteLine(givenNamesComparer.Compare("Jane Yang", "Jane Ellen Yang")); // Output: 0 (Both have "Jane Yang")
            Console.WriteLine(givenNamesComparer.Compare("Charlie Williams", "Charlie Steve Williams")); // Output: -1 (Charlie < Charlie Steve)
            Console.WriteLine(givenNamesComparer.Compare("Ryan Huang", "Ryan Daniel Huang")); // Output: -1 (Ryan < Ryan Daniel)
        }
    }
}