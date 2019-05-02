using System;

namespace MongoDB
{
    internal sealed class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }
    }
}