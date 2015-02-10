/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/9/12
 * Time: 10:27 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.Models');

Ext.define('SOS.Models.BaseModel',
{
	extend: "Ext.data.Model",

	constructor: function()
	{
		this.addEvents('changed', 'deleted');

		this.callParent(arguments);
		return this;
	},

	NotifyChanged: function ()
	{
		this.fireEvent('changed', this);
	},

	NotifyDeleted: function ()
	{
		this.fireEvent('deleted', this);
	},

	RegisterChangeHandler: function(callback, scope)
	{
		this.mon(this, 'changed', callback, scope);
	},

	UnregisterChangeHandler: function(callback, scope)
	{
		this.mun(this, 'changed', callback, scope);
	},

	RegisterDeleteHandler: function(callback, scope)
	{
		this.mon(this, 'deleted', callback, scope);
	},

	UnregisterDeleteHandler: function(callback, scope)
	{
		this.mun(this, 'deleted', callback, scope);
	}
});

SOS.Models.DefineModel = function(modelName, config)
{
	var cls = Ext.define.call(Ext, modelName, config,
		function()
		{
			/** Initialize. */
			var me = this;
			var proto = me.prototype;

			/** Define getters/setters for the fields. */
			var propDesc =
			{
				enumerable: true,
				configurable: true
			};

			Ext.each(proto.fields.items, function(f)
			{
				/** Init. */
				var propName = f.name;
				propDesc.get = function()
				{
					return this.get(propName);
				};

				propDesc.set = function(value)
				{
					this.set(propName, value);
				};

				Object.defineProperties(proto, propName, propDesc);
			});
		});
};