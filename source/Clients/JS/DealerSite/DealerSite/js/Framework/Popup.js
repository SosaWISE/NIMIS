/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/13/12
 * Time: 12:47 AM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.Framework');

Ext.define('SOS.Framework.Popup', {

	extend: 'Ext.window.Window',

	statics:
	{
		create: function (popupModel) {
			/** Init. */
			var popup;

			/** Create instance of popupModel. */
			popup = new SOS.Framework.Popup(popupModel);

			/** Return created instance. */
			return popup;
		},

		PopupFormValidation: function(options){
			/** Validate arguments. */
			if ((!options)) return;

			try{
				/** Initialize. */
				var defaultTitle = "Default Text Goes Here";
				var templateKey = options.TemplateKey || 'form-validation.html';
				var baseModel = new SOS.Models.Popups.Base({
					Title: options.Title || defaultTitle
					, width: options.Width || 545
					, height: options.Height || 'auto'
					, tpl: SOS.UI.Services.GetTemplate(templateKey)
					, data: {
						id: options.id
						, Title: options.Title || defaultTitle
						, MessageBody: options.MessageBody
						, ValidationList: options.ValidationList
					}
					, events: [
						{
							id: "hdr-close-x"
							, event: "click"
							, fn: function (me)
								{
									me.close();
									me.destroy();
								}
						},
						{
							id: "form-valid-close"
							, event: "click"
							, fn: function (me)
							{
								me.close();
								me.destroy();
							}
						},
						{
							id: "img-valid-close"
							, event: "click"
							, fn: function (me)
							{
								me.close();
								me.destroy();
							}
						}
					]
				});

				/** Show popup. */
				SOS.Framework.Popup.create(baseModel).show();

			} catch (ex) {
				return alert(ex);
			}
		},
		GeneralPopup: function (options)
		{
			/** Validate Arguments. */
			if (!options) alert("General Popups require options argument being passed.");

			try
			{
				/** Initialize. */
				var defaultTitle = "General Message";
				var templateKey = options.TemplateKey || "general-message.html";
				var baseModel;
				var oEvents = [
					{
						id: "gen-close-x"
						, event: "click"
						, fn: function (me)
					{
						me.close();
						me.destroy();
					}
					},
					{
						id: "gen-close"
						, event: "click"
						, fn: function (me)
						{
							me.close();
							me.destroy();
						}
					},
					{
						id: "img-close-x"
						, event: "click"
						, fn: function (me)
						{
							me.close();
							me.destroy();
						}
					}
				];

				if (options.self !== undefined)
				{
					baseModel = new SOS.Models.Popups.Base({
						Title: options.data.Title
						, tpl: SOS.UI.Services.GetTemplate(templateKey)
						, data: {
							Title: options.data.Title
							, MessageBody: options.data.MessageBody
							, MessageType: options.data.MessageType
							, Code: options.data.Code
						}
						, events : oEvents
					});
				}
				else
				{
					baseModel = new SOS.Models.Popups.Base(
					{
						Title: options.Title || defaultTitle
						, tpl: SOS.UI.Services.GetTemplate(templateKey)
						, data: {
							Title: options.Title || defaultTitle
							, MessageBody: options.MessageBody
							, MessageType: options.PopupType || "Warning"
						}
						, events: oEvents
					});
				}

				/** Show popup. */
				var oMDialog = SOS.Framework.Popup.create(baseModel);
				oMDialog.show();
			}
			catch (oEx)
			{
				return alert(oEx);
			}
		},

		Confirm: function (options)
		{
			/** Validate Arguments. */
			if (!options) alert("General Popups require options argument being passed.");

			try
			{
				/** Initialize. */
				function fnNo(me)
				{
					if (options.OnCancel) options.OnCancel();
					me.close();
					me.destroy();
				}
				function fnYes(me)
				{
					if (options.OnConfirm) options.OnConfirm();
					me.close();
					me.destroy();
				}
				var defaultTitle = "General Message";
				var templateKey = options.TemplateKey || "confirm-message.html";
				var baseModel;
				var oEvents = [
					{
						id: "gen-close-x"
						, event: "click"
						, fn: fnNo
					},
					{
						id: "gen-close"
						, event: "click"
						, fn: fnNo
					},
					{
						id: "img-close-x"
						, event: "click"
						, fn: fnNo
					},
					{
						id: "img-Yes-x"
						, event: 'click'
						, fn: fnYes
					},
					{
						id: "gen-yes"
						, event: 'click'
						, fn: fnYes
					}
				];

				if (options.self !== undefined)
				{
					baseModel = new SOS.Models.Popups.Base({
						Title: options.data.Title
						, tpl: SOS.UI.Services.GetTemplate(templateKey)
						, data: {
							Title: options.data.Title
							, MessageBody: options.data.MessageBody
							, MessageType: options.data.MessageType || "Question"
							, Code: options.data.Code
						}
						, events : oEvents
					});
				}
				else
				{
					baseModel = new SOS.Models.Popups.Base(
						{
							Title: options.Title || defaultTitle
							, tpl: SOS.UI.Services.GetTemplate(templateKey)
							, data: {
							Title: options.Title || defaultTitle
							, MessageBody: options.MessageBody
							, MessageType: options.PopupType || "Question"
						}
							, events: oEvents
						});
				}

				/** Show popup. */
				var oMDialog = SOS.Framework.Popup.create(baseModel);
				oMDialog.show();
			}
			catch (oEx)
			{
				return alert(oEx);
			}
		}
	},

	/**
	* @constructor This is the constructor description
	* @parameter {object} Kin2.PopupModel.{model}
	*/
	constructor: function (popupModel) {

		this.popupModel = popupModel;

		var me = this;

		/** Add handle to move window arround. */
		popupModel.Title = "Click here to move window.";

		Ext.Object.each(popupModel.data, function (key, value) {
			if (key !== 'events') {
				me[key] = value;
			}
		});

		this.listeners = {
			afterRender: function () {
				this.AfterRender();
			}
		};

		this.callParent(popupModel);
	},

	/**
	* @AfterRender	Gets called after the popup is rendered
	*				Creates the events tied to each event
	*/
	AfterRender: function () {
		var me = this;
		Ext.each(this.popupModel.get('events'), function (fn) {
			if (fn) {
				var el = Ext.get(fn.id);
				el.on(fn.event, function () {
					fn.fn(me);
				});
			}
		});
	}
});