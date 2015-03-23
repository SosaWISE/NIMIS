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
	public class ContactsController : ApiController
    {
        // GET api/contacts
		[HttpGet, Route("Contacts")]
		public Result<List<CxContact>> Get()
        {
			#region Initialize

			const string METHOD_NAME = "REST Get All Contacts";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(user.HRUserID, (user.HRUserID == 0), "<li>'HRUserID' must be passed.</li>")
				};
				Result<List<CxContact>> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IConnextService>();

					// ** Execute FOS Call
					var fnsResult = service.ContactReadAll(user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					// ** Create result value package
					// ReSharper disable once RedundantCast
					var fnsResultList = (List<IFnsCxContact>)fnsResult.GetTValue();
					var resultList = fnsResultList.Select(fnsCxContact => ConvertTo.CastFnsToCxContact(fnsCxContact)).ToList();

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

        // GET api/contacts/5
		[HttpGet, Route("Contacts/{id}")]
		public Result<CxContact> Get(int id)
        {
			#region Initialize

			const string METHOD_NAME = "REST Get Contacts";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(user.HRUserID, (user.HRUserID == 0), "<li>'HRUserID' must be passed.</li>")
				};
				Result<CxContact> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IConnextService>();

					// ** Execute FOS Call
					var fnsResult = service.ContactRead(id, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					// ** Create result value package
					// ReSharper disable once RedundantCast
					var fnsCxContact = (IFnsCxContact)fnsResult.GetTValue();
					var resultList = ConvertTo.CastFnsToCxContact(fnsCxContact);

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

        // POST api/contacts
		[HttpPost, Route("Contacts")]
		public Result<CxContact> Post([FromBody]CxContact contact)
        {
			#region Initialize

			const string METHOD_NAME = "REST POST Contacts";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(contact, (contact == null), "<li>'contact' must be passed.</li>"),
				};
				if (contact != null)
					argArray.Add(new CORSArg(contact.AddressId, (contact.AddressId == 0), "<li>'contact.AddressId' must be passed.</li>"));

				Result<CxContact> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IConnextService>();

					// ** Execute FOS Call
					IFnsCxContact fnsContact = ConvertTo.CastToFnsCxContact(contact);
					var fnsResult = service.ContactCreate(fnsContact, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					// ** Create result value package
					// ReSharper disable once RedundantCast
					var fnsCxContact = (IFnsCxContact)fnsResult.GetTValue();
					var resultList = ConvertTo.CastFnsToCxContact(fnsCxContact);

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

        // PUT api/contacts/5
		[HttpPut, Route("Contacts/{id}")]
		public Result<CxContact> Put(int id, [FromBody]CxContact contact)
        {
			#region Initialize

			const string METHOD_NAME = "REST PUT Contacts";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(contact, (contact == null), "<li>'contact' must be passed.</li>"),
					new CORSArg(id, (id == 0), "<li>'id' must be passed.</li>")
				};
				Result<CxContact> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
// ReSharper disable once PossibleNullReferenceException
					contact.ContactID = id;
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IConnextService>();

					// ** Execute FOS Call
					IFnsCxContact fnsContact = ConvertTo.CastToFnsCxContact(contact);
					var fnsResult = service.ContactUpdate(fnsContact, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					// ** Create result value package
					// ReSharper disable once RedundantCast
					var fnsCxContact = (IFnsCxContact)fnsResult.GetTValue();
					var resultList = ConvertTo.CastFnsToCxContact(fnsCxContact);

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

        // DELETE api/contacts/5
		[HttpDelete, Route("Contacts/{id}")]
		public Result<CxContact> Delete(int id)
        {
			#region Initialize

			const string METHOD_NAME = "REST DELETE Contacts";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.Authorize(METHOD_NAME, null, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(user.HRUserID, (user.HRUserID == 0), "<li>'HRUserID' must be passed.</li>")
				};
				Result<CxContact> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY
				try
				{
					// ** Create a service object
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IConnextService>();

					// ** Execute FOS Call
					var fnsResult = service.ContactDelete(id, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;

					// Check for success
					if (result.Code != 0)
						return result;

					// ** Create result value package
					// ReSharper disable once RedundantCast
					var fnsCxContact = (IFnsCxContact)fnsResult.GetTValue();
					var resultList = ConvertTo.CastFnsToCxContact(fnsCxContact);

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