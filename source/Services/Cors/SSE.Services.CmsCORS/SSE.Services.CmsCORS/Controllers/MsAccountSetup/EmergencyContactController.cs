using System;
using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.FunctionalServices.Models.CentralStation;
using SOS.Services.Interfaces.Models.MonitoringStation;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.MsAccountSetup
{
	[RoutePrefix("MsAccountSetupSrv")]
	public class EmergencyContactController : ApiController
    {
		// POST EmergencyContactSrv/EmergencyContact
	    [Route("EmergencyContacts/")]
	    [HttpPost]
		public CmsCORSResult<MsEmergencyContact> EmergencyContactCreate([FromBody] MsEmergencyContact value)
	    {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "EmergencyContactCreate";
			var result = new CmsCORSResult<MsEmergencyContact>((int)ErrorCodes.Initializing, string.Format("Initializing '{0}'.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
		    return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			    , user =>
			    {
				    #region Parameter Validation

				    var argArray = new List<CORSArg>();
				    if (value == null)
				    {
					    argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
				    }
				    else
				    {
					    argArray.Add(new CORSArg(value.AccountId, (value.AccountId == 0), "<li>'AccountId' was not passed.</li>"));
					    argArray.Add(new CORSArg(value.RelationshipId, (value.RelationshipId == 0), "<li>'RelationshipId' was not passed.</li>"));
					    argArray.Add(new CORSArg(value.OrderNumber, (value.OrderNumber == 0), "<li>'OrderNumber' was not passed.</li>"));
					    argArray.Add(new CORSArg(value.FirstName, (string.IsNullOrEmpty(value.FirstName)), "<li>'FirstName' was not passed.</li>"));
					    argArray.Add(new CORSArg(value.LastName, (string.IsNullOrEmpty(value.LastName)), "<li>'LastName' was not passed.</li>"));
					    argArray.Add(new CORSArg(value.Phone1, (string.IsNullOrEmpty(value.Phone1)), "<li>'Phone1' was not passed.</li>"));
					    argArray.Add(new CORSArg(value.Phone1TypeId, (value.Phone1TypeId == 0), "<li>'Phone1TypeId' was not passed.</li>"));

					    if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;
				    }

				    #endregion Parameter Validation

				    #region TRY

				    try
				    {
					    // ** Create Service
					    var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();

					    // ** Prepare arguents
						var fnsMsEmergencyContact = new FnsMsEmergencyContact(fxBindData, value);
						IFnsResult<IFnsMsEmergencyContact> fnsResult = mcService.EmergencyContactCreate(fnsMsEmergencyContact, user.GPEmployeeID);

					    // ** Save result
					    result.Code = fnsResult.Code;
					    result.SessionId = user.SessionID;
					    result.Message = fnsResult.Message;

					    // ** Get Values
					    var fnsResultValue = (IFnsMsEmergencyContact) fnsResult.GetValue();
					    if (result.Code == (int) CmsResultCodes.Success && fnsResultValue != null)
					    {
						    var resultValue = new MsEmergencyContact
						    {
								EmergencyContactID = fnsResultValue.EmergencyContactID,
								EmergencyContactTypeId = fnsResultValue.EmergencyContactTypeId,
							    CustomerId = fnsResultValue.CustomerId,
							    AccountId = fnsResultValue.AccountId,
							    RelationshipId = fnsResultValue.RelationshipId,
							    OrderNumber = fnsResultValue.OrderNumber,
							    Allergies = fnsResultValue.Allergies,
							    MedicalConditions = fnsResultValue.MedicalConditions,
							    HasKey = fnsResultValue.HasKey,
							    DOB = fnsResultValue.DOB,
							    Prefix = fnsResultValue.Prefix,
							    FirstName = fnsResultValue.FirstName,
							    MiddleName = fnsResultValue.MiddleName,
							    LastName = fnsResultValue.LastName,
							    Postfix = fnsResultValue.Postfix,
							    Email = fnsResultValue.Email,
							    Password = fnsResultValue.Password,
							    Phone1 = fnsResultValue.Phone1,
							    Phone1TypeId = fnsResultValue.Phone1TypeId,
							    Phone2 = fnsResultValue.Phone2,
							    Phone2TypeId = fnsResultValue.Phone2TypeId,
							    Phone3 = fnsResultValue.Phone3,
							    Phone3TypeId = fnsResultValue.Phone3TypeId,
							    Comment1 = fnsResultValue.Comment1
						    };
						    result.Value = resultValue;
					    }
				    }
					    #endregion TRY

					#region CATCH

				    catch (Exception ex)
				    {
					    result.Code = (int) CmsResultCodes.ExceptionThrown;
					    result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
						    ex.Message);
				    }

				    #endregion CATCH

				    #region Result

				    return result;

				    #endregion Result
			    });
		}

        [Route("EmergencyContacts/{id}")]
        [HttpGet]
        public CmsCORSResult<MsEmergencyContact> EmergencyContactRead(long id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "EmergencyContactRead";
            var result = new CmsCORSResult<MsEmergencyContact>((int)ErrorCodes.Initializing, string.Format("Initializing '{0}'.", METHOD_NAME));

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
                {
                    #region TRY

                    try
                    {
                        // ** Create Service
                        var mcService =
                            SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();

                        // ** Prepare arguents
                        IFnsResult<IFnsMsEmergencyContact> fnsResult =
                            mcService.EmergencyContactRead(id);

                        // ** Save result
                        result.Code = fnsResult.Code;
                        result.SessionId = user.SessionID;
                        result.Message = fnsResult.Message;

                        // ** Get Values
                        var fnsResultValue = (IFnsMsEmergencyContact) fnsResult.GetValue();
                        if (result.Code == (int) CmsResultCodes.Success && fnsResultValue != null)
                        {
                            var resultValue = new MsEmergencyContact
                            {
                                EmergencyContactID = fnsResultValue.EmergencyContactID,
                                
                            };
                            result.Value = resultValue;
                        }
                    }
                        #endregion TRY

                    #region CATCH

                    catch (Exception ex)
                    {
                        result.Code = (int)CmsResultCodes.ExceptionThrown;
                        result.Message = string.Format("The following exception was thrown from '{0}' method: {1}",
                            METHOD_NAME,
                            ex.Message);
                    }

                    #endregion CATCH

                    #region Result

                    return result;

                    #endregion Result
                });

        }

        [Route("EmergencyContacts/{id}")]
        [HttpPost]
        public CmsCORSResult<MsEmergencyContact> EmergencyContactUpdate(long id, [FromBody] MsEmergencyContact value)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "EmergencyContactUpdate";
            var result = new CmsCORSResult<MsEmergencyContact>((int)ErrorCodes.Initializing, string.Format("Initializing '{0}'.", METHOD_NAME));

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
                {
                    #region Parameter Validation

                    var argArray = new List<CORSArg>();
                    if (value == null)
                    {
                        argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
                    }
                    else
                    {
	                    argArray.Add(new CORSArg(id, (id == 0 && value.EmergencyContactID == 0),
		                    "<i>the emc id must be passed.</i>"));
                        argArray.Add(new CORSArg(value.CustomerId, (value.CustomerId == 0),
                            "<li>'CustomerId' was not passed.</li>"));
                        argArray.Add(new CORSArg(value.AccountId, (value.AccountId == 0),
                            "<li>'AccountId' was not passed.</li>"));
                        argArray.Add(new CORSArg(value.RelationshipId, (value.RelationshipId == 0),
                            "<li>'RelationshipId' was not passed.</li>"));
                        argArray.Add(new CORSArg(value.OrderNumber, (value.OrderNumber == 0),
                            "<li>'OrderNumber' was not passed.</li>"));
                        argArray.Add(new CORSArg(value.FirstName, (string.IsNullOrEmpty(value.FirstName)),
                            "<li>'FirstName' was not passed.</li>"));
                        argArray.Add(new CORSArg(value.LastName, (string.IsNullOrEmpty(value.LastName)),
                            "<li>'LastName' was not passed.</li>"));
                        argArray.Add(new CORSArg(value.Phone1, (string.IsNullOrEmpty(value.Phone1)),
                            "<li>'Phone1' was not passed.</li>"));
                        argArray.Add(new CORSArg(value.Phone1TypeId, (value.Phone1TypeId == 0),
                            "<li>'Phone1TypeId' was not passed.</li>"));

                        if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;
                    }

                    #endregion Parameter Validation

                    #region TRY

                    try
                    {
						/** Place the id in the right place. */
// ReSharper disable once PossibleNullReferenceException
	                    if (value.EmergencyContactID == 0) value.EmergencyContactID = id;

                        // ** Create Service
                        var mcService =
                            SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();

                        // ** Prepare arguents
                        var fnsMsEmergencyContact = new FnsMsEmergencyContact(fxBindData, value);
                        IFnsResult<IFnsMsEmergencyContact> fnsResult =
                            mcService.EmergencyContactUpdate(fnsMsEmergencyContact, user.GPEmployeeID);

                        // ** Save result
                        result.Code = fnsResult.Code;
                        result.SessionId = user.SessionID;
                        result.Message = fnsResult.Message;

                        // ** Get Values
                        var fnsResultValue = (IFnsMsEmergencyContact) fnsResult.GetValue();
                        if (result.Code == (int) CmsResultCodes.Success && fnsResultValue != null)
                        {
                            var resultValue = new MsEmergencyContact
                            {
                                EmergencyContactID = fnsResultValue.EmergencyContactID,
                                CustomerId = fnsResultValue.CustomerId,
                                AccountId = fnsResultValue.AccountId,
                                RelationshipId = fnsResultValue.RelationshipId,
                                OrderNumber = fnsResultValue.OrderNumber,
                                Allergies = fnsResultValue.Allergies,
                                MedicalConditions = fnsResultValue.MedicalConditions,
                                HasKey = fnsResultValue.HasKey,
                                DOB = fnsResultValue.DOB,
                                Prefix = fnsResultValue.Prefix,
                                FirstName = fnsResultValue.FirstName,
                                MiddleName = fnsResultValue.MiddleName,
                                LastName = fnsResultValue.LastName,
                                Postfix = fnsResultValue.Postfix,
                                Email = fnsResultValue.Email,
                                Password = fnsResultValue.Password,
                                Phone1 = fnsResultValue.Phone1,
                                Phone1TypeId = fnsResultValue.Phone1TypeId,
                                Phone2 = fnsResultValue.Phone2,
                                Phone2TypeId = fnsResultValue.Phone2TypeId,
                                Phone3 = fnsResultValue.Phone3,
                                Phone3TypeId = fnsResultValue.Phone3TypeId,
                                Comment1 = fnsResultValue.Comment1
                            };
                            result.Value = resultValue;
                        }
                    }
                        #endregion TRY

                    #region CATCH

                    catch (Exception ex)
                    {
                        result.Code = (int) CmsResultCodes.ExceptionThrown;
                        result.Message = string.Format("The following exception was thrown from '{0}' method: {1}",
                            METHOD_NAME,
                            ex.Message);
                    }

                    #endregion CATCH

                    #region Result

                    return result;

                    #endregion Result
                });
        }

        [Route("EmergencyContacts/{id}")]
        [HttpDelete]
        public CmsCORSResult<MsEmergencyContact> EmergencyContactDelete(long id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "EmergencyContactDelete";
            var result = new CmsCORSResult<MsEmergencyContact>((int)ErrorCodes.Initializing, string.Format("Initializing '{0}'.", METHOD_NAME));

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>

                {
                    #region TRY

                    try
                    {
                        // ** Create Service
                        var mcService =
                            SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();

                        // ** Prepare arguents
                        IFnsResult<IFnsMsEmergencyContact> fnsResult =
                            mcService.EmergencyContactDelete(id, user.GPEmployeeID);

                        // ** Save result
                        result.Code = fnsResult.Code;
                        result.SessionId = user.SessionID;
                        result.Message = fnsResult.Message;

                        // ** Get Values
                        var fnsResultValue = (IFnsMsEmergencyContact) fnsResult.GetValue();
                        if (result.Code == (int) CmsResultCodes.Success && fnsResultValue != null)
                        {
                            var resultValue = new MsEmergencyContact
                            {
                                EmergencyContactID = fnsResultValue.EmergencyContactID,
                                
                            };
                            result.Value = resultValue;
                        }
                    }
                        #endregion TRY

                    #region CATCH

                    catch (Exception ex)
                    {
                        result.Code = (int)CmsResultCodes.ExceptionThrown;
                        result.Message = string.Format("The following exception was thrown from '{0}' method: {1}",
                            METHOD_NAME,
                            ex.Message);
                    }

                    #endregion CATCH

                    #region Result

                    return result;

                    #endregion Result
                });
        }


		[Route("EmergencyContactPhoneTypes")]
		[HttpGet]
		public CmsCORSResult<List<MsEmergencyContactPhoneType>> EmergencyContactPhoneTypes()
	    {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get PhoneTypes";
            var result = new CmsCORSResult<List<MsEmergencyContactPhoneType>>((int)CmsResultCodes.Initializing
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
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
                    IFnsResult<List<IFnsMsEmergencyContactPhoneType>> oFnsModel = oService.EmergencyContactPhoneTypesGet(user.GPEmployeeID);
					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oMsEmergencyContactPhoneTypeList = ConvertTo.CastFnsToMsEmergencyPhoneTypeList((List<IFnsMsEmergencyContactPhoneType>)oFnsModel.GetValue());


					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
                    result.Value = oMsEmergencyContactPhoneTypeList;
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

		[Route("Accounts/{id}/EmergencyContactPhoneTypes")]
		[HttpGet]
		public CmsCORSResult<List<MsEmergencyContactPhoneType>> EmergencyContactPhoneTypes(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get PhoneTypes";
			var result = new CmsCORSResult<List<MsEmergencyContactPhoneType>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
					{
						new CORSArg(id, id == 0, "<li>'id' is empty and is required.</li>")
					};

				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation
				#region TRY

				try
				{
					// ** Create Service
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
					IFnsResult<List<IFnsMsEmergencyContactPhoneType>> oFnsModel = oService.EmergencyContactPhoneTypesGet(id, user.GPEmployeeID);
					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oMsEmergencyContactPhoneTypeList = ConvertTo.CastFnsToMsEmergencyPhoneTypeList((List<IFnsMsEmergencyContactPhoneType>)oFnsModel.GetValue());


					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = oMsEmergencyContactPhoneTypeList;
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

		[Route("Accounts/{id}/EmergencyContactRelationships")]
		[HttpGet]
		public CmsCORSResult<List<MsEmergencyContactRelationship>> EmergencyContactRelationships(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get Relationships";
			var result = new CmsCORSResult<List<MsEmergencyContactRelationship>>((int)CmsResultCodes.Initializing
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
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
					IFnsResult<List<IFnsMsEmergencyContactRelationship>> oFnsModel = oService.EmergencyContactRelationShipsGet(id, user.GPEmployeeID);
					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var relationshipList = ConvertTo.CastFnsToMsEmergencyContactRelationshipList((List<IFnsMsEmergencyContactRelationship>)oFnsModel.GetValue());


					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = relationshipList;
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

        [Route("Accounts/{id}/EmergencyContactAuthorities")]
        [HttpGet]
        public CmsCORSResult<List<MsEmergencyContactAuthority>> EmergencyContactAuthorities(long id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get Authorities";
            var result = new CmsCORSResult<List<MsEmergencyContactAuthority>>((int)CmsResultCodes.Initializing
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
                    var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
                    IFnsResult<List<IFnsMsEmergencyContactAuthority>> oFnsModel = oService.EmergencyContactAuthoritiesGet(id, user.GPEmployeeID);
                    /** Check corsResult. */
                    if (oFnsModel.Code != 0)
                    {
                        result.Code = oFnsModel.Code;
                        result.Message = oFnsModel.Message;
                        return result;
                    }

                    /** Setup return corsResult. */
                    var authorityList = ConvertTo.CastFnsToMsEmergencyContactAuthorityList((List<IFnsMsEmergencyContactAuthority>)oFnsModel.GetValue());


                    /** Save success results. */
                    result.Code = (int)CmsResultCodes.Success;
                    result.SessionId = user.SessionID;
                    result.Message = "Success";
                    result.Value = authorityList;
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

        [Route("Accounts/{id}/EmergencyContactTypes")]
        [HttpGet]
        public CmsCORSResult<List<MsEmergencyContactType>> EmergencyContactTypes(long id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get Contact Types";
            var result = new CmsCORSResult<List<MsEmergencyContactType>>((int)CmsResultCodes.Initializing
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
                    var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
                    IFnsResult<List<IFnsMsEmergencyContactType>> oFnsModel = oService.EmergencyContactTypesGet(id, user.GPEmployeeID);
                    /** Check corsResult. */
                    if (oFnsModel.Code != 0)
                    {
                        result.Code = oFnsModel.Code;
                        result.Message = oFnsModel.Message;
                        return result;
                    }

                    /** Setup return corsResult. */
                    var contactTypeList = ConvertTo.CastFnsToMsEmergencyContactTypeList((List<IFnsMsEmergencyContactType>)oFnsModel.GetValue());


                    /** Save success results. */
                    result.Code = (int)CmsResultCodes.Success;
                    result.SessionId = user.SessionID;
                    result.Message = "Success";
                    result.Value = contactTypeList;
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


        #region Private Methods

	    private void fxBindData(IFnsMsEmergencyContact fnsContact, object contact)
	    {
			/** Init value. */
		    const int AG_MONITORING_CONTACT = 5;
		    const int AG_AUTHORITY_FULL = 8;
		    var item = (MsEmergencyContact) contact;

			fnsContact.EmergencyContactID = item.EmergencyContactID;
			fnsContact.EmergencyContactTypeId = item.EmergencyContactTypeId == 0 ? AG_MONITORING_CONTACT : item.EmergencyContactTypeId;
			fnsContact.CustomerId = item.CustomerId;
			fnsContact.AccountId = item.AccountId;
			fnsContact.RelationshipId = item.RelationshipId;
		    fnsContact.AuthorityId = item.AuthorityId == 0 ? AG_AUTHORITY_FULL: item.AuthorityId;
			fnsContact.OrderNumber = item.OrderNumber;
			fnsContact.Allergies = item.Allergies;
			fnsContact.MedicalConditions = item.MedicalConditions;
			fnsContact.HasKey = item.HasKey;
			fnsContact.DOB = item.DOB;
			fnsContact.Prefix = item.Prefix;
			fnsContact.FirstName = item.FirstName;
			fnsContact.MiddleName = item.MiddleName;
			fnsContact.LastName = item.LastName;
			fnsContact.Postfix = item.Postfix;
			fnsContact.Email = item.Email;
			fnsContact.Password = item.Password;
			fnsContact.Phone1 = item.Phone1;
			fnsContact.Phone1TypeId = item.Phone1TypeId;
			fnsContact.Phone2 = item.Phone2;
			fnsContact.Phone2TypeId = item.Phone2TypeId;
			fnsContact.Phone3 = item.Phone3;
			fnsContact.Phone3TypeId = item.Phone3TypeId;
			fnsContact.Comment1 = item.Comment1;
		}

	    #endregion Private Methods
	}

    
}
