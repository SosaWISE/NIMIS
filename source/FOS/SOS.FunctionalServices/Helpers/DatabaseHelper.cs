using SubSonic;
using System;
using System.Transactions;

namespace SOS.FunctionalServices.Helpers
{
	public static class DatabaseHelper
	{
		public static bool UseTransaction(string providerName, Func<bool> action, TimeSpan? timeout = null)
		{
			var provider = DataService.GetInstance(providerName);

			TransactionOptions tranOptions = new TransactionOptions();
			tranOptions.IsolationLevel = IsolationLevel.Serializable;
			tranOptions.Timeout = timeout ?? new TimeSpan(0, 0, 30);
			using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tranOptions))
			{
				using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope(provider))
				{
					if (action())
					{
						// complete the transaction
						ts.Complete();
						return true;
					}
					// not needed since the using statement calls Dispose
					//else
					//{
					//	ts.Dispose();
					//}
					return false;
				}
			}
		}

		//public static void UseTransaction(string providerName, Func<bool> action, TimeSpan? timeout = null)
		//{
		//	using (var ts = CreateTransaction(timeout))
		//	{
		//		UseSharedScope(ts, providerName, action);
		//	}
		//}
		//public static TransactionScope CreateTransaction(TimeSpan? timeout = null)
		//{
		//	TransactionOptions tranOptions = new TransactionOptions();
		//	tranOptions.IsolationLevel = IsolationLevel.Serializable;
		//	tranOptions.Timeout = timeout ?? new TimeSpan(0, 0, 30);
		//	return new TransactionScope(TransactionScopeOption.Required, tranOptions);
		//}
		//public static void UseSharedScope(TransactionScope ts, string providerName, Func<bool> action)
		//{
		//	using (CreateSharedScope(providerName))
		//	{
		//		if (action())
		//		{
		//			// complete the transaction
		//			ts.Complete();
		//		}
		//		// not needed since the using statement calls Dispose
		//		//else
		//		//{
		//		//	ts.Dispose();
		//		//}
		//	}
		//}
		//public static SharedDbConnectionScope CreateSharedScope(string providerName)
		//{
		//	var provider = DataService.GetInstance(providerName);
		//	return new SharedDbConnectionScope(provider);
		//}
	}
}
