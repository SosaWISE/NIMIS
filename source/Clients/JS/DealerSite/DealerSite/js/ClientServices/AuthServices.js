/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/11/12
 * Time: 12:09 AM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.ClientServices');

Ext.define('SOS.ClientServices.AuthServices',
{
	extend: 'Ext.util.Observable',

	statics:
	{
		/** Exceptions */
		EXP_MSG_NO_ARG_PASSED: "No configuration argument was passed.",
		EXP_MSG_NO_APP_TOKEN: "Missing Application Token.",

		/** Events. */
		EVNT_SOSSTART_SUCCESS: 'SessionStartSuccess',
		EVNT_SOSSTART_FAILURE: 'SessionStartFailure',
		EVNT_SOSAUTHN_SUCCESS: 'AuthenticationSuccess',
		EVNT_SOSAUTHN_FAILURE: 'AuthenticationFailure',
		EVNT_CHCKSESS_SUCCESS: 'CheckSessionOnSuccess',
		EVNT_CHCKSESS_FAILURE: 'CheckSessionOnFailure',
		EVNT_TERMSESS_SUCCESS: 'TermSessionOnSuccess',
		EVNT_TERMSESS_FAILURE: 'TermSessionOnFailure',
		EVNT_TOKNAUTH_SUCCESS: 'TokenAuthenOnSuccess',
		EVNT_TOKNAUTH_FAILURE: 'TokenAuthenOnFailure',

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
		this._svc = SOS.IAuthSvcClient.Instance;

		/** Save listeners. */
		this.listeners = config.listeners;
		this.callParent([arguments]);

		/** Return result. */
		return me;
	},

	/**
	 * 
	 * @param szApplicationToken {string} Application Token.
	 * @param callbackScope {object} Scope callback will be in.
	 */
	SosStart: function(szApplicationToken, callbackScope)
	{
		/** Init. */
		var me = this;
		var oResult = null;
		callbackScope = callbackScope !== undefined
			? callbackScope
			: me;

		/** Validate arguments. */
		if (szApplicationToken === undefined || szApplicationToken === null)
			throw SOS.ClientServices.AuthServices.EXP_MSG_NO_APP_TOKEN;

		try
		{
			me._svc.SosStart(szApplicationToken
			, function(result)
			{
				/** Check that we have a valid result. */
				me.fireEvent(SOS.ClientServices.AuthServices.EVNT_SOSSTART_SUCCESS, result);
			}
			, callbackScope);
		}
		catch(ex)
		{
			var oMsgItem = new SOS.Models.MessageItem(
				{
					Title: 'Exception caught'
					, MessageBody: Ext.String.format('Exception thrown on SosStart:<br />', ex)
					, MessageType: 'Failure'
					, Exception: ex
				});
			oMsgItem.ConsoleLog();
			me.fireEvent(SOS.ClientServices.AuthServices.EVNT_SOSSTART_FAILURE, oMsgItem);
		}

		/** Return default value. */
		return oResult;
	},

	SosAuthenticate: function (lSessionId, lDealerId, szUsername, szPassword, callbackScope)
	{
		/** Init. */
		var me = this;
		var oHdrResult;
		callbackScope = callbackScope !== undefined
			? callbackScope
			: me;

		/** Validate Arguments. */
		if (lSessionId === undefined || isNaN(lSessionId))
			throw Ext.String.format('Invalid SessionId "{0}"', lSessionId);

		if (lDealerId === undefined || isNaN(lDealerId))
			throw Ext.String.format('Invalid DealerId: "{0}"', lDealerId);

		if (szUsername === undefined || szUsername === '')
			throw Ext.String.format('Invalid username "{0}"', szUsername);

		if (szPassword === undefined || szPassword === '')
			throw Ext.String.format('Invalid password "{0}"', szPassword);


		/** Execute. */
		try
		{
			oHdrResult = me._svc.SosWiseCrmAuthenticate(lSessionId, lDealerId, szUsername, szPassword,
			function (result, textStatus, jqXhr)
			{
				switch(result.Code)
				{
					case 0:
						me.fireEvent(SOS.ClientServices.AuthServices.EVNT_SOSAUTHN_SUCCESS, result);
						break;
					default:
						var oMessageItem = new SOS.Models.MessageItem(
							{
								Title: textStatus,
								MessageBody: Ext.String.format('Unsuccessful request: {0}', result.Message),
								MessageType: 'Failure',
								Result: result,
								Code: result.Code
							}
						);
						me.fireEvent(SOS.ClientServices.AuthServices.EVNT_SOSAUTHN_FAILURE, oMessageItem);
						break;
				}

				/** Get the hdr result. */
				oHdrResult = jqXhr;
			},
			callbackScope);
		}
		catch(ex)
		{
			var oMsgItem = new SOS.Models.MessageItem(
				{
					Title: 'Exception caught'
					, MessageBody: Ext.String.format('Exception thrown on SosAuthenticate:<br />', ex)
					, MessageType: 'Failure'
					, Exception: ex
				});
			oMsgItem.ConsoleLog();
			me.fireEvent(SOS.ClientServices.AuthServices.EVNT_SOSAUTHN_FAILURE, oMsgItem);
		}

		/** Default return. */
		return oHdrResult;
	},

	CheckSessionStatus: function (callbackScope)
	{
		/** Init. */
		var me = this;
		var oHdrResult;
		callbackScope = callbackScope !== undefined
			? callbackScope
			: me;

		/** Execute. */
		try
		{
			oHdrResult = me._svc.CheckSessionStatus(
				function (result, textStatus)
				{
					switch(result.Code)
					{
						case 0:
							me.fireEvent(SOS.ClientServices.AuthServices.EVNT_CHCKSESS_SUCCESS, result);
							break;
						default:
							var oMessageItem = new SOS.Models.MessageItem(
								{
									Title: textStatus,
									MessageBody: Ext.String.format('Unsuccessful request: {0}', result.Message),
									MessageType: 'Failure',
									Result: result,
									Code: result.Code
								}
							);
							me.fireEvent(SOS.ClientServices.AuthServices.EVNT_CHCKSESS_FAILURE, oMessageItem);
							break;
					}
				},
			callbackScope);
		}
		catch (ex)
		{
			var oMsgItem = new SOS.Models.MessageItem(
				{
					Title: 'Exception caught'
					, MessageBody: Ext.String.format('Exception thrown on CheckSessionStatus:<br />', ex)
					, MessageType: 'Failure'
					, Exception: ex
				});
			oMsgItem.ConsoleLog();
			me.fireEvent(SOS.ClientServices.AuthServices.EVNT_CHCKSESS_FAILURE, oMsgItem);
		}

		/** Return header. */
		return oHdrResult;
	},

	TerminateCurrentSession: function(oCallbackScope)
	{
		/** Initialize. */
		var me = this;
		var xhr = null;

		/** Execute. */
		try
		{
			xhr = me._svc.TerminateCurrentSession(function(response, textStatus){
				switch (response.Code)
				{
					case 0:
						me.fireEvent(SOS.ClientServices.AuthServices.EVNT_TERMSESS_SUCCESS, response);
						break;
					default:
						var oMessageItem = new SOS.Models.MessageItem(
							{
								Title: textStatus,
								MessageBody: Ext.String.format('Unsuccessful request: {0}', response.Message),
								MessageType: 'Failure',
								Result: response,
								Code: response.Code
							}
						);
						me.fireEvent(SOS.ClientServices.AuthServices.EVNT_TERMSESS_FAILURE, oMessageItem);
						break;
				}
			}, oCallbackScope);
		}
		catch(oEx)
		{
			var oMsgItem = new SOS.Models.MessageItem({
				Title: "Exception thrown on TerminateCurrentSession"
				, MessageBody: Ext.String.format("The following error occurred: {0}",
					oEx)
				, MessageType: "Fatal"
				, Exception: oEx
			});
			me.fireEvent(SOS.ClientServices.AuthServices.EVNT_TERMSESS_FAILURE, oMsgItem);
		}

		/** Return result. */
		return xhr;
	},

	TokenAuthentication: function(sTokenStream, callbackScope)
	{
		/** Initialize. */
		var me = this;
		var xhr = null;
		// ** Url Encode string
		sTokenStream = encodeURIComponent(sTokenStream);

		/** Execute. */
		try
		{
			xhr = me._svc.TokenAuthentication(sTokenStream, function (oResponse, textStatus)
			{
				switch (oResponse.Code)
				{
					case 0:
						me.fireEvent(SOS.ClientServices.AuthServices.EVNT_TOKNAUTH_SUCCESS, oResponse);
						break;
					default:
						var oMessageItem = new SOS.Models.MessageItem(
							{
								Title: textStatus,
								MessageBody: Ext.String.format('Unsuccessful request: {0}', oResponse.Message),
								MessageType: 'Failure',
								Result: oResponse,
								Code: oResponse.Code
							}
						);
						me.fireEvent(SOS.ClientServices.AuthServices.EVNT_TOKNAUTH_FAILURE, oMessageItem);
						break;
				}
			}, callbackScope);
		}
		catch(oEx)
		{
			var oMsgItem = new SOS.Models.MessageItem({
				Title: "Exception thrown on TokenAuthentication"
				, MessageBody: Ext.String.format("The following error occurred: {0}",
					oEx)
				, MessageType: "Fatal"
				, Exception: oEx
			});
			me.fireEvent(SOS.ClientServices.AuthServices.EVNT_TOKNAUTH_FAILURE, oMsgItem);
		}

		/** Return result. */
		return xhr;
	}
});
