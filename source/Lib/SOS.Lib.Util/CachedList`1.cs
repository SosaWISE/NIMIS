using System;
using System.Collections.Generic;

namespace SOS.Lib.Util
{
	public class CachedList<T>
	{
		private readonly Func<List<T>> _getList;
		private readonly object _syncRoot = new object();
		private List<T> _list;

		public CachedList(Func<List<T>> getList)
		{
			_getList = getList;
		}

		public List<T> List
		{
			get
			{
				if (_list == null)
				{
					lock (_syncRoot)
					{
						if (_list == null)
						{
							_list = _getList();
						}
					}
				}
				return _list;
			}
		}

		public void ClearCache()
		{
			_list = null;
		}
	}
}