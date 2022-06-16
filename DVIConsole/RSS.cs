using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DVIConsole
{
    public class RSS
    {
        public RSS() { }
        public RSS(List<string> news)
        {
            foreach (var line in news)
            {
                this.News.Add(line);
            } 
        }

        public List<string> News { get; set; } = new List<string>();

        public void RunTheLine()
        {
            var allText = string.Join(" ", News.Select(t => t + new string(' ', 8)));
            var visibleOnConsole = allText.ToList().GetRange(0, 80);
            var notVisible = allText.ToList().GetRange(80, allText.Length - 80);

            while (true)
            {
                Console.SetCursorPosition(2, 32);
                Console.Write(new string(visibleOnConsole.ToArray()));
                Thread.Sleep(200);
                var c = visibleOnConsole[0];
                visibleOnConsole.RemoveAt(0);
                notVisible.Add(c);
                visibleOnConsole.Add(notVisible[0]);
                notVisible.RemoveAt(0);
            }
        }
    }
}