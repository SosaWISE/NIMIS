/**
 * Created with JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 4/21/12
 * Time: 12:49 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.ns("SOS.Panels");

Ext.define("SOS.Panels.MonitoringInfoContainer",
{
	extend: "Ext.container.Container",
	alias: "widget.monitoringInfoContainer",

	constructor: function(options)
	{
		/** Initialize. */
		me = this;
		options = options || {};

		var szTpl = SOS.UI.Services.GetTemplate("MonitoringInfoContainer.html");

		var config = Ext.merge(options,
			{
				tpl: szTpl
			});

		/** Init base. */
		me.callParent([config]);
	}
});