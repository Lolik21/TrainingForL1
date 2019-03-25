using System;

namespace ExceptionHandlingTask1
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var isStopInput = false;
            while (!isStopInput)
            {
                Console.WriteLine("Enter string:");
                var enteredValue = Console.ReadLine();
                if (string.IsNullOrEmpty(enteredValue))
                {
                    Console.WriteLine("Entered empty string. Try again.");
                }
                else
                {
                    Console.WriteLine($"First symbol: {enteredValue[0]}");
                    if (enteredValue[0] == 'q')
                    {
                        isStopInput = true;
                    }
                }
                
            }
        }
    }
}
