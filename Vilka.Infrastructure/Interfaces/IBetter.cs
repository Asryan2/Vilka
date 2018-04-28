using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vilka.DB;

namespace Vilka.Infrastructure
{
	public interface IBetter
	{
		void FillDB();
		void FillCompareTable();
		Task FillCompareTableAsync();
	}
}
