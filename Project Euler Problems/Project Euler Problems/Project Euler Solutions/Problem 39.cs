using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Euler_Problems
{
    /// <summary>
    /// <para><a href="https://projecteuler.net/problem=39">Problem 39</a> - Integer Right Triangles</para>
    /// </summary>
    public static class Problem_39
    {
        /// <summary>
        /// Solves the problem with default parameters
        /// </summary>
        /// <param name="debugPrint"></param>
        public static void Run(bool debugPrint = false) =>
            Run(1000, debugPrint);

        /// <summary>
        /// Solves the problem with special parameters
        /// </summary>
        /// <param name="perimeter"></param>
        /// <param name="debugPrint"></param>
        public static void Run(int perimeter, bool debugPrint = false)
        {
            // Record largest number of solutions and the perimiter
            int largest = 0;
            int perim = 0;

            // For each perimiter greater than the minimum possible
            for (int p = 3; p <= perimeter; p++)
            {
                int count = 0;
                // Find all right triangles (Brute force)
                // TODO: Make this part more efficient
                for (int i = 1; i < p - 2; i++)
                    for (int j = 1; j < i; j++)
                        // Pythagorean theorem case
                        if (i * i + j * j == (p - (i + j)) * (p - (i + j)))
                        {
                            if(debugPrint)
                                Console.WriteLine("{0}: [{1},{2},{3}]", p, j, i, Math.Sqrt(i*i+j*j));
                            count++;
                        }
                // Record largest
                if (count > largest)
                {
                    largest = count;
                    perim = p;
                }
            }
            Console.WriteLine("The Maximised Value is: {0} with {1} triangles", largest, perim);
        }
    }
}
