/**
 * Created with JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 4/21/12
 * Time: 6:03 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.ns("SOS.Panels");

Ext.define("SOS.Panels.MainNavPanel",
{
	extend: "Ext.container.Container",
	alias: "widget.mainNavPanel",

	constructor: function(options)
	{
	 	/** Initialize. */
		 var me = this;
		options = options || {};
		var oData = options.data !== undefined
			? options.data
			: { IsMasterDealer: false }

		/** Check if Master Dealer is logged in. */
		if (SOS.AppService.UserInfo)
		{
			oData.IsMasterDealer = SOS.AppService.UserInfo.DealerId === 5000;
			console.log(SOS.AppService.ActiveUser, SOS.AppService.UserInfo);
		}

		/** Get template. */
		var szTpl = SOS.UI.Services.GetTemplate("MainNavPanel.html");

		var config = Ext.merge(options,
			{
				tpl: szTpl
				, data: oData
				, listeners:
				{
					afterrender: me.OnAfterRender
				}
			});

		/** call parent constructor. */
		me.callParent([config]);
	},

	/** Member functions. */
	OnAfterRender: function ()
	{
		/** Initialize. */
		var me = this;
		var aMainLogoutEl = $("#aMainLogout");
		aMainLogoutEl.unbind().bind('click', function (){

			/** Confirm that the user wants to logout. */
			SOS.Framework.Popup.Confirm({
				Title: "About to Exit the App."
				, MessageBody: "Are you sure you want to exit the applicaiton?"
				, OnConfirm: function ()
				{
					me.Logout(me);
				}
			});
		});

		this.show();
	},

	Logout: function (rootScope)
	{
		/** Initialize. */
		function onSuccess(response)
		{
			/** Displace result to console. */
			console.log(response);

			/** Refresh the page. */
			window.location.href = SOSConfig.REST_BASE;
		}
		function onFailure(oMsgItem) { SOS.Framework.Popup.GeneralPopup(oMsgItem); }
		var oSrv = new SOS.ClientServices.AuthServices();
		oSrv.on(SOS.ClientServices.AuthServices.EVNT_TERMSESS_SUCCESS, onSuccess);
		oSrv.on(SOS.ClientServices.AuthServices.EVNT_TERMSESS_FAILURE, onFailure);
		oSrv.TerminateCurrentSession(rootScope);
	}

});
