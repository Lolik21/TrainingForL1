using System;

namespace DebugVsRelease
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.WriteLine($"{i}*{j} = {i * j}");
                }
            }
        }
    }
}
