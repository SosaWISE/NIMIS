/**
 * Created with JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 6/2/12
 * Time: 11:28 PM
 * To change this template use File | Settings | File Templates.
 */

Ext.ns("SOS.Modals");

Ext.define("SOS.Modals.MyCalendarEvent",
{
	extend: "Ext.window.Window",
	alias: "widget.myCalendarEvent",

	constructor: function (options)
	{
		/** Initialize. */
		//var me = this;
		options = options || {};
		var tplCalendarEvent = SOS.UI.Services.GetTemplate('MyCalendarEvent.html');
		options.data = Ext.merge(options.data, {
			Title: "Create or Edit an Event Calendar"
		});

		/** Build Configuration options for the window. */
		var config = Ext.merge(options,
			{
				id: "myCalendarEventModal",
				title: '<img src="/img/DragTab-Yellow.png" alt="Click here to move the window" />',
				modal: true,
				tpl: tplCalendarEvent,
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

	OnAfterRender: function ()
	{
		/** Initialize. */
		var me = this;
		$("#myCalendarEvent-dialog .btn-close-x").bind("click", me.CloseModal);

		$("#frmCalEventSDate").mask("99/99/9999 99:99 aa", { placeholder: " " });
		//$("#frmCalEventSDate").datepicker();
		$("#frmCalEventSDate").datetimepicker({
			ampm: true
		});
		$("#frmCalEventEDate").mask("99/99/9999 99:99 aa", { placeholder: " " });
		$("#frmCalEventEDate").datepicker();

		/** TextAreas UI. */
		$("#frmCalEventDesc").resizable({
			handles: "se"
		});

		/** Add metadata to supporting input fields. */
		this.InitCombos();
		$("#frmCalEventReminder").combobox();
		$("#frmCalEventMediaType").combobox();

	},

	CloseModal: function ()
	{
		/** Initialize. */
		var viewContainer = Ext.getCmp("myCalendarEventModal");
		SOS.Framework.Popup.Confirm({
			Title: "Do You Want to Close?"
			, MessageBody: "You are about to close this event. &nbsp;Are you sure you want to leave?"
			, OnConfirm: function ()
			{
				viewContainer.close();
				viewContainer.destroy();
			}
		});
	},

	InitCombos: function ()
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
			switch(response.ListName)
			{
				case "ReminderDelayType":
					SOS.Utils.Lists.BindOptionsToSelectEl($("#frmCalEventReminder"), response.Value);
					break;
				case "ReminderMediaType":
					SOS.Utils.Lists.BindOptionsToSelectEl($("#frmCalEventMediaType"), response.Value);
					break;
			}
		}
		function onFailure(oMsgItem){ SOS.Framework.Popup.GeneralPopup(oMsgItem); }
		oSrv.on(SOS.ClientServices.CmsServices.EVNT_OPTITEMS_SUCCESS, onSuccess, this);
		oSrv.on(SOS.ClientServices.CmsServices.EVNT_OPTITEMS_FAILURE, onFailure, this);
		oSrv.OptionItemsGet("ReminderDelayType", this);
		oSrv.OptionItemsGet("ReminderMediaType", this);
	},

	statics: {
		StartModal : function (argObject)
		{
			/** Initialize. */
			var options = { data: argObject.data };
			var oStart = new SOS.Modals.MyCalendarEvent(options);

			/** Open wizard. */
			oStart.show();
		}
	}
});