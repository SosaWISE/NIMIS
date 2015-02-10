/**
 * Created with JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 4/28/12
 * Time: 10:13 AM
 * To change this template use File | Settings | File Templates.
 */
Ext.ns('SOS.Panels');

Ext.define("SOS.Panels.LeadsPanelItem",
{
	extend: 'Ext.container.Container',
	alias: 'widget.leadsPanelItem',

	constructor: function(options)
	{
		options = options || {};
		var szTpl = SOS.UI.Services.GetTemplate('leads-panel-view.html');

		this._panelItemKey = Ext.String.format("ld-{0}-{1}"
			, options.data.LeadId, options.data.CustomerMasterFileId);
		this._disposition = options.data.LeadDisposition;
		this._dispositionDate = options.data.LeadDispositionDateChange === undefined
			? options.data.CreatedOn
			: options.data.LeadDispositionDateChange;

		//this._source = options.data.LeadSource;
		this._data = options.data;

		var config = Ext.merge(options,
			{
				tpl: szTpl,
				data: options.data,
				listeners:
				{
					scope: this,
					afterrender: this.OnAfterRender
				}
			});

		/** Init base. */
		this.callParent([config]);

	},

	/** Member functions. */
	OnAfterRender: function ()
	{
		/** get a handle to the buttons. */
		this._btnConvertToCustomerEl = $(this.el.dom).find(".convert-customer");
		this._btnEditLeadEl = $(this.el.dom).find(".edit-lead");
		this._btnCloseBtnEl = $(this.el.dom).find(".close-lead");
		this._btnDispLBtnEl = $(this.el.dom).find(".disp-lead");
		this._btnScheABtnEl = $(this.el.dom).find(".schd-appt");

		/** Bind events. */
		this._btnConvertToCustomerEl.bind('click', this._panelItemKey, this.ConvertToCustomer);
		this._btnEditLeadEl.bind('click', this._panelItemKey, this.EditLeadWizard);
		this._btnCloseBtnEl.bind('click', this._panelItemKey, this.CloseLeadItem);
		this._btnDispLBtnEl.bind('click', { id: this._panelItemKey
			, disp: this._disposition
			, dispDate: this._dispositionDate }, this.DispLeadItem);
		this._btnScheABtnEl.bind('click', {id: this._panelItemKey }, this.ScheduleAppointment);

//		this.show();
	},

	ScheduleAppointment: function (arg)
	{
		SOS.Modals.MyCalendar.StartModal(arg);
	},

	DispLeadItem: function (arg)
	{
		/** Initialize. */
		SOS.Framework.Popup.Confirm({
			Title: "Disposition the Lead Record?"
			, MessageBody: "This window will allow you to disposition the lead."
			, PopupType: "Question"
			, OnConfirm: function () {
				SOS.Modals.LeadDisposition.StartTheWizard(arg.data.id, arg.data.disp, arg.data.dispDate);
			}
			, OnCancel: function () {}
		});
	},

	CloseLeadItem: function (arg)
	{
		/** Initialize. */
		SOS.Framework.Popup.Confirm({
			Title: "Closing the Lead Record?"
			, MessageBody: "Are you sure you want to change the disposition of the Lead.?"
			, PopupType: "Question"
			, OnConfirm: function () {
				SOS.Panels.LeadsPanel.CloseLead({id: arg.data})
			}
			, OnCancel: function () {}
		});
	},

	ConvertToCustomer: function (arg)
	{
//		SOS.Framework.Popup.GeneralPopup({
//			Title: "Create Lead into Customer"
//			, MessageBody: "This is a lead that will be converted into a customer.  What up with that?"
//			, PopupType: "Warning"
//		})

		/** Initialize. */
		SOS.Modals.LeadConvertToCustomer.StartTheWizard(arg.data);
	},

	EditLeadWizard: function (arg)
	{
		console.log(arg, arg.data);
		SOS.Modals.LeadCreateUpdate.StartEditWizard(arg.data);
//		SOS.Framework.Popup.GeneralPopup({
//			Title: "Edit Lead Information"
//			, MessageBody: "This is a lead that will be converted into a customer.  What up with that?"
//			, PopupType: "Warning"
//		});
	}
});