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

        public void RunTheLine(int index)
        {
            var allText = string.Join(" ", News.Select(t => t + new string(' ', 8)));
            var visibleOnConsole = allText.ToList().GetRange(index, 80);
            var notVisible = allText.ToList().GetRange(80, allText.Length - 80);


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