/**
 * Created with JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 4/21/12
 * Time: 1:12 PM
 * To change this template use File | Settings | File Templates.
 */

Ext.ns("SOS.Panels");

Ext.define("SOS.Panels.RepInfoContainer",
{
	extend: "Ext.container.Container",
	alias: "widget.repInfoContainer",

	constructor: function(options)
	{
		/** Initialize. */
		var me = this;
		options = options || {};

		var szTpl = SOS.UI.Services.GetTemplate("RepInfoContainer.html");

		/** Set config. */
		var config = Ext.merge(options,
		{
			tpl: szTpl
		});

		/** Init base. */
		me.callParent([config]);
	}
});