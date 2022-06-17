using System;
using System.Diagnostics;

namespace DVIConsole
{
    class DVIMain
    {
        private static DVIWriter writer = new DVIWriter();
        private static RSS rss = new RSS();
        static void Main(string[] args)
        {
            Console.SetWindowSize(125, 35);
            Console.CursorVisible = false;

            writer.LayoutWriter();
            writer.RSSLoader();
            Writer();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();

            TimeSpan interval = TimeSpan.FromMilliseconds(300000); //300.000ms = 5 min

            while (true)
            {
                writer.ClockLoader();
                writer.RSSWriter();
                
                if (stopwatch.Elapsed > interval)
                {
                    Console.Clear();
                    writer.LayoutWriter();
                    Writer();
                    stopwatch.Restart();
                }

                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.X) break; // PRESS X TO EXIT
            }
        }
        public static void Writer()
        {
            writer.StockWriter();
            writer.TempAndHumWriter();
            
        }
    }
}