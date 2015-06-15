using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NXS.Lib
{
	public class LockBy<T> : IDisposable
	{
		// i can't figure out a way for this to work with auto removing when there are no more locks using the id...
		// http://johnculviner.com/achieving-named-lock-locker-functionality-in-c-4-0/
		//private readonly ConcurrentDictionary<T, Id<T>> _dict = new ConcurrentDictionary<T, Id<T>>();

		private readonly Dictionary<T, Id<T>> _dict;

		public LockBy(IEqualityComparer<T> comparer = null)
		{
			_dict = new Dictionary<T, Id<T>>(comparer);
		}

		bool _disposed = false;
		public void Dispose()
		{
			lock (_dict)
			{
				if (_disposed)
					return;
				_disposed = true;

				_dict.Clear();
			}
		}

		public void Lock(T id, Action lockedAction)
		{
			if (_disposed)
				throw new Exception("SessionStore is disposed");

			Exception error = null;

			Id<T> idlock;
			lock (_dict)
			{
				// try to get current idlock
				if (!_dict.TryGetValue(id, out idlock))
				{
					// create new idlock
					_dict[id] = idlock = new Id<T>(id);
				}
				// increment usage count
				idlock.Count++;
			}

			// lock by id
			lock (idlock)
			{
				try
				{
					// call action
					lockedAction();
				}
				catch (Exception ex)
				{
					// save exception to be thrown later
					error = ex;
				}
			}

			lock (_dict)
			{
				// decrement usage count
				idlock.Count--;
				if (idlock.Count <= 0)
				{
					// remove when there are no pending locks
					_dict.Remove(id);
				}
			}

			if (error != null)
			{
				throw error;
			}
		}

		private class Id<IdType>
		{
			public int Count;
			IdType id;

			public Id(IdType id)
			{
				this.id = id;
			}
		}
	}
}
