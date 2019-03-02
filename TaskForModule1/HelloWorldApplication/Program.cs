using System;
using TimeManager;

namespace HelloWorldApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var timeService = new TimeService();
            var currentTime = timeService.GetCurrentTime();
            Console.WriteLine(currentTime);
        }
    }
}
