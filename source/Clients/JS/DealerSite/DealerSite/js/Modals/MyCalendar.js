/**
 * Created with JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 6/2/12
 * Time: 4:08 PM
 * To change this template use File | Settings | File Templates.
 */

Ext.ns("SOS.Modals");

Ext.define("SOS.Modals.MyCalendar",
{
	extend: "Ext.window.Window",
	alias: "widget.myCalendar",

	constructor: function (options)
	{
		/** Initialize. */
		var me = this;
		options = options || {};
		var tplCalendar = SOS.UI.Services.GetTemplate('MyCalendar.html');

		/** Build Configuration options for the window. */
		var config = Ext.merge(options,
			{
				id: "myCalendarModal",
				title: '<img src="/img/DragTab-Yellow.png" alt="Click here to move the window" />',
				tpl: tplCalendar,
				data: {
					LeadId: options.CompoundLeadId.LeadId
					, CustomerMasterFileId: options.CompoundLeadId.CustomerMasterFileId},
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
		$("#myCalendar-dialog .btn-close-x").bind("click", me.CloseModal);

		var date = new Date();
		var d = date.getDate();
		var m = date.getMonth();
		var y = date.getFullYear();

		var calendar = $('#myCalendar').fullCalendar({
			header: {
				left: 'prev,next today',
				center: 'title',
				right: 'month,agendaWeek,agendaDay'
			},
			selectable: true,
			selectHelper: true,
			select: function(start, end, allDay) {
				var title = prompt('Event Title:');
				if (title) {
					calendar.fullCalendar('renderEvent',
						{
							title: title,
							start: start,
							end: end,
							allDay: allDay
						},
						true // make the event "stick"
					);
				}
				calendar.fullCalendar('unselect');
			},
			editable: true,
			eventClick: function(calEvent, jsEvent, view)
			{
				debugger;
				var oData = {
					EventID: calEvent.id
					, EventType: "Lead Followup Appt"
					, EventTitle: calEvent.title
					, Description: "This is the long description of the event"
					, EventCustInfo: "<span>Andres Sosa</span>"
					, StartTime: calEvent.start
					, EndTime: calEvent.end
					, URL: calEvent.url
					, IsAllDay: calEvent.allDay
				};

				SOS.Modals.MyCalendarEvent.StartModal({ data: oData });
//				alert('Event: ' + calEvent.title);
//				alert('Coordinates: ' + jsEvent.pageX + ',' + jsEvent.pageY);
//				alert('View: ' + view.name);

				// change the border color just for fun
				$(this).css('border-color', 'red');

			},
			eventDragStart: function (event, jsEvent, ui, view)
			{
				console.log("Start:", event, jsEvent, ui, view);
			},
			eventDragStop: function (event, jsEvent, ui, view)
			{
				console.log("Stop:", event, jsEvent, ui, view);
			},
			eventSources: [{
				url: SOSConfig.API_BASE_CMS_SERVICE + "/DealerUserAppointmentsGet"
				, color: 'yellow'
				, textColor: 'black'
			}]
//			, events: [
//				{
//					title: 'All Day Event',
//					start: new Date(y, m, 1)
//				},
//				{
//					title: 'Long Event',
//					start: new Date(y, m, d-5),
//					end: new Date(y, m, d-2)
//				},
//				{
//					id: 999,
//					title: 'Repeating Event',
//					start: new Date(y, m, d-3, 16, 0),
//					allDay: false
//				},
//				{
//					id: 999,
//					title: 'Repeating Event',
//					start: new Date(y, m, d+4, 16, 0),
//					allDay: false
//				},
//				{
//					title: 'Meeting',
//					start: new Date(y, m, d, 10, 30),
//					allDay: false
//				},
//				{
//					title: 'Lunch',
//					start: new Date(y, m, d, 12, 0),
//					end: new Date(y, m, d, 14, 0),
//					allDay: false
//				},
//				{
//					title: 'Birthday Party',
//					start: new Date(y, m, d+1, 19, 0),
//					end: new Date(y, m, d+1, 22, 30),
//					allDay: false
//				},
//				{
//					title: 'Click for Google',
//					start: new Date(y, m, 28),
//					end: new Date(y, m, 29),
//					url: 'http://google.com/'
//				}
//			]
		});
	},
	CloseModal: function ()
	{
		/** Initialize. */
		var viewContainer = Ext.getCmp("myCalendarModal");
		SOS.Framework.Popup.Confirm({
			Title: "Do you want to leave?"
			, MessageBody: "You are about to close the your calendar. &nbsp;Are you sure you want to leave?"
			, OnConfirm: function ()
			{
				viewContainer.close();
				viewContainer.destroy();
			}
		});
	},

	statics: {
		StartModal : function (argObject)
		{
			/** Initialize. */
			var oLeadId = SOS.Utils.Strings.getLeadIdObject(argObject.data.id);
			var options = { CompoundLeadId: oLeadId };
			var oStart = new SOS.Modals.MyCalendar(options);

			/** Open wizard. */
			oStart.show();
		}
	}
});