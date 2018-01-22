using System;
using System.Diagnostics;

namespace PageControlCalculation.DemoConsole
{
    internal class Program
    {
        public static void Main()
        {
            Console.WriteLine("***** STARTING DEMO *****");

            var sw = new Stopwatch();
            sw.Start();

            DemoCanvas.Start();

            sw.Stop();

            Console.WriteLine($"***** FINISHED DEMO : {sw.Elapsed} *****");

            Console.ReadKey();
        }
    }
}
