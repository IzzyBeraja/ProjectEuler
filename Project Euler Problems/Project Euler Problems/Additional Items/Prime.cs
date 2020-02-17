using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project_Euler_Problems
{
    public static class Prime
    {
        /// <summary>
        /// Generates an enumeration of Primes given a max value of int
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static IEnumerable<int> GeneratePrimesSieve(int maxValue)
        {
            if (maxValue > 40000000)
                throw new ArgumentOutOfRangeException("MaxValue provided is too large (>40 million)");
            if (maxValue >= 2)
            {
                int[] temp = new int[maxValue / 2];
                temp[0] = 2;

                int nextVal = 3;
                for (int i = 1; i < temp.Length; i++)
                {
                    temp[i] = nextVal;
                    nextVal += 2;
                }

                HashSet<int> x = new HashSet<int>(temp);
                for (int i = 0; x.ElementAt(i) < Math.Ceiling(Math.Sqrt(maxValue)); i++)
                    for (int j = x.ElementAt(i) * x.ElementAt(i); j < maxValue; j += x.ElementAt(i))
                        x.Remove(j);

                foreach (int val in x)
                    yield return val;
            }
        }

        /// <summary>
        /// Checks if value is prime using the <a href="https://www.geeksforgeeks.org/primality-test-set-3-miller-rabin/">Miller-Rabin primality test</a>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPrime(int value)
        {
            return IsPrime(new BigInteger(value));
        }

        /// <summary>
        /// Checks if value is prime using the <a href="https://www.geeksforgeeks.org/primality-test-set-3-miller-rabin/">Miller-Rabin primality test</a>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPrime(BigInteger value)
        {
            // Evens and less than 4 but not 3 (corner cases)
            if (value == 3 || value == 2)
                return true;
            else if (value % 2 == 0 || value < 4)
                return false;

            // Find r and d values for d*2^r = (n-1)
            int r = LargestR(value - 1);
            int d = int.Parse(((value - 1) / BigInteger.Pow(2, r)).ToString());

            return MillerTest(value, d, r);
        }

        /// <summary>
        /// Finds the largest R value given a BigInteger value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static int LargestR(BigInteger value)
        {
            int r = 0;
            while (value % 2 == 0)
            {
                value /= 2;
                r++;
            }
            return r;
        }

        /// <summary>
        /// Returns the required accuracy needed for Miller Test based on value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static int RequiredAccuracy(BigInteger value)
        {
            int accuracy = 0;
            if (value < 2047)
                accuracy = 1;
            else if (value < 1373653)
                accuracy = 2;
            else if (value < 25326001)
                accuracy = 3;
            else if (value < 3215031751)
                accuracy = 4;
            else if (value < 2152302898747)
                accuracy = 5;
            else if (value < 3474749660383)
                accuracy = 6;
            else
                accuracy = 7;
            return accuracy;
        }

        /// <summary>
        /// Miller Test of Primality
        /// </summary>
        /// <param name="n"></param>
        /// <param name="d"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private static bool MillerTest(BigInteger n, int d, int r)
        {
            BigInteger x;
            List<int> Accuracy = new List<int> { 2, 3, 5, 7, 11, 13, 17 };
            
            for (int i = 0; i < RequiredAccuracy(n); i++)
            {
                int a = Accuracy[i];
                x = BigInteger.ModPow(a, d, n);
                if (x == 1 || x == n - 1)
                    continue;
                else
                {
                    for (int j = 0; j < r - 1; j++)
                    {
                        x = BigInteger.ModPow(x, 2, n);
                        if (x == 1)
                            return false;
                        else if (x == n - 1)
                            break;
                    }
                }

                if (x != n - 1)
                    return false;
            }
            return true;
        }
    }
}
