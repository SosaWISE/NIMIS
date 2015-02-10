/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/10/12
 * Time: 11:24 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.Views');

Ext.define('SOS.Views.LoginView',
{
	extend: 'Ext.window.Window',

	statics:
	{
		Instance: function ()
		{
			/** Initialize. */
			return new SOS.Views.LoginView();
		}
	},

	events:
	[
		{
			id: 'login-form-close',
			event: 'click',
			fn: function(cmp)
			{
				cmp.close();
			}
		}
	],

	listeners:
	{
		afterRender: function ()
		{
			this.OnAfterRender();
		}
	},

	/**
	 * Builds a view for the login form.
	 * @param options {object}
	 */
	constructor: function(options)
	{
		/** Initialization. */
		var me = this;
		me.SessionId = options.SessionId;

		var config = {
			id: 'login-dialog',
			ui: 'none',
			width: 383,
			height: 300,
			plain: true,
			modal: true,
			frame: false,
			preventHeader: true,
			minimizable: false,
			collapsible: false,
			closable: true,
			draggable: false,
			resizable: false,
			shadow: false,
			border: false,
			bodyStyle: { background: '#FFFFFF', overflow: 'visible' },
			style: { overflow: 'visible' },
			tpl: SOS.UI.Services.GetTemplate('LoginView.html'),
			data: {
				SessionId: options.SessionId
			}
		};

		/** Call parent constructor. */
		me.callParent([config]);
	},

	/** Member functions. */
	OnAfterRender: function()
	{
		/** Initialize. */
		var me = this;

		/** Bind close event. */
		me.closeEl = Ext.get('login-form-close');
		me.closeEl.on('click', function()
		{
			me.close();
			me.destroy();
		});

		/** Bind fields to el. */
		me.dealerEl = Ext.get('dealerId');
		me.usernameEl = Ext.get('username');
		me.passwordEl = Ext.get('password');

		/** Bind submit event. */
		me.submitImgEl = Ext.get('imgLogin');
		me.submitImgEl.on('click', me.SubmitForm, this);
		me.submitEl = Ext.get('aLoginSubmit');
		me.submitEl.on('click', me.SubmitForm, this);
	},

	SubmitForm: function()
	{
		/** Init. */
		var me = this;
		var svc = SOS.ClientServices.AuthServices.Create();

		/** Validate form. */
		var validationMsg = this.ValidateForm();
		if(validationMsg !== '') {
			var options = {
				id: 'login-frm-validation'
				, Title: 'Login Form Failed Validation'
				, MessageBody: 'Failed form validation.  See items below:'
				, ValidationList: validationMsg
				, Events: [
					{
						id: 'hdr-close-x'
						, event: 'click'
						, fn: function (dlg) {
							dlg.close();
						}
					}
					,{
						id: 'form-valid-close'
						, event: 'click'
						, fn: function(dlg) {
							dlg.close();
						}
					}
				]
			};
			SOS.Framework.Popup.PopupFormValidation(options);
			return;
		}

		/** Execute. */
		try
		{
			function onSuccess(result)
			{
				SOS.AppService.DispatchEvent(SOS.AppEvents.AppLogin, result.Value);
				me.close();
				me.destroy();

				/** Refresh the page. */
				window.location.href = SOSConfig.REST_BASE;
			}
			function onFailure(oMessageItem)
			{
				/** Initialize. */
				oMessageItem.data.Title = 'Login Failed';
				oMessageItem.data.MessageType = 'Warning';
				//var genMessage = new SOS.Models.Popups.GeneralModel(oMessageItem);
				SOS.Framework.Popup.GeneralPopup(oMessageItem);
			}

			/** Execute. */
			svc.on(SOS.ClientServices.AuthServices.EVNT_SOSAUTHN_SUCCESS, onSuccess);
			svc.on(SOS.ClientServices.AuthServices.EVNT_SOSAUTHN_FAILURE, onFailure);
			svc.SosAuthenticate(this.SessionId
			, this.dealerEl.dom.value, this.usernameEl.dom.value, this.passwordEl.dom.value);
		}
		catch(ex)
		{
			alert(ex);
		}
	},

	ValidateForm: function ()
	{
		/** Initialize. */
		var dealerValue = this.dealerEl.dom.value;
		var usernameValue = this.usernameEl.dom.value;
		var passwordValue = this.passwordEl.dom.value;
		var validationMsg = '';

		/** Check dealerId. */
		if(dealerValue === ''){
			validationMsg = validationMsg + '<li>Dealer ID is a required field.</li>';
		}
		if(usernameValue === ''){
			validationMsg = validationMsg + '<li>Username is a required field.</li>';
		}
		if(passwordValue === ''){
			validationMsg = validationMsg + '<li>Password is a required field.</li>';
		}

		/** Default execution. */
		return validationMsg;
	}
});

