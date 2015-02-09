using System;
using System.Globalization;
using SOS.Data.SosCrm;
using SOS.FOS.MerchantServices.Helper;
using SOS.FOS.MerchantServices.Interfaces;
using SOS.FOS.MerchantServices.Models;
using SOS.Lib.AuthorizeNet.AIM.Responses;
using SSE.Lib.Interfaces;
using SSE.Lib.Interfaces.FOS;

namespace SOS.FOS.MerchantServices
{
	public class Main
	{
		public IFosResult<IFosMerchResponseModel> CcProcess(IFosInvoicePaymentInfoModel oFnsInvoicePaymentInfoModel, string sUserId)
		{
			/** Initialize. */
			var oResult = new FosResultModel<IFosMerchResponseModel>((int) ErrorCodes.Initializing
				, string.Format(ErrorCodes.Initializing.Message(), "CcProcess"));

			var oFosResponse = new FosMerchResponseModel();
			var oAuthReq = new Lib.AuthorizeNet.AIM.Requests.AuthorizationRequest(oFnsInvoicePaymentInfoModel.CardNumber
				, oFnsInvoicePaymentInfoModel.ExpMonthYear, oFnsInvoicePaymentInfoModel.Amount
				, oFnsInvoicePaymentInfoModel.Description);
			// TODO: Figure out how to pick the MG. 
			MG_AuthorizeNetConfig oMg = SosCrmDataContext.Instance.MG_AuthorizeNetConfigs.LoadByPrimaryKey(1);

			/** Add Card Code. */
			oAuthReq.AddCardCode(oFnsInvoicePaymentInfoModel.Cvv);
			/** Add Invoice Number. */
			oAuthReq.AddInvoice(oFnsInvoicePaymentInfoModel.Invoice.InvoiceID.ToString(CultureInfo.InvariantCulture));
			/** Add Purchase ID. */
			oAuthReq.AddMerchantValue("PurchaseID", oFnsInvoicePaymentInfoModel.Payment.PaymentID.ToString(CultureInfo.InvariantCulture));

			/** Create Auth Gateway. */
			var oGate = new Lib.AuthorizeNet.AIM.Gateway(oMg.ApiLogin, oMg.TransactionKey, oMg.IsTestMode);

			/** Initialize Execution. */
			var oTrans = new MG_Transaction
				{
					GatewayId = MG_Gateway.MetaData.AuthorizeNetID
					, InvoiceId = oFnsInvoicePaymentInfoModel.Invoice.InvoiceID
					, Success = false
				};
			oTrans.Save(sUserId);
			oFnsInvoicePaymentInfoModel.Payment.TransactionId = oTrans.TransactionID;
			oFnsInvoicePaymentInfoModel.Payment.TransactionSuccess = false;
			oFnsInvoicePaymentInfoModel.Payment.Save(sUserId);

			var oTranAuthorizeNet = new MG_AuthorizeNetTransaction
			{
				TransactionId = oTrans.TransactionID
				, AuthorizeNetConfigId = oMg.AuthorizeNetConfigID
			};

			/** EXECUTE TRANSACTION. */
			IGatewayResponse oResponse = oGate.Send(oAuthReq);

			/** Validate response. */
			if (oResponse == null)
			{
				oTranAuthorizeNet.Save(sUserId);
				oResult.Code = (int) ErrorCodes.MerchantReturnedNull;
				oResult.Message = string.Format(ErrorCodes.MerchantReturnedNull.Message(), "AuthorizeNet");
				return oResult;
			}
			
			/** Save Response to Transaction Table. */
			oTranAuthorizeNet.AuthTransactionID = oResponse.TransactionID;
			oTranAuthorizeNet.InvoiceNumber = oResponse.InvoiceNumber;
			oTranAuthorizeNet.TransationType = ((GatewayResponse)(oResponse)).TransactionType;
			oTranAuthorizeNet.Method = ((GatewayResponse)(oResponse)).Method;
			oTranAuthorizeNet.Amount = oResponse.Amount;
			oTranAuthorizeNet.Approved = oResponse.Approved;
			oTranAuthorizeNet.AuthorizationCode = oResponse.AuthorizationCode;
			oTranAuthorizeNet.CardNumber = oResponse.CardNumber;
			oTranAuthorizeNet.Message = oResponse.Message;
			oTranAuthorizeNet.ResponseCode = oResponse.ResponseCode;
			oTranAuthorizeNet.MD5Hash = ((GatewayResponse)(oResponse)).MD5Hash;
			oTranAuthorizeNet.Save(sUserId);

			/** Handles if successful or not. */
			if (oResponse.ResponseCode.Equals("1")) // This is a successfull transaction.
			{
				oResult.Code = (int) ErrorCodes.Success;
				oResult.Message = string.Format(ErrorCodes.Success.Message());
				oFnsInvoicePaymentInfoModel.Payment.TransactionSuccess = true;
				oFnsInvoicePaymentInfoModel.Payment.ActualTransactionAmount = oResponse.Amount;
				oFnsInvoicePaymentInfoModel.Payment.PostedDate = DateTime.Now;
				oFnsInvoicePaymentInfoModel.Payment.Save(sUserId);
				oFnsInvoicePaymentInfoModel.Invoice.CurrentTransactionAmount = oResponse.Amount;
				oTrans.Success = true;
				oTrans.Save(sUserId);
			}
			else
			{
				oFnsInvoicePaymentInfoModel.Payment.TransactionSuccess = false;
				oResult.Code = (int)ErrorCodes.MerchantTransFailed;
				oResult.Message = string.Format(ErrorCodes.MerchantTransFailed.Message(), "AuthorizeNet"
					, oResponse.Message);
			}

			/** Acquire the result value. */
			oFosResponse.Amount = oResponse.Amount;
			oFosResponse.Approved = oResponse.Approved;
			oFosResponse.AuthorizationCode = oResponse.AuthorizationCode;
			oFosResponse.CardNumber = oResponse.CardNumber;
			oFosResponse.InvoiceNumber = oResponse.InvoiceNumber;
			oFosResponse.Message = oResponse.Message;
			oFosResponse.ResponseCode = oResponse.ResponseCode;
			oFosResponse.TransactionID = oResponse.TransactionID;
			oResult.Value = oFosResponse;
			
			/** Return result. */
			return oResult;
		}

		public IFosResult<IFosMerchResponseModel> ECheck(IFosInvoicePaymentInfoModel oFnsInvoicePaymentInfoModel, string sUserId)
		{
			/** Initialize. */
			var oResult = new FosResultModel<IFosMerchResponseModel>((int)ErrorCodes.Initializing
				, string.Format(ErrorCodes.Initializing.Message(), "ECheck"));

			/** TODO: Add all business logic.  For now we will return successfull. */
			var oFosResponse = new FosMerchResponseModel();

			/** Initialize Execution. */
			//MG_AuthorizeNetConfig oMg = SosCrmDataContext.Instance.MG_AuthorizeNetConfigs.LoadByPrimaryKey(1);

			var oTrans = new MG_Transaction
				{
					GatewayId = MG_Gateway.MetaData.AuthorizeNetID
					, InvoiceId = oFnsInvoicePaymentInfoModel.Invoice.InvoiceID
					, Success = false
				};
			oTrans.Save(sUserId);
			oFnsInvoicePaymentInfoModel.Payment.TransactionId = oTrans.TransactionID;
			oFnsInvoicePaymentInfoModel.Payment.TransactionSuccess = false;
			oFnsInvoicePaymentInfoModel.Payment.Save(sUserId);

			//var oTranAuthorizeNet = new MG_AuthorizeNetTransaction
			//{
			//    TransactionId = oTrans.TransactionID
			//    , AuthorizeNetConfigId = oMg.AuthorizeNetConfigID
			//};

			/********** START SUBMIT TO MG HERE...  **********/
			/********** END   SUBMIT TO MG HERE...  **********/

			/** Perform decision here if the transaction was successfull. */
			oFnsInvoicePaymentInfoModel.Payment.TransactionSuccess = false;
			oResult.Code = (int)ErrorCodes.Success;
			oResult.Message = string.Format(ErrorCodes.Success.Message());
			oFnsInvoicePaymentInfoModel.Payment.TransactionSuccess = true;
			oFnsInvoicePaymentInfoModel.Payment.ActualTransactionAmount = oFnsInvoicePaymentInfoModel.Amount;
			oFnsInvoicePaymentInfoModel.Payment.PostedDate = DateTime.Now;
			oFnsInvoicePaymentInfoModel.Payment.Save(sUserId);
			oFnsInvoicePaymentInfoModel.Invoice.CurrentTransactionAmount = oFnsInvoicePaymentInfoModel.Amount;
			oTrans.Success = true;
			oTrans.Save(sUserId);

			/** Save result information to be returned. */
			oFosResponse.Amount = oFnsInvoicePaymentInfoModel.Amount;
			oFosResponse.Approved = true;
			oFosResponse.InvoiceNumber = oFnsInvoicePaymentInfoModel.Invoice.InvoiceID.ToString(CultureInfo.InvariantCulture);
			oFosResponse.Message = "Check has been processed.";
			oFosResponse.TransactionID = oTrans.TransactionID.ToString(CultureInfo.InvariantCulture);
			oResult.Value = oFosResponse;

			/** Return result. */
			return oResult;
		}
	}
}
