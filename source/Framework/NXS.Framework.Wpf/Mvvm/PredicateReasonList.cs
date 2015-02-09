using System;
using System.Collections.Generic;

namespace NXS.Framework.Wpf.Mvvm
{
	public class PredicateReasonList<T> : IEnumerable<PredicateReason<T>>
	{
		List<PredicateReason<T>> _list;
		public PredicateReasonList()
		{
			_list = new List<PredicateReason<T>>();
		}

		public static PredicateReasonList<T> Create()
		{
			return new PredicateReasonList<T>();
		}
		public PredicateReasonList<T> Add(Predicate<T> predicate, string invalidReason)
		{
			return this.Add(new PredicateReason<T>(predicate, invalidReason));
		}
		public PredicateReasonList<T> Add(PredicateReason<T> predicateReason)
		{
			if (predicateReason == null)
				throw new ArgumentNullException("predicateReason");

			_list.Add(predicateReason);
			return this;
		}

		#region IEnumerable<PredicateReason<T>> Members

		public IEnumerator<PredicateReason<T>> GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
