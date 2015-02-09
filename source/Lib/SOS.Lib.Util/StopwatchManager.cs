using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SOS.Lib.Util
{
	public class TimeSpanDictionary
	{
		public TimeSpanDictionary()
		{
			ElapsedTimes = new Dictionary<string, TimeSpan>();
		}

		public TimeSpan TotalElapsed { get; set; }

		public Dictionary<string, TimeSpan> ElapsedTimes { get; private set; }

		public void Add(string key, TimeSpan elapsedTime)
		{
			ElapsedTimes.Add(key, elapsedTime);
		}
	}

	public class StopwatchManager
	{
		/// <summary>
		/// Keeps track of time for each operation done during the total time
		/// </summary>
		private Stopwatch _innerWatch;

		/// <summary>
		/// Stores elapsed times
		/// </summary>
		private TimeSpanDictionary _timeDict;

		/// <summary>
		/// Keeps track of total time
		/// </summary>
		private Stopwatch _totalWatch;

		public StopwatchManager(bool start)
		{
			if (start) Start();
		}

		public void Start()
		{
			_timeDict = new TimeSpanDictionary();
			_totalWatch = Stopwatch.StartNew();
			_innerWatch = Stopwatch.StartNew();
		}

		public void RestartInner(string nameKey)
		{
			StopInner(nameKey);
			StartInner();
		}

		public void StopInner(string nameKey)
		{
			_innerWatch.Stop();
			_timeDict.Add(nameKey, _innerWatch.Elapsed);
		}

		public void StartInner()
		{
			_innerWatch.Reset();
			_innerWatch.Start();
		}

		public TimeSpanDictionary Finish()
		{
			_totalWatch.Stop();
			_timeDict.TotalElapsed = _totalWatch.Elapsed;

			TimeSpanDictionary result = _timeDict;

			//reset values
			_timeDict = null;
			_totalWatch = null;
			_innerWatch = null;

			return result;
		}
	}
}