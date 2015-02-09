using System;

namespace SOS.FOS.MonitoringStationServices.Utilities.Exceptions
{
	public class CsExceptionNoContract : Exception
	{
		#region .ctor

		public CsExceptionNoContract(long accountId, string csNo)
			: base(string.Format("The account with msAccountId: '{0}' and CsNo of '{1}' does not have a contract.  Please go back to Sales Information and assign it a contract length.", accountId, csNo))
		{
		}

		#endregion .ctor

	}
}
