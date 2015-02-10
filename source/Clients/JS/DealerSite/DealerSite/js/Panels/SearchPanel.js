/**
 * Created with JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 4/24/12
 * Time: 8:29 PM
 * To change this template use File | Settings | File Templates.
 */

Ext.ns("SOS.Panels");

Ext.define("SOS.Panels.SearchPanel",
{
	extend: "Ext.panel.Panel",
	alias: "widget.SosSearchPanel",

	/** .ctor. */
	constructor: function(options)
	{
		/** Initialize. */
		var me = this;
		options = options || {};
		var szTplInputForm = SOS.UI.Services.GetTemplate('SearchPanelInputForm.html');

		var config = Ext.merge(options,
		{
			//autoScroll: true
			//, width: '100%'
			//height: 'auto'
			padding: '80 0 0 0'
//			, tpl: szTplInputForm
//			, data:
//			{
//				title: "Search Panel. Leads"
//			}
			, listeners:
			{
				afterrender: me.OnAfterRender
			},
			items:
				[
					{
						id: "searchOuter",
						xtype: "container",
						style: {
							'background': "-webkit-linear-gradient(top, #fff, #ddd)",
							'border-radius': "10px",
							'padding': "1px"
						},
						items:
						[
							{
								id: "searchForm",
								xtype: "container",
								data:
									{
										FirstName: '[first name here.]'
										, LastName: "[last name here.]"
									},
								tpl: szTplInputForm
							},
							{
								id: "searchResult",
								xtype: "container"
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
	OnAfterRender: function()
	{
		/** Initialize. */
		var me = this;

		/** Get el of fields. */
		me.fldFirstNameEl = Ext.get("frmFirstName");
		me.fldLastNameEl = Ext.get("frmLastName");
		me.fldDispositionEl = Ext.get("frmDispositionSrc");
		me.fldSourceEl = Ext.get("frmSourceSrc");
		me.fldPhoneNumberEl = Ext.get("frmPhoneNumber");
		$(me.fldPhoneNumberEl.dom).mask("(999) 999-9999", { placeholder: " " });
		me.fldEmailEl = Ext.get("frmEmail");
		me.fldLeadIdEl = Ext.get("frmLeadId");
		$(me.fldLeadIdEl.dom).mask("9999999", { placeholder: " " });

		/** Bind click events to handlers. */
		me.imgSearchLeadEl = Ext.get("imgSearchLead");
		me.imgSearchLeadEl.on("click", me.OnSearch, this);
		me.aSearchLeadSubmitEl = Ext.get("aSearchLeadSubmit");
		me.aSearchLeadSubmitEl.on("click", me.OnSearch, this);

		/** Add metadata to supporting input fields. */
		this.InitCombos();
		$(me.fldDispositionEl.dom).combobox();
		$(me.fldSourceEl.dom).combobox();

		/** Show panel. */
		this.show();
	},

	InitCombos: function()
	{
		/** Initialize comboboxes. */
		var me = this;
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
									if (select.attr("id") !== "frmSourceSrc")
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
					this.BindOptionsToSelectEl($(me.fldDispositionEl.dom), response.Value);
					break;
				case "Source":
					this.BindOptionsToSelectEl($(me.fldSourceEl.dom), response.Value);
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

	OnResize: function (cmp, w, h)
	{
		this.callParent(arguments);
	},

	OnActivate: function()
	{
		console.log('SearchPanel.OnActivate');
		this._active = true;
		this.show();
	},

	OnDeactivate: function()
	{
		this._active = false;
		this.hide();
	},

	OnSearch: function()
	{
		/** Initialize. */
		var me = this;
		var nLeadId = Number(me.fldLeadIdEl.dom.value);

		/** Perform search. */
		this.ExecuteSearch(me.fldFirstNameEl.dom.value
			, me.fldLastNameEl.dom.value
			, me.fldPhoneNumberEl.dom.value
			, me.fldEmailEl.dom.value
			, nLeadId > 0 ? nLeadId : null
			, me.fldDispositionEl.dom.value
			, me.fldSourceEl.dom.value
		);

	},

	/** Member Functions. */
	ExecuteSearch: function (szFirstName, szLastName, szPhone, szEmail, nLeadId, nDispositionId, nSourceId)
	{
		/** Initialize. */
		var me = this;
		var oServices = new SOS.ClientServices.CmsServices();
		function onSuccess(response)
		{
			/** Initialize. */
			var oTplResults = SOS.UI.Services.GetTemplate("SearchPanelResult.html");
			var oData = {
				Leads: response.Value
			};

			/** Bind data to grid. */
			oTplResults.overwrite("searchResult", oData);

			/** Bind click event to the results. */
			$("tr.search-result-tr").click(me.OpenLeads);
		}
		function onFailure(oMessageItem)
		{
			SOS.Framework.Popup.GeneralPopup(oMessageItem);
		}
		oServices.on(SOS.ClientServices.CmsServices.EVNT_LEADSRCH_SUCCESS, onSuccess);
		oServices.on(SOS.ClientServices.CmsServices.EVNT_LEADSRCH_FAILURE, onFailure);

		/** Execute. */
		var nPageSize = 30;
		var nPageNumber = 1;
		oServices.QlSearch(szFirstName, szLastName, szPhone, szEmail, nLeadId, nDispositionId, nSourceId, nPageSize, nPageNumber, this);
	},

	OpenLeads: function (meTr)
	{
		console.log("ID:", meTr.currentTarget.id);
		/** Initialize. */
		var oArg = { Id: meTr.currentTarget.id, NoteAccount: true };
		SOS.Panels.LeadsPanel.OpenLead(oArg);
		SOS.AppService.DispatchEvent("panelactivated", SOS.Views.MainWindowViewport.Panels.LeadsPanel);
	}
});