using System;

namespace SOS.FunctionalServices.Helpers
{
	public static class FieldAssignment
	{
		/// <summary>
		/// This will check to see if the values are different and is we should call the action.
		/// </summary>
		/// <param name="newValue">int</param>
		/// <param name="oldValue">int</param>
		/// <param name="action">function</param>
		public static void AssignIfDirty(int newValue, int oldValue, Action action)
		{
			if (newValue == 0) return;
			if (newValue != oldValue) action();
		}

		public static void AssignIfDirty(long newValue, long oldValue, Action action)
		{
			if (newValue == 0) return;
			if (newValue != oldValue) action();
		}

		public static void AssignIfDirty(string newValue, string oldValue, Action action)
		{
			if (string.IsNullOrEmpty(newValue)) return;
			if (!newValue.Equals(oldValue)) action();
		}

		public static void AssignIfDirty(bool newValue, bool oldValue, Action action)
		{
			if (!newValue.Equals(oldValue)) action();
		}

		public static void AssignIfDirty(DateTime? newValue, DateTime? oldValue, Action action)
		{
			if (!newValue.Equals(oldValue)) action();
		}

		public static void AssignIfDirty<T>(T newValue, T oldValue, Action action)
		{
			if (!newValue.Equals(oldValue)) action();
		}

	}
}
