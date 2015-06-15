using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Lib.Authentication
{
	public interface IFarmTokenKeyStore
	{
		// Summary:
		//     Purges encryption keys
		void Purge();
		//
		// Summary:
		//     Retrieves encryption keys
		//
		// Returns:
		//     Keys
		IDictionary<DateTime, byte[]> Retrieve();
		//
		// Summary:
		//     Remove expired encryption keys and possibly adds a new key
		//
		// Parameters:
		//   purgeExpiration:
		//     If keys are older than this date they will be removed
		//   validExpiration:
		//     If keys are older than this date they are no longer valid
		//   newKey:
		//     If there are no keys after removing expired and invalid keys, this key will be added
		//
		// Returns:
		//     Keys
		IDictionary<DateTime, byte[]> Update(DateTime purgeExpiration, DateTime validExpiration, byte[] newKey);
	}
}
