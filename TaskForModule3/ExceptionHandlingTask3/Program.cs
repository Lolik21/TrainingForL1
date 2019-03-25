using System;
using System.Diagnostics;

namespace ExceptionHandlingTask3
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            var sum1 = 0;
            sw.Start();

            for (int j = 0; j < 100; j++)
            {
                for (var i = 0; i < 10; i++)
                {
                    try
                    {
                        sum1 += i / (i - 5);
                    }
                    catch (DivideByZeroException)
                    {
                        // ignored
                    }
                }
            }

            sw.Stop();
            Console.WriteLine($"Total time 'try-cath': {sw.ElapsedMilliseconds} ms, sum = {sum1}.");

            sw.Reset();
            var sum2 = 0;
            sw.Start();

            for (int j = 0; j < 100; j++)
            {
                for (var i = 0; i < 10; i++)
                {
                    if (i - 5 != 0)
                    {
                        sum2 += i / (i - 5);
                    }
                }
            }

            sw.Stop();
            Console.WriteLine($"Total time 'if': {sw.ElapsedMilliseconds} ms, sum = {sum2}.");

            Console.WriteLine($"sum2/sum1 = {sum2 / sum1}");

            Console.ReadKey();
        }
    }
}
