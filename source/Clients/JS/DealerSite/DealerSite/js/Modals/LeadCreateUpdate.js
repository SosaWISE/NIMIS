/**
 * Created with JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 5/5/12
 * Time: 7:38 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.ns("SOS.Modals");

Ext.define("SOS.Modals.LeadCreateUpdate",
{
	extend: "Ext.window.Window",
	alias: "widget.leadCreateUpdate",
	CompoundLeadId: null,

	constructor: function (options)
	{
		/** Initialize.  */
		var me = this;
		options = options || {};
		var tplLeadSourceDisposition = SOS.UI.Services.GetTemplate('LeadCreateUpdateSourceDisposition.html');
		var tplLeadInfo = SOS.UI.Services.GetTemplate('LeadCreateUpdatePersonalInfo.html');
		var tplLeadHomeAddress = SOS.UI.Services.GetTemplate("LeadCreateUpdateHomeAddress.html");
		var tplLeadProducts = SOS.UI.Services.GetTemplate("LeadCreateUpdateProduct.html");
		me.FieldsToHide = ["frmLeadSSN","frmLeadSSNDiv"];

		if (options.CompoundLeadId)
		{
			this.CompoundLeadId = options.CompoundLeadId;
		}

		/** Build Configuration options for the window. */
		var config = Ext.merge(options,
			{
				id: "leadCreateUpdateWizFrm",
				title: '<img src="/img/DragTab-Yellow.png" alt="Click here to move the window" />',
				closable: true,
				width: "650",
//				height: "auto",
//				maxWidth: 700,
				maxHeight: 750,
				modal: true,
				layout: "card",
				items: [
					{
						id: "leadCUForm0",
						xtype: "container",
						renderTpl: tplLeadSourceDisposition,
						renderData: {
							Title: "Source of the Lead"
						},
						renderSelectors: {
							frmLeadSourceEl: "#frmLeadSource",
							frmLeadDispositionEl: "#frmLeadDisposition",
							frmLeadSourceDateGenEl: "#frmLeadSourceDateGen"
						}
					},
					{
						id: "leadCUForm1",
						xtype: "container",
						renderTpl: tplLeadInfo,
						renderData: {
							Title: "Enter Personal Information",
							Username: "DealerWISE",
							LeadId: "[New Lead]",
							Salutation: "",
							FirstName: "",
							MiddleName: "",
							LastName: "",
							Suffix: "",
							DOB: "",
							SSN: "",
							Gender: "",
							Email: "",
							Language: "",
							PhoneHome: "",
							PhoneMobile: "",
							PhoneWork: ""
						},
						renderSelectors:
						{
							frmLeadSalutationEl : "#frmLeadSalutation",
							frmLeadFirstNameEl : "#frmLeadFirstName",
							frmLeadMiddleNameEl: "#frmLeadMiddleName",
							frmLeadLastNameEl: "#frmLeadLastName",
							frmLeadSuffixEl: "#frmLeadSuffix",
							frmLeadDOBEl: "#frmLeadDOB",
							frmLeadSSNEl: "#frmLeadSSN",
							frmLeadGenderEl: "#frmLeadGender",
							frmLeadEmailEl: "#frmLeadEmail",
							frmLeadLanguageEl: "#frmLeadLanguage",
							frmLeadPhoneHomeEl: "#frmLeadPhoneHome",
							frmLeadPhoneMobileEl: "#frmLeadPhoneMobile",
							frmLeadPhoneWorkEl: "#frmLeadPhoneWork"
						}
					},
					{
						id: "leadCUForm2",
						xtype: "container",
						renderTpl: tplLeadHomeAddress,
						renderData: {
							Title: "Enter Home Address",
							LeadId: "[New Lead]"
						},
						renderSelectors:
						{
							frmLeadStreetEl: "#frmLeadStreet",
							frmLeadStreet2El: "#frmLeadStreet2",
							frmLeadCityEl: "#frmLeadCity",
							frmLeadStateEl: "#frmLeadState",
							frmLeadPostalCodeEl: "#frmLeadPostalCode",
							frmLeadCountryEl: "#frmLeadCountry",
							frmLeadTimeZoneEl: "#frmLeadTimeZone"
						}
					},
					{
						id: "leadCUForm3",
						xtype: "container",
						renderTpl: tplLeadProducts,
						renderData: {
							Title: "Select Products Offered",
							SalesRepInfo: Ext.String.format("{0} ({1})", SOS.AppService.UserInfo.Fullname
								, SOS.AppService.UserInfo.Username),
							ContactDate: SOS.Utils.Strings.formatDateLong(new Date()),
							LeadId: "[New Lead]"
						},
						renderSelectors:
						{
							frmLeadProductsEl: "#frmLeadProducts",
							frmLeadSalesRepNameEl: "#frmLeadSalesRepName",
							frmLeadContactDateEl: "#frmLeadContactDate"
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
		StartTheWizard: function ()
		{
			/** Initialize. */
			var options = {};
			var oStart = new SOS.Modals.LeadCreateUpdate(options);

			/** Open wizard. */
			oStart.show();
		},
		StartEditWizard: function(sLeadCompoundId)
		{
			/** Initialize. */
			var oLeadId = SOS.Utils.Strings.getLeadIdObject(sLeadCompoundId);
			var options = { CompoundLeadId: oLeadId };
			var oStart = new SOS.Modals.LeadCreateUpdate(options);

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

	OnAfterRender: function (cmp)
	{
		/** Initialize. */
		var me = this;
		me._viewContainer = Ext.getCmp("leadCreateUpdateWizFrm");
		var frm00 = Ext.getCmp("leadCUForm0");
		var frm01 = Ext.getCmp("leadCUForm1");
		var frm02 = Ext.getCmp("leadCUForm2");
		console.log("OnAfterRender cmp passed as an argument:", cmp);
		var frmLeadBtnNext0El = $("#frmLeadBtnNext0");
		var frmLeadImgNext0El = $("#frmLeadImgNext0");
		var frmLeadBtnPrev1El = $("#frmLeadBtnPrev1");
		var frmLeadImgPrev1El = $("#frmLeadImgPrev1");
		var frmLeadBtnNext1El = $("#frmLeadBtnNext1");
		var frmLeadImgNext1El = $("#frmLeadImgNext1");
		var frmLeadBtnPrev2El = $("#frmLeadBtnPrev2");
		var frmLeadImgPrev2El = $("#frmLeadImgPrev2");
		var frmLeadBtnNext2El = $("#frmLeadBtnNext2");
		var frmLeadImgNext2El = $("#frmLeadImgNext2");
		var frmLeadBtnPrev3El = $("#frmLeadBtnPrev3");
		var frmLeadImgPrev3El = $("#frmLeadImgPrev3");
		var frmLeadBtnSbmt3El = $("#frmLeadBtnSubmit");
		var frmLeadImgSbmt3El = $("#frmLeadImgSubmit");
		$(frm00.frmLeadSourceDateGenEl.dom).mask("99/99/9999", { placeholder: " " });
		$(frm01.frmLeadPhoneHomeEl.dom).mask("(999) 999-9999", { placeholder: " " });
		$(frm01.frmLeadPhoneMobileEl.dom).mask("(999) 999-9999", { placeholder: " " });
		$(frm01.frmLeadPhoneWorkEl.dom).mask("(999) 999-9999", { placeholder: " " });
		$(frm01.frmLeadDOBEl.dom).mask("99/99/9999", { placeholder: " " });
		$(frm01.frmLeadSSNEl.dom).mask("999-99-9999", { placeholder: " " });
		$(frm02.frmLeadPostalCodeEl.dom).mask("99999?-9999", { placeholder: " "});

		frmLeadBtnNext0El.unbind().bind("click", me.MoveNext);
		frmLeadImgNext0El.unbind().bind("click", me.MoveNext);
		frmLeadBtnPrev1El.unbind().bind("click", me.MoveNext);
		frmLeadImgPrev1El.unbind().bind("click", me.MoveNext);
		frmLeadBtnNext1El.unbind().bind("click", me.MoveNext);
		frmLeadImgNext1El.unbind().bind("click", me.MoveNext);
		frmLeadBtnNext2El.unbind().bind("click", me.MoveNext);
		frmLeadImgNext2El.unbind().bind("click", me.MoveNext);
		frmLeadBtnPrev2El.unbind().bind("click", me.MoveNext);
		frmLeadImgPrev2El.unbind().bind("click", me.MoveNext);
		frmLeadBtnPrev3El.unbind().bind("click", me.MoveNext);
		frmLeadImgPrev3El.unbind().bind("click", me.MoveNext);
		frmLeadBtnSbmt3El.unbind().bind("click", me.SaveLead);
		frmLeadImgSbmt3El.unbind().bind("click", me.SaveLead);

		/** Bind event to close objects. */
		$("#leadCu-Source-dialog .btn-close-x").bind("click", me.CloseWiz);
		$("#leadCu-Personal-dialog .btn-close-x").bind("click", me.CloseWiz);
		$("#leadCu-HomeAddress-dialog .btn-close-x").bind("click", me.CloseWiz);
		$("#leadCu-Product-dialog .btn-close-x").bind("click", me.CloseWiz);

		/** Add metadata to supporting input fields. */
		this.InitCombos();
		$(frm00.frmLeadSourceEl.dom).combobox();
		$(frm00.frmLeadDispositionEl.dom).combobox();

		// ** Init datepicker. */
		frm00.frmLeadSourceDateGenEl.dom.value = SOS.Utils.Strings.formatDate(new Date(), 'm/d/Y');
		$(frm00.frmLeadSourceDateGenEl.dom).datepicker();

		/** Hide fields that need to be hidden. */
		this.HideItems();

		/** Check for Edit call. */
		if (this.CompoundLeadId)
		{
			this.DataBindForm();
		}
	},

	InitCombos: function()
	{
		/** Initialize comboboxes. */
		$.widget( "ui.combobox", {
			_create: function() {
				var input,
					self = this,
					select = this.element.hide(),
					selected = select.children( ":selected" ),
					value = selected.val() ? selected.text() : "",
					wrapper = $( "<span>" )
						.addClass( "ui-combobox" )
						.insertAfter( select );

				input = $( "<input>" )
					.appendTo( wrapper )
					.val( value )
					.addClass( "ui-state-default" )
					.autocomplete({
						delay: 0,
						minLength: 0,
						source: function( request, response ) {
							var matcher = new RegExp( $.ui.autocomplete.escapeRegex(request.term), "i" );
							response( select.children( "option" ).map(function() {
								var text = $( this ).text();
								if ( this.value && ( !request.term || matcher.test(text) ) )
									return {
										label: text.replace(
											new RegExp(
												"(?![^&;]+;)(?!<[^<>]*)(" +
													$.ui.autocomplete.escapeRegex(request.term) +
													")(?![^<>]*>)(?![^&;]+;)", "gi"
											), "<strong>$1</strong>" ),
										value: text,
										option: this
									};
							}) );
						},
						select: function( event, ui ) {
							ui.item.option.selected = true;
							self._trigger( "selected", event, {
								item: ui.item.option
							});
						},
						change: function( event, ui ) {
							if ( !ui.item ) {
								var matcher = new RegExp( "^" + $.ui.autocomplete.escapeRegex( $(this).val() ) + "$", "i" ),
									valid = false;
								select.children( "option" ).each(function() {
									if ( $( this ).text().match( matcher ) ) {
										this.selected = valid = true;
										return false;
									}
								});
								if ( !valid )
								{
									/** Check to see which combobox fired. */
									if (select.attr("id") !== "frmLeadSource")
									{
										/**remove invalid value, as it didn't match anything*/
										 $( this ).val( "" );
										 select.val( "" );
										 input.data( "autocomplete" ).term = "";
										 return false;
									}
									/** Confirm and execute the adding of a new item to Source. */
									SOS.Framework.Popup.Confirm({
										Title: "Add new Source?"
										, MessageBody: Ext.String.format("Are you sure you want to add '{0}' as a new source?"
											, input.data( "autocomplete" ).term)
										, OnConfirm: function ()
										{
											/** Init. */
											var oSrv = new SOS.ClientServices.CmsServices();
											function onSuccess(response)
											{
												select.append($("<option></option>")
													.attr("value", response.Value.Value)
													.text(response.Value.Text));

												/** Select the item just added. */
												select.children("option:selected").remove(); // Removes the selection.
												var oOption = select.children("option").find("[value='" + response.Value.Value+ "']");
												//alert(option.attr("value")); //undefined
												oOption.attr('selected', 'selected');
											}
											function onFailure(oMsgItem)
											{
												SOS.Framework.Popup.GeneralPopup(oMsgItem);
											}
											oSrv.on(SOS.ClientServices.CmsServices.EVNT_OPTITMAD_SUCCESS, onSuccess);
											oSrv.on(SOS.ClientServices.CmsServices.EVNT_OPTITMAD_FAILURE, onFailure);
											oSrv.OptionItemAdd("Source", input.data("autocomplete").term, this);
										}
									});
								}
							}
						}
					})
					.addClass( "ui-widget ui-widget-content ui-corner-left" );

				input.data( "autocomplete" )._renderItem = function( ul, item ) {
					return $( "<li></li>" )
						.data( "item.autocomplete", item )
						.append( "<a>" + item.label + "</a>" )
						.appendTo( ul );
				};

				$( "<a>" )
					.attr( "tabIndex", -1 )
					.attr( "title", "Show All Items" )
					.appendTo( wrapper )
					.button({
						icons: {
							primary: "ui-icon-triangle-1-s"
						},
						text: false
					})
					.removeClass( "ui-corner-all" )
					.addClass( "ui-corner-right ui-button-icon" )
					.click(function() {
						// close if already visible
						if ( input.autocomplete( "widget" ).is( ":visible" ) ) {
							input.autocomplete( "close" );
							return;
						}

						// work around a bug (likely same cause as #5265)
						$( this ).blur();

						// pass empty string as value to search for, displaying all results
						input.autocomplete( "search", "" );
						input.focus();
					});
			},

			destroy: function() {
				this.wrapper.remove();
				this.element.show();
				$.Widget.prototype.destroy.call( this );
			}
		});

		/** Init metadata of the comboboxes. */
		var oSrv = new SOS.ClientServices.CmsServices();
		function onSuccess(response)
		{
			/** Init. */
			var frm00 = Ext.getCmp("leadCUForm0");
			switch(response.ListName)
			{
				case "Disposition":
					this.BindOptionsToSelectEl($(frm00.frmLeadDispositionEl.dom), response.Value);
					break;
				case "Source":
					this.BindOptionsToSelectEl($(frm00.frmLeadSourceEl.dom), response.Value);
					break;
			}
		}
		function onFailure(oMsgItem){ SOS.Framework.Popup.GeneralPopup(oMsgItem); }
		oSrv.on(SOS.ClientServices.CmsServices.EVNT_OPTITEMS_SUCCESS, onSuccess, this);
		oSrv.on(SOS.ClientServices.CmsServices.EVNT_OPTITEMS_FAILURE, onFailure, this);
		oSrv.OptionItemsGet("Source", this);
		oSrv.OptionItemsGet("Disposition", this);
	},

	BindOptionsToSelectEl: function (sSelector, oOptionsList)
	{
		/** Build List. */
		$.each(oOptionsList, function (index, oItem)
		{
			console.log(index, oItem);
			sSelector.append($("<option></option>")
				.attr("value", oItem.Value)
				.text(oItem.Text));
		});
	},

	/**
	 * Validate the form
	 */
	ValidateForm0: function ()
	{
		var bResult = true;
		var frm00 = Ext.getCmp("leadCUForm0");
		var aMessages = [];

		// ** Check fields
		if (frm00.frmLeadSourceEl.dom.value === "")
		{
			aMessages.push({FieldName: "Lead Source", Message: "Please select or enter the source of the lead."});
			bResult = false;
		}
		/** Check the disposition field. */
		if (frm00.frmLeadDispositionEl.dom.value === "")
		{
			aMessages.push({FieldName: "Lead Disposition", Message: "Please select the disposition of the lead."});
			bResult = false;
		}
		/** Check the Generated Date. */
		if (frm00.frmLeadSourceDateGenEl.dom.value === "")
		{
			aMessages.push({FieldName: "Date Generated", Message:"Can not be blank.  Please enter the date this lead was generated."});
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
					Title: "Invalid Source",
					MessageBody: "The following errors were generated:",
					ValidationList: szListOfErrors
				}
			);
		}

		/** Return result.*/
		return bResult;
	},

	/**
	 * Validates the form and returns a bool with either it was successful or not.
	 */
	ValidateForm1: function ()
	{
		/** Initialize. */
		var bResult = true;  // Set default value.
		var frm01 = Ext.getCmp("leadCUForm1");
		var aMessages = [];

		// ** Check fields
		if (frm01.frmLeadFirstNameEl.dom.value === ""
			|| frm01.frmLeadFirstNameEl.dom.value === null)
		{
			aMessages.push({FieldName: "First Name", Message: "You must enter a value in this field."});
			bResult = false;
		}
		if (frm01.frmLeadLastNameEl.dom.value === ""
			|| frm01.frmLeadLastNameEl.dom.value === null)
		{
			aMessages.push({FieldName: "Last Name", Message: "You must enter a value in this field."});
			bResult = false;
		}
//		// ** Business Rule 1
//		if (frm01.frmLeadDOBEl.dom.value === ""
//			&& frm01.frmLeadSSNEl.dom.value ==="")
//		{
//			aMessages.push({
//				FieldName: "DOB and SSN", Message: "There has to be a value in one of these two fields."
//			});
//			bResult = false;
//		}
		// ** Business Rule 2
		if (frm01.frmLeadPhoneHomeEl.dom.value === ""
			&& frm01.frmLeadPhoneMobileEl.dom.value === ""
			&& frm01.frmLeadPhoneWorkEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Phones", Message: "There must be at least one phone number entered."
			});
			bResult = false;
		}
		// ** Business Rule 3
		if (frm01.frmLeadEmailEl.dom.value === "")
		{
			aMessages.push({FieldName: "Email"
				, Message: "This is not a required field.  If you would like to receive event notifications please enter an email address."})
		}
		if (frm01.frmLeadLanguageEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Language"
				, Message: "Please be sure to select a language.  If your language is not available please select English."
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
					Title: "Invalid Personal Information",
					MessageBody: "The following errors were generated:",
					ValidationList: szListOfErrors
				}
			);
		}

		/** Return result.*/
		return bResult;
	},

	/**
	 * Validates the form and returns a bool with either it was successful or not.
	 */
	ValidateForm3: function ()
	{
		/** Initialize. */
		var bResult = true;  // Set default value.
		var frm03 = Ext.getCmp("leadCUForm3");
		var aMessages = [];
		// ** Products
		if (frm03.frmLeadProductsEl.dom.value === "")
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
					Title: "Invalid Product Offerings",
					MessageBody: "The following errors were generated:",
					ValidationList: szListOfErrors
				}
			);
		}

		/** Return result.*/
		return bResult;
	},

	/**
	 * Validates the form and returns a bool with either it was successful or not.
	 */
	ValidateForm2: function ()
	{
		/** Initialize. */
		var bResult = true;  // Set default value.
		var frm02 = Ext.getCmp("leadCUForm2");
		var aMessages = [];
		// ** Street
		if (frm02.frmLeadStreetEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Address"
				, Message: "This field is a required field.  Please enter a valid street."
			});
			bResult = false;
		}
		// ** City
		if (frm02.frmLeadCityEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "City"
				, Message: "This field is a required field.  Please enter a City name."
			});
			bResult = false;
		}
		// ** State
		if (frm02.frmLeadStateEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "State"
				, Message: "This field is a required field.  Please select a state."
			});
			bResult = false;
		}
		// ** Postal Code
		if (frm02.frmLeadPostalCodeEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Postal Code"
				, Message: "This field is a required field.  Please enter a valid zip code."
			});
			bResult = false;
		}
		// ** Country
		if (frm02.frmLeadCountryEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Country"
				, Message: "This field is a required field.  Please select a country."
			});
			bResult = false;
		}
		// ** Time zone
		if (frm02.frmLeadTimeZoneEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Time Zone"
				, Message: "This field is a required field.  Please select a time zone."
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
					Title: "Invalid Personal Information",
					MessageBody: "The following errors were generated:",
					ValidationList: szListOfErrors
				}
			);
		}

		/** Return result.*/
		return bResult;
	},

	DataBindForm: function ()
	{
		/** Initialize. */
		var me = this;
		var lLeadId = this.CompoundLeadId.LeadId;
		var lCustomerMasterFileId = this.CompoundLeadId.CustomerMasterFileId;
		var oSrv = new SOS.ClientServices.CmsServices();
		var oData = {};
		function fxSuccess(oResponse)
		{
			console.log(oResponse.Value);
			oData = oResponse.Value;
			me.FormBinding(oData);
		}
		function fxFailure(oMsgItem)
		{
			SOS.Framework.Popup.GeneralPopup(oMsgItem);
		}
		oSrv.on(SOS.ClientServices.CmsServices.EVNT_LEDFSRCH_SUCCESS, fxSuccess);
		oSrv.on(SOS.ClientServices.CmsServices.EVNT_LEDFSRCH_FAILURE, fxFailure);

		/** Execute call. */
		var oDef = oSrv.QlGetLeadFull(lLeadId, lCustomerMasterFileId, false, this);
		console.log(oDef);
	},

	FormBinding: function(oData)
	{
		/** Initialize. */
		var frm00 = Ext.getCmp("leadCUForm0");
		var frm01 = Ext.getCmp("leadCUForm1");
		var frm02 = Ext.getCmp("leadCUForm2");
		var frm03 = Ext.getCmp("leadCUForm3");
	debugger;
		/** Form 00 */
		$(frm00.frmLeadSourceEl.dom).val(oData.LeadSourceId.toString());
		$(frm00.frmLeadDispositionEl.dom).val(oData.LeadDispositionId.toString());

	},

	SaveLead: function (/*event*/)
	{
		/** Validate form. */
		/*var me = this;*/
		var viewContainer = Ext.getCmp("leadCreateUpdateWizFrm");
		if (!viewContainer.ValidateForm3()) return;

		/** Initialize. */
		var frm00 = Ext.getCmp("leadCUForm0");
		var frm01 = Ext.getCmp("leadCUForm1");
		var frm02 = Ext.getCmp("leadCUForm2");
		var frm03 = Ext.getCmp("leadCUForm3");
		var oData = {};
		var oSrv = new SOS.ClientServices.CmsServices();

		/** Bind Data to object. */
		oData.CustomerTypeId = "LEAD";
		oData.DealerId = SOS.AppService.UserInfo.DealerId;
		oData.TeamLocationId = SOS.AppService.UserInfo.TeamLocationId;
		oData.LeadSourceId = frm00.frmLeadSourceEl.dom.value;
		oData.LeadDispositionId = frm00.frmLeadDispositionEl.dom.value;
		oData.SeasonId = SOS.AppService.UserInfo.SeasonId;
		oData.SalesRepId = SOS.AppService.UserInfo.SalesRepId;
		oData.Salutation = frm01.frmLeadSalutationEl.dom.value;
		oData.FirstName = frm01.frmLeadFirstNameEl.dom.value;
		if (frm01.frmLeadMiddleNameEl.dom.value)
			oData.MiddleName = frm01.frmLeadMiddleNameEl.dom.value;
		oData.LastName = frm01.frmLeadLastNameEl.dom.value;
		if (frm01.frmLeadSuffixEl.dom.value)
			oData.Suffix = frm01.frmLeadSuffixEl.dom.value;
		if (frm01.frmLeadGenderEl.dom.value)
			oData.Gender = frm01.frmLeadGenderEl.dom.value;
		if (frm01.frmLeadSSNEl.dom.value)
			oData.SSN = frm01.frmLeadSSNEl.dom.value;
		if (frm01.frmLeadDOBEl.dom.value)
			oData.DOB = frm01.frmLeadDOBEl.dom.value;
		if (frm01.frmLeadEmailEl.dom.value)
			oData.Email = frm01.frmLeadEmailEl.dom.value;
		if (frm01.frmLeadPhoneHomeEl.dom.value)
			oData.PhoneHome = frm01.frmLeadPhoneHomeEl.dom.value;
		if (frm01.frmLeadPhoneWorkEl.dom.value)
			oData.PhoneWork = frm01.frmLeadPhoneWorkEl.dom.value;
		if (frm01.frmLeadPhoneMobileEl.dom.value)
			oData.PhoneMobile = frm01.frmLeadPhoneMobileEl.dom.value;
		oData.LocalizationId = frm01.frmLeadLanguageEl.dom.value;
		// ** Home Address
		oData.Address = {};
		oData.Address.DealerId = oData.DealerId;
		oData.Address.StreetAddress = frm02.frmLeadStreetEl.dom.value;
		if (frm02.frmLeadStreet2El.dom.value)
			oData.Address.StreetAddress2 = frm02.frmLeadStreet2El.dom.value;
		oData.Address.City = frm02.frmLeadCityEl.dom.value;
		oData.Address.StateId = frm02.frmLeadStateEl.dom.value;
		var szPostalCode = frm02.frmLeadPostalCodeEl.dom.value;
		var oPostalCode = szPostalCode.split("-");
		oData.Address.PostalCode = oPostalCode[0];
		if (oPostalCode.length > 1)
			oData.Address.PlusFour = oPostalCode[1];
		oData.Address.CountryId = frm02.frmLeadCountryEl.dom.value;
		oData.Address.TimeZoneId = frm02.frmLeadTimeZoneEl.dom.value;

		// ** Products
		var prodArray = [];
		if ($(frm03.frmLeadProductsEl.dom).val() !== null)
		{
			$.each($(frm03.frmLeadProductsEl.dom).val(), function (index, value)
			{
				prodArray.push({ ProductSkwId: value });
			});
		}
		oData.ProductSkwIdList = prodArray;

		/** Print Saved information. */
		console.log(oData);

		/** Init the service call. */
		function onSuccess (response)
		{
			SOS.UI.Services.Hide();
			SOS.Framework.Popup.GeneralPopup({
				Title: "Successfully Created a New Lead",
				MessageBody: Ext.String.format("A new lead has been created for {0}.  With a Lead Id of {1}."
				, response.Value.FirstName
				, response.Value.CustomerMasterFileId),
				PopupType: "Success"
			});

			/** Open a new panel for the just created lead.
			 * ld-1000041-3000039 */
			var oArg = {
				Id: Ext.String.format("ld-{0}-{1}", response.Value.LeadID, response.Value.CustomerMasterFileId)
				, NoteAccount: false
			};
			SOS.Panels.LeadsPanel.OpenLead(oArg);

			/** Close this window and open the new lead information into a new record. */
			Ext.getCmp("leadCreateUpdateWizFrm").close();
		}
		function onFailure (oMsgItem)
		{
			SOS.UI.Services.Hide();
			SOS.Framework.Popup.GeneralPopup(oMsgItem);
		}
		oSrv.on(SOS.ClientServices.CmsServices.EVNT_LEADCRUP_SUCCESS, onSuccess);
		oSrv.on(SOS.ClientServices.CmsServices.EVNT_LEADCRUP_FAILURE, onFailure);

		SOS.UI.Services.ShowBusy("Saving Lead...");
		oSrv.QlLeadCreateUpdate(oData, this);
	},

	MoveNext: function (event)
	{
		/** Initialize. */
		/*var me = this;*/
		var viewContainer = Ext.getCmp("leadCreateUpdateWizFrm");
		var frm00 = Ext.getCmp("leadCUForm0");
		var frm01 = Ext.getCmp("leadCUForm1");
		var frm02 = Ext.getCmp("leadCUForm2");
		var frm03 = Ext.getCmp("leadCUForm3");

		console.log(event);
		/** Check to see which next button was clicked. */
		switch(event.currentTarget.id)
		{
			case "frmLeadBtnNext0":
			case "frmLeadImgNext0":
				if (viewContainer.ValidateForm0())
				{
					viewContainer.getLayout().setActiveItem("leadCUForm1");
					frm01.frmLeadSalutationEl.dom.focus();
				}
				break;
			case "frmLeadBtnPrev1":
			case "frmLeadImgPrev1":
				viewContainer.getLayout().setActiveItem("leadCUForm0");
				frm00.frmLeadSourceEl.dom.focus();
				break;
			case "frmLeadBtnNext1":
			case "frmLeadImgNext1":
				if (viewContainer.ValidateForm1())
				{
					viewContainer.getLayout().setActiveItem("leadCUForm2");
					frm02.frmLeadStreetEl.dom.focus();
				}
				break;
			case "frmLeadBtnPrev2":
			case "frmLeadImgPrev2":
				viewContainer.getLayout().setActiveItem("leadCUForm1");
				frm01.frmLeadPhoneMobileEl.dom.focus();
				break;
			case "frmLeadBtnNext2":
			case "frmLeadImgNext2":
				if (viewContainer.ValidateForm2())
				{
					viewContainer.getLayout().setActiveItem("leadCUForm3");
					frm03.frmLeadProductsEl.dom.focus();
				}
				break;
			case "frmLeadBtnPrev3":
			case "frmLeadImgPrev3":
				viewContainer.getLayout().setActiveItem("leadCUForm2");
				frm02.frmLeadPostalCodeEl.dom.focus();
				break;
		}
	},
	/**
	 * This function will close the wizard.
	 */
	CloseWiz: function (/*event*/)
	{
		/** Initialize. */
		var viewContainer = Ext.getCmp("leadCreateUpdateWizFrm");
		SOS.Framework.Popup.Confirm({
			Title: "Do you want to leave?"
			, MessageBody: "If you leave you will lose all your changes.  Do you want to leave?"
			, OnConfirm: function ()
			{
				viewContainer.close();
				viewContainer.destroy();
			}
		});
	}
});
