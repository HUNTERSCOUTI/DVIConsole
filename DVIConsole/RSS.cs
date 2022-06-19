using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace DVIConsole
{
    public class RSS
    {
        public List<string> HLHolder = new List<string>(); // Headlines Holder

        public void RSSLoader()
        {
            const string url = "https://nordjyske.dk/rss/nyheder";

            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);

            reader.Close();

            foreach (SyndicationItem title in feed.Items)
            {
                string hl = title.Title.Text;
                HLHolder.Add(hl);
            }
        }
        public void RunTheLine(int index)
        {
            string allText = string.Join(" ", HLHolder.Select(t => t + new string(' ', 8)));
            List<char> visibleOnConsole = allText.ToList().GetRange(index, 80);
            List<char> notVisible = allText.ToList().GetRange(80, allText.Length - 80);

            Console.SetCursorPosition(1, 34);
            Console.Write(new string(visibleOnConsole.ToArray()));

            var c = visibleOnConsole[0];
            visibleOnConsole.RemoveAt(0);
            notVisible.Add(c);
            visibleOnConsole.Add(notVisible[0]);
            notVisible.RemoveAt(0);
        }
    }
}



