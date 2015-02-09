


using System;
using SubSonic;
using SOS.Data;

namespace SOS.Data.GpsTracking
{
	public partial class GpsTrackingDataContext
	{
		#region Internal Instance

		private static GpsTrackingDataContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();

		public static GpsTrackingDataContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new GpsTrackingDataContext();
						}
					}
				}
				return _contextInstance;
			}
		}

		#endregion // Internal Instance

		#region Controllers Properties

		GS_AccountGeoFenceCircleController _GS_AccountGeoFenceCircles;
		public GS_AccountGeoFenceCircleController GS_AccountGeoFenceCircles
		{
			get
			{
				if (_GS_AccountGeoFenceCircles == null) _GS_AccountGeoFenceCircles = new GS_AccountGeoFenceCircleController();
				return _GS_AccountGeoFenceCircles;
			}
		}

		GS_AccountGeoFencePointController _GS_AccountGeoFencePoints;
		public GS_AccountGeoFencePointController GS_AccountGeoFencePoints
		{
			get
			{
				if (_GS_AccountGeoFencePoints == null) _GS_AccountGeoFencePoints = new GS_AccountGeoFencePointController();
				return _GS_AccountGeoFencePoints;
			}
		}

		GS_AccountGeoFencePolygonController _GS_AccountGeoFencePolygons;
		public GS_AccountGeoFencePolygonController GS_AccountGeoFencePolygons
		{
			get
			{
				if (_GS_AccountGeoFencePolygons == null) _GS_AccountGeoFencePolygons = new GS_AccountGeoFencePolygonController();
				return _GS_AccountGeoFencePolygons;
			}
		}

		GS_AccountGeoFenceRectangleController _GS_AccountGeoFenceRectangles;
		public GS_AccountGeoFenceRectangleController GS_AccountGeoFenceRectangles
		{
			get
			{
				if (_GS_AccountGeoFenceRectangles == null) _GS_AccountGeoFenceRectangles = new GS_AccountGeoFenceRectangleController();
				return _GS_AccountGeoFenceRectangles;
			}
		}

		GS_AccountGeoFenceReportModeController _GS_AccountGeoFenceReportModes;
		public GS_AccountGeoFenceReportModeController GS_AccountGeoFenceReportModes
		{
			get
			{
				if (_GS_AccountGeoFenceReportModes == null) _GS_AccountGeoFenceReportModes = new GS_AccountGeoFenceReportModeController();
				return _GS_AccountGeoFenceReportModes;
			}
		}

		GS_AccountGeoFenceController _GS_AccountGeoFences;
		public GS_AccountGeoFenceController GS_AccountGeoFences
		{
			get
			{
				if (_GS_AccountGeoFences == null) _GS_AccountGeoFences = new GS_AccountGeoFenceController();
				return _GS_AccountGeoFences;
			}
		}

		GS_AccountGeoFenceTypeController _GS_AccountGeoFenceTypes;
		public GS_AccountGeoFenceTypeController GS_AccountGeoFenceTypes
		{
			get
			{
				if (_GS_AccountGeoFenceTypes == null) _GS_AccountGeoFenceTypes = new GS_AccountGeoFenceTypeController();
				return _GS_AccountGeoFenceTypes;
			}
		}

		GS_DeviceTypeController _GS_DeviceTypes;
		public GS_DeviceTypeController GS_DeviceTypes
		{
			get
			{
				if (_GS_DeviceTypes == null) _GS_DeviceTypes = new GS_DeviceTypeController();
				return _GS_DeviceTypes;
			}
		}

		GS_EventController _GS_Events;
		public GS_EventController GS_Events
		{
			get
			{
				if (_GS_Events == null) _GS_Events = new GS_EventController();
				return _GS_Events;
			}
		}

		GS_EventTypeController _GS_EventTypes;
		public GS_EventTypeController GS_EventTypes
		{
			get
			{
				if (_GS_EventTypes == null) _GS_EventTypes = new GS_EventTypeController();
				return _GS_EventTypes;
			}
		}

		KW_CommandMessageController _KW_CommandMessages;
		public KW_CommandMessageController KW_CommandMessages
		{
			get
			{
				if (_KW_CommandMessages == null) _KW_CommandMessages = new KW_CommandMessageController();
				return _KW_CommandMessages;
			}
		}

		KW_DeviceController _KW_Devices;
		public KW_DeviceController KW_Devices
		{
			get
			{
				if (_KW_Devices == null) _KW_Devices = new KW_DeviceController();
				return _KW_Devices;
			}
		}

		KW_RequestController _KW_Requests;
		public KW_RequestController KW_Requests
		{
			get
			{
				if (_KW_Requests == null) _KW_Requests = new KW_RequestController();
				return _KW_Requests;
			}
		}

		LP_AVCFGCodeController _LP_AVCFGCodes;
		public LP_AVCFGCodeController LP_AVCFGCodes
		{
			get
			{
				if (_LP_AVCFGCodes == null) _LP_AVCFGCodes = new LP_AVCFGCodeController();
				return _LP_AVCFGCodes;
			}
		}

		LP_CommandMessageAVCFGFFController _LP_CommandMessageAVCFGFFs;
		public LP_CommandMessageAVCFGFFController LP_CommandMessageAVCFGFFs
		{
			get
			{
				if (_LP_CommandMessageAVCFGFFs == null) _LP_CommandMessageAVCFGFFs = new LP_CommandMessageAVCFGFFController();
				return _LP_CommandMessageAVCFGFFs;
			}
		}

		LP_CommandMessageAVRMCController _LP_CommandMessageAVRMCs;
		public LP_CommandMessageAVRMCController LP_CommandMessageAVRMCs
		{
			get
			{
				if (_LP_CommandMessageAVRMCs == null) _LP_CommandMessageAVRMCs = new LP_CommandMessageAVRMCController();
				return _LP_CommandMessageAVRMCs;
			}
		}

		LP_CommandMessageEAVACKController _LP_CommandMessageEAVACKs;
		public LP_CommandMessageEAVACKController LP_CommandMessageEAVACKs
		{
			get
			{
				if (_LP_CommandMessageEAVACKs == null) _LP_CommandMessageEAVACKs = new LP_CommandMessageEAVACKController();
				return _LP_CommandMessageEAVACKs;
			}
		}

		LP_CommandMessageEAVGOF3Controller _LP_CommandMessageEAVGOF3s;
		public LP_CommandMessageEAVGOF3Controller LP_CommandMessageEAVGOF3s
		{
			get
			{
				if (_LP_CommandMessageEAVGOF3s == null) _LP_CommandMessageEAVGOF3s = new LP_CommandMessageEAVGOF3Controller();
				return _LP_CommandMessageEAVGOF3s;
			}
		}

		LP_CommandMessageEAVGOF6Controller _LP_CommandMessageEAVGOF6s;
		public LP_CommandMessageEAVGOF6Controller LP_CommandMessageEAVGOF6s
		{
			get
			{
				if (_LP_CommandMessageEAVGOF6s == null) _LP_CommandMessageEAVGOF6s = new LP_CommandMessageEAVGOF6Controller();
				return _LP_CommandMessageEAVGOF6s;
			}
		}

		LP_CommandMessageEAVRSP3Controller _LP_CommandMessageEAVRSP3s;
		public LP_CommandMessageEAVRSP3Controller LP_CommandMessageEAVRSP3s
		{
			get
			{
				if (_LP_CommandMessageEAVRSP3s == null) _LP_CommandMessageEAVRSP3s = new LP_CommandMessageEAVRSP3Controller();
				return _LP_CommandMessageEAVRSP3s;
			}
		}

		LP_CommandMessageEAVRSP4Controller _LP_CommandMessageEAVRSP4s;
		public LP_CommandMessageEAVRSP4Controller LP_CommandMessageEAVRSP4s
		{
			get
			{
				if (_LP_CommandMessageEAVRSP4s == null) _LP_CommandMessageEAVRSP4s = new LP_CommandMessageEAVRSP4Controller();
				return _LP_CommandMessageEAVRSP4s;
			}
		}

		LP_CommandMessageECHKController _LP_CommandMessageECHKs;
		public LP_CommandMessageECHKController LP_CommandMessageECHKs
		{
			get
			{
				if (_LP_CommandMessageECHKs == null) _LP_CommandMessageECHKs = new LP_CommandMessageECHKController();
				return _LP_CommandMessageECHKs;
			}
		}

		LP_CommandMessageController _LP_CommandMessages;
		public LP_CommandMessageController LP_CommandMessages
		{
			get
			{
				if (_LP_CommandMessages == null) _LP_CommandMessages = new LP_CommandMessageController();
				return _LP_CommandMessages;
			}
		}

		LP_CommandNameController _LP_CommandNames;
		public LP_CommandNameController LP_CommandNames
		{
			get
			{
				if (_LP_CommandNames == null) _LP_CommandNames = new LP_CommandNameController();
				return _LP_CommandNames;
			}
		}

		LP_CommandTypeController _LP_CommandTypes;
		public LP_CommandTypeController LP_CommandTypes
		{
			get
			{
				if (_LP_CommandTypes == null) _LP_CommandTypes = new LP_CommandTypeController();
				return _LP_CommandTypes;
			}
		}

		LP_DeviceController _LP_Devices;
		public LP_DeviceController LP_Devices
		{
			get
			{
				if (_LP_Devices == null) _LP_Devices = new LP_DeviceController();
				return _LP_Devices;
			}
		}

		LP_DeviceStatusController _LP_DeviceStatuses;
		public LP_DeviceStatusController LP_DeviceStatuses
		{
			get
			{
				if (_LP_DeviceStatuses == null) _LP_DeviceStatuses = new LP_DeviceStatusController();
				return _LP_DeviceStatuses;
			}
		}

		LP_EventCodeController _LP_EventCodes;
		public LP_EventCodeController LP_EventCodes
		{
			get
			{
				if (_LP_EventCodes == null) _LP_EventCodes = new LP_EventCodeController();
				return _LP_EventCodes;
			}
		}

		LP_GsGeoFenceController _LP_GsGeoFences;
		public LP_GsGeoFenceController LP_GsGeoFences
		{
			get
			{
				if (_LP_GsGeoFences == null) _LP_GsGeoFences = new LP_GsGeoFenceController();
				return _LP_GsGeoFences;
			}
		}

		LP_RequestNameController _LP_RequestNames;
		public LP_RequestNameController LP_RequestNames
		{
			get
			{
				if (_LP_RequestNames == null) _LP_RequestNames = new LP_RequestNameController();
				return _LP_RequestNames;
			}
		}

		LP_RequestController _LP_Requests;
		public LP_RequestController LP_Requests
		{
			get
			{
				if (_LP_Requests == null) _LP_Requests = new LP_RequestController();
				return _LP_Requests;
			}
		}

		SS_CommandMessageNameController _SS_CommandMessageNames;
		public SS_CommandMessageNameController SS_CommandMessageNames
		{
			get
			{
				if (_SS_CommandMessageNames == null) _SS_CommandMessageNames = new SS_CommandMessageNameController();
				return _SS_CommandMessageNames;
			}
		}

		SS_CommandMessageController _SS_CommandMessages;
		public SS_CommandMessageController SS_CommandMessages
		{
			get
			{
				if (_SS_CommandMessages == null) _SS_CommandMessages = new SS_CommandMessageController();
				return _SS_CommandMessages;
			}
		}

		SS_CommandMessageTypeController _SS_CommandMessageTypes;
		public SS_CommandMessageTypeController SS_CommandMessageTypes
		{
			get
			{
				if (_SS_CommandMessageTypes == null) _SS_CommandMessageTypes = new SS_CommandMessageTypeController();
				return _SS_CommandMessageTypes;
			}
		}

		SS_DeviceRequestController _SS_DeviceRequests;
		public SS_DeviceRequestController SS_DeviceRequests
		{
			get
			{
				if (_SS_DeviceRequests == null) _SS_DeviceRequests = new SS_DeviceRequestController();
				return _SS_DeviceRequests;
			}
		}

		SS_DeviceController _SS_Devices;
		public SS_DeviceController SS_Devices
		{
			get
			{
				if (_SS_Devices == null) _SS_Devices = new SS_DeviceController();
				return _SS_Devices;
			}
		}

		#endregion //Controllers Properties

		#region View Controllers Properties

		GS_AccountGeoFencePointsViewController _GS_AccountGeoFencePointsViews;
		public GS_AccountGeoFencePointsViewController GS_AccountGeoFencePointsViews
		{
			get
			{
				if (_GS_AccountGeoFencePointsViews == null) _GS_AccountGeoFencePointsViews = new GS_AccountGeoFencePointsViewController();
				return _GS_AccountGeoFencePointsViews;
			}
		}

		GS_AccountGeoFencesViewController _GS_AccountGeoFencesViews;
		public GS_AccountGeoFencesViewController GS_AccountGeoFencesViews
		{
			get
			{
				if (_GS_AccountGeoFencesViews == null) _GS_AccountGeoFencesViews = new GS_AccountGeoFencesViewController();
				return _GS_AccountGeoFencesViews;
			}
		}

		GS_EventsViewController _GS_EventsViews;
		public GS_EventsViewController GS_EventsViews
		{
			get
			{
				if (_GS_EventsViews == null) _GS_EventsViews = new GS_EventsViewController();
				return _GS_EventsViews;
			}
		}

		GS_EventsLastViewController _GS_EventsLastViews;
		public GS_EventsLastViewController GS_EventsLastViews
		{
			get
			{
				if (_GS_EventsLastViews == null) _GS_EventsLastViews = new GS_EventsLastViewController();
				return _GS_EventsLastViews;
			}
		}

		GS_EventTypesViewController _GS_EventTypesViews;
		public GS_EventTypesViewController GS_EventTypesViews
		{
			get
			{
				if (_GS_EventTypesViews == null) _GS_EventTypesViews = new GS_EventTypesViewController();
				return _GS_EventTypesViews;
			}
		}

		LP_CommandMessageAVRMCsViewController _LP_CommandMessageAVRMCsViews;
		public LP_CommandMessageAVRMCsViewController LP_CommandMessageAVRMCsViews
		{
			get
			{
				if (_LP_CommandMessageAVRMCsViews == null) _LP_CommandMessageAVRMCsViews = new LP_CommandMessageAVRMCsViewController();
				return _LP_CommandMessageAVRMCsViews;
			}
		}

		LP_CommandMessageEAVRSP4sViewController _LP_CommandMessageEAVRSP4sViews;
		public LP_CommandMessageEAVRSP4sViewController LP_CommandMessageEAVRSP4sViews
		{
			get
			{
				if (_LP_CommandMessageEAVRSP4sViews == null) _LP_CommandMessageEAVRSP4sViews = new LP_CommandMessageEAVRSP4sViewController();
				return _LP_CommandMessageEAVRSP4sViews;
			}
		}

		#endregion //View Controllers Properties
	}

	#region Controllers

	public class GS_AccountGeoFenceCircleController : BaseTableController<GS_AccountGeoFenceCircle, GS_AccountGeoFenceCircleCollection> { }
	public class GS_AccountGeoFencePointController : BaseTableController<GS_AccountGeoFencePoint, GS_AccountGeoFencePointCollection> { }
	public class GS_AccountGeoFencePolygonController : BaseTableController<GS_AccountGeoFencePolygon, GS_AccountGeoFencePolygonCollection> { }
	public class GS_AccountGeoFenceRectangleController : BaseTableController<GS_AccountGeoFenceRectangle, GS_AccountGeoFenceRectangleCollection> { }
	public class GS_AccountGeoFenceReportModeController : BaseTableController<GS_AccountGeoFenceReportMode, GS_AccountGeoFenceReportModeCollection> { }
	public class GS_AccountGeoFenceController : BaseTableController<GS_AccountGeoFence, GS_AccountGeoFenceCollection> { }
	public class GS_AccountGeoFenceTypeController : BaseTableController<GS_AccountGeoFenceType, GS_AccountGeoFenceTypeCollection> { }
	public class GS_DeviceTypeController : BaseTableController<GS_DeviceType, GS_DeviceTypeCollection> { }
	public class GS_EventController : BaseTableController<GS_Event, GS_EventCollection> { }
	public class GS_EventTypeController : BaseTableController<GS_EventType, GS_EventTypeCollection> { }
	public class KW_CommandMessageController : BaseTableController<KW_CommandMessage, KW_CommandMessageCollection> { }
	public class KW_DeviceController : BaseTableController<KW_Device, KW_DeviceCollection> { }
	public class KW_RequestController : BaseTableController<KW_Request, KW_RequestCollection> { }
	public class LP_AVCFGCodeController : BaseTableController<LP_AVCFGCode, LP_AVCFGCodeCollection> { }
	public class LP_CommandMessageAVCFGFFController : BaseTableController<LP_CommandMessageAVCFGFF, LP_CommandMessageAVCFGFFCollection> { }
	public class LP_CommandMessageAVRMCController : BaseTableController<LP_CommandMessageAVRMC, LP_CommandMessageAVRMCCollection> { }
	public class LP_CommandMessageEAVACKController : BaseTableController<LP_CommandMessageEAVACK, LP_CommandMessageEAVACKCollection> { }
	public class LP_CommandMessageEAVGOF3Controller : BaseTableController<LP_CommandMessageEAVGOF3, LP_CommandMessageEAVGOF3Collection> { }
	public class LP_CommandMessageEAVGOF6Controller : BaseTableController<LP_CommandMessageEAVGOF6, LP_CommandMessageEAVGOF6Collection> { }
	public class LP_CommandMessageEAVRSP3Controller : BaseTableController<LP_CommandMessageEAVRSP3, LP_CommandMessageEAVRSP3Collection> { }
	public class LP_CommandMessageEAVRSP4Controller : BaseTableController<LP_CommandMessageEAVRSP4, LP_CommandMessageEAVRSP4Collection> { }
	public class LP_CommandMessageECHKController : BaseTableController<LP_CommandMessageECHK, LP_CommandMessageECHKCollection> { }
	public class LP_CommandMessageController : BaseTableController<LP_CommandMessage, LP_CommandMessageCollection> { }
	public class LP_CommandNameController : BaseTableController<LP_CommandName, LP_CommandNameCollection> { }
	public class LP_CommandTypeController : BaseTableController<LP_CommandType, LP_CommandTypeCollection> { }
	public class LP_DeviceController : BaseTableController<LP_Device, LP_DeviceCollection> { }
	public class LP_DeviceStatusController : BaseTableController<LP_DeviceStatus, LP_DeviceStatusCollection> { }
	public class LP_EventCodeController : BaseTableController<LP_EventCode, LP_EventCodeCollection> { }
	public class LP_GsGeoFenceController : BaseTableController<LP_GsGeoFence, LP_GsGeoFenceCollection> { }
	public class LP_RequestNameController : BaseTableController<LP_RequestName, LP_RequestNameCollection> { }
	public class LP_RequestController : BaseTableController<LP_Request, LP_RequestCollection> { }
	public class SS_CommandMessageNameController : BaseTableController<SS_CommandMessageName, SS_CommandMessageNameCollection> { }
	public class SS_CommandMessageController : BaseTableController<SS_CommandMessage, SS_CommandMessageCollection> { }
	public class SS_CommandMessageTypeController : BaseTableController<SS_CommandMessageType, SS_CommandMessageTypeCollection> { }
	public class SS_DeviceRequestController : BaseTableController<SS_DeviceRequest, SS_DeviceRequestCollection> { }
	public class SS_DeviceController : BaseTableController<SS_Device, SS_DeviceCollection> { }

	#endregion //Controllers

	#region View Controllers

	public class GS_AccountGeoFencePointsViewController : BaseViewController<GS_AccountGeoFencePointsView, GS_AccountGeoFencePointsViewCollection> { }
	public class GS_AccountGeoFencesViewController : BaseViewController<GS_AccountGeoFencesView, GS_AccountGeoFencesViewCollection> { }
	public class GS_EventsViewController : BaseViewController<GS_EventsView, GS_EventsViewCollection> { }
	public class GS_EventsLastViewController : BaseViewController<GS_EventsLastView, GS_EventsLastViewCollection> { }
	public class GS_EventTypesViewController : BaseViewController<GS_EventTypesView, GS_EventTypesViewCollection> { }
	public class LP_CommandMessageAVRMCsViewController : BaseViewController<LP_CommandMessageAVRMCsView, LP_CommandMessageAVRMCsViewCollection> { }
	public class LP_CommandMessageEAVRSP4sViewController : BaseViewController<LP_CommandMessageEAVRSP4sView, LP_CommandMessageEAVRSP4sViewCollection> { }

	#endregion //View Controllers
}
