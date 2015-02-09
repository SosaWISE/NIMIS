/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 05/07/14
 * Time: 10:37am
 * 
 * Description:  All Qualify Objects are here.
 *********************************************************************************************************************/

using System;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.QualifyLead;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.QualifyLead;

namespace SOS.FunctionalServices
{
	public class QualifyLeadServices : IQualifyLeadServices
	{
		#region QlAddress CRUD

		private QL_Address LoadFromFns(object addressRaw, QL_Address result = null)
		{
			// ** Initialize
			var fnsAddress = (IFnsQlAddress)addressRaw;
			if (result == null)
			{
				result = fnsAddress.AddressID > 0 
					? SosCrmDataContext.Instance.QL_Addresses.LoadByPrimaryKey(fnsAddress.AddressID) 
					: new QL_Address();
			}

			// ** Bind data
			result.DealerId = fnsAddress.DealerId;
			result.AddressTypeId = fnsAddress.AddressTypeId;
			result.AddressValidationStateId = fnsAddress.AddressValidationStateId;
			result.CarrierRoute = fnsAddress.CarrierRoute;
			result.City = fnsAddress.City;
			result.CongressionalDistric = fnsAddress.CongressionalDistric;
			result.CountryId = fnsAddress.CountryId;
			result.County = fnsAddress.County;
			result.CountyCode = fnsAddress.CountyCode;
			result.DeliveryPoint = fnsAddress.DeliveryPoint;
			result.DPV = fnsAddress.DPV;
			result.DPVFootnote = fnsAddress.DPVFootnote;
			result.DPVResponse = fnsAddress.DPVResponse;
			result.Extension = fnsAddress.Extension;
			result.ExtensionNumber = fnsAddress.ExtensionNumber;
			result.Latitude = fnsAddress.Latitude;
			result.Longitude = fnsAddress.Longitude;
			result.Phone = fnsAddress.Phone;
			result.PlusFour = fnsAddress.PlusFour;
			result.PostalCode = fnsAddress.PostalCode;
			result.PostalCodeFull = fnsAddress.PostalCodeFull;
			result.PostDirectional = fnsAddress.PostDirectional;
			result.PreDirectional = fnsAddress.PreDirectional;
			result.SalesRepId = fnsAddress.SalesRepId;
			result.SeasonId = fnsAddress.SeasonId;
			result.StateId = fnsAddress.StateId;
			result.StreetAddress = fnsAddress.StreetAddress;
			result.StreetAddress2 = fnsAddress.StreetAddress2;
			result.StreetName = fnsAddress.StreetName;
			result.StreetNumber = fnsAddress.StreetNumber;
			result.StreetType = fnsAddress.StreetType;
			result.TeamLocationId = fnsAddress.TeamLocationId;
			result.TimeZoneId = fnsAddress.TimeZoneId;
			result.Urbanization = fnsAddress.Urbanization;
			result.UrbanizationCode = fnsAddress.UrbanizationCode;
			result.ValidationVendorId = fnsAddress.ValidationVendorId;
			//result.IsActive = fnsAddress.IsActive;
			//result.IsDeleted = fnsAddress.IsDeleted;
			//result.CreatedBy = fnsAddress.CreatedBy;
			//result.CreatedOn = fnsAddress.CreatedOn;

			// ** Return result 
			return result;
		}

		public IFnsResult<IFnsQlAddress> QlAddressCreate(IFnsQlAddress address, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QlAddressCreate";
			var result = new FnsResult<IFnsQlAddress>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var qlAddress = SosCrmDataContext.Instance.QL_Addresses.BindData(address, null, LoadFromFns);
				qlAddress.Save(gpEmployeeId);
				var resultValue = new FnsQlAddress(qlAddress);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQlAddress>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsQlAddress> QlAddressRead(long addressId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QlAddressRead";
			var result = new FnsResult<IFnsQlAddress>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var qlAddress = SosCrmDataContext.Instance.QL_Addresses.LoadByPrimaryKeySoft(addressId);
				if (qlAddress == null)
				{
					result.Code = (int)ErrorCodes.SqlItemNotFound;
					result.Message = "Item not found.";
					result.Value = null;
					return result;
				}

				var resultValue = new FnsQlAddress(qlAddress);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQlAddress>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsQlAddress> QlAddressUpdate(IFnsQlAddress fnsAddress, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QlAddressUpdate";
			var result = new FnsResult<IFnsQlAddress>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var qlAddress = SosCrmDataContext.Instance.QL_Addresses.LoadByPrimaryKey(fnsAddress.AddressID);
				qlAddress = SosCrmDataContext.Instance.QL_Addresses.BindData(fnsAddress, qlAddress, LoadFromFns);
				qlAddress.Save(gpEmployeeId);
				var resultValue = new FnsQlAddress(qlAddress);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQlAddress>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsQlAddress> QlAddressDelete(long addressId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QlAddressDelete";
			var result = new FnsResult<IFnsQlAddress>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var qlAddress = SosCrmDataContext.Instance.QL_Addresses.LoadByPrimaryKey(addressId);
				qlAddress.IsDeleted = true;
				qlAddress.Save(gpEmployeeId);
				var resultValue = new FnsQlAddress(qlAddress);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQlAddress>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion QlAddress CRUD

		#region QlQualifyCustomerInfoRead
		public IFnsResult<IFnsQlQualifyCustomerInfo> QlQualifyCustomerInfoReadByLeadId(long leadId, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QlQualifyCustomerInfoReadByLeadId";
			var result = new FnsResult<IFnsQlQualifyCustomerInfo>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var qlQualifyCustomerInfo = SosCrmDataContext.Instance.QL_QualifyCustomerInfoViews.LoadByLeadId(leadId);
				if (qlQualifyCustomerInfo == null)
				{
					result.Code = (int)ErrorCodes.SqlItemNotFound;
					result.Message = "Qualify Lead Info not found.";
					result.Value = null;
					return result;
				}

				var resultValue = new FnsQlQualifyCustomerInfo(qlQualifyCustomerInfo);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQlQualifyCustomerInfo>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsQlQualifyCustomerInfo> QlQualifyCustomerInfoReadByCustomerId(long customerId, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QlQualifyCustomerInfoReadByCustomerId";
			var result = new FnsResult<IFnsQlQualifyCustomerInfo>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var qlQualifyCustomerInfo = SosCrmDataContext.Instance.QL_QualifyCustomerInfoViews.LoadByCustomerId(customerId);
				if (qlQualifyCustomerInfo == null)
				{
					result.Code = (int)ErrorCodes.SqlItemNotFound;
					result.Message = "Item not found.";
					result.Value = null;
					return result;
				}

				var resultValue = new FnsQlQualifyCustomerInfo(qlQualifyCustomerInfo);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQlQualifyCustomerInfo>
				{
					Code = (int)ErrorCodes.UnexpectedException
					,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsQlQualifyCustomerInfo> QlQualifyCustomerInfoReadByAccountId(long accountId, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "QlQualifyCustomerInfoReadByAccountId";
			var result = new FnsResult<IFnsQlQualifyCustomerInfo>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var qlQualifyCustomerInfo = SosCrmDataContext.Instance.QL_QualifyCustomerInfoViews.LoadByAccountId(accountId);
				if (qlQualifyCustomerInfo == null)
				{
					result.Code = (int)ErrorCodes.SqlItemNotFound;
					result.Message = "Qualify Lead Info not found.";
					result.Value = null;
					return result;
				}

				var resultValue = new FnsQlQualifyCustomerInfo(qlQualifyCustomerInfo);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQlQualifyCustomerInfo>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion QlQualifyCustomerInfoRead
	}
}
