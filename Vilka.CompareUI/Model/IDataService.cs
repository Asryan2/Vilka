using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vilka.CompareUI.Model
{
	public interface IDataService
	{
		void ComperatorGetNextLeague(Action<string, string, string, string, Action<bool>> callback);
	}
}
