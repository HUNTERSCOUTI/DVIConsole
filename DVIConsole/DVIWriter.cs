using System;
using System.Collections.Generic;

namespace DVIConsole
{
    public class DVIWriter
    {
        private readonly DVIService.monitorSoapClient ds = new DVIService.monitorSoapClient();
        private readonly RSS rss = new RSS();   

        public void LayoutWriter()
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
        public void RSSWriter(int index)
        {
            rss.RSSLoader();
            Console.ForegroundColor = ConsoleColor.Yellow;
            rss.RunTheLine(index);
        }

        public void StockWriter()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (var k = 0; k < ds.StockItemsUnderMin().Count; k++)
            {
                Console.SetCursorPosition(40, k+7);
                Console.Write(ds.StockItemsUnderMin()[k]);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            for (var j = 0; j < ds.StockItemsOverMax().Count; j++)
            {
                Console.SetCursorPosition(40, j+17);
                Console.Write(ds.StockItemsOverMax()[j]);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            for (var i = 0; i < ds.StockItemsMostSold().Count; i++)
            {
                Console.SetCursorPosition(40, i+26);
                Console.Write(ds.StockItemsMostSold()[i]);
            }
        }
        public void TempAndHumWriter()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.SetCursorPosition(8, 7);
            Console.Write(ds.StockTemp() + "° Celcius");

            Console.SetCursorPosition(8, 8);
            Console.Write(ds.StockHumidity() + "° Celcius");
                
            Console.SetCursorPosition(8, 12);
            Console.Write(ds.OutdoorTemp() + "° Celcius");

            Console.SetCursorPosition(8, 13);
            Console.Write(ds.OutdoorHumidity() + "° Celcius");
        }
        public void ClockWriter()
        {
            DateTime timeUtc = DateTime.UtcNow;

            var copenhagenTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(timeUtc, "Romance Standard Time");
            var useast = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(timeUtc, "Eastern Standard Time");
            var singaporeTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(timeUtc, "Singapore Standard Time");

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.SetCursorPosition(16, 18);
            Console.Write(copenhagenTime.ToString()); // KØBENHAVN
            Console.SetCursorPosition(16, 20);
            Console.Write(useast.ToString()); // USA ØST
            Console.SetCursorPosition(16, 22);
            Console.Write(singaporeTime.ToString()); // SINGAPORE
        }

        public static readonly char[][] Layout =
        {
            "                                   WINESYS                                ".ToCharArray(), //74 wide
            "                                                                          ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "      Temperatur og Fugtighed         |         Lagerstatus               ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "  Lager                               | Varer under under minimum:        ".ToCharArray(),
            "  -------------                       | ----------------------------------".ToCharArray(),
            "  Temp: LOADING...                    | LOADING...                        ".ToCharArray(),
            "  Fugt: LOADING...                    |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "  Udenfor                             |                                   ".ToCharArray(),
            "  -------------                       |                                   ".ToCharArray(),
            "  Temp: LOADING...                    |                                   ".ToCharArray(),
            "  Fugt: LOADING...                    |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "--------------------------------------| Varer over maksimum:              ".ToCharArray(),
            "             Dato / Tid               | ----------------------------------".ToCharArray(),
            "                                      | LOADING...                        ".ToCharArray(),
            "   Koebenhavn : LOADING...            |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "   USA Est    : LOADING...            |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "   Singapore  : LOADING...            |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "                                      | Mest solgte i dag:                ".ToCharArray(),
            "                                      | ----------------------------------".ToCharArray(),
            "                                      | LOADING...                        ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            "            ---------------           |                                   ".ToCharArray(),
            "           |Press x to exit|          |                                   ".ToCharArray(),
            "            ---------------           |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray(),
            " Nyheder:                             |                                   ".ToCharArray(),
            " ---------------                      |                                   ".ToCharArray(),
            " LOADING...                           |                                   ".ToCharArray()//33 long
        };
    }
}