using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOS.Data.SosCrm;
using SOS.FOS.MonitoringStationServices.AvantGuard;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FOS.MonitoringStationServicesUT
{
	[TestClass]
	public class UnitTest1
	{
		#region Member Variables

		private const string _TEMPLATE_TRANSMITTER = "XX0501";
		private const string _NEW_TRANSMITTER = "XX0503";

		#endregion Member Variables

		[TestMethod]
		public void CreateAccountFailure ()
		{
			/** Initialize. */
			Helpers.InitializeAndConfigure.Instance.Initialize();

			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();

			/** Check failure of invalid arguments passed. */
			try
			{
				const string szNewTransmitterCode = _NEW_TRANSMITTER;
				oService.CreateMobileAccount(_TEMPLATE_TRANSMITTER, szNewTransmitterCode, null);
			}
			catch (Exception oEx)
			{
				/** Check the exception thrown. */
				Assert.IsTrue(oEx.GetBaseException().GetType() == typeof(MonitoringStationServices.AvantGuard.AGExceptions.AGExceptionDeviceCopy));

				/** Check result. */
				var oAGException = (MonitoringStationServices.AvantGuard.AGExceptions.AGExceptionDeviceCopy) oEx;

				Assert.IsFalse(oAGException.Success, "Success field should have returned false");
				Assert.IsTrue(oAGException.Message.Equals("End Xmit# is Required"), string.Format("UserErrorMessage should be 'End Xmit# is Required', it is: '{0}'", oAGException.Message));
				Assert.IsTrue(oAGException.ErrorTypeNum == 0, string.Format("ErrorTypeNum should be '0', it is: '{0}'", oAGException.ErrorTypeNum));
			}
		}

		[TestMethod]
		public void CreateAccountSuccess()
		{
			/** Initialize. */
			Helpers.InitializeAndConfigure.Instance.Initialize();

			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();

			/** Check failure of invalid arguments passed. */
			try
			{
				const string szNewTransmitterCode = _NEW_TRANSMITTER;
				var oResult = oService.CreateMobileAccount(_TEMPLATE_TRANSMITTER, szNewTransmitterCode, szNewTransmitterCode);

				/** Check result. */
				var oAGResponse = (IFnsAGResponseBase) oResult.GetValue();
				Assert.IsTrue(oAGResponse.Success, "Success field should have returned true");
				Assert.IsTrue(string.IsNullOrWhiteSpace(oAGResponse.UserErrorMessage));
				Assert.IsTrue(oAGResponse.ErrorTypeNum == 0, string.Format("ErrorTypeNum should be '0', it is: '{0}'", oAGResponse.ErrorTypeNum));
			}
			catch (Exception oEx)
			{
				/** Check the exception thrown. */
				Assert.IsTrue(false, oEx.Message);
			}
		}

		[TestMethod]
		public void CreateAccountExisting()
		{
			/** Initialize. */
			Helpers.InitializeAndConfigure.Instance.Initialize();

			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();

			/** Check failure of invalid arguments passed. */
			try
			{
				const string szNewTransmitterCode = _NEW_TRANSMITTER;
				oService.CreateMobileAccount(_TEMPLATE_TRANSMITTER, szNewTransmitterCode, szNewTransmitterCode);
			}
			catch (Exception oEx)
			{
				/** Check the exception thrown. */
				Assert.IsTrue(oEx.GetBaseException().GetType() == typeof(AGExceptions.AGExceptionDeviceCopy));

				/** Check result. */
				var oAGException = (AGExceptions.AGExceptionDeviceCopy)oEx;

				Assert.IsFalse(oAGException.Success, "Success field should have returned false");
				const string SZ_ERRO_MSG = "One or more Transmitters in the range already exist in the database.";
				Assert.IsTrue(oAGException.Message.Equals("One or more Transmitters in the range already exist in the database."), string.Format("UserErrorMessage should be '{0}', it is: '{1}'", SZ_ERRO_MSG, oAGException.Message));
				Assert.IsTrue(oAGException.ErrorTypeNum == 0, string.Format("ErrorTypeNum should be '0', it is: '{0}'", oAGException.ErrorTypeNum));
			}
		}

		[TestMethod]
		public void SetOOS()
		{
			/** Initialize. */
			Helpers.InitializeAndConfigure.Instance.Initialize();
			var cs = new MonitoringStationServices.AvantGuard.CentralStation();

			/** Check failure of invalid arguments passed. */
			try
			{
				var indAcct = SosCrmDataContext.Instance.MS_IndustryAccounts.LoadByPrimaryKey(10179);  // This contains the I33502 CSID.

				string oosCat = null;  // This turns it on.
				DateTime? startDate = null;
				string startTime = null;
				const string COMMENT_BIG = "This is a unit test call";
				const string GP_EMPLOYEE_ID = "SOSA001";
				const string GP_TECH_ID = "SYST001";

				var msSubmit = new MS_AccountSubmit
				{
					AccountId = indAcct.AccountId,
					IndustryAccountId = indAcct.IndustryAccountID,
					AccountSubmitTypeId = (short) MS_AccountSubmitType.AccountSubmitTypeEnum.Undefined,
					GPTechId = GP_TECH_ID,
					MonitoringStationOSId = indAcct.ReceiverLine.MonitoringStationOSId,
					DateSubmitted = DateTime.Now
				};
				msSubmit.Save(GP_EMPLOYEE_ID);
				oosCat = AGOOSTypes.ACTIVE;
				// ReSharper disable ExpressionIsAlwaysNull
				var result = cs.AccountServiceStatusSet(msSubmit, oosCat, startDate, startTime, COMMENT_BIG, GP_EMPLOYEE_ID);
				// ReSharper restore ExpressionIsAlwaysNull

				Assert.IsTrue(result.Code == 0, string.Format("The following message was returned: {0}", result.Message));

				/**
				 * Turn Service On Pending. */
				msSubmit = new MS_AccountSubmit
				{
					AccountId = indAcct.AccountId,
					IndustryAccountId = indAcct.IndustryAccountID,
					AccountSubmitTypeId = (short)MS_AccountSubmitType.AccountSubmitTypeEnum.Undefined,
					GPTechId = GP_TECH_ID,
					MonitoringStationOSId = indAcct.ReceiverLine.MonitoringStationOSId,
					DateSubmitted = DateTime.Now
				};
				msSubmit.Save(GP_EMPLOYEE_ID);
				oosCat = AGOOSTypes.PENDING;
				startDate = DateTime.Now;
				// ReSharper disable ExpressionIsAlwaysNull
				result = cs.AccountServiceStatusSet(msSubmit, oosCat, startDate, startTime, COMMENT_BIG, GP_EMPLOYEE_ID);
				// ReSharper restore ExpressionIsAlwaysNull

				Assert.IsTrue(result.Code == 0, string.Format("The following message was returned: {0}", result.Message));

				/**
				 * Turn Service On Cancel. */
				msSubmit = new MS_AccountSubmit
				{
					AccountId = indAcct.AccountId,
					IndustryAccountId = indAcct.IndustryAccountID,
					AccountSubmitTypeId = (short)MS_AccountSubmitType.AccountSubmitTypeEnum.Undefined,
					GPTechId = GP_TECH_ID,
					MonitoringStationOSId = indAcct.ReceiverLine.MonitoringStationOSId,
					DateSubmitted = DateTime.Now
				};
				msSubmit.Save(GP_EMPLOYEE_ID);
				oosCat = AGOOSTypes.CANCEL;
				startDate = DateTime.Now;
				// ReSharper disable ExpressionIsAlwaysNull
				result = cs.AccountServiceStatusSet(msSubmit, oosCat, startDate, startTime, COMMENT_BIG, GP_EMPLOYEE_ID);
				// ReSharper restore ExpressionIsAlwaysNull

				Assert.IsTrue(result.Code == 0, string.Format("The following message was returned: {0}", result.Message));
			}
			catch (Exception oEx)
			{
				Assert.IsTrue(false, string.Format("The following exception was thrown: {0}", oEx.Message));
			}
		}
	}
}
