using System;

namespace SOS.Lib.Util
{
	public class EventArgs<T> : EventArgs
	{
		public EventArgs()
		{
		}

		public EventArgs(T value)
		{
			Value = value;
		}

		public T Value { get; set; }
	}
}