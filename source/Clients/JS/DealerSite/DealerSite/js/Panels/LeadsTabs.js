/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 2/10/12
 * Time: 8:48 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.Panels');

Ext.define('SOS.Panels.LeadsTabs',
{
	extend: 'Ext.container.Container',
	alias: 'widget.leadsTabs',

	constructor: function (options)
	{
		/** Initialize. */
		var me = this;
		options = options || {};
		var szTpl = SOS.UI.Services.GetTemplate('leads-tab.html');

		var config = Ext.merge(options,
			{
				//tpl: szTpl
				items:
					[
						{
							tpl: ['<div class="card-hdr" id="cur-LeadId" style="float: right;">Current LeadID: <strong>{LeadId}</strong></div>'],
							data: {LeadId: options.data.leadId }
						},
						{
							id: "leadsTabPanel",
							xtype: "container"
						},
						{
							tpl:
								[
									'<div id="divLeadsPlus" class="card-sys-tab">+</div>',
									'<div>&nbsp;<img alt="{ClickPlusMessage}" src="/img/list1-marker-left.gif" style="vertical-align: 30%;" />&nbsp;{ClickPlusMessage}</div>'
								],
							data: { ClickPlusMessage: options.data.clickPlusMessage }
						}
					]
			});

		/** Init base. */
		this.callParent([config]);
	},
	statics: {
		OnCreateNewLead: function ()
		{
			// ** Call the new window
			SOS.Modals.LeadCreateUpdate.StartTheWizard();
		},

		UnSelectAllTabs: function()
		{
			/** Initialize. */
			var oTabsSel = $(".card-sys-tab");
			oTabsSel.removeClass("selected");
		},

		/**
		 *
		 * @param oItem {SOS.Panels.LeadsPanelItem}
		 * @constructor
		 */
		AddTab: function (oItem)
		{
			/** Initialize. */
			var leadsTabPanelEl = $("#leadsTabPanel");
			var lLeadId = oItem.data.LeadId;
			var lCustomerMasterFileId = oItem.data.CustomerMasterFileId;
			var cSelector = "ld-" + lLeadId + "-" + lCustomerMasterFileId + "-tab";
			var curLeadTitle = $("#cur-LeadId strong");
			curLeadTitle.html("");  // Clear the data.
			curLeadTitle.text(lLeadId); // Set the current leadId

			/** Check to see if it's already there. */
			if ($("#" + cSelector).length === 0)
			{
				var szHtml = Ext.String.format('<div id="{0}" class="card-sys-tab">{1}</div>'
					, cSelector
					, Ext.String.format("{0} {1} (LD-{2})", oItem.data.FirstName, oItem.data.LastName, lCustomerMasterFileId));
				leadsTabPanelEl.append(szHtml);
			}

			SOS.Panels.LeadsTabs.UnSelectAllTabs();
			var oTabEl = $("#" + cSelector);
			oTabEl.addClass("selected");

			/** Bind action to tab element. */
			oTabEl.unbind("click").bind("click", function (me)
			{
				console.log(me);
				/** Initialize. */
				var arg =
				{
					Id: me.currentTarget.id.replace("-tab", ""),
					NoteAccount: false
				};
				SOS.Panels.LeadsPanel.OpenLead(arg);
			});
		},
		RemoveTab: function (arg)
		{
			/** Initialize. */
			var oItemEl = $("#" + arg.id);
			oItemEl.remove();
		},
		SelectTab: function (oItem)
		{
			/** Create selector. */
			var oTabEl = $("#" + oItem._panelItemKey + "-tab");

			SOS.Panels.LeadsTabs.UnSelectAllTabs();
			oTabEl.addClass("selected");
		}
	},

//	listeners:
//		{
//			afterrender: this.OnAfterRender
//		},

	/** Member functions. */
	OnAfterRender: function ()
	{
		/** Initialize. */
		var me = this;
		var statusColor = "#FFFF00";
		var options = {
				Cmp: "moni-status32334",
			Context: "2d",
			CenterX: 2,
			CenterY: 2,
			Radius: 4,
			FillStyle: statusColor,
			LineWidth: 1,
			StrokeStyle: statusColor,
			Width: 15,
			Height: 15
		};
//		SOS.Utils.Geometry.Circle(options);
//
//		statusColor = "#FF0000";
//		options.Cmp = "moni-status34343";
//		options.FillStyle = statusColor;
//		options.StrokeStyle = statusColor;
//		SOS.Utils.Geometry.Circle(options);
//
//		statusColor = "#00FF00";
//		options.Cmp = "moni-status32323";
//		options.FillStyle = statusColor;
//		options.StrokeStyle = statusColor;
//		SOS.Utils.Geometry.Circle(options);

//		this.show();

		/** Bind handlers to items...*/
		me.divLeadsPlusEl = Ext.get("divLeadsPlus");
		me.divLeadsPlusEl.on('click', SOS.Panels.LeadsTabs.OnCreateNewLead);
	}
});