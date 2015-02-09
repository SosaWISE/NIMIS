using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOS.Data;
using SOS.Data.SosCrm;
using SOS.FOS.MerchantServices.Interfaces;
using SOS.FOS.MerchantServices.Models;
using SSE.Lib.Interfaces;
using SSE.Lib.Interfaces.FOS;

namespace SOS.FOS.MerchantServicesUT
{
	[TestClass]
	public class AuthorizeNetUnitTest
	{
		public const string USER_ID = "TestUSER";
		public AuthorizeNetUnitTest()
		{
			/**  Load configuration. */
			string environment = ConfigurationManager.AppSettings["Environment"] ?? Environment.MachineName;
			Lib.Util.Configuration.ConfigurationSettings.Current.SetProperties("Preferences", environment);

			// Setup SubSonic Connections
			SubSonicConfigHelper.SetupConnectionStrings();
		}

		[TestMethod]
		public void AuthCaptureTest()
		{
			/** Initialize. */
			var oMSrv = new MerchantServices.Main();
			var oInvoice = SosCrmDataContext.Instance.AE_Invoices.LoadByPrimaryKey(10000026);
			var oPayment = new AE_Payment
				{
					AccountId = oInvoice.AccountId
					, DocDate = DateTime.Now
					, PostedDate = DateTime.Now
					, OriginalTransactionAmount = oInvoice.OriginalTransactionAmount
					, PaymentTypeId = AE_PaymentType.MetaData.Credit_CardID
				};
			oPayment.Save(USER_ID);
			/** Tie Payment to Invoice. */
			var oAeInvPayment = new AE_InvoicePaymentJoin
				{
					InvoiceId = oInvoice.InvoiceID
					, PaymentId = oPayment.PaymentID
				};
			oAeInvPayment.Save(USER_ID);


			var oFnsInvoicePaymentInfoModel = new FosInvoicePaymentInfoModel(oInvoice, oPayment)
				{
					Amount = oInvoice.OriginalTransactionAmount,
					CardNumber = "4111111111111111",
					ExpMonthYear = "1212",
					Cvv = "123"
				};

			/** Execute a transaction. */
			IFosResult<IFosMerchResponseModel> oResult = oMSrv.CcProcess(oFnsInvoicePaymentInfoModel, USER_ID);

			Assert.IsTrue(oResult.Code == 0);
		}
	}
}
