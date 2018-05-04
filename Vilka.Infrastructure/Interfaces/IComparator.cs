using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vilka.Infrastructure.Interfaces
{
    public interface IComparator<T, O>
    {
        bool Compare(T one, T two, ref O oneOptimization, ref O twoOptimization);
    }
}
