using System;

namespace ExceptionHandlingTask2
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            IStringParser stringParser = new StringParser();
            try
            {
                while (true)
                {
                    Console.WriteLine("Enter value:");
                    var value = Console.ReadLine();
                    if (value == "q")
                    {
                        return;
                    }
                    Console.WriteLine(stringParser.ParseToInt(value));
                }
            }
            catch (ArgumentNullOrWhiteSpaceException argumentNullOrWhiteSpaceException)
            {
                Console.WriteLine(argumentNullOrWhiteSpaceException.Message);
            }
            catch (FormatException formatException)
            {
                Console.WriteLine(formatException.Message);
            }
            catch (OverflowException overflowException)
            {
                Console.WriteLine(overflowException.Message);
            }

            Console.ReadKey();
        }
    }
}
