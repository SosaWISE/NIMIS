/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 2/22/12
 * Time: 10:52 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns("SOS.Controllers");

Ext.define("SOS.Controllers.AddressCompController",
{
	extend: "Ext.util.Observable",

	events:
	{
		"Save": true
	},

	constructor: function (config)
	{
		/** Call parent constructor. */
		this.callParent(arguments);
	},

	listeners:
	{
		Save: function(params)
		{
			this.OnSave(params);
		}
	},

	OnSave: function (params)
	{

	}
});
