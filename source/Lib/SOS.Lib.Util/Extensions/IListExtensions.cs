using System;
using System.Collections.Generic;

namespace SOS.Lib.Util.Extensions
{
	public static class IListExtensions
	{
		public static List<TOutput> ConvertAll<T, TOutput>(this IList<T> list, Converter<T, TOutput> converter)
		{
			if (list is List<T>)
			{
				return ((List<T>)list).ConvertAll<TOutput>(converter);
			}

			// Default execution path
			var oResult = new List<TOutput>();
			foreach (var v in list)
			{
				oResult.Add(converter(v));
			}


			return oResult;
		}
	}
}