/**
 * Created with JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 5/15/12
 * Time: 11:51 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.ns("SOS.Modals");

Ext.define("SOS.Modals.LeadConvertToCustomer", {

	extend: "Ext.window.Window",
	alias: "widget.leadConvertToCustomer",

	constructor: function (options)
	{
		/** Initialize.  */
		var me = this;
		options = options || {};
		var tplSelectProd = SOS.UI.Services.GetTemplate('LeadConvToCust-SelectProd.html');
		var tplBillingInfo = SOS.UI.Services.GetTemplate('LeadConvToCust-BillingInfo.html');
		var tplBillingAddress = SOS.UI.Services.GetTemplate('LeadConvToCust-BillingAddress.html');
		var tplPaymentInfo = SOS.UI.Services.GetTemplate('LeadConvToCust-PaymentInfo.html');
		var sCompoundLeadId = Ext.String.format("{0}-{1}", options.CompoundLeadId.LeadId
					, options.CompoundLeadId.CustomerMasterFileId);
		me.CompoundLeadId = options.CompoundLeadId;
		me.FieldsToHide = ["frmPaySSN","frmPaySSNDiv"];

		/** Build Configuration options for the window. */
		var config = Ext.merge(options,
			{
				id: "leadConvertToCustomerFrm",
				title: '<img src="/img/DragTab-Yellow.png" alt="Click here to move the window" />',
				titleCollapse: true,
				closable: true,
				draggable: true,
				width: "650",
//				height: "auto",
//				maxWidth: 700,
				maxHeight: 750,
				modal: true,
				layout: "card",
				items: [
					{
						id: "leadCustConvForm1",
						xtype: "container",
						renderTpl: tplSelectProd,
						renderData: {
							ActionToPerform: "Select a Product for New Customer",
							Title: "Choose an Available Product",
							Username: "DealerWISE",
							CompoundLeadId: sCompoundLeadId,
							LeadId: options.CompoundLeadId.LeadId,
							CustomerMasterFileId: options.CompoundLeadId.CustomerMasterFileId,
							SalesRepInfo: Ext.String.format("{0} ({1})"
								, SOS.AppService.UserInfo.Fullname, SOS.AppService.UserInfo.Username)
								, PurchaseDate: SOS.Utils.Strings.formatDateLong(new Date())
						},
						renderSelectors:
						{
							frmPayImgNext1El : "#frmPayImgNext1"
							, frmPayBtnNext1El: "#frmPayBtnNext1"
							, frmPayProductsEl: "#frmPayProducts"
						}
					},
					{
						id: "leadCustConvForm2",
						xtype: "container",
						renderTpl: tplBillingInfo,
						renderData: {
							Title: "Enter Credit Card Billing Name",
							CompoundLeadId: sCompoundLeadId
						},
						renderSelectors:
						{
							frmPayBtnPrev2El: "#frmPayBtnPrev2"
							, frmPayImgPrev2El: "#frmPayImgPrev2"
							, frmPayImgNext2El: "#frmPayImgNext2"
							, frmPayBtnNext2El: "#frmPayBtnNext2"
							, frmPayInfoSameAsCustEl: "#frmPayInfoSameAsCust"
							, frmPaySalutationEl: "#frmPaySalutation"
							, frmPayFirstNameEl: "#frmPayFirstName"
							, frmPayMiddleNameEl: "#frmPayMiddleName"
							, frmPayLastNameEl: "#frmPayLastName"
							, frmPaySuffixEl: "#frmPaySuffix"
							, frmPayDOBEl: "#frmPayDOB"
							, frmPaySSNEl: "#frmPaySSN"
							, frmPayGenderEl: "#frmPayGender"
							, frmPayEmailEl: "#frmPayEmail"
							, frmPayLanguageEl: "#frmPayLanguage"
							, frmPayPhoneHomeEl: "#frmPayPhoneHome"
							, frmPayPhoneMobileEl: "#frmPayPhoneMobile"
							, frmPayPhoneWorkEl: "#frmPayPhoneWork"
						}
					},
					{
						id: "leadCustConvForm3",
						xtype: "container",
						renderTpl: tplBillingAddress,
						renderData: {
							Title: "Enter Credit Card Billing Address",
							CompoundLeadId: sCompoundLeadId
						},
						renderSelectors:
						{
							frmPayBtnPrev3El: "#frmPayBtnPrev3"
							, frmPayImgPrev3El: "#frmPayImgPrev3"
							, frmPayImgNext3El: "#frmPayImgNext3"
							, frmPayBtnNext3El: "#frmPayBtnNext3"
							, frmPayStreetEl: "#frmPayStreet"
							, frmPayStreet2El: "#frmPayStreet2"
							, frmPayCityEl: "#frmPayCity"
							, frmPayStateEl: "#frmPayState"
							, frmPayPostalCodeEl: "#frmPayPostalCode"
							, frmPayCountryEl: "#frmPayCountry"
							, frmPayTimeZoneEl: "#frmPayTimeZone"
							, frmPayAddressSameAsCustEl: "#frmPayAddressSameAsCust"
						}
					},
					{
						id: "leadCustConvForm4",
						xtype: "container",
						renderTpl: tplPaymentInfo,
						renderData: {
							Title: "Enter Credit Card Information",
							CompoundLeadId: sCompoundLeadId
						},
						renderSelectors:
						{
							frmPayNameOnCardEl: "#frmPayNameOnCard"
							, frmPayCardNumberEl: "#frmPayCardNumber"
							, frmPayExpMonthEl: "#frmPayExpMonth"
							, frmPayExpYearEl: "#frmPayExpYear"
							, frmPayCcvEl: "#frmPayCcv"
							, frmPayMethodOfPayCreditCardEl: "#frmPayMethodOfPayCreditCard"
							, frmPayMethodOfPayCheckEl: "#frmPayMethodOfPayCheck"
							, frmPayPoNumberEl: "#frmPayPoNumber"
							, frmPayRoutingNumberEl: "#frmPayRoutingNumber"
							, frmPayAccountNumberEl: "#frmPayAccountNumber"
							, frmPayCheckNumberEl: "#frmPayCheckNumber"
							, frmPayPoNumber2El: "#frmPayPoNumber2"
							, frmPayBtnPrev4El: "#frmPayBtnPrev4"
							, frmPayImgPrev4El: "#frmPayImgPrev4"
							, frmPayBtnSubmitEl: "#frmPayBtnSubmit"
							, frmPayImgSubmitEl: "#frmPayImgSubmit"
	}
					}
				],
				activeItem: 0, // This is the first item in the items list.
				listeners:
				{
					scope: this,
					afterrender: this.OnAfterRender
				}
			});

		/** Call Parent object. */
		this.callParent([config]);
	},

	statics: {
		StartTheWizard: function (sLeadCompoundId)
		{
			/** Initialize. */
			var oLeadId = SOS.Utils.Strings.getLeadIdObject(sLeadCompoundId);
			var options = { CompoundLeadId: oLeadId };
			var oStart = new SOS.Modals.LeadConvertToCustomer(options);

			/** Open wizard. */
			oStart.show();
		}
	},
	HideItems: function ()
	{
		$.each(this.FieldsToHide, function(index, value)
			{
				console.log(index, value);
				$("#" + value).hide();
			}
		);
	},

	IsElHidden: function (elId)
	{
		return $.inArray(elId, me.FieldsToHide) > -1;
	},

	OnAfterRender: function ()
	{
		/** Initialize. */
		var me = this;
		me._viewContainer = Ext.getCmp("leadConvertToCustomerFrm");
		var oFrm01 = Ext.getCmp("leadCustConvForm1");
		var oFrm02 = Ext.getCmp("leadCustConvForm2");
		var oFrm03 = Ext.getCmp("leadCustConvForm3");
		var oFrm04 = Ext.getCmp("leadCustConvForm4");

		/** Add Masks to fields. */
		$(oFrm02.frmPayPhoneHomeEl.dom).mask("(999) 999-9999", { placeholder: " " });
		$(oFrm02.frmPayPhoneMobileEl.dom).mask("(999) 999-9999", { placeholder: " " });
		$(oFrm02.frmPayPhoneWorkEl.dom).mask("(999) 999-9999", { placeholder: " " });
		$(oFrm02.frmPayDOBEl.dom).mask("99/99/9999", { placeholder: " " });
		$(oFrm02.frmPaySSNEl.dom).mask("999-99-9999", { placeholder: " " });
		$(oFrm03.frmPayPostalCodeEl.dom).mask("99999?-9999", { placeholder: " "});
		$(oFrm04.frmPayRoutingNumberEl.dom).mask("999999999", { placeholder: " " });

		/** Bind close objects. */
		$("#leadConv-Product-dialog .btn-close-x").bind("click", me.CloseWiz);
		$("#leadConv-BillInfo-dialog .btn-close-x").bind("click", me.CloseWiz);
		$("#leadConv-BillAddress-dialog .btn-close-x").bind("click", me.CloseWiz);
		$("#leadConv-PaymentInfo-dialog .btn-close-x").bind("click", me.CloseWiz);

		/** Bind navigation buttons. */
		$(oFrm01.frmPayBtnNext1El.dom).unbind().bind("click", me.MoveNext);
		$(oFrm01.frmPayImgNext1El.dom).unbind().bind("click", me.MoveNext);
		$(oFrm02.frmPayBtnNext2El.dom).unbind().bind("click", me.MoveNext);
		$(oFrm02.frmPayImgNext2El.dom).unbind().bind("click", me.MoveNext);
		$(oFrm02.frmPayBtnPrev2El.dom).unbind().bind("click", me.MoveNext);
		$(oFrm02.frmPayImgPrev2El.dom).unbind().bind("click", me.MoveNext);
		$(oFrm02.frmPayInfoSameAsCustEl.dom).unbind().bind("click", me.ToggleFormPayInfo);
		$(oFrm03.frmPayBtnNext3El.dom).unbind().bind("click", me.MoveNext);
		$(oFrm03.frmPayImgNext3El.dom).unbind().bind("click", me.MoveNext);
		$(oFrm03.frmPayBtnPrev3El.dom).unbind().bind("click", me.MoveNext);
		$(oFrm03.frmPayImgPrev3El.dom).unbind().bind("click", me.MoveNext);
		$(oFrm03.frmPayAddressSameAsCustEl.dom).unbind().bind("click", me.ToggleFormPayInfo);
		$(oFrm04.frmPayBtnPrev4El.dom).unbind().bind("click", me.MoveNext);
		$(oFrm04.frmPayImgPrev4El.dom).unbind().bind("click", me.MoveNext);
		$(oFrm04.frmPayMethodOfPayCreditCardEl.dom).unbind().bind("click", me.ToggleFormPayMethod);
		$(oFrm04.frmPayMethodOfPayCheckEl.dom).unbind().bind("click", me.ToggleFormPayMethod);
		$(oFrm04.frmPayBtnSubmitEl.dom).unbind().bind("click", me.SubmitForm);
		$(oFrm04.frmPayImgSubmitEl.dom).unbind().bind("click", me.SubmitForm);

		/** Hide fields that need to be hidden. */
		this.HideItems();

	},

	SubmitForm: function(event)
	{
		console.log("Submit Button was pressed:", event);
		/** Initialize. */
		var me = Ext.getCmp("leadConvertToCustomerFrm");
		me._viewContainer = Ext.getCmp("leadConvertToCustomerFrm");

		/** Check to see if the form passes validation. */
		if (!me._viewContainer.ValidateForm4()) { return; }

		/** Execute submission. */
		function onSuccess(response){
			SOS.UI.Services.Hide();
			SOS.Framework.Popup.GeneralPopup({
				Title: "Successfully Processed Payment"
				, MessageBody: Ext.String.format("Successfully process your payment.<br />To view Sales Receipt <a target='_blank' href='{0}{1}'>click here</a>."
					, SOSConfig.API_BASE, response.Value.InvoiceFilePath)
				, PopupType: "Success"
			});
			me.close();
			me.destroy();
			console.log("Success Response back:", response);
		}
		function onFailure(oMessageItem){
			SOS.UI.Services.Hide();
			SOS.Framework.Popup.GeneralPopup(oMessageItem);
		}
		var oSrv = new SOS.ClientServices.CmsServices();

		// ** Bind events.
		SOS.UI.Services.ShowBusy("Creating New Customer<br /><span>Processing Payment</span>");
		oSrv.on(SOS.ClientServices.CmsServices.EVNT_CUSTNEWI_SUCCESS, onSuccess);
		oSrv.on(SOS.ClientServices.CmsServices.EVNT_CUSTNEWI_FAILURE, onFailure);
		oSrv.AeCreateNewCustomer(me._viewContainer.GetJsonData(), me);
	},

	/**
	 * @description This gathers all the data and creates the object that will be deserialized
	 * to a strongly typed class in the Rest WS.
	 * @return {Object} This is of the PaymentInformation format.
	 */
	GetJsonData: function ()
	{
		/** Initialize. */
		var me = this;
		var oFrm01 = me._viewContainer.items.getByKey("leadCustConvForm1");
		var oFrm02 = me._viewContainer.items.getByKey("leadCustConvForm2");
		var oFrm03 = me._viewContainer.items.getByKey("leadCustConvForm3");
		var oFrm04 = me._viewContainer.items.getByKey("leadCustConvForm4");
		var oResult = {
			LeadId: me.CompoundLeadId.LeadId
			, CustomerMasterFileId: me.CompoundLeadId.CustomerMasterFileId
			, ProductSkws: oFrm01.frmPayProductsEl.dom.value
			, ContractTemplateID: 1 // TODO: This is the default template.
			, BillingInfo: {
				SameAsCustomer: $(oFrm02.frmPayInfoSameAsCustEl.dom).is(':checked')
				, Salutation: oFrm02.frmPaySalutationEl.dom.value
				, FirstName: oFrm02.frmPayFirstNameEl.dom.value
				, MiddleName: oFrm02.frmPayMiddleNameEl.dom.value
				, LastName: oFrm02.frmPayLastNameEl.dom.value
				, Suffix: oFrm02.frmPaySuffixEl.dom.value
				, DOB: oFrm02.frmPayDOBEl.dom.value !== ""
					? oFrm02.frmPayDOBEl.dom.value
					: undefined
				, SSN: oFrm02.frmPaySSNEl.dom.value !== ""
					? oFrm02.frmPaySSNEl.dom.value
					: undefined
				, Gender: oFrm02.frmPayGenderEl.dom.value
				, Email: oFrm02.frmPayEmailEl.dom.value
				, Language: oFrm02.frmPayLanguageEl.dom.value
				, PhoneHome: oFrm02.frmPayPhoneHomeEl.dom.value
				, PhoneMobile: oFrm02.frmPayPhoneMobileEl.dom.value
				, PhoneWork: oFrm02.frmPayPhoneWorkEl.dom.value
			}
			, BillingAddress: {
				SameAsCustomer: $(oFrm03.frmPayAddressSameAsCustEl.dom).is(':checked')
				, Street: oFrm03.frmPayStreetEl.dom.value
				, Street2: oFrm03.frmPayStreet2El.dom.value
				, City: oFrm03.frmPayCityEl.dom.value
				, StateId: oFrm03.frmPayStateEl.dom.value
				, PostalCode: oFrm03.frmPayPostalCodeEl.dom.value
				, CountryId: oFrm03.frmPayCountryEl.dom.value
				, TimeZoneId: oFrm03.frmPayTimeZoneEl.dom.value
			}
			, PaymentInformation: {}
		};

		/** Get Payment information. */
		var isCreditCardMethod = $(oFrm04.frmPayMethodOfPayCreditCardEl.dom).attr("checked") !== undefined;
		oResult.PaymentInformation = {
			PaymentMethod: isCreditCardMethod
					? oFrm04.frmPayMethodOfPayCreditCardEl.dom.value
					: oFrm04.frmPayMethodOfPayCheckEl.dom.value
			, PONumber: isCreditCardMethod
				? oFrm04.frmPayPoNumberEl.dom.value
				: oFrm04.frmPayPoNumber2El.dom.value
			// Credit Card
			, NameOnCard: oFrm04.frmPayNameOnCardEl.dom.value
			, CCNumber: oFrm04.frmPayCardNumberEl.dom.value
			, CCV: oFrm04.frmPayCcvEl.dom.value
			, ExpMonth: oFrm04.frmPayExpMonthEl.dom.value
			, ExpYear: oFrm04.frmPayExpYearEl.dom.value
			// Check
			, RoutingNumber: oFrm04.frmPayRoutingNumberEl.dom.value !== ""
				? oFrm04.frmPayRoutingNumberEl.dom.value
				: undefined
			, AccountNumber: oFrm04.frmPayAccountNumberEl.dom.value !== ""
				? oFrm04.frmPayAccountNumberEl.dom.value
				: undefined
			, CheckNumber: oFrm04.frmPayCheckNumberEl.dom.value !== ""
				? oFrm04.frmPayCheckNumberEl.dom.value
				: undefined
		};

		/** Return result. */
		return oResult;
	},

	ToggleFormPayMethod: function (event)
	{
		console.log("Item clicked:", event.currentTarget.id);
		/** Initialize. */
		var me = this;
		me._viewContainer = Ext.getCmp("leadConvertToCustomerFrm");
		var oFrm04 = Ext.getCmp("leadCustConvForm4");
		var oSelectorShow, oSelectorHide, oSelectorFocus;

		/** Pick show and hide selectors. */
		switch(this.id)
		{
			case "frmPayMethodOfPayCreditCard":
				oSelectorShow = $("#frmPayCreditCardFields");
				oSelectorHide = $("#frmPayCheckFields");
				oSelectorFocus = oFrm04.frmPayNameOnCardEl.dom;
				break;
			case "frmPayMethodOfPayCheck":
				oSelectorShow = $("#frmPayCheckFields");
				oSelectorHide = $("#frmPayCreditCardFields");
				oSelectorFocus = oFrm04.frmPayRoutingNumberEl.dom;
				break;
		}

		/** Toggle. */
		oSelectorShow.show();
		oSelectorHide.hide();
		oSelectorFocus.focus();
	},

	MoveNext: function (event)
	{
		/** Initialize. */
		var me = this;
		me._viewContainer = Ext.getCmp("leadConvertToCustomerFrm");
		var oFrm01 = Ext.getCmp("leadCustConvForm1");
		var oFrm02 = Ext.getCmp("leadCustConvForm2");
		var oFrm03 = Ext.getCmp("leadCustConvForm3");
		var oFrm04 = Ext.getCmp("leadCustConvForm4");

		/** Check to see which next button was clicked. */
		switch(event.currentTarget.id)
		{
			case "frmPayBtnNext1":
			case "frmPayImgNext1":
			case "frmPayBtnPrev3":
			case "frmPayImgPrev3":
				if (me._viewContainer.ValidateForm1())
				{
					me._viewContainer.getLayout().setActiveItem("leadCustConvForm2");
					oFrm02.frmPayInfoSameAsCustEl.dom.focus();
				}
				break;
			case "frmPayBtnPrev2":
			case "frmPayImgPrev2":
				me._viewContainer.getLayout().setActiveItem("leadCustConvForm1");
				oFrm01.frmPayProductsEl.dom.focus();
				break;
			case "frmPayBtnNext2":
			case "frmPayImgNext2":
			case "frmPayBtnPrev4":
			case "frmPayImgPrev4":
				if (me._viewContainer.ValidateForm2())
				{
					me._viewContainer.getLayout().setActiveItem("leadCustConvForm3");
					oFrm03.frmPayAddressSameAsCustEl.dom.focus();
				}
				break;
			case "frmPayBtnNext3":
			case "frmPayImgNext3":
				if (me._viewContainer.ValidateForm3())
				{
					me._viewContainer.getLayout().setActiveItem("leadCustConvForm4");
					oFrm04.frmPayNameOnCardEl.dom.focus();
				}
				break;
		}
	},

	/**
	 *
	 * @return bool If the form validated or not.
	 */
	ValidateForm1: function ()
	{
		/** Initialize. */
		var bResult = true;  // Set default value.
		var frm01 = Ext.getCmp("leadCustConvForm1");
		var aMessages = [];

		// ** Products
		if (frm01.frmPayProductsEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Product Offerings"
				, Message: "Please select the product that was offered to the lead."
			});
			bResult = false;
		}

		/** Show the validation screen. */
		if (!bResult)
		{
			var szListOfErrors = "";
			Ext.Array.each(aMessages, function (oItem/*, index*/){
				szListOfErrors += Ext.String.format("<li><strong>{0}</strong>: {1}</li>"
					, oItem.FieldName, oItem.Message);
			});
			SOS.Framework.Popup.PopupFormValidation(
				{
					Title: "Invalid Product Selection",
					MessageBody: "The following errors were generated:",
					ValidationList: szListOfErrors
				}
			);
		}

		/** Return result.*/
		return bResult;
	},

	ValidateForm2: function ()
	{
		/** Initialize. */
		var bResult = true;
		var frm02 = Ext.getCmp("leadCustConvForm2");
		var aMessages = [];

		// ** Check that the same as customer check box is not checked.
		if ($(frm02.frmPayInfoSameAsCustEl.dom).is(':checked')) { return true; }

		// ** Billing information.
		if (frm02.frmPayFirstNameEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "First Name"
				, Message : "Please enter a first name for the person paying the bill."
			});
			bResult = false;
		}
		if (frm02.frmPayLastNameEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Last Name"
				, Message: "Please enter the last name for the person paying the bill."
			});
			bResult = false;
		}
//		// ** Business Rule 1
//		if (frm02.frmPayDOBEl.dom.value === ""
//			&& frm02.frmPaySSNEl.dom.value ==="")
//		{
//			aMessages.push({
//				FieldName: "DOB and SSN", Message: "There has to be a value in one of these two fields."
//			});
//			bResult = false;
//		}
		// ** Business Rule 2
		if (frm02.frmPayPhoneHomeEl.dom.value === ""
			&& frm02.frmPayPhoneMobileEl.dom.value === ""
			&& frm02.frmPayPhoneWorkEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Phones", Message: "There must be at least one phone number entered."
			});
			bResult = false;
		}
		// ** Business Rule 3
		if (frm02.frmPayEmailEl.dom.value === "")
		{
			aMessages.push({FieldName: "Email"
				, Message: "This is not a required field.  If you would like to receive event notifications please enter an email address."})
		}
		if (frm02.frmPayLanguageEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Language"
				, Message: "Please be sure to select a language.  If your language is not available please select English."
			});
			bResult = false;
		}

		if (frm02.frmPayGenderEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Gender"
				, Message: "Please select the gender of the person paying the bill."
			});
			bResult = false;
		}

		/** Show the validation screen. */
		if (!bResult)
		{
			var szListOfErrors = "";
			Ext.Array.each(aMessages, function (oItem/*, index*/){
				szListOfErrors += Ext.String.format("<li><strong>{0}</strong>: {1}</li>"
					, oItem.FieldName, oItem.Message);
			});
			SOS.Framework.Popup.PopupFormValidation(
				{
					Title: "Invalid Billing Information",
					MessageBody: "The following errors were generated:",
					ValidationList: szListOfErrors
				}
			);
		}

		/** Return result.*/
		return bResult;
	},

	ValidateForm3: function ()
	{
		/** Initialize. */
		var bResult = true;
		var frm03 = Ext.getCmp("leadCustConvForm3");
		var aMessages = [];

		// ** Check that the same as customer check box is not checked.
		if ($(frm03.frmPayAddressSameAsCustEl.dom).is(':checked')) { return true; }

		// ** Billing information.
		if (frm03.frmPayStreetEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Address"
				, Message : "Please enter the street address for the person paying the bill."
			});
			bResult = false;
		}
		if (frm03.frmPayCityEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "City"
				, Message : "Please enter the city name for the person paying the bill."
			});
			bResult = false;
		}
		if (frm03.frmPayStateEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "State"
				, Message : "Please select the State for the person paying the bill."
			});
			bResult = false;
		}
		if (frm03.frmPayPostalCodeEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Postal Code"
				, Message : "Please enter the Postal Code for the person paying the bill."
			});
			bResult = false;
		}
		if (frm03.frmPayCountryEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Country"
				, Message : "Please select the Country for the person paying the bill."
			});
			bResult = false;
		}
		if (frm03.frmPayTimeZoneEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Time Zone"
				, Message : "Please select the Time Zone for the person paying the bill."
			});
			bResult = false;
		}

		/** Show the validation screen. */
		if (!bResult)
		{
			var szListOfErrors = "";
			Ext.Array.each(aMessages, function (oItem/*, index*/){
				szListOfErrors += Ext.String.format("<li><strong>{0}</strong>: {1}</li>"
					, oItem.FieldName, oItem.Message);
			});
			SOS.Framework.Popup.PopupFormValidation(
				{
					Title: "Invalid Billing Address",
					MessageBody: "The following errors were generated:",
					ValidationList: szListOfErrors
				}
			);
		}

		/** Return result.*/
		return bResult;
	},

	ValidateForm4: function ()
	{
		/** Initialize. */
		var bResult = true;
		var oFrm04 = Ext.getCmp("leadCustConvForm4");
		var aMessages = [];

		/** Check the payment method. */
		if ($(oFrm04.frmPayMethodOfPayCreditCardEl.dom).attr("checked"))
		{
			// ** CC Payment information.
			if (oFrm04.frmPayNameOnCardEl.dom.value === "")
			{
				aMessages.push({
					FieldName: "Name on Card"
					, Message : "Please enter the name as it appears on the Credit Card."
				});
				bResult = false;
			}
			if (oFrm04.frmPayCardNumberEl.dom.value === "")
			{
				aMessages.push({
					FieldName: "Credit Card Number"
					, Message: "You must enter a valid Credit Card Number."
				});
				bResult = false;
			}
			if (oFrm04.frmPayExpMonthEl.dom.value === "")
			{
				aMessages.push({
					FieldName: "Expiration Month"
					, Message: "Please select the expiration month."
				});
				bResult = false;
			}
			if (oFrm04.frmPayExpYearEl.dom.value === "")
			{
				aMessages.push({
					FieldName: "Expiration Year"
					, Message: "Please select the expiration year."
				});
				bResult = false;
			}
			if (oFrm04.frmPayCcvEl.dom.value === "")
			{
				aMessages.push({
					FieldName: "Credit Card Verification ID"
					, Message: "Please enter the CVV of the card."
				});
				bResult = false;
			}
		}
		else
		{
			if (oFrm04.frmPayRoutingNumberEl.dom.value === "")
			{
				aMessages.push({
					FieldName: "Routing Number"
					, Message: "Please enter the bank routing number found on your check."
				});
				bResult = false;
			}
			if (oFrm04.frmPayAccountNumberEl.dom.value === "")
			{
				aMessages.push({
					FieldName: "Bank Account Number"
					, Message: "Please enter the account number found on your check."
				});
				bResult = false;
			}
			if (oFrm04.frmPayCheckNumberEl.dom.value === "")
			{
				aMessages.push({
					FieldName: "Check Number"
					, Message: "Please enter the check number found on your check."
				});
				bResult = false;
			}
		}

		/** Show the validation screen. */
		if (!bResult)
		{
			var szListOfErrors = "";
			Ext.Array.each(aMessages, function (oItem/*, index*/){
				szListOfErrors += Ext.String.format("<li><strong>{0}</strong>: {1}</li>"
					, oItem.FieldName, oItem.Message);
			});
			SOS.Framework.Popup.PopupFormValidation(
				{
					Title: "Invalid Payment Information",
					MessageBody: "The following errors were generated:",
					ValidationList: szListOfErrors
				}
			);
		}

		/** Return result.*/
		return bResult;
	},

	ToggleFormPayInfo: function (event)
	{
		/** Initialize. */
		var oSelector;

		switch(this.id)
		{
			case "frmPayInfoSameAsCust":
				oSelector = $("#leadConv-BillInfo-dialog :input");
				break;
			case "frmPayAddressSameAsCust":
				oSelector = $("#leadConv-BillAddress-dialog :input");
				break;
		}

		// ** Toggle if selector is found.
		if (oSelector)
		{
			if ($(this).is(':checked'))
			{
				oSelector.attr('disabled','disabled');
				oSelector.addClass("nmlFieldDisabled");
			} else {
				oSelector.removeAttr('disabled');
				oSelector.removeClass("nmlFieldDisabled");
			}
		}

		// ** Always keep the checkbox enabled.
		$(event.currentTarget).removeAttr('disabled');
	},
	/**
	 * This function will close the wizard.
	 * @param event object that was fired.
	 */
	CloseWiz: function (event)
	{
		console.log("CloseWize:", event);
		/** Initialize. */
		var me = this;
		me._viewContainer = Ext.getCmp("leadConvertToCustomerFrm");
		SOS.Framework.Popup.Confirm({
			Title: "Not Done."
			, MessageBody: "Are you sure you want to exit the form?"
			, OnConfirm: function ()
			{
				me._viewContainer.close();
				me._viewContainer.destroy();
			}
		});
	}
});