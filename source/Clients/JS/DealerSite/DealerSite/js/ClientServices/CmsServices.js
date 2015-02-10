/**
 * Created by JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 3/1/12
 * Time: 10:11 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.ns('SOS.ClientServices');

Ext.define('SOS.ClientServices.CmsServices',
{
	extend: 'Ext.util.Observable',

	statics:
	{
		/** Exceptions */
		EXP_MSG_NO_ARG_PASSED: "No configuration argument was passed.",
		EXP_MSG_NO_APP_TOKEN: "Missing Application Token.",
		EXP_MSG_DEALERID_IS_MISSING: "Missing DealerId.",
		EXP_MSG_FIRSTNME_IS_MISSING: "Missing FirstName",
		EXP_MSG_LASTNAME_IS_MISSING: "Missing LastName",
		EXP_MSG_ADDRESSS_IS_MISSING: "Missing Address",
		EXP_MSG_CITYNAME_IS_MISSING: "Missing City",
		EXP_MSG_STATE_ID_IS_MISSING: "Missing State",
		EXP_MSG_POSTALCD_IS_MISSING: "Missing Postal",
		EXP_MSG_EMAILADR_IS_MISSING: "Missing Email",
		EXP_MSG_PREPHONE_IS_MISSING: "Missing PremisePhone",
		EXP_MSG_LEAD__ID_IS_MISSING: "Missing LeadId",
		EXP_MSG_CUSTMFID_IS_MISSING: "Missing CustomerMasterFileId",
		EXP_MSG_LISTNAME_IS_MISSING: "Missing ListName",
		EXP_MSG_ITEMNAME_IS_MISSING: "Missing ItemName",

		/** Events. */
		EVNT_SOSSTART_SUCCESS: 'SessionStartSuccess',
		EVNT_SOSSTART_FAILURE: 'SessionStartFailure',
		EVNT_SOSAUTHN_SUCCESS: 'AuthenticationSuccess',
		EVNT_SOSAUTHN_FAILURE: 'AuthenticationFailure',
		EVNT_CHCKSESS_SUCCESS: 'CheckSessionOnSuccess',
		EVNT_CHCKSESS_FAILURE: 'CheckSessionOnFailure',
		EVNT_CBSCLEAD_SUCCESS: 'CBasicQLLeadOnSuccess',
		EVNT_CBSCLEAD_FAILURE: 'CBasicQLLeadOnFailure',
		EVNT_LEADSRCH_SUCCESS: 'LeadSearchOnSuccess',
		EVNT_LEADSRCH_FAILURE: 'LeadSearchOnFailure',
		EVNT_LEDFSRCH_SUCCESS: 'LeadFullSrchOnSuccess',
		EVNT_LEDFSRCH_FAILURE: 'LeadFullSrchOnFailure',
		EVNT_GETNOTES_SUCCESS: 'GetNotesFullOnSuccess',
		EVNT_GETNOTES_FAILURE: 'GetNotesFullOnFailure',
		EVNT_LEADCRUP_SUCCESS: 'LeadCreatUpdatSuccess',
		EVNT_LEADCRUP_FAILURE: 'LeadCreatUpdatFailure',
		EVNT_CUSTNEWI_SUCCESS: 'CustCreateNewISuccess',
		EVNT_CUSTNEWI_FAILURE: 'CustCreateNewIFailure',
		EVNT_OPTITEMS_SUCCESS: 'GetOptionsItemSuccess',
		EVNT_OPTITEMS_FAILURE: 'GetOptionsItemFailure',
		EVNT_OPTITMAD_SUCCESS: 'AddOptionsItemSuccess',
		EVNT_OPTITMAD_FAILURE: 'AddOptionsItemFailure',
		EVNT_LEADDISP_SUCCESS: 'ChangeLeadDispSuccess',
		EVNT_LEADDISP_FAILURE: 'ChangeLeadDispFailure',

		/** Singleton function. */
		Create: function()
		{
			return new SOS.ClientServices.AuthServices({});
		}
	},

	/** Private Properties. */
	/*************************
	 * @private
	 *************************/
	_svc: null,

	constructor: function(config)
	{
		/** Init. */
		var me = this;
		if (config === undefined)
			config = {};

		/** Add events. */
		this.addEvents(
			{
				'SessionStartSuccess': true,
				'SessionStartFailure': true
			}
		);

		/** Take instance of service. */
		this._svc = SOS.ICmsSvcClient.Instance;

		/** Save listeners. */
		this.listeners = config.listeners;
		this.callParent([arguments]);

		/** Return result. */
		return me;
	},
	QlLeadBasicCreate: function (nDealerId, szFirstName, szLastName, szAddress, szCity, szState, szPostal, szEmail
		, szPremisePhone, callbackScope)
	{
		/** Initialize. */
		var me = this;
		var xhr = null;

		/** Validate Arguments. */
		if (nDealerId === undefined || nDealerId == null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_DEALERID_IS_MISSING;
		if (szFirstName === undefined || szFirstName == null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_FIRSTNME_IS_MISSING;
		if (szLastName === undefined || szLastName == null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_LASTNAME_IS_MISSING;
		if (szAddress === undefined || szAddress == null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_ADDRESSS_IS_MISSING;
		if (szCity === undefined || szCity == null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_CITYNAME_IS_MISSING;
		if (szState === undefined || szState == null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_STATE_ID_IS_MISSING;
		if (szPostal === undefined || szPostal == null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_POSTALCD_IS_MISSING;
		if (szEmail === undefined || szEmail == null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_EMAILADR_IS_MISSING;
		if (szPremisePhone === undefined || szPremisePhone == null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_PREPHONE_IS_MISSING;

		/** Execute */
		try
		{
			me._svc.QlLeadBasicCreate(nDealerId, szFirstName, szLastName, szAddress, szCity, szState, szPostal, szEmail
				, szPremisePhone
				, function (response)
				  {
					  switch(response.Code)
					  {
						  case 0:
							  me.fireEvent(SOS.ClientServices.CmsServices.EVNT_CBSCLEAD_SUCCESS, response);
							  break;
						  default:
							  var oMsgItem = new SOS.Models.MessageItem(
								  {
									  Title: "Server Error"
									  , MessageBody: response.Message
									  , MessageType: "Failure"
									  , Code: response.Code
									  , Result: response
								  });
							  me.fireEvent(SOS.ClientServices.CmsServices.EVNT_CBSCLEAD_FAILURE, oMsgItem);
							  break;
					  }
				  }
				, callbackScope);
		}
		catch (oEx)
		{
			var oMsgItem = new SOS.Models.MessageItem({
				Title: "Exception thrown on QLLeadBasicCreate"
				, MessageBody: Ext.String.format("The following error occurred:<br />{0}"
					, oEx)
				, MessageType: "Fatal"
				, Exception: oEx
			});
			me.fireEvent(SOS.ClientServices.CmsServices.EVNT_CBSCLEAD_FAILURE, oMsgItem);
		}
		/** Return default result. */
		return xhr;
	},

	QlSearch: function (szFirstName, szLastName, szPhone, szEmail, nLeadId, nDispositionId, nSourceId, nPageSize, nPageNumber, szcallbackscope)
	{
		/** Initialize. */
		var me = this;
		var xhr = null;

		/** Validate Arguments. */
		if (szFirstName === null && szLastName === null && szPhone === null && szEmail === null && nLeadId === null
			&& nPageSize === null && nPageNumber === null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_NO_ARG_PASSED;

		/** Execute. */
		try
		{
			me._svc.QlSearch(szFirstName, szLastName, szPhone, SOS.AppService.UserInfo.DealerId, szEmail, nLeadId
				, nDispositionId, nSourceId, nPageSize, nPageNumber
				, function (response)
				  {
					  switch(response.Code)
					  {
						  case 0:
							  me.fireEvent(SOS.ClientServices.CmsServices.EVNT_LEADSRCH_SUCCESS, response);
							  break;
						  default:
							  var oMsgItem = new SOS.Models.MessageItem(
								  {
									  Title: "Server Error"
									  , MessageBody: response.Message
									  , MessageType: "Failure"
									  , Code: response.Code
									  , Result: response
								  });
							  me.fireEvent(SOS.ClientServices.CmsServices.EVNT_LEADSRCH_FAILURE, oMsgItem);
							  break;
					  }
				  }
				, szcallbackscope);
		}
		catch (oEx)
		{
			var oMsgItm = new SOS.Models.MessageItem({
				Title: "Exception thrown on QlSearch"
				, MessageBody: Ext.String.format("The following error occurred: {0}"
					, oEx)
				, MessageType: "Fatal"
				, Exception: oEx
			});
			me.fireEvent(SOS.ClientServices.CmsServices.EVNT_LEADSRCH_FAILURE, oMsgItm);
		}

		/** Return default result. */
		return xhr;
	},

	QlGetLeadFull: function (lLeadId, lCustomerMasterFileId, bNoteAccount, szCallbackScope)
	{
		/** Initialize. */
		var me = this;
		var xhr = null;

		/** Validate Arguments. */
		if (lLeadId === undefined || lLeadId === null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_LEAD__ID_IS_MISSING;
		if (lCustomerMasterFileId === undefined || lCustomerMasterFileId === null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_CUSTMFID_IS_MISSING;
		if (bNoteAccount === undefined || bNoteAccount === null)
			bNoteAccount = false;

		try
		{
			xhr = me._svc.QlGetLeadFull(lLeadId, lCustomerMasterFileId, bNoteAccount
				, function (response)
				{
					switch(response.Code)
					{
						case 0:
							me.fireEvent(SOS.ClientServices.CmsServices.EVNT_LEDFSRCH_SUCCESS, response);
							break;
						default:
							var oMsgItem = new SOS.Models.MessageItem({
								Title: "Error Retrieving Lead Info",
								MessageBody: response.Message,
								MessageType: "Error",
								Code: response.Code
							});
							me.fireEvent(SOS.ClientServices.CmsServices.EVNT_LEDFSRCH_FAILURE, oMsgItem);
							break;
					}
				}
				, szCallbackScope);
		}
		catch (oEx)
		{
			var oMsgItm = new SOS.Models.MessageItem({
				Title: "Exception thrown on QlGetLeadFull"
				, MessageBody: Ext.String.format("The following error occurred: {0}"
					, oEx)
				, MessageType: "Fatal"
				, Exception: oEx
			});
			me.fireEvent(SOS.ClientServices.CmsServices.EVNT_LEDFSRCH_FAILURE, oMsgItm);
		}

		/** Return deferred. */
		return xhr;
	},

	McGetNotes: function (lLeadId, lCustomerId, lCMFId, oCallbackScope)
	{
		/** Initialize. */
		var me = this;
		var xhr = null;

		/** validate arguments. */
		if((lLeadId === null || lLeadId === undefined)
			&& (lCustomerId === null || lCustomerId === undefined)
			&& (lCMFId === null || lCMFId === undefined))
		{
			throw SOS.ClientServices.CmsServices.EXP_MSG_NO_ARG_PASSED;
		}

		try
		{
			xhr = me._svc.QlGetAccountNotesByID(lLeadId, lCustomerId, lCMFId
				, function (response)
				{
					switch(response.Code)
					{
						case 0:
							me.fireEvent(SOS.ClientServices.CmsServices.EVNT_GETNOTES_SUCCESS, response);
							break;
						default:
							var oMsgItem = new SOS.Models.MessageItem({
								Title: "Error Acquiring Notes",
								MessageBody: response.Message,
								MessageType: "Error",
								Code: response.Code
							});
							me.fireEvent(SOS.ClientServices.CmsServices.EVNT_GETNOTES_FAILURE, oMsgItem);
							break;
					}
				}
				, oCallbackScope);
		}
		catch (oEx)
		{
			var oMsgItm = new SOS.Models.MessageItem({
				Title: "Exception thrown on McGetNotes"
				, MessageBody: Ext.String.format("The following error occurred: {0}"
					, oEx)
				, MessageType: "Fatal"
				, Exception: oEx
			});
			me.fireEvent(SOS.ClientServices.CmsServices.EVNT_GETNOTES_FAILURE, oMsgItm);
		}

		/** return hdr. */
		return xhr;
	},

	/**
	 * If there is a LeadId then it will update it in the database.  Otherwise it creates one and returns the LeadId and
	 * the CustomerMasterFileId.
	 * @param oLeadFullData {object} This is the data that we will be returning to the web server.
	 * @param oCallbackScope {object} The called back scope.
	 * @return {object} returns a deferred.
	 */
	QlLeadCreateUpdate: function (oLeadFullData, oCallbackScope)
	{
		/** Initialize. */
		var me = this;
		var xhr = null;

		/** validate arguments. */
		if (oLeadFullData === undefined || oLeadFullData === null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_NO_ARG_PASSED;

		try
		{
			xhr = me._svc.QlLeadCreateUpdate(oLeadFullData
				, function (response)
				{
					switch(response.Code)
					{
						case 0:
							me.fireEvent(SOS.ClientServices.CmsServices.EVNT_LEADCRUP_SUCCESS, response);
							break;
						default:
							var oMsgItem = new SOS.Models.MessageItem({
								Title: "Error Occurred"
								, MessageBody: response.Message
								, MessageType: "Error"
								, Code: response.Code
							});
							me.fireEvent(SOS.ClientServices.CmsServices.EVNT_LEADCRUP_FAILURE, oMsgItem);
							break;
					}
				}
				, oCallbackScope
			);
		}
		catch (oEx)
		{
			var oMsgItem = new SOS.Models.MessageItem({
			 	Title: "Exception thrown on QlLeadCreateUpdate"
				, MessageBody: Ext.String.format("The following error occurred: {0}"
					, oEx)
				, MessageType: "Fatal"
				, Exception: oEx
			});
			me.fireEvent(SOS.ClientServices.CmsServices.EVNT_LEADCRUP_FAILURE, oMsgItem);
		}

		/** Return deferred. */
		return xhr;
	},

	OptionItemAdd: function (sListName, sItemName, oCallBackScope)
	{
		/** Initialize. */
		var me = this;
		var xhr = null;

		/** valudate arguments. */
		if (sListName === undefined || sListName === null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_LISTNAME_IS_MISSING;
		if (sItemName === undefined || sItemName === null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_ITEMNAME_IS_MISSING;

		try
		{
			xhr = me._svc.OptionItemAdd(sListName, sItemName
				, function (response)
				{
					switch(response.Code)
					{
						case 0:
							me.fireEvent(SOS.ClientServices.CmsServices.EVNT_OPTITMAD_SUCCESS, response);
							break;
						default:
							var oMsgItem = new SOS.Models.MessageItem({
								Title: "Error Occurred"
								, MessageBody: response.Message
								, MessageType: "Error"
								, Code: response.Code
							});
							me.fireEvent(SOS.ClientServices.CmsServices.EVNT_OPTITMAD_FAILURE, oMsgItem);
							break;
					}
				}
				, oCallBackScope);
		}
		catch (oEx)
		{
			var oMsgItem = new SOS.Models.MessageItem({
				Title: "Exception thrown on GetOptionItems"
				, MessageBody: Ext.String.format("The following error occurred: {0}"
					, oEx)
				, MessageType: "Fatal"
				, Exception: oEx
			});
			me.fireEvent(SOS.ClientServices.CmsServices.EVNT_OPTITMAD_FAILURE, oMsgItem);
		}

		/** Return deffered. */
		return xhr;
	},

	OptionItemsGet: function (sListName, oCallBackScope)
	{
		/** Initialize. */
		var me = this;
		var xhr = null;

		/** valudate arguments. */
		if (sListName === undefined || sListName === null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_LISTNAME_IS_MISSING;

		try
		{
			xhr = me._svc.OptionItemsGet(sListName
			, function (response)
				{
					switch(response.Code)
					{
						case 0:
							response.ListName = sListName;
							me.fireEvent(SOS.ClientServices.CmsServices.EVNT_OPTITEMS_SUCCESS, response);
							break;
						default:
							var oMsgItem = new SOS.Models.MessageItem({
								Title: "Error Occurred"
								, MessageBody: response.Message
								, MessageType: "Error"
								, Code: response.Code
							});
							me.fireEvent(SOS.ClientServices.CmsServices.EVNT_OPTITEMS_FAILURE, oMsgItem);
							break;
					}
				}
			, oCallBackScope);
		}
		catch (oEx)
		{
			var oMsgItem = new SOS.Models.MessageItem({
				Title: "Exception thrown on GetOptionItems"
				, MessageBody: Ext.String.format("The following error occurred: {0}"
					, oEx)
				, MessageType: "Fatal"
				, Exception: oEx
			});
			me.fireEvent(SOS.ClientServices.CmsServices.EVNT_OPTITEMS_FAILURE, oMsgItem);
		}

		/** Return deffered. */
		return xhr;
	},

	AeCreateNewCustomer: function (oNewCustomerInfo, oCallbackScope)
	{
		/** Initialize. */
		var me = this;
		var xhr = null;

		/** validate arguments. */
		if (oNewCustomerInfo === undefined || oNewCustomerInfo === null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_NO_ARG_PASSED;

		try
		{
			xhr = me._svc.AeCreateNewCustomer(oNewCustomerInfo
			, function (response){
					switch(response.Code)
					{
						case 0:
							me.fireEvent(SOS.ClientServices.CmsServices.EVNT_CUSTNEWI_SUCCESS, response);
							break;
						default:
							var oMsgItem = new SOS.Models.MessageItem({
								Title: "Error Occurred"
								, MessageBody: response.Message
								, MessageType: "Error"
								, Code: response.Code
							});
							me.fireEvent(SOS.ClientServices.CmsServices.EVNT_CUSTNEWI_FAILURE, oMsgItem);
							break;
					}
				}
			, oCallbackScope);
		}
		catch(oEx)
		{
			var oMsgItem = new SOS.Models.MessageItem({
				Title: "Exception thrown on AeCreateNewCustomer"
				, MessageBody: Ext.String.format("The following error occurred: {0}"
					, oEx)
				, MessageType: "Fatal"
				, Exception: oEx
			});
			me.fireEvent(SOS.ClientServices.CmsServices.EVNT_CUSTNEWI_FAILURE, oMsgItem);
		}

		/** Return deferred. */
		return xhr;
	},

	LeadDispositionUpdate: function (nLeadID, nLeadDispositionId, oCallbackScope)
	{
		/** Initialize. */
		var me = this;
		var xhr = null;

		/** validate arguments. */
		if (nLeadID === undefined || nLeadID === null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_NO_ARG_PASSED;
		if (nLeadDispositionId === undefined || nLeadDispositionId === null)
			throw SOS.ClientServices.CmsServices.EXP_MSG_NO_ARG_PASSED;

		try
		{
			xhr = me._svc.LeadDispositionUpdate(nLeadID, nLeadDispositionId
				, function (response){
					switch(response.Code)
					{
						case 0:
							me.fireEvent(SOS.ClientServices.CmsServices.EVNT_LEADDISP_SUCCESS, response);
							break;
						default:
							var oMsgItem = new SOS.Models.MessageItem({
								Title: "Error Occurred"
								, MessageBody: response.Message
								, MessageType: "Error"
								, Code: response.Code
							});
							me.fireEvent(SOS.ClientServices.CmsServices.EVNT_LEADDISP_FAILURE, oMsgItem);
							break;
					}
				}
				, oCallbackScope);
		}
		catch(oEx)
		{
			var oMsgItem = new SOS.Models.MessageItem({
				Title: "Exception thrown on LeadDispositionUpdate"
				, MessageBody: Ext.String.format("The following error occurred: {0}"
					, oEx)
				, MessageType: "Fatal"
				, Exception: oEx
			});
			me.fireEvent(SOS.ClientServices.CmsServices.EVNT_LEADDISP_FAILURE, oMsgItem);
		}

		/** Return deferred. */
		return xhr;
	}
});