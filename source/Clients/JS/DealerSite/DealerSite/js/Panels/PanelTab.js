/**
 * Created with JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 4/28/12
 * Time: 5:01 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.ns("SOS.Panels");

Ext.define("SOS.Panels.PanelTab",
{
	extend: 'Ext.container.Container',
	alias: 'widget.panelTabs',

	constructor: function (options)
	{
		/** Initialize. */
		var me = this;
		options = options || {};

		/** Init properties. */
		me._tabName = (options.TabName !== undefined)
			? options.TabName
			: "[NO NAME]";
		me._tabId = (options.TabId !== undefined)
			? options.TabId
			: 'NO_ID';
		me._tabCaption = (options.TabCaption !== undefined)
			? options.TabCaption
			: "[NO CAPTION]";
		me._tabType = (options.TabType !== undefined)
			? options.TabType
			: "GenTab";

		var config = Ext.merge (options,
			{
				tpl: ['<div class="card-sys-tab">{TabName}<canvas id="{TabType}-{TabId}" style="width:15px;height:15px;padding: 2px 2px 0 20px;"></canvas></div>'],
				data: {
					TabId: me._tabId,
					TabName: me._tabName,
					TabCaption: me._tabCaption,
					TabType: me._tabType
				}
			});

		/** Init base. */
		this.callParent([config]);

	},

	/** START PROPERTIES */
	TabName: function() { return this._tabName; },
	TabId: function() { return this._tabId; },
	TabCaption: function() { return this._tabCaption; },
	/** END   PROPERTIES */

	OnAfterRender: function ()
	{
		/** Initialize. */
		//var me = this;
	}
}
);