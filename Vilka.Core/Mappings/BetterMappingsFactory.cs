using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vilka.Core
{
	public static class BetterMappingsFactory
	{
		private static Dictionary<Type, BetterMappings> cache = new Dictionary<Type, BetterMappings>();
		public static BetterMappings Get(Type mappingType)
		{
			object instance = Activator.CreateInstance(mappingType);
			if (cache.ContainsKey(mappingType)) return cache[mappingType];
			if (instance is BetterMappings)
			{
				cache.Add(mappingType, (BetterMappings)instance);
				return (BetterMappings)instance;
			}
			throw new InvalidOperationException("Mapping Type is not of type BetterMappings.");
		}
	}
}
