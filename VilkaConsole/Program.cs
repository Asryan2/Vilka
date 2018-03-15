using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VilkaConsole.Helpers;

namespace VilkaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Better888 better = new Better888();
			DateTime start = DateTime.Now;
			better.FillDB();
			//var a = Comparator.Similarity("FK Cukaricki", "FK Cukaricki");
			TimeSpan took = DateTime.Now - start;
			Console.WriteLine(took.ToString("G"));
            //foreach(Event e in events)
            //{
            //    Console.WriteLine(e.Home + " - " + e.Away + "; League = " + e.League + "; Region = " + e.Region);
            //}
			//test
            Console.ReadLine();
        }
    }
}
