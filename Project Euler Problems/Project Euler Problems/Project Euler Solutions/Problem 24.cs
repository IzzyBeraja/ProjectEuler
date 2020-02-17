using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project_Euler_Problems
{
    // Doesn't work properly

    /// <summary>
    /// <para><a href="https://projecteuler.net/problem=24">Problem 24</a> - Lexicographic permutations</para>
    /// </summary>
    public class Problem_24
    {
        /// <summary>
        /// Solves the problem
        /// </summary>
        public static void Run(int value = 1000000, IEnumerable<string> range = null)
        {
            /* 
             * This problem uses the Factorial Number System:
             * https://en.wikipedia.org/wiki/Factorial_number_system
             * The factoradic is calculated and the permutation is then created from that factoradic.
            */

            // Range not included
            if (range is null)
                range = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            // Convert value to factoradic (1,000,000th number)
            // This includes permutations of 01 and 10 so answer is offset by 1
            string factoradic = ToFactoradic(value - 1);
            // Convert factoradic to lexicographic permutation
            string permutation = ToPermutation(factoradic, range);
            Console.WriteLine($"The permutation at index {value} is {permutation}.");
        }

        /// <summary>
        /// Converts an integer into its factoradic representation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ToFactoradic(int value)
        {
            // Conversion occurs through successive divisions; kind of like a reverse factorial
            string factoradic = "";
            for(int i = 1; value > 0; i++)
            {
                factoradic = value % i + factoradic;
                value /= i;
            }
            return factoradic;
        }

        /// <summary>
        /// Converts a factoradic into its lexicographic permutation index
        /// </summary>
        /// <param name="factoradic"></param>
        /// <returns></returns>
        private static string ToPermutation(string factoradic, IEnumerable<string> range)
        {
            if (range.Count() < factoradic.Length)
                return "undefined";

            List<string> choices = range.ToList();
            string permutation = "";

            foreach(char val in factoradic)
            {
                int index = int.Parse(val.ToString());
                permutation += choices[index];
                choices.RemoveAt(index);
            }

            return permutation;
        }

        /// <summary>
        /// Creates an enumerable containing integer factorials
        /// </summary>
        /// <remarks> O(n) </remarks>
        /// <param name="value"></param>
        /// <returns></returns>
        private static IEnumerable<BigInteger> Factorial(int value)
        {
            // 0! = 1
            if (value >= 0)
                yield return 1;

            // Calculate additional factorials
            BigInteger factorial = 1;
            for(int i = 1; i <= value; i++)
            {
                factorial *= i;
                yield return factorial;
            }
        }
    }
}
