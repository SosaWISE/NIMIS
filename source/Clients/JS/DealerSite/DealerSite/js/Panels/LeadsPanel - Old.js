/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/25/12
 * Time: 8:31 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.Panels');

Ext.define('SOS.Panels.LeadsPanel',
{
//	extend: 'Ext.container.Container',
	extend: 'Ext.panel.Panel',
	alias: 'widget.SosLeadsPanel',
	_active: true,

	/** .ctor. */
	constructor: function(options)
	{
		/** Initialize. */
		var me = this;
		options = options || {};
		var szTpl = SOS.UI.Services.GetTemplate('leads-panel.html');

		var config = Ext.merge(options,
			{
				//autoScroll: true
				//, width: '100%'
				 //height: 'auto'
				padding: '80 0 0 0'
				, tpl: szTpl
				, data: {spookyDude: 'George Sorros', leadId: 123233}
				, listeners:
				{
					afterrender: me.OnAfterRender
				}
				, items:
					[
						{
							id: 'leadOuter',
							xtype: 'container',
							//height: 'auto',
							//width: '100%',
							style: {
								'background': '-webkit-linear-gradient(top, #fff, #ddd)',
								'border-radius': '10px'
								//width: '1170px'
							},
							items:
								[
									{
										id: 'sysTabs',
										name: 'sysTabs',
										xtype: 'leadsTabs',
										padding: '20 12 0 12',
										//width: '100%',
										data: {spookyDude: 'George Sorros', leadId: 123233}
									},
									{
										id: 'leadContRight',
										name: 'container',
										//height: 'auto',
										style: { 'float' : 'right', 'width' : '242px' },
										items:
											[
												{
													id: 'repInfo',
													name: 'repInfo',
													xtype: 'repInfoContainer',
													data: {
														RepName: 'Andres Sosa',
														RepId: 'SOSA001'
													}
												}
											]
									},
									{
										id: 'leadContMiddle',
										xtype: 'container',
										style: { 'float' : 'right', 'width' : '572px' },
										items:
											[
												{
													id: 'leadContMiddleInnerRight',
													name: 'leadContMiddleInnerRight',
													xtype: 'container',
													style: { 'float' : 'right'},
													items:
													[
														{
															id: 'monitoringInfo',
															name: 'monitoringInfo',
															xtype: 'monitoringInfoContainer',
															data: {Title: "Monitoring Information"}
														}
													]
												},
												{
													id: 'leadContMiddleInnerLeft',
													name: 'leadContMiddleInnerLeft',
													xtype: 'container',
													//style: { 'float' : 'left'},
													items:
														[
															{
																id: 'billingInfo',
																name: 'billingInfo',
																xtype: 'monitoringInfoContainer',
																data: {Title: "Billing Information"}
															}
														]
												}
											]
									},
									{
										id: 'leadContLeft',
										xtype: 'container',
										//height: 'auto',
										style: { 'width' : '354px'},
										items:
											[
												{
													id: 'sysAddress',
													name: 'sysAddress',
													xtype: 'addrContainer',
													data: {spookyDude: 'Andres Sosa', AddressId: 123123}
												}
											]
									}
								]
						}
					]
			});

		this.callParent([config]);

		this.on('activate', this.OnActivate, this);
		this.on('deactivate', this.OnDeactivate, this);
	},

	/** Member functions. */
	OnAfterRender: function ()
	{
		/** Initialize. */
		var sysTabEl = Ext.getCmp("sysTabs");
		sysTabEl.OnAfterRender();
		console.log('sysTabEl: ', sysTabEl);

		/** Initialize. */
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
		SOS.Utils.Geometry.Circle(options);

		statusColor = "#FF0000";
		options.Cmp = "moni-status34343";
		options.FillStyle = statusColor;
		options.StrokeStyle = statusColor;
		SOS.Utils.Geometry.Circle(options);

		statusColor = "#00FF00";
		options.Cmp = "moni-status32323";
		options.FillStyle = statusColor;
		options.StrokeStyle = statusColor;
		SOS.Utils.Geometry.Circle(options);

		this.show();
	},

	OnResize: function (cmp, w, h)
	{
		this.callParent(arguments);
	},

	OnActivate: function()
	{
		console.log('LeadsPanel.OnActivate');
		this._active = true;
		this.show();
	},

	OnDeactivate: function()
	{
		this._active = false;
		this.hide();
	}

});
