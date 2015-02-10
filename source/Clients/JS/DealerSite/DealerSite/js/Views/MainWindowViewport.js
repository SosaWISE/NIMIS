/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/25/12
 * Time: 7:53 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.Views');

Ext.define('SOS.Views.MainWindowViewport',
{
	extend: 'Ext.container.Viewport',

	statics: {
		Panels: {
			LeadsPanel: 'LeadsPanel'
			, SearchPanel: 'SearchPanel'
			, CustomerPanel: 'CustomerPanel'
			, AppointmentPanel: 'AppointmentPanel'
			, OrderPanel: 'OrderPanel'
			, SettingPanel: 'SettingPanel'
		}
	},

	/**
	 * Constructor for this Viewport.
	 * @param config {object}
	 */
	constructor: function (config)
	{
		var me = this;
		config = config || {};

		var activePanel = 0;
		if (config.startPanel)
		{
			switch (config.startPanel)
			{
				case SOS.Views.MainWindowViewport.Panels.LeadsPanel:
					activePanel = 0;
					break;
				case SOS.Views.MainWindowViewport.Panels.SearchPanel:
					activePanel = 1;
					break;
				case SOS.Views.MainWindowViewport.Panels.AppointmentPanel:
					activePanel = 2;
					break;
				case SOS.Views.MainWindowViewport.Panels.OrderPanel:
					activePanel = 3;
					break;
				case SOS.Views.MainWindowViewport.Panels.SettingPanel:
					activePanel = 4;
					break;
				case SOS.Views.MainWindowViewport.Panels.CustomerPanel:
					activePanel = 5;
					break;
			}
		}

		config = Ext.apply(config,
		{
			id: 'sos-main-window',
			layout: 'vbox',
			items:
			[
				{
					itemId: 'mainNavPanel',
					id: 'mainNavPanel',
					xtype: 'mainNavPanel',
					data: {
						UserFullName : SOS.AppService.UserInfo.Fullname,
						UID: SOS.AppService.UserInfo.UserId,
						LastLoginDateTime: Ext.util.Format.dateRenderer('(D) M-d-Y')(SOS.AppService.UserInfo.LastLogin),
						DealerName: SOS.AppService.UserInfo.DealerName
					}
				},
				{
					id: 'view-container'
					, xtype: 'container'
					, layout: 'card'
					//, width: 'auto'
					, flex: 1
					//, id: 'view-container'
					, activeItem: activePanel
					, items:
					[
						{
							itemId: 'main-leadsPanel',
							id: 'main-leadsPanel',
							xtype: 'SosLeadsPanel',
							//width: '100%',
							listeners:
							{
								added: function (cmp)
								{
									console.log('added: ', cmp);
									me._leadsPanel = cmp;
								}
							}
						},
						{
							itemId: "main-searchPanel",
							id: "main-searchPanel",
							xtype: "SosSearchPanel",
							listeners:
							{
								added: function(cmp)
								{
									console.log('added: ', cmp);
									me._searchPanel = cmp;
								}
							}
						}
					]
				}
			],
			listeners:
			{
				scope: this,
				//resize: this.OnResize,
				afterrender: this.OnAfterRender
			}
		});

		this.callParent([config]);
	},

	/** Member Properties. */
	get Width()
	{
		return this._width;
	},
	get Height()
	{
		return this._height;
	},
	get LeadsPanel()
	{
		return this._leadsPanel;
	},
	get SearchPanel()
	{
		return this._searchPanel;
	},

	/** Event Handlers. */
	OnAfterRender: function _mw_OnAfterRender()
	{
		this._viewContainer = Ext.getCmp('view-container');

		SOS.AppService.RegisterHandler('applogin', this.OnAppLogin, this);
		SOS.AppService.RegisterHandler('applogout', this.OnAppLogout, this);
		SOS.AppService.RegisterHandler('panelactivated', this.OnActivatePanel, this);

		/** Try superfish here. */
		$('.sf-menu').superfish().find('li').not('.current')
			.hover(
			function(){
				$(this).find('>a').stop().animate({backgroundPosition:'0 0'})
			},
			function(){
				if($(this).hasClass('sfHover'))
				{
					return;
				}
				else
				{
					if($(this).parent().parent().is('li')){
						$(this).find('>a').stop().animate({backgroundPosition:'-141px 0'})
					}
					else {$(this).find('>a').stop().animate({backgroundPosition:'0 -94px'})}
				}
			}
		);

		/** Bind Events. */
		$("#hlSearchLeads").click(function (el)
		{
			SOS.AppService.DispatchEvent('panelactivated', SOS.Views.MainWindowViewport.Panels.SearchPanel);
		});
		$("#hlNewLeads").click(function (el)
		{
			SOS.Modals.LeadCreateUpdate.StartTheWizard();
		});
		$("#hlLeadsMain").click(function (el)
		{
			SOS.AppService.DispatchEvent("panelactivated", SOS.Views.MainWindowViewport.Panels.LeadsPanel);
		});


		//TODO:  Remove this once layout is done.
		//SOS.AppService.DispatchEvent('panelactivated', SOS.Views.MainWindowViewport.Panels.LeadsPanel);
	},
	OnAppLogin: function ()
	{
		this_navPanel.show();
	},
	OnAppLogout: function()
	{
		this._navPanel.hide();
		this.OnActivePanel('kinect');
	},
	OnActivatePanel: function _mw_OnActivePanel(panelName)
	{
		console.log('Activating panel: ' + panelName);

		switch(panelName)
		{
			case SOS.Views.MainWindowViewport.Panels.LeadsPanel:
				this._viewContainer.getLayout().setActiveItem('main-leadsPanel');
				//var oItem = $("#main-leadsPanel");
				//oItem.css('display', 'inline-block');
				this._leadsPanel.OnActivate();
				break;
			case SOS.Views.MainWindowViewport.Panels.SearchPanel:
				this._viewContainer.getLayout().setActiveItem('main-searchPanel');
				this._searchPanel.OnActivate();
				break;
		}
	},

	OnResize: function(cmp, w, h/*, opts*/)
	{
		this._width = w;
		this._height = h;

		console.log('MainWindow.OnResize(' + w + ', ' + h + ')');

		if (this._viewContainer) { this._viewContainer.setSize(w, h - 48); }
		if (this._leadsPanel)	 { this._leadsPanel.setSize	  (w, h - 48); }
	}
});