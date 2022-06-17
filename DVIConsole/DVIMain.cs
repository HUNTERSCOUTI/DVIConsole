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
            Console.Title = "WineSys";
            Console.CursorVisible = false;

            writer.LayoutWriter();
            //writer.RSSWriter();
            Writer();

            DateTime updateTime = DateTime.Now.AddMinutes(5); // Sets the update time to 5 minutes
            DateTime rssMove = DateTime.Now.AddMilliseconds(200);

            int index = 0;

            while (true)
            {
                writer.ClockWriter();

                if (DateTime.Now > rssMove) 
                {
                    writer.RSSWriter(index);
                    index++;
                    rssMove = DateTime.Now.AddMilliseconds(200);
                }

                if (DateTime.Now > updateTime) // Once the current time is more than the update time, it will reset
                {
                    Console.Clear();
                    writer.LayoutWriter();
                    //writer.RSSWriter();
                    Writer();
                    updateTime = DateTime.Now.AddMinutes(5);
                }

                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.X) break; // PRESS X TO EXIT
            }
        }
        public static void Writer()
        {
            writer.TempAndHumWriter();
            writer.StockWriter();

        }
    }
}