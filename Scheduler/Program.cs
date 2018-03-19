using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduler
{
	class Program
	{
		static void Main(string[] args)
		{
			DeleteLiveJob job = new DeleteLiveJob();
			Task.Run(new Action(() => {
				while (true)
				{
					job.Run();
					Thread.Sleep(600000);
				}
			}));
			while (true)
			{
				Thread.Sleep(600000);
			}
		}
	}
}
