using System;
using System.Collections.Generic;
using System.Globalization;
using SOS.Data.SosCrm;
using SOS.Lib.Util.Configuration;

namespace SOS.Lib.LaipacAPI.Helper
{
	public class MsAccounts
	{
		#region .ctor
		
		private MsAccounts()
		{
		}

		#endregion .ctor

		#region Memeber Variables 

		private const int _CACHE_EXPIRES_IN_MINUTES = 30;
		public Dictionary<long, GS_Account> AccountsList { get; private set; }
		public long CacheExpiresInMinutes {
			get 
			{
				long result;
				if (!long.TryParse(ConfigurationSettings.Current.GetConfig("LAIPACAPI.AccountUnitCacheExpiresIn"), out result))
				{
					result = _CACHE_EXPIRES_IN_MINUTES;
				}

				/** Return result. */
				return result;
			}
		}

		public DateTime CacheStart = DateTime.Now;

		#endregion Memeber Variables 

		#region Member Functions

		public GS_Account FindByUnitID(long lUnitID)
		{
			/** Check if it's present. */
			if (AccountsList == null || CacheStart.AddMinutes(CacheExpiresInMinutes) <= DateTime.Now) AccountsList = new Dictionary<long, GS_Account>();

			if (AccountsList.ContainsKey(lUnitID))
				return AccountsList[lUnitID];

			/** Find the new Unit. */
			GS_Account oAccount = GPSUnit.GetMsAccount(lUnitID.ToString(CultureInfo.InvariantCulture));

			/** Add if account is not null to the dictionary. */
			if (oAccount != null) AccountsList.Add(lUnitID, oAccount);

			/** Default value. */
			return oAccount;
		}

		#endregion Member Functions
	}
}
