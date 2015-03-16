using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dataway.Extensions
{
	public static class EnumHelper
	{
		public static T Parse<T>(string value) where T : struct
		{
			T result = default(T);
			Enum.TryParse<T>(value, out result);
			return result;
		}

		public static T Parse<T>(string value, bool ignoreCase) where T : struct
		{
			T result = default(T);
			Enum.TryParse<T>(value, ignoreCase, out result);
			return result;
		}
	}
}
