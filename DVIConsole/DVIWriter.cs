﻿using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;

namespace DVIConsole
{
    public class DVIWriter
    {
        private readonly DVIService.monitorSoapClient ds = new DVIService.monitorSoapClient();
        private static RSS rss = new RSS();
        List<string> headLines = new List<string>();

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

        public void RSSWriter()
        {
            Console.SetCursorPosition(2, 32);

            foreach (var line in headLines)
            {
                rss.News.Add(line);
            }
            rss.RunTheLine();
        }

        public void RSSLoader()
        {
            const string url = "https://nordjyske.dk/rss/nyheder";

            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);

            reader.Close();

            foreach (SyndicationItem title in feed.Items)
            {
                String hl = title.Title.Text;
                headLines.Add(hl);
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
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.SetCursorPosition(8, 4);
            Console.Write(ds.StockTemp());

            Console.SetCursorPosition(8, 5);
            Console.Write(ds.StockHumidity());

            Console.SetCursorPosition(8, 8);
            Console.Write(ds.OutdoorTemp());

            Console.SetCursorPosition(8, 9);
            Console.Write(ds.OutdoorHumidity());
        }
        public void ClockLoader()
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
            "            ---------------           |                                   ".ToCharArray(),
            "                                      |                                   ".ToCharArray() //31 long
        };
    }
}