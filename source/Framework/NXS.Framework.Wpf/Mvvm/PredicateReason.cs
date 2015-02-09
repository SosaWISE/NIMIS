using System;

namespace NXS.Framework.Wpf.Mvvm
{
	public class PredicateReason<T>
	{
		public Predicate<T> Predicate { get; private set; }
		public string InvalidReason { get; private set; }

		public PredicateReason(Predicate<T> predicate, string invalidReason)
		{
			if (predicate == null)
				throw new ArgumentNullException("predicate");
			if (invalidReason == null)
				throw new ArgumentNullException("invalidReason");

			this.Predicate = predicate;
			this.InvalidReason = invalidReason;
		}
	}
}
