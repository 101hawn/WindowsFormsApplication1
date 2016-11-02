using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        static void Main()
        {
            /*
            if (File.Exists("output.txt"))
            {
                var last = File.ReadLines("output.txt").LastOrDefault();
                BigInteger lasti;
                if (BigInteger.TryParse(last, out lasti))
                {
                    File.Move("output.txt", "output-" + lasti + "("+ new Random().Next() + ")"+ ".txt");
                    i = lasti;
                    File.AppendAllLines("output.txt", Enumerable.Repeat(i.ToString(), 1));
                }
                else
                {
                    File.Move("output.txt", "output" + new Random().Next() + ".txt");
                }
                
            }
            */
            running = !running;
            run();
        }

        static ConcurrentDictionary<string, int> cache = new ConcurrentDictionary<string, int>();
        static ConcurrentDictionary<int, int> numcache = new ConcurrentDictionary<int, int>();
        static ConcurrentDictionary<int, int> sumToThirdcache = new ConcurrentDictionary<int, int>();
        static ConcurrentBag<BigInteger> numbers = new ConcurrentBag<BigInteger>();
        private static int index = 0;
        static int cacheSum(int number)
        {
            return numcache.GetOrAdd(number, (x) => cacheSum(x.ToString()));
        }

        static int cacheSum(string digits)
        {
            return cache.GetOrAdd(digits, sum);
        }

        static int sum(string digits)
        {
            if (digits.Length == 1)
                return int.Parse(digits);
            return cacheSum(digits.Substring(0, digits.Length / 2)) + cacheSum(digits.Substring(digits.Length / 2));
        }
        private static bool running;
        private const int batch = 1000000;
        private static void run()
        {
            
            while (running)
            {
                a = 3 * i;
                Parallel.For(0, batch, calc);
                i = i + batch;
                numbers.Select(s => "FOUND: " + s.ToString()).ToList().ForEach(Console.WriteLine);
                Console.WriteLine(i);
                numbers = new ConcurrentBag<BigInteger>();
                cache= new ConcurrentDictionary<string, int>();
                numcache = new ConcurrentDictionary<int, int>();
                sumToThirdcache=new ConcurrentDictionary<int, int>();
                
            }
        }
        private static BigInteger a;
        private static BigInteger i = 0;
        static void calc(int x)
        {
            var first = a + 3 * x + 14;
            var sum = cacheSum(first.ToString());
            var third = sumToThirdcache.GetOrAdd(sum, (y) =>
            {
                var second = (3 * y) + 2;
                return cacheSum(second);
            });
            if (third != 8 && third!=17)
            {
               numbers.Add(i + x);
            }
        }
        
    }

}

