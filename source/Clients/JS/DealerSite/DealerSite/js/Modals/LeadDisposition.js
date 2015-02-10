/**
 * Created with JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 5/30/12
 * Time: 06:35 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.ns("SOS.Modals");

Ext.define("SOS.Modals.LeadDisposition", {

	extend: "Ext.window.Window",
	alias: "widget.leadDisposition",

	constructor: function (options)
	{
		/** Initialize.  */
		var me = this;
		options = options || {};
		var tplSelectProd = SOS.UI.Services.GetTemplate('LeadUpdateDisposition.html');
		var sCompoundLeadId = Ext.String.format("{0}-{1}", options.CompoundLeadId.LeadId
			, options.CompoundLeadId.CustomerMasterFileId);
		me.CompoundLeadId = options.CompoundLeadId;

		/** Build Configuration options for the window. */
		var config = Ext.merge(options,
			{
				id: "leadDispositionFrm",
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
				items:[
					{
						id: "leadDispositionForm1",
						xtype: "container",
						renderTpl: tplSelectProd,
						renderData: {
							ActionToPerform: "Select a Product for New Customer",
							Title: "Select the appropriate disposition.",
							CompoundLeadId: sCompoundLeadId,
							LeadId: options.CompoundLeadId.LeadId,
							CurrentDisp: options.DispositionInfo,
							AsOfDate: SOS.Utils.Strings.formatDate(new Date(options.DispositionDate))
						},
						renderSelectors: {
							frmLeadDispositionEl: "#frmLeadDisposition"
							, frmDispImgSaveEl: "#frmDispImgSave0"
							, frmDispBtnSaveEl: "#frmDispBtnSave0"
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
		StartTheWizard: function (sLeadCompoundId, oDispositionInfo, oDispositionDate)
		{
			/** Initialize. */
			var oLeadId = SOS.Utils.Strings.getLeadIdObject(sLeadCompoundId);
			var options = { CompoundLeadId: oLeadId, DispositionInfo: oDispositionInfo
					, DispositionDate: oDispositionDate };
			var oStart = new SOS.Modals.LeadDisposition(options);

			/** Open wizard. */
			oStart.show();
		}
	},

	OnAfterRender: function ()
	{
		/** Initialize. */
		var me = this;
		me._viewContainer = Ext.getCmp("leadDispositionFrm");
		var oFrm00 = Ext.getCmp("leadDispositionForm1");

		/** Bind events to buttons. */
		$("#leadCu-disposition-dialog .btn-close-x").bind("click", me.CloseDialog);
		$(oFrm00.frmDispBtnSaveEl.dom).unbind().bind("click", me.Save);
		$(oFrm00.frmDispImgSaveEl.dom).unbind().bind("click", me.Save);

		/** Add metadata to supporting input fields. */
		this.InitCombos();
		$(oFrm00.frmLeadDispositionEl.dom).combobox();

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
									/**remove invalid value, as it didn't match anything*/
									$( this ).val( "" );
									select.val( "" );
									input.data( "autocomplete" ).term = "";
									return false;
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
			var frm00 = Ext.getCmp("leadDispositionForm1");
			switch(response.ListName)
			{
				case "Disposition":
					this.BindOptionsToSelectEl($(frm00.frmLeadDispositionEl.dom), response.Value);
					break;
			}
		}
		function onFailure(oMsgItem){ SOS.Framework.Popup.GeneralPopup(oMsgItem); }
		oSrv.on(SOS.ClientServices.CmsServices.EVNT_OPTITEMS_SUCCESS, onSuccess, this);
		oSrv.on(SOS.ClientServices.CmsServices.EVNT_OPTITEMS_FAILURE, onFailure, this);
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

	Save: function ()
	{
		/** Initialize. */
		var viewContainer = Ext.getCmp("leadDispositionFrm");
		var oFrm00 = Ext.getCmp("leadDispositionForm1");

		/** Validate form first. */
		if (!viewContainer.ValidateForm(oFrm00)) return;

		function onSuccess(resposne)
		{
			SOS.Framework.Popup.GeneralPopup({
				Title: "Successfully Updated the Disposition"
				, MessageBody: "The disposition of this lead has been updated."
				, PopupType: "Success"
			});

			viewContainer.close();
			viewContainer.destroy();

			/** Reopen the Leads Card to see the changes and the new notes. */
			var oArg = { Id: Ext.String.format("ld-{0}-{1}"
				, viewContainer.CompoundLeadId.LeadId
				, viewContainer.CompoundLeadId.CustomerMasterFileId), NoteAccount: false };
			SOS.Panels.LeadsPanel.OpenLead(oArg);
			SOS.AppService.DispatchEvent("panelactivated", SOS.Views.MainWindowViewport.Panels.LeadsPanel);
		}
		function onFailure(oMsgItem)
		{
			SOS.Framework.Popup.GeneralPopup(oMsgItem);
		}

		var oSrv = new SOS.ClientServices.CmsServices();
		oSrv.on(SOS.ClientServices.CmsServices.EVNT_LEADDISP_SUCCESS, onSuccess);
		oSrv.on(SOS.ClientServices.CmsServices.EVNT_LEADDISP_FAILURE, onFailure);
		oSrv.LeadDispositionUpdate(viewContainer.CompoundLeadId.LeadId
			, oFrm00.frmLeadDispositionEl.dom.value, this);
	},
	ValidateForm: function (oFrm00)
	{
		/** Initialize. */
		var bResult = true;
		var aMessages = [];

		/** Check the disposition field. */
		if (oFrm00.frmLeadDispositionEl.dom.value === "")
		{
			aMessages.push({
				FieldName: "Disposition"
				, Message : "Please select a disposition from the list."
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
					Title: "Invalid Disposition Information",
					MessageBody: "The following errors were generated:",
					ValidationList: szListOfErrors
				}
			);
		}

		/** Return result.*/
		return bResult;
	},

	/**
	 * This function will close the wizard.
	 */
	CloseDialog: function (/*event*/)
	{
		/** Initialize. */
		var viewContainer = Ext.getCmp("leadDispositionFrm");
		SOS.Framework.Popup.Confirm({
			Title: "Do you want to leave?"
			, MessageBody: "Are you sure you want to leave?"
			, OnConfirm: function ()
			{
				viewContainer.close();
				viewContainer.destroy();
			}
		});
	}

});