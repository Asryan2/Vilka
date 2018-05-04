using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vilka.Core;

namespace VilkaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
			VilkaApplicaiton.Run();
			DictionaryBuilder.Build();
			//BetterVivaro better = new BetterVivaro();
			//DateTime start = DateTime.Now;
			//better.FillDB();
			//TimeSpan took = DateTime.Now - start;
			//Console.WriteLine(took.ToString("G"));
			//foreach(Event e in events)
			//{
			//    Console.WriteLine(e.Home + " - " + e.Away + "; League = " + e.League + "; Region = " + e.Region);
			//}
			//test
			Console.ReadLine();
        }
    }
}
