


using System;
using SubSonic;
using SOS.Data;

namespace NXS.Data.Connext
{
	public partial class NxseConnextDataContext
	{
		#region Internal Instance

		private static NxseConnextDataContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();

		public static NxseConnextDataContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new NxseConnextDataContext();
						}
					}
				}
				return _contextInstance;
			}
		}

		#endregion // Internal Instance

		#region Controllers Properties

		CX_AddressController _CX_Addresses;
		public CX_AddressController CX_Addresses
		{
			get
			{
				if (_CX_Addresses == null) _CX_Addresses = new CX_AddressController();
				return _CX_Addresses;
			}
		}

		CX_AppointmentController _CX_Appointments;
		public CX_AppointmentController CX_Appointments
		{
			get
			{
				if (_CX_Appointments == null) _CX_Appointments = new CX_AppointmentController();
				return _CX_Appointments;
			}
		}

		CX_ContactNoteController _CX_ContactNotes;
		public CX_ContactNoteController CX_ContactNotes
		{
			get
			{
				if (_CX_ContactNotes == null) _CX_ContactNotes = new CX_ContactNoteController();
				return _CX_ContactNotes;
			}
		}

		CX_ContactController _CX_Contacts;
		public CX_ContactController CX_Contacts
		{
			get
			{
				if (_CX_Contacts == null) _CX_Contacts = new CX_ContactController();
				return _CX_Contacts;
			}
		}

		CX_ContactTypeController _CX_ContactTypes;
		public CX_ContactTypeController CX_ContactTypes
		{
			get
			{
				if (_CX_ContactTypes == null) _CX_ContactTypes = new CX_ContactTypeController();
				return _CX_ContactTypes;
			}
		}

		#endregion //Controllers Properties

		#region View Controllers Properties

		#endregion //View Controllers Properties
	}

	#region Controllers

	public class CX_AddressController : BaseTableController<CX_Address, CX_AddressCollection> { }
	public class CX_AppointmentController : BaseTableController<CX_Appointment, CX_AppointmentCollection> { }
	public class CX_ContactNoteController : BaseTableController<CX_ContactNote, CX_ContactNoteCollection> { }
	public class CX_ContactController : BaseTableController<CX_Contact, CX_ContactCollection> { }
	public class CX_ContactTypeController : BaseTableController<CX_ContactType, CX_ContactTypeCollection> { }

	#endregion //Controllers

	#region View Controllers


	#endregion //View Controllers
}
