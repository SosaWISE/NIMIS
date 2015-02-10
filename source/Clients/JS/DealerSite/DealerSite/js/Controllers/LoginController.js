/**
 * Created by JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 12/26/11
 * Time: 11:39 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.ns('SOS.Controllers');

Ext.define('SOS.Controllers.LoginController',
{
	extend: 'Ext.util.Observable',

	statics:
	{
		create: function () {
			return new SOS.Controllers.Login({SessionId: SOS.AppService.SessionId});
		}
	},

	events:
	{
		"Authenticate" : true,
		"ChangePassword" : true
	},

	listeners:
	{
		Authenticate: function(dealerId, username, password)
		{
			this.onAuthenticate(dealerId, username, password);
		},
		ChangePassword: function(dealerId, email, ssn)
		{}
	},

	constructor: function (config)
	{
		/** Validate arguments. */
		if (config === undefined) throw 'config object not passed to LoginController.';
		if (config.SessionId === undefined) throw 'SessionId is missing in the config object.';

		/** Get required values. */
		this.sessionId = config.SessionId;

		/** Call parent constructor. */
		this.callParent(arguments);
	},

	/** Member functions. */
	onAuthenticate: function (dealerId, username, password, callback)
	{
		/** Initialize. */
		var authModel = new SOS.Models.AuthenticationModel({
			SessionId: this.SessionId,
			DealerId: dealerId,
			Username: username,
			Password: password
		});

		/** Build handlers. */
		function onSuccess(result)
		{
			/** validate the result and bind to authentication object. */

		}
	}

});
