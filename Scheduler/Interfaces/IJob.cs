using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Interfaces
{
	interface IJob
	{
		void Run();
	}

	interface IJob<T>
	{
		void Run(T data);
	}
}
