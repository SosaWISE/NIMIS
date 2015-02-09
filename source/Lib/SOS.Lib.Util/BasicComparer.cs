using System;
using System.Collections;
using System.Collections.Generic;

namespace SOS.Lib.Util
{
	public class BasicComparer<T> : IComparer<T>, IComparer where T : class
	{
		public BasicComparer(Comparison<T> comparison)
		{
			Comparison = comparison;
		}

		public Comparison<T> Comparison { get; set; }

		#region IComparer Members

		public int Compare(object x, object y)
		{
			return Compare(x as T, y as T);
		}

		#endregion

		#region IComparer<T> Members

		public int Compare(T x, T y)
		{
			if (Comparison != null)
			{
				return Comparison(x, y);
			}
			else
			{
				return 0;
			}
		}

		#endregion
	}
}