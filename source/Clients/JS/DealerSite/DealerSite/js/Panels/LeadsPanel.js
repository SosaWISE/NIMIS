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
		this._bus = new Ext.util.Observable();
		this._bus.addEvents(SOS.Panels.LeadsPanel.EventNames.CloseLead
			, SOS.Panels.LeadsPanel.EventNames.EditLead
			, SOS.Panels.LeadsPanel.EventNames.ConvertLead);

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
										data: {clickPlusMessage: 'Click plus sign (+) to add a new lead', leadId: 123456}
									},
									{
										id: 'leadsMainPane',
										name: 'leadsMainPane',
										xtype: 'panel',
										layout: 'card'  // This layout is used for tabbing...
									}
								]
						}
					]
			});

		this.callParent([config]);

		this.on('activate', this.OnActivate, this);
		this.on('deactivate', this.OnDeactivate, this);
	},

	RegisterHandler: function(eventName, handler, scope)
	{
		this._bus.on(eventName, handler, scope);
	},

	UnregisterHandler: function(eventName, handler, scope)
	{
		this._bus.un(eventName, handler, scope);
	},

	statics: {
		GetLeadsPanelItemData: function (oLeadDataFull) {
			/** Initialize. */
			var oResult;
			oResult = { data: {} };

			/** Build data. */
			// ** Leads personal information.
			oResult.data.Key = Ext.String.format("{0}-{1}", oLeadDataFull.LeadId, oLeadDataFull.CustomerMasterFileId);
			oResult.data.LeadId = oLeadDataFull.LeadID;
			oResult.data.CustomerMasterFileId = oLeadDataFull.CustomerMasterFileId;
			oResult.data.LeadDisposition = oLeadDataFull.LeadDisposition;
			oResult.data.LeadSource = oLeadDataFull.LeadSource;
			oResult.data.Salutation = oLeadDataFull.Salutation;
			oResult.data.FirstName = oLeadDataFull.FirstName;
			oResult.data.MiddleName = oLeadDataFull.MiddleName;
			oResult.data.LastName = oLeadDataFull.LastName;
			oResult.data.Suffix = oLeadDataFull.Suffix;
			oResult.data.Gender = oLeadDataFull.Gender;
			oResult.data.Email = oLeadDataFull.Email;
			oResult.data.DOB = SOS.Utils.Strings.formatDate(oLeadDataFull.DOB);
			//oResult.data.DOB = Ext.util.Format.dateRenderer('M/d/Y')(oLeadDataFull.DOB);
			if (oLeadDataFull.PhoneHome)
				oResult.data.PhoneHome = SOS.Utils.Strings.formatPhone(oLeadDataFull.PhoneHome);
			if (oLeadDataFull.PhoneWork)
				oResult.data.PhoneWork = SOS.Utils.Strings.formatPhone(oLeadDataFull.PhoneWork);
				//oResult.data.PhoneWork = oLeadDataFull.PhoneWork.replace(/(\d{3})(\d{3})(\d{4})/, "($1) $2-$3");
			if (oLeadDataFull.PhoneMobile)
				oResult.data.PhoneMobile = SOS.Utils.Strings.formatPhone(oLeadDataFull.PhoneMobile);
				//oResult.data.PhoneMobile = oLeadDataFull.PhoneMobile.replace(/(\d{3})(\d{3})(\d{4})/, "($1) $2-$3");
			oResult.data.CreatedOn = SOS.Utils.Strings.formatDateLong(oLeadDataFull.CreatedOn);

			oResult.data.LocalizationName = oLeadDataFull.Localization.LocalizationName;


			// ** Build Data for address.
			oResult.data.AddressId = oLeadDataFull.AddressId;
			oResult.data.StreetAddress = oLeadDataFull.Address.StreetAddress;
			oResult.data.StreetAddress2 = oLeadDataFull.Address.StreetAddress2;
			oResult.data.City = oLeadDataFull.Address.City;
			oResult.data.StateName = oLeadDataFull.Address.StateId;
			oResult.data.PostalCode = oLeadDataFull.Address.PostalCode;
			oResult.data.County = oLeadDataFull.Address.County;
			oResult.data.TimeZone = oLeadDataFull.Address.TimeZone.TimeZoneName;
			oResult.data.RepId = oLeadDataFull.SalesRep.GPEmployeeID;
			oResult.data.RepFullName = oLeadDataFull.SalesRep.FirstName + " "
				+ oLeadDataFull.SalesRep.LastName;
			oResult.data.RepPictureUrl = oLeadDataFull.SalesRep.UserId;
			oResult.data.RepEmail = oLeadDataFull.SalesRep.Email;
			if (oLeadDataFull.SalesRep.PhoneHome)
				oResult.data.RepPhoneHome = oLeadDataFull.SalesRep.PhoneHome.replace(/(\d{3})(\d{3})(\d{4})/, "($1) $2-$3");
			if (oLeadDataFull.SalesRep.PhoneCell)
				oResult.data.RepPhoneCell = oLeadDataFull.SalesRep.PhoneCell.replace(/(\d{3})(\d{3})(\d{4})/, "($1) $2-$3");
			if (oLeadDataFull.SalesRep.PhoneFax)
				oResult.data.RepPhoneFax = oLeadDataFull.SalesRep.PhoneFax.replace(/(\d{3})(\d{3})(\d{4})/, "($1) $2-$3");
			oResult.data.RepCompanyName = oLeadDataFull.SalesRep.CompanyName;
			oResult.data.RepRecruitedDate = Ext.util.Format.dateRenderer('M/d/Y')(oLeadDataFull.SalesRep.RecruitedDate);
			oResult.data.RepIsLocked = oLeadDataFull.SalesRep.IsLocked;
			oResult.data.RepIsActive = oLeadDataFull.SalesRep.IsActive;
			oResult.data.RepIsDeleted = oLeadDataFull.SalesRep.IsDeleted;

			/** Get Product List. */
			oResult.data.ProductsList = oLeadDataFull.ProductSkwIdList;

			/** Return result. */
			return oResult;
		},

		OpenLead: function (arg)
		{
			/** Decompose argument. */
			var aArg = SOS.Utils.Strings.getLeadIdObject(arg.Id);
			var lLeadId = aArg.LeadId;
			var lCustomerMasterFileId = aArg.CustomerMasterFileId;
			var sKey = lLeadId + "-" + lCustomerMasterFileId;
			/** Initialize. */
			var oLeadMainPaneCmp = Ext.getCmp("leadsMainPane");
			var nIndex = oLeadMainPaneCmp.items.indexOfKey(sKey);
			var oSrv = new SOS.ClientServices.CmsServices();
			var oData = {};
			var oDefIsSuccess;

			// Set up callbacks
			function onSuccess(oResponse)
			{
				/** Initialize. */
				oData = SOS.Panels.LeadsPanel.GetLeadsPanelItemData(oResponse.Value);
				oDefIsSuccess = true;
			}
			function onFailure(oMsgItem)
			{
				SOS.Framework.Popup.GeneralPopup(oMsgItem);
				oDefIsSuccess = false;
			}
			oSrv.on(SOS.ClientServices.CmsServices.EVNT_LEDFSRCH_SUCCESS, onSuccess);
			oSrv.on(SOS.ClientServices.CmsServices.EVNT_LEDFSRCH_FAILURE, onFailure);

			/** Execute call. */
			var oDef = oSrv.QlGetLeadFull(lLeadId, lCustomerMasterFileId, arg.NoteAccount, this);

			// ** Bind
			$.when(oDef).done(function ()
			{
				if (oDefIsSuccess)
				{
					/** Initialize. */
					var oDef2IsSuccess;
					/** Get Leads notes. */
					function onSuccessGetNotes(oResponse)
					{
						oData.data.Notes = oResponse.Value;
						oDef2IsSuccess = true;
					}
					function onFailureGetNotes(oMsgItem)
					{
						SOS.Framework.Popup.GeneralPopup(oMsgItem);
						oDef2IsSuccess = false;
					}
					oSrv.on(SOS.ClientServices.CmsServices.EVNT_GETNOTES_SUCCESS, onSuccessGetNotes);
					oSrv.on(SOS.ClientServices.CmsServices.EVNT_GETNOTES_FAILURE, onFailureGetNotes);
					/** Execute call. */
					var oDef2 = oSrv.McGetNotes(lLeadId, null, null, this);

					$.when(oDef2).done(function()
					{
						if (oDef2IsSuccess)
						{
							var oItem = new SOS.Panels.LeadsPanelItem(oData);
							oLeadMainPaneCmp.items.add(sKey, oItem);
							nIndex = oLeadMainPaneCmp.items.indexOfKey(sKey);

							/** Show tab tied to this lead. */
							SOS.Panels.LeadsTabs.AddTab(oLeadMainPaneCmp.items.getAt(nIndex));

							/** Show Pane for this lead. */
							oLeadMainPaneCmp.layout.setActiveItem(nIndex);
						}
					});
				}
			});
		},

		CloseLead: function (arg)
		{
			/** Decompose argument. */
			var aArg = SOS.Utils.Strings.getLeadIdObject(arg.id);
			var lLeadId = aArg.LeadId;
			var lCustomerMasterFileId = aArg.CustomerMasterFileId;
			var sKey = lLeadId + "-" + lCustomerMasterFileId;
			console.log(Ext.String.format("Close Lead: {0}", sKey));
			/** Initialize. */
			var oLeadMainPaneCmp = Ext.getCmp("leadsMainPane");
			var nIndex = oLeadMainPaneCmp.items.indexOfKey(sKey);

			/** Remove item from items list of the panel. */
			if (nIndex > -1)
			{
				var oItem = oLeadMainPaneCmp.items.getByKey(sKey);
				oLeadMainPaneCmp.items.removeAtKey(sKey);
				oItem.destroy();

				/** Remove the tab as well. */
				SOS.Panels.LeadsTabs.RemoveTab({ id: "ld-" + sKey + "-tab"});

				/** Show the correct panel after removing the one above. */
				if (oLeadMainPaneCmp.items.length > 0)
				{
					// ** Check to see that the current position is not empty.
					if (oLeadMainPaneCmp.items.length > nIndex)
					{
						oLeadMainPaneCmp.layout.setActiveItem(nIndex);
					}
					else
					{
						nIndex = nIndex == 0 ? 0 : nIndex - 1;
						oLeadMainPaneCmp.layout.setActiveItem(nIndex);
					}

					oItem = oLeadMainPaneCmp.items.getAt(nIndex);
					SOS.Panels.LeadsTabs.SelectTab(oItem);
				}
			}
			else
			{
				/** Display an error message. */
				SOS.Framework.Popup.GeneralPopup({
					Title: "Invalid key passed."
					, MessageBody: Ext.String.format("Sorry but the Lead ('{0}') you are trying to close is not in the items list."
						, sKey)
					, PopupType: "Error"
				});
			}
		},

		EventNames: {
			CloseLead: "CloseLead",
			EditLead: "EditLead",
			ConvertLead: "ConvertLead"
		}
//		NewLead: function () {
//			/** Initialize. */
//			console.log('ADDING A NEW LEAD...');
//			var oLeadsMainPaneCmp = Ext.getCmp("leadsMainPane");
//			var oItem = new SOS.Panels.LeadsPanelItem();
//			oLeadsMainPaneCmp.items.add("NewLead-Item", oItem);
//			var nIndex = oLeadsMainPaneCmp.items.indexOfKey("NewLead-Item");
//			oLeadsMainPaneCmp.layout.setActiveItem(nIndex);
//
//			/** Return item. */
//			return oItem;
//		}
	},

	/** Member functions. */
	OnAfterRender: function ()
	{
		/** Initialize. */
		var sysTabEl = Ext.getCmp("sysTabs");
		sysTabEl.OnAfterRender();
		console.log('sysTabEl: ', sysTabEl);

		/** Initialize. */
//		var statusColor = "#FFFF00";
//		var options = {
//				Cmp: "moni-status32334",
//			Context: "2d",
//			CenterX: 2,
//			CenterY: 2,
//			Radius: 4,
//			FillStyle: statusColor,
//			LineWidth: 1,
//			StrokeStyle: statusColor,
//			Width: 15,
//			Height: 15
//		};
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
