using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using Timer = System.Timers.Timer;

namespace DVIConsole
{
    class DVIMain
    {
        /* TO DO LIST:
                Temp and Humidity
                Time Update constantly
         */



        private static DVI dvi = new DVI();
        static void Main(string[] args)
        {
            Timer t = new Timer(300000); //300.000ms = 5 min
            t.AutoReset = true;
            t.Start();

            dvi.LayoutDraw();
            Writer();

            while (true)
            {
                dvi.ClockWriter();
                t.Elapsed += (s, e) =>
                {
                    Console.Clear();
                    dvi.LayoutDraw();
                    Writer();
                };

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
        private readonly DVIService.monitorSoapClient ds = new DVIService.monitorSoapClient();
        private readonly string[] timeZone = { "Romance Standard Time", "Eastern Standard Time", "Singapore Standard Time" };
        public int TimeZoneId = 0;
        public void LayoutDraw()
        {
            Console.SetCursorPosition(0, 0);
            for (var r = 0; r < Layout.Length; r++)
            {
                for (var c = 0; c < Layout[r].Length; c++)
                {
                    Console.Write(Layout[r][c]);
                }
                Console.WriteLine();
            }
        }

        public void StockWriter()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (var k = 0; k < ds.StockItemsUnderMin().Count; k++)
            {
                Console.SetCursorPosition(40, k+5);
                Console.Write(ds.StockItemsUnderMin()[k]);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            for (var j = 0; j < ds.StockItemsOverMax().Count; j++)
            {
                Console.SetCursorPosition(40, j+14);
                Console.Write(ds.StockItemsOverMax()[j]);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            for (var i = 0; i < ds.StockItemsMostSold().Count; i++)
            {
                Console.SetCursorPosition(40, i+23);
                Console.Write(ds.StockItemsMostSold()[i]);
            }
        }

        public void TempAndHumWriter()
        {
            //\u2103 *C
        }

        public void ClockWriter()
        {
            int y = 15;
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(16, y);
                Console.Write(ClockLoader().ToString());
                y += 2;
            }
        }

        public DateTime ClockLoader() //https://docs.microsoft.com/en-us/dotnet/api/system.timezoneinfo.converttimefromutc?view=net-6.0#examples
        {
            DateTime timeUtc = DateTime.UtcNow;

            try
            {
                TimeZoneInfo toTimezone = TimeZoneInfo.FindSystemTimeZoneById(timeZone[TimeZoneId]);
                

                DateTime time = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, toTimezone);

                TimeZoneId++;

                return time;
            }
            catch (TimeZoneNotFoundException)
            {
                Console.Write("ERROR(1)");
            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine("ERROR(2)");
            }
            return timeUtc;
        }

        /*
        //Temp og Fugt
        ds.StockTemp();
        ds.OutdoorTemp();
        ds.OutdoorHumidity();
        ds.StockHumidity();
        */

        public static readonly char[][] Layout =
        {
            "                                      |                                   ".ToCharArray(), //74 wide
            "      Temperatur og Fugtighed         |         Lagerstatus               ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "  Lager:                              | Varer under under minimum:        ".ToCharArray(),
            "  Temp:                               |-----------------------------------".ToCharArray(),
            "  Fugt:                               |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "  Udenfor:                            |                                   ".ToCharArray(),
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
