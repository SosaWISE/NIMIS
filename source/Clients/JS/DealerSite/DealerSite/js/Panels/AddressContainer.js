/**
 * Created with JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 4/21/12
 * Time: 9:55 AM
 * To change this template use File | Settings | File Templates.
 */
Ext.ns("SOS.Panels");

Ext.define("SOS.Panels.AddressContainer",
{
	extend: 'Ext.container.Container',
	alias: 'widget.addrContainer',

	constructor: function(options)
	{
		/** Initialize. */
		//var me = this;
		options = options || {};
		options.data.addBtnCaption = "Add Address";

		var szTpl = SOS.UI.Services.GetTemplate("AddressContainer.html");

		var config = Ext.merge(options,
			{
				tpl: szTpl
			});

		/** Init base. */
		this.callParent([config]);
	}
});