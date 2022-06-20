using System;

namespace DVIConsole
{
    class DVIMain
    {
        private static DVIWriter writer = new DVIWriter();
        static void Main(string[] args)
        {
            Console.SetWindowSize(125, 35);
            Console.Title = "WineSys";
            Console.CursorVisible = false;

            Writer();

            DateTime updateTime = DateTime.Now.AddMinutes(5); // Tid mellem hver program opdatering
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

                if (DateTime.Now > updateTime)
                {
                    Console.Clear();
                    Writer();
                    updateTime = DateTime.Now.AddMinutes(5);
                }

                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.X) break;  // Tryk X for at lukke programmet
            }
        }
        public static void Writer()
        {
            writer.LayoutWriter();
            writer.TempAndHumWriter();
            writer.StockWriter();
        }
    }
}