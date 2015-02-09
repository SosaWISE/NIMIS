using System;
// ReSharper disable once CheckNamespace


namespace SOS.Data.SosCrm
{
// ReSharper disable once InconsistentNaming
	public partial class MS_AccountSubmit
	{
		public static MS_AccountSubmit CreateFromParent(MS_AccountSubmit parentAccountSubmit, short accountSubmitTypeId, bool wasSuccessfull = false)
		{
			var accountSubmit = new MS_AccountSubmit
			{
				AccountSubmitTypeId = accountSubmitTypeId,
				AccountId = parentAccountSubmit.AccountId,
				IndustryAccountId = parentAccountSubmit.IndustryAccountId,
				MonitoringStationOSId = parentAccountSubmit.MonitoringStationOSId,
				GPTechId = parentAccountSubmit.GPTechId,
				DateSubmitted = DateTime.UtcNow,
				WasSuccessfull = wasSuccessfull,
				CreatedBy = parentAccountSubmit.CreatedBy,
				CreatedOn = DateTime.UtcNow
			};

			// ** Return result
			return accountSubmit;
		}
	}
}
