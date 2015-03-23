using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using NXS.Data.Connext;
using NXS.Data.Connext.ControllerExtensions;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Connext;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.Connext;
using SOS.Lib.Core.ErrorHandling;

namespace SOS.FunctionalServices
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class ConnextService : IConnextService
	{
		#region CRUD Contacts

		private FnsCxContact Save(IFnsCxContact contact, string gpEmployeeId)
		{
			CX_Contact cxContact = contact.ContactID > 0 
				? NxseConnextDataContext.Instance.CX_Contacts.LoadByPrimaryKey(contact.ContactID) 
				: new CX_Contact();
			
			/** Bind data. */
			cxContact.ContactTypeId = contact.ContactTypeId;
			cxContact.DealerId = contact.DealerId;
			cxContact.LocalizationId = contact.LocalizationId;
			cxContact.TeamLocationId = contact.TeamLocationId;
			cxContact.SeasonId = contact.SeasonId;
			cxContact.SalesRepId = contact.SalesRepId;
			cxContact.Salutation = contact.Salutation;
			cxContact.FirstName = contact.FirstName;
			cxContact.MiddleName = contact.MiddleName;
			cxContact.LastName = contact.LastName;
			cxContact.Suffix = contact.Suffix;
			cxContact.Gender = contact.Gender;
			cxContact.SSN = contact.SSN;
			cxContact.DOB = contact.DOB;
			cxContact.Email = contact.Email;
			cxContact.PhoneHome = contact.PhoneHome;
			cxContact.PhoneWork = contact.PhoneWork;
			cxContact.PhoneMobile = contact.PhoneMobile;
			cxContact.CurrentSystem = contact.CurrentSystem;
			cxContact.IsActive = contact.IsActive;
			cxContact.IsDeleted = contact.IsDeleted;
			cxContact.CreatedOn = contact.CreatedOn;
			cxContact.CreatedBy = contact.CreatedBy;
			cxContact.Save(gpEmployeeId);

			return new FnsCxContact(cxContact);
		}

		public IFnsResult<IFnsCxContact> ContactCreate(IFnsCxContact contact, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "FOS -- ContactCreate";
			var result = new FnsResult<IFnsCxContact>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Validate data. */
				if (contact == null || string.IsNullOrEmpty(gpEmployeeId))
				{
					result.Code = BaseErrorCodes.ErrorCodes.ArgumentValidation.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.ArgumentValidation.Message(), "Contact or GpEmployeeID");
					return result;
				}

				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = Save(contact, gpEmployeeId);
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsCxContact>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsCxContact>> ContactReadAll(string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "FOS -- ContactRead";
			var result = new FnsResult<List<IFnsCxContact>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Validate data. */
				if (string.IsNullOrEmpty(gpEmployeeId))
				{
					result.Code = BaseErrorCodes.ErrorCodes.ArgumentValidation.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.ArgumentValidation.Message(), "GpEmployeeID");
					return result;
				}

				/** Execute load. */
				var contactColl = NxseConnextDataContext.Instance.CX_Contacts.LoadBySalesRepId(gpEmployeeId);
				var contactList = contactColl.Select(contact => new FnsCxContact(contact)).ToList();

				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = new List<IFnsCxContact>(contactList);
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsCxContact>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsCxContact> ContactRead(long contactId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "FOS -- ContactRead";
			var result = new FnsResult<IFnsCxContact>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Validate data. */
				if (contactId == 0 || string.IsNullOrEmpty(gpEmployeeId))
				{
					result.Code = BaseErrorCodes.ErrorCodes.ArgumentValidation.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.ArgumentValidation.Message(), "ContactId or GpEmployeeID");
					return result;
				}

				/** Execute load. */
				var contact = NxseConnextDataContext.Instance.CX_Contacts.LoadByPrimaryKey(contactId);

				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = new FnsCxContact(contact);
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsCxContact>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsCxContact> ContactUpdate(IFnsCxContact contact, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "FOS -- ContactCreate";
			var result = new FnsResult<IFnsCxContact>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Validate data. */
				if (contact == null || string.IsNullOrEmpty(gpEmployeeId))
				{
					result.Code = BaseErrorCodes.ErrorCodes.ArgumentValidation.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.ArgumentValidation.Message(), "Contact or GpEmployeeID");
					return result;
				}

				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = Save(contact, gpEmployeeId);
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsCxContact>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsCxContact> ContactDelete(long contactId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "FOS -- ContactDelete";
			var result = new FnsResult<IFnsCxContact>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Validate data. */
				if (contactId == 0 || string.IsNullOrEmpty(gpEmployeeId))
				{
					result.Code = BaseErrorCodes.ErrorCodes.ArgumentValidation.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.ArgumentValidation.Message(), "ContactId or GpEmployeeID");
					return result;
				}

				/** Execute load. */
				var contact = NxseConnextDataContext.Instance.CX_Contacts.LoadByPrimaryKey(contactId);
				contact.IsDeleted = true;
				contact.IsActive = false;
				contact.Save(gpEmployeeId);

				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = new FnsCxContact(contact);
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsCxContact>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion CRUD Contacts

		#region CRUD Address

		private FnsCxAddress Save(IFnsCxAddress address, string gpEmployeeId)
		{
			CX_Address cxAddress = address.AddressID > 0
				? NxseConnextDataContext.Instance.CX_Addresses.LoadByPrimaryKey(address.AddressID)
				: new CX_Address();

			/** Bind data. */
			cxAddress.AddressID = address.AddressID;
			cxAddress.DealerId = address.DealerId;
			cxAddress.AddressTypeId = address.AddressTypeId;
			cxAddress.AddressValidationStateId = address.AddressValidationStateId;
			cxAddress.CarrierRoute = address.CarrierRoute;
			cxAddress.City = address.City;
			cxAddress.CongressionalDistric = address.CongressionalDistric;
			cxAddress.CountryId = address.CountryId;
			cxAddress.County = address.County;
			cxAddress.CountyCode = address.CountyCode;
			cxAddress.DeliveryPoint = address.DeliveryPoint;
			cxAddress.DPV = address.DPV;
			cxAddress.DPVFootnote = address.DPVFootnote;
			cxAddress.DPVResponse = address.DPVResponse;
			cxAddress.Extension = address.Extension;
			cxAddress.ExtensionNumber = address.ExtensionNumber;
			cxAddress.Latitude = address.Latitude;
			cxAddress.Longitude = address.Longitude;
			cxAddress.Phone = address.Phone;
			cxAddress.PlusFour = address.PlusFour;
			cxAddress.PostalCode = address.PostalCode;
			cxAddress.PostalCodeFull = address.PostalCodeFull;
			cxAddress.PostDirectional = address.PostDirectional;
			cxAddress.PreDirectional = address.PreDirectional;
			cxAddress.SalesRepId = address.SalesRepId;
			cxAddress.SeasonId = address.SeasonId;
			cxAddress.StateId = address.StateId;
			cxAddress.StreetAddress = address.StreetAddress;
			cxAddress.StreetAddress2 = address.StreetAddress2;
			cxAddress.StreetName = address.StreetName;
			cxAddress.StreetNumber = address.StreetNumber;
			cxAddress.StreetType = address.StreetType;
			cxAddress.TeamLocationId = address.TeamLocationId;
			cxAddress.TimeZoneId = address.TimeZoneId;
			cxAddress.Urbanization = address.Urbanization;
			cxAddress.UrbanizationCode = address.UrbanizationCode;
			cxAddress.ValidationVendorId = address.ValidationVendorId;
			cxAddress.IsActive = address.IsActive;
			cxAddress.IsDeleted = address.IsDeleted;
			cxAddress.CreatedBy = address.CreatedBy;
			cxAddress.CreatedOn = address.CreatedOn;
			cxAddress.Save(gpEmployeeId);

			return new FnsCxAddress(cxAddress);
		}

		public IFnsResult<IFnsCxAddress> AddressCreate(IFnsCxAddress address, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "FOS -- AddressCreate";
			var result = new FnsResult<IFnsCxAddress>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Validate data. */
				if (address == null || string.IsNullOrEmpty(gpEmployeeId))
				{
					result.Code = BaseErrorCodes.ErrorCodes.ArgumentValidation.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.ArgumentValidation.Message(), "Contact or GpEmployeeID");
					return result;
				}

				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = Save(address, gpEmployeeId);
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsCxAddress>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsCxAddress>> AddressReadAll(string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "FOS -- AddressReadAll";
			var result = new FnsResult<List<IFnsCxAddress>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Validate data. */
				if (string.IsNullOrEmpty(gpEmployeeId))
				{
					result.Code = BaseErrorCodes.ErrorCodes.ArgumentValidation.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.ArgumentValidation.Message(), "GpEmployeeID");
					return result;
				}

				/** Execute load. */
				var addressColl = NxseConnextDataContext.Instance.CX_Addresses.LoadBySalesRepId(gpEmployeeId);
				var addressList = addressColl.Select(contact => new FnsCxAddress(contact)).ToList();

				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = new List<IFnsCxAddress>(addressList);
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsCxAddress>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsCxAddress> AddressRead(long addressId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "FOS -- AddressRead";
			var result = new FnsResult<IFnsCxAddress>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Validate data. */
				if (addressId == 0 || string.IsNullOrEmpty(gpEmployeeId))
				{
					result.Code = BaseErrorCodes.ErrorCodes.ArgumentValidation.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.ArgumentValidation.Message(), "AddressId or GpEmployeeID");
					return result;
				}

				/** Execute load. */
				var contact = NxseConnextDataContext.Instance.CX_Addresses.LoadByPrimaryKey(addressId);

				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = new FnsCxAddress(contact);
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsCxAddress>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsCxAddress> AddressUpdate(IFnsCxAddress address, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "FOS -- AddressUpdate";
			var result = new FnsResult<IFnsCxAddress>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Validate data. */
				if (address == null || string.IsNullOrEmpty(gpEmployeeId))
				{
					result.Code = BaseErrorCodes.ErrorCodes.ArgumentValidation.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.ArgumentValidation.Message(), "Address or GpEmployeeID");
					return result;
				}

				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = Save(address, gpEmployeeId);
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsCxAddress>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsCxAddress> AddressDelete(long addressId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "FOS -- AddressDelete";
			var result = new FnsResult<IFnsCxAddress>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Validate data. */
				if (addressId == 0 || string.IsNullOrEmpty(gpEmployeeId))
				{
					result.Code = BaseErrorCodes.ErrorCodes.ArgumentValidation.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.ArgumentValidation.Message(), "addressId or GpEmployeeID");
					return result;
				}

				/** Execute load. */
				var contact = NxseConnextDataContext.Instance.CX_Addresses.LoadByPrimaryKey(addressId);
				contact.IsDeleted = true;
				contact.IsActive = false;
				contact.Save(gpEmployeeId);

				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = new FnsCxAddress(contact);
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsCxAddress>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion CRUD Address
	}
}
