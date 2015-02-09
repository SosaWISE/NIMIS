using System;
using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.QualifyLead;
using SOS.Lib.Util.Extensions;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.Qualify
{
	[RoutePrefix("QualifySrv")]
	public class AddressController : ApiController
	{
		#region Public REST CRUD
		// GET api/address/5
		[Route("Addresses/{id}")]
		[HttpGet]
		public CmsCORSResult<QlAddress> Get(long id)
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "QlAddress Get";
			var result = new CmsCORSResult<QlAddress>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region TRY

					try
					{
						// ** Create Service
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IQualifyLeadServices>();
						IFnsResult<IFnsQlAddress> oFnsModel = oService.QlAddressRead(id, user.GPEmployeeID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oQlAddress = ConvertTo.CastFnsToQlAddress((IFnsQlAddress)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oQlAddress;
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result
				});
		}

        // POST api/address
		[Route("Addresses/")]
		[HttpPost]
		public CmsCORSResult<QlAddress> Post([FromBody]QlAddress value)
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "QlAddress Post";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>();
				argArray.Add(new CORSArg(value.DealerId, (value.DealerId == 0), "<li>'DealerId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.ValidationVendorId, (string.IsNullOrEmpty(value.ValidationVendorId)), "<li>'ValidationVendorId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.AddressValidationStateId, (string.IsNullOrEmpty(value.AddressValidationStateId)), "<li>'AddressValidationStateId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.StateId, (string.IsNullOrEmpty(value.StateId)), "<li>'StateId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.CountryId, (string.IsNullOrEmpty(value.CountryId)), "<li>'CountryId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.TimeZoneId, (value.TimeZoneId == 0), "<li>'TimeZoneId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.AddressTypeId, (string.IsNullOrEmpty(value.AddressTypeId)), "<li>'AddressTypeId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.SeasonId, (value.SeasonId == 0), "<li>'SeasonId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.TeamLocationId, (value.TeamLocationId == 0), "<li>'TeamLocationId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.SalesRepId, (string.IsNullOrEmpty(value.SalesRepId)), "<li>'SalesRepId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.StreetAddress, (string.IsNullOrEmpty(value.StreetAddress)), "<li>'StreetAddress' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.City, (string.IsNullOrEmpty(value.City)), "<li>'City' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.PostalCode, (string.IsNullOrEmpty(value.PostalCode)), "<li>'PostalCode' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.Latitude, (value.Latitude.IsZero()), "<li>'Latitude' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.Longitude, (value.Longitude.IsZero()), "<li>'Longitude' can not be blank.</li>"));


				CmsCORSResult<QlAddress> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Initialize 
					IFnsQlAddress fnsQlAddress = new Address(value);

					// ** Create Service
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IQualifyLeadServices>();
					IFnsResult<IFnsQlAddress> oFnsModel = oService.QlAddressCreate(fnsQlAddress, user.GPEmployeeID);
					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oQlAddress = ConvertTo.CastFnsToQlAddress((IFnsQlAddress)oFnsModel.GetValue());


					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = oQlAddress;
				}
				#endregion TRY

				#region CATCH

				catch (Exception ex)
				{
					result.Code = (int)CmsResultCodes.ExceptionThrown;
					result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
						ex.Message);
				}

				#endregion CATCH

				#region Result

				return result;

				#endregion Result
			});
		}

        // PUT api/address/5
		[Route("Addresses/{id}")]
		[HttpPost]
		public CmsCORSResult<QlAddress> Update(int id, [FromBody]QlAddress value)
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "QlAddress Put";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>();
				argArray.Add(new CORSArg(value.DealerId, (value.DealerId == 0), "<li>'DealerId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.ValidationVendorId, (string.IsNullOrEmpty(value.ValidationVendorId)), "<li>'ValidationVendorId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.AddressValidationStateId, (string.IsNullOrEmpty(value.AddressValidationStateId)), "<li>'AddressValidationStateId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.StateId, (string.IsNullOrEmpty(value.StateId)), "<li>'StateId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.CountryId, (string.IsNullOrEmpty(value.CountryId)), "<li>'CountryId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.TimeZoneId, (value.TimeZoneId == 0), "<li>'TimeZoneId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.AddressTypeId, (string.IsNullOrEmpty(value.AddressTypeId)), "<li>'AddressTypeId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.SeasonId, (value.SeasonId == 0), "<li>'SeasonId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.TeamLocationId, (value.TeamLocationId == 0), "<li>'TeamLocationId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.SalesRepId, (string.IsNullOrEmpty(value.SalesRepId)), "<li>'SalesRepId' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.StreetAddress, (string.IsNullOrEmpty(value.StreetAddress)), "<li>'StreetAddress' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.City, (string.IsNullOrEmpty(value.City)), "<li>'City' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.PostalCode, (string.IsNullOrEmpty(value.PostalCode)), "<li>'PostalCode' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.Latitude, (value.Latitude.IsZero()), "<li>'Latitude' can not be blank.</li>"));
				argArray.Add(new CORSArg(value.Longitude, (value.Longitude.IsZero()), "<li>'Longitude' can not be blank.</li>"));


				CmsCORSResult<QlAddress> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Initialize 
					IFnsQlAddress fnsQlAddress = new Address(value);

					// ** Create Service
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IQualifyLeadServices>();
					IFnsResult<IFnsQlAddress> oFnsModel = oService.QlAddressUpdate(fnsQlAddress, user.GPEmployeeID);
					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oQlAddress = ConvertTo.CastFnsToQlAddress((IFnsQlAddress)oFnsModel.GetValue());


					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = oQlAddress;
				}
				#endregion TRY

				#region CATCH

				catch (Exception ex)
				{
					result.Code = (int)CmsResultCodes.ExceptionThrown;
					result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
						ex.Message);
				}

				#endregion CATCH

				#region Result

				return result;

				#endregion Result
			});
		}

        // DELETE api/address/5
		[Route("Addresses/{id}")]
		[HttpDelete]
		public CmsCORSResult<bool> Delete(int id)
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "QlAddress Delete";
			var result = new CmsCORSResult<bool>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region TRY

					try
					{
						// ** Create Service
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IQualifyLeadServices>();
						IFnsResult<IFnsQlAddress> oFnsModel = oService.QlAddressDelete(id, user.GPEmployeeID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = true;
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result
				});
		}
		#endregion Public REST CRUD

		#region Private

		private class Address : IFnsQlAddress
		{
			#region .ctor

			public Address(QlAddress address)
			{
				AddressID = address.AddressID;
				DealerId = address.DealerId;
				AddressTypeId = address.AddressTypeId;
				AddressValidationStateId = address.AddressValidationStateId;
				CarrierRoute = address.CarrierRoute;
				City = address.City;
				CongressionalDistric = address.CongressionalDistric;
				CountryId = address.CountryId;
				County = address.County;
				CountyCode = address.CountyCode;
				DeliveryPoint = address.DeliveryPoint;
				DPV = address.DPV;
				DPVFootnote = address.DPVFootnote;
				DPVResponse = address.DPVResponse;
				Extension = address.Extension;
				ExtensionNumber = address.ExtensionNumber;
				Latitude = address.Latitude;
				Longitude = address.Longitude;
				Phone = address.Phone;
				PlusFour = address.PlusFour;
				PostalCode = address.PostalCode;
				PostalCodeFull = address.PostalCodeFull;
				PostDirectional = address.PostDirectional;
				PreDirectional = address.PreDirectional;
				SalesRepId = address.SalesRepId;
				SeasonId = address.SeasonId;
				StateId = address.StateId;
				StreetAddress = address.StreetAddress;
				StreetAddress2 = address.StreetAddress2;
				StreetName = address.StreetName;
				StreetNumber = address.StreetNumber;
				StreetType = address.StreetType;
				TeamLocationId = address.TeamLocationId;
				TimeZoneId = address.TimeZoneId;
				Urbanization = address.Urbanization;
				UrbanizationCode = address.UrbanizationCode;
				ValidationVendorId = address.ValidationVendorId;
				IsActive = address.IsActive;
				IsDeleted = address.IsDeleted;
				CreatedBy = address.CreatedBy;
				CreatedOn = address.CreatedOn;
			}

			#endregion .ctor

			#region Properties
			public long AddressID { get; private set; }
			public int DealerId { get; private set; }
			public string AddressTypeId { get; private set; }
			public string AddressValidationStateId { get; private set; }
			public string CarrierRoute { get; private set; }
			public string City { get; private set; }
			public int? CongressionalDistric { get; private set; }
			public string CountryId { get; private set; }
			public string County { get; private set; }
			public string CountyCode { get; private set; }
			public string DeliveryPoint { get; private set; }
			public bool DPV { get; private set; }
			public string DPVFootnote { get; private set; }
			public string DPVResponse { get; private set; }
			public string Extension { get; private set; }
			public string ExtensionNumber { get; private set; }
			public double Latitude { get; private set; }
			public double Longitude { get; private set; }
			public string Phone { get; private set; }
			public string PlusFour { get; private set; }
			public string PostalCode { get; private set; }
			public string PostalCodeFull { get; private set; }
			public string PostDirectional { get; private set; }
			public string PreDirectional { get; private set; }
			public string SalesRepId { get; private set; }
			public int SeasonId { get; private set; }
			public string StateId { get; private set; }
			public string StreetAddress { get; private set; }
			public string StreetAddress2 { get; private set; }
			public string StreetName { get; private set; }
			public string StreetNumber { get; private set; }
			public string StreetType { get; private set; }
			public int TeamLocationId { get; private set; }
			public int TimeZoneId { get; private set; }
			public string Urbanization { get; private set; }
			public string UrbanizationCode { get; private set; }
			public string ValidationVendorId { get; private set; }
			public bool IsActive { get; private set; }
			public bool IsDeleted { get; private set; }
			public string CreatedBy { get; private set; }
			public DateTime CreatedOn { get; private set; }
			#endregion Properties
		}

		#endregion Private

	}
}
