using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Project_Euler_Problems
{
    /// <summary>
    /// <para><a href="https://projecteuler.net/problem=43">Problem 43</a> - Sub-string divisibility</para>
    /// </summary>
    public static class Problem_43
    {
        /// <summary>
        /// Solves problem with default settings
        /// </summary>
        public static void Run()
        {
            // Get a list of all values with 3 digits that are divisible by 17
            List<string> vals = GetDivisible(17);
            // Find all 3 digit ints that are divisible by n whose last 2 digits
            // are the first 2 digits of each val in the current list
            List<int> sequence = new List<int>() { 13, 11, 7, 5, 3, 2 };
            for(int i = 0; i < sequence.Count; i++)
                vals = GetNum(vals, sequence[i]);
            // Find last value in each string
            vals = getLastVal(vals);
            // Print sum of values
            Console.WriteLine(vals.Select(x => double.Parse(x)).Sum());
        }

        /// <summary>
        /// Returns a list of three digit numbers divisible by 17
        /// </summary>
        /// <returns></returns>
        private static List<string> GetDivisible(int num)
        {
            List<string> list = new List<string>();
            for (int i = 1; i < 1000; i++)
                if (i % num == 0 && !ContainsRepeats(i))
                    list.Add(i.ToString("000"));
            return list;
        }

        /// <summary>
        /// Returns true if a string contains repeat characters
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static bool ContainsRepeats(int x) =>
            x.ToString().Distinct().Count() < x.ToString().Length;

        /// <summary>
        /// Finds each 3 digit value divisible by val whose last 2 digits are the starting 2 digits of each value in list without repeating digits
        /// </summary>
        /// <example>
        /// val = 2, list = [102, 150] -> returns [0102, 3102, 4102, 5102, 6102, 7102, 8102, 9102]
        /// </example>
        /// <param name="list"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private static List<string> GetNum(List<string> list, int val)
        {
            // Pandigital number range
            List<int> span = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            List<string> result = new List<string>();

            // Evaluate for each val in list
            for (int i = 0; i < list.Count; i++)
            {
                foreach (int j in span)
                {
                    // Skip over values that are already accounted for
                    if (!list[i].Contains(j.ToString()))
                    {
                        // Get first two values of the string
                        int temp = int.Parse(j + list[i].Substring(0, 2));
                        // Only multiples of val are added to list
                        if (temp % val == 0)
                            result.Add(j + list[i]);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Finds the missing digit in each string and inserts at index 0
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<string> getLastVal(List<string> list)
        {
            // foreach val in list, add the missing digit 0-9 to the front
            for(int i = 0; i < list.Count; i++)
                list[i] = (45 - list[i].Select(x => int.Parse(x.ToString())).Sum()) + list[i];
            return list;
        }
    }
}
