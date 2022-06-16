using System;
using Timer = System.Timers.Timer;

namespace DVIConsole
{
    class DVIMain
    {
        private static DVI dvi = new DVI();
        static void Main(string[] args)
        {
            Timer t = new Timer(10000); //300.000ms = 5 min
            t.AutoReset = true; t.Start(); // Timer Settings & Start
            Timer clock = new Timer(100); //300.000ms = 5 min
            clock.AutoReset = true; clock.Start(); // Timer Settings & Start

            dvi.LayoutDraw();
            Writer();

            t.Elapsed += (s, e) =>
            {
                Console.Clear();
                dvi.LayoutDraw();
                Writer();
            };

            while (true)
            {
                dvi.ClockWriter();
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.X) break; // PRESS X TO EXIT
            }
        }

        public static void Writer()
        {
            dvi.StockWriter();
            dvi.TempAndHumWriter();
            
        }
    }

    public class DVI
    {
        //private readonly DVIService.monitorSoapClient ds = new DVIService.monitorSoapClient();
        public void LayoutDraw()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            for (var r = 0; r < Layout.Length; r++)
            {
                for (var c = 0; c < Layout[r].Length; c++)
                {
                    Console.Write(Layout[r][c]);
                }
                Console.WriteLine(); //Goes one line down
            }
        }
        public void StockWriter()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (var k = 0; k < 4; k++)
            {
                Console.SetCursorPosition(40, k+5);
                //Console.Write(ds.StockItemsUnderMin()[k]);
                Console.Write("TESTTESTTEST");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            for (var j = 0; j < 2; j++)
            {
                Console.SetCursorPosition(40, j+14);
                //Console.Write(ds.StockItemsOverMax()[j]);
                Console.Write("TESTTESTTEST");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            for (var i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(40, i+23);
                //Console.Write(ds.StockItemsMostSold()[i]);
                Console.Write("TESTTESTTEST");
            }
        }
        public void TempAndHumWriter()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.SetCursorPosition(8, 4);
            //Console.Write(ds.StockTemp());
            Console.Write("TESTTESTTEST");
            Console.SetCursorPosition(8, 5);
            //Console.Write(ds.StockHumidity());
            Console.Write("TESTTESTTEST");

            Console.SetCursorPosition(8, 8);
            //Console.Write(ds.OutdoorTemp());
            Console.Write("TESTTESTTEST");
            Console.SetCursorPosition(8, 9);
            //Console.Write(ds.OutdoorHumidity());
            Console.Write("TESTTESTTEST");
        }
        public void ClockWriter()
        {
            DateTime timeUtc = DateTime.UtcNow;

            var copenhagenTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(timeUtc, "Romance Standard Time");
            var useast = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(timeUtc, "Eastern Standard Time");
            var singaporeTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(timeUtc, "Singapore Standard Time");

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.SetCursorPosition(16, 15);
            Console.Write(copenhagenTime.ToString()); // KØBENHAVN
            Console.SetCursorPosition(16, 17);
            Console.Write(useast.ToString()); // USA ØST
            Console.SetCursorPosition(16, 19);
            Console.Write(singaporeTime.ToString()); // SINGAPORE
        }

        public static readonly char[][] Layout =
        {
            "                                      |                                   ".ToCharArray(), //74 wide
            "      Temperatur og Fugtighed         |         Lagerstatus               ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "  Lager                               | Varer under under minimum:        ".ToCharArray(),
            "  Temp:                               |-----------------------------------".ToCharArray(),
            "  Fugt:                               |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "  Udenfor                             |                                   ".ToCharArray(),
            "  Temp:                               |                                   ".ToCharArray(),
            "  Fugt:                               |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "--------------------------------------| Varer over maksimum:              ".ToCharArray(),
            "             Dato / Tid               |-----------------------------------".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "   Koebenhavn :                       |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "   USA Est    :                       |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "   Singapore  :                       |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "                                      | Mest solgte i dag:                ".ToCharArray(),
            "                                      |-----------------------------------".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "            ---------------           |                                   ".ToCharArray(),
            "           |Press x to exit|          |                                   ".ToCharArray(),
            "            ---------------           |                                   ".ToCharArray() //30 long
        };
    }
}