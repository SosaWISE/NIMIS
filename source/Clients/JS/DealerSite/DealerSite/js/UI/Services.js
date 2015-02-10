/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 2/10/12
 * Time: 9:23 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns("SOS.UI");

SOS.UI.Services = (function ()
{
	/** Private members. */
	var waiter = null;
	var init = function()
	{
		if (waiter !== null) return;

		waiter = new SOS.UI.WaitControl(window.MainWindow);

	};
	var extTemplates = {};

	/** Define Member Functions. */
	return {
		Initialize: function ()
		{
			SOS.AppService.RegisterHandler('somethinghappened.', SOS.UI.Service.ShowSomethingHappened);
		},

		ShowBusy: function (msg)
		{
			init();
			waiter.msg = msg;
			waiter.show();
		},

		Hide: function()
		{
			init();
			waiter.hide();
		},

		UpdateMessage: function (msg)
		{
			init();
			waiter.UpdateMessage(msg);
		},

		ShowError: function (options)
		{
			var dlg = new SOS.Framework.Popup.ServerErrorDialog(options);
			dlg.show();
		},

		GetTemplate: function (templateId)
		{
			/** Initialize. */
			var tmpl = extTemplates[templateId];

			/** Check that the template is in the dictionary, */
			if (tmpl === undefined)
			{
				/** Initialize. */
				var tmplEl = Ext.fly(templateId);
				if (!tmplEl)
				{
					/** Not found on the page. */
					console.log(Ext.String.format("*** template not found: [{0}] ***", templateId));
					extTemplates[templateId] = { Available: false};
					return null;
				}
				else
				{
					/** Found and will be placed in the dictionary. */
					var type = tmplEl.getAttribute("type");
					var template = (type === "text/ext-template")
							? new Ext.XTemplate(tmplEl.dom.innerHTML)
							: tmplEl.dom.innerHTML;

					tmpl =
					{
						Available: true,
						Template: template,
						Type: type
					};

					extTemplates[templateId] = tmpl;
				}
			}

			/** If template is unavailable return null. */
			if (!tmpl.Available) return null;

			/** Default path of execution. */
			return tmpl.Template;
		},

		ShowSomethingHappened: function ()
		{
			/** Show that something happened. */
			alert("You need to crate a popup message here that something happened.");
		}
	};
})();