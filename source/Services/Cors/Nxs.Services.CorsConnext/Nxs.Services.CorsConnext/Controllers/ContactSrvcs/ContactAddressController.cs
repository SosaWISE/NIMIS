using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Nxs.Services.CorsConnext.Helpers;
using Nxs.Services.CorsConnext.Models;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models.Connext;
using SOS.Lib.Core;
using SOS.Services.Interfaces.Models.Connext;

namespace Nxs.Services.CorsConnext.Controllers.ContactSrvcs
{
	[RoutePrefix("ContactSrvcs")]
	public class ContactAddressController : ApiController
    {
        // GET api/contactaddress
		[HttpGet, Route("Addresses")]
		public Result<List<CxAddress>> Get()
		{
			#region Initialize

			const string METHOD_NAME = "REST Get All Addresses";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(user.HRUserID, (user.HRUserID == 0), "<li>'HRUserID' must be passed.</li>")
				};
				Result<List<CxAddress>> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IConnextService>();

					// ** Execute FOS Call
					var fnsResult = service.AddressReadAll(user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					// ** Create result value package
					// ReSharper disable once RedundantCast
					var fnsResultList = (List<IFnsCxAddress>)fnsResult.GetTValue();
					var resultList = fnsResultList.Select(fnsCxAddress => ConvertTo.CastFnsToCxAddress(fnsCxAddress)).ToList();

					result.Value = resultList;

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

		// GET api/contactaddress/5
		[HttpGet, Route("Addresses/{id}")]
		public Result<CxAddress> Get(int id)
		{
			#region Initialize

			const string METHOD_NAME = "REST Get Address";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(id, (id == 0), "<li>'AddressID' must be passed.</li>")
				};
				Result<CxAddress> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IConnextService>();

					// ** Execute FOS Call
					var fnsResult = service.AddressRead(id, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					// ** Create result value package
					// ReSharper disable once RedundantCast
					var fnsCxAddress = (IFnsCxAddress)fnsResult.GetTValue();
					var resultList = ConvertTo.CastFnsToCxAddress(fnsCxAddress);

					result.Value = resultList;

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

        // POST api/contactaddress
		[HttpPost, Route("Addresses")]
		public Result<CxAddress> Post([FromBody]CxAddress address)
		{
			#region Initialize

			const string METHOD_NAME = "REST POST Address";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(address, (address == null), "<li>'address' must be passed.</li>"),
				};


				Result<CxAddress> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IConnextService>();

					// ** Execute FOS Call
					IFnsCxAddress fnsAddress = ConvertTo.CastToFnsCxAddress(address);
					var fnsResult = service.AddressCreate(fnsAddress, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					// ** Create result value package
					// ReSharper disable once RedundantCast
					var fnsCxAddress = (IFnsCxAddress)fnsResult.GetTValue();
					var resultList = ConvertTo.CastFnsToCxAddress(fnsCxAddress);

					result.Value = resultList;

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

        // PUT api/contactaddress/5
		[HttpPut, Route("Addresses/{id}")]
		public Result<CxAddress> Put(int id, [FromBody]CxAddress address)
		{
			#region Initialize

			const string METHOD_NAME = "REST PUT Address";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(address, (address == null), "<li>'address' must be passed.</li>"),
					new CORSArg(id, (id == 0), "<li>'id' must be passed.</li>")
				};
				Result<CxAddress> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ReSharper disable once PossibleNullReferenceException
					address.AddressID = id;
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IConnextService>();

					// ** Execute FOS Call
					IFnsCxAddress fnsContact = ConvertTo.CastToFnsCxAddress(address);
					var fnsResult = service.AddressUpdate(fnsContact, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					// ** Create result value package
					// ReSharper disable once RedundantCast
					var fnsCxAddress = (IFnsCxAddress)fnsResult.GetTValue();
					var resultList = ConvertTo.CastFnsToCxAddress(fnsCxAddress);

					result.Value = resultList;

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

        // DELETE api/contactaddress/5
		[HttpDelete, Route("Addresses/{id}")]
		public Result<CxAddress> Delete(int id)
		{
			#region Initialize

			const string METHOD_NAME = "REST DELETE Address";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(user.HRUserID, (user.HRUserID == 0), "<li>'HRUserID' must be passed.</li>"),
					new CORSArg(id, (id == 0), "<li>'addressId' must be passed.</li>")
				};
				Result<CxAddress> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IConnextService>();

					// ** Execute FOS Call
					var fnsResult = service.AddressDelete(id, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					// ** Create result value package
					// ReSharper disable once RedundantCast
					var fnsCxAddress = (IFnsCxAddress)fnsResult.GetTValue();
					var resultList = ConvertTo.CastFnsToCxAddress(fnsCxAddress);

					result.Value = resultList;

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
    }
}