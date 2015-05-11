


using System;
using SubSonic;
using SOS.Data;

namespace NXS.Data.Licensing
{
	public partial class LicensingDataContext
	{
		#region Internal Instance
		
		private static LicensingDataContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();
		
		public static LicensingDataContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new LicensingDataContext();
						}
					}
				}
				return _contextInstance;
			}
		}
		
		#endregion // Internal Instance
		
		#region Controllers Properties

		LM_AgencyController _LM_Agencies;
		public LM_AgencyController LM_Agencies
		{
			get
			{
				if (_LM_Agencies == null) _LM_Agencies = new LM_AgencyController();
				return _LM_Agencies;
			}
		}

		LM_AttachmentController _LM_Attachments;
		public LM_AttachmentController LM_Attachments
		{
			get
			{
				if (_LM_Attachments == null) _LM_Attachments = new LM_AttachmentController();
				return _LM_Attachments;
			}
		}

		LM_AttachmentTypeController _LM_AttachmentTypes;
		public LM_AttachmentTypeController LM_AttachmentTypes
		{
			get
			{
				if (_LM_AttachmentTypes == null) _LM_AttachmentTypes = new LM_AttachmentTypeController();
				return _LM_AttachmentTypes;
			}
		}

		LM_LicenseItemController _LM_LicenseItems;
		public LM_LicenseItemController LM_LicenseItems
		{
			get
			{
				if (_LM_LicenseItems == null) _LM_LicenseItems = new LM_LicenseItemController();
				return _LM_LicenseItems;
			}
		}

		LM_LicenseController _LM_Licenses;
		public LM_LicenseController LM_Licenses
		{
			get
			{
				if (_LM_Licenses == null) _LM_Licenses = new LM_LicenseController();
				return _LM_Licenses;
			}
		}

		LM_LicenseStatusController _LM_LicenseStatuses;
		public LM_LicenseStatusController LM_LicenseStatuses
		{
			get
			{
				if (_LM_LicenseStatuses == null) _LM_LicenseStatuses = new LM_LicenseStatusController();
				return _LM_LicenseStatuses;
			}
		}

		LM_LocationController _LM_Locations;
		public LM_LocationController LM_Locations
		{
			get
			{
				if (_LM_Locations == null) _LM_Locations = new LM_LocationController();
				return _LM_Locations;
			}
		}

		LM_LocationTypeController _LM_LocationTypes;
		public LM_LocationTypeController LM_LocationTypes
		{
			get
			{
				if (_LM_LocationTypes == null) _LM_LocationTypes = new LM_LocationTypeController();
				return _LM_LocationTypes;
			}
		}

		LM_LockController _LM_Locks;
		public LM_LockController LM_Locks
		{
			get
			{
				if (_LM_Locks == null) _LM_Locks = new LM_LockController();
				return _LM_Locks;
			}
		}

		LM_MessageTokenController _LM_MessageTokens;
		public LM_MessageTokenController LM_MessageTokens
		{
			get
			{
				if (_LM_MessageTokens == null) _LM_MessageTokens = new LM_MessageTokenController();
				return _LM_MessageTokens;
			}
		}

		LM_NoteController _LM_Notes;
		public LM_NoteController LM_Notes
		{
			get
			{
				if (_LM_Notes == null) _LM_Notes = new LM_NoteController();
				return _LM_Notes;
			}
		}

		LM_NoteTypeController _LM_NoteTypes;
		public LM_NoteTypeController LM_NoteTypes
		{
			get
			{
				if (_LM_NoteTypes == null) _LM_NoteTypes = new LM_NoteTypeController();
				return _LM_NoteTypes;
			}
		}

		LM_RequirementItemController _LM_RequirementItems;
		public LM_RequirementItemController LM_RequirementItems
		{
			get
			{
				if (_LM_RequirementItems == null) _LM_RequirementItems = new LM_RequirementItemController();
				return _LM_RequirementItems;
			}
		}

		LM_RequirementController _LM_Requirements;
		public LM_RequirementController LM_Requirements
		{
			get
			{
				if (_LM_Requirements == null) _LM_Requirements = new LM_RequirementController();
				return _LM_Requirements;
			}
		}

		LM_RequirementTypeController _LM_RequirementTypes;
		public LM_RequirementTypeController LM_RequirementTypes
		{
			get
			{
				if (_LM_RequirementTypes == null) _LM_RequirementTypes = new LM_RequirementTypeController();
				return _LM_RequirementTypes;
			}
		}

		SAE_LicenseIndexController _SAE_LicenseIndices;
		public SAE_LicenseIndexController SAE_LicenseIndices
		{
			get
			{
				if (_SAE_LicenseIndices == null) _SAE_LicenseIndices = new SAE_LicenseIndexController();
				return _SAE_LicenseIndices;
			}
		}

		#endregion //Controllers Properties
		
		#region View Controllers Properties

		AllRequirementsPerLocationViewController _AllRequirementsPerLocationViews;
		public AllRequirementsPerLocationViewController AllRequirementsPerLocationViews
		{
			get
			{
				if (_AllRequirementsPerLocationViews == null) _AllRequirementsPerLocationViews = new AllRequirementsPerLocationViewController();
				return _AllRequirementsPerLocationViews;
			}
		}

		LicenseLocationsViewController _LicenseLocationsViews;
		public LicenseLocationsViewController LicenseLocationsViews
		{
			get
			{
				if (_LicenseLocationsViews == null) _LicenseLocationsViews = new LicenseLocationsViewController();
				return _LicenseLocationsViews;
			}
		}

		LM_RequirementsViewController _LM_RequirementsViews;
		public LM_RequirementsViewController LM_RequirementsViews
		{
			get
			{
				if (_LM_RequirementsViews == null) _LM_RequirementsViews = new LM_RequirementsViewController();
				return _LM_RequirementsViews;
			}
		}

		LM_SalesRepRequirementsViewController _LM_SalesRepRequirementsViews;
		public LM_SalesRepRequirementsViewController LM_SalesRepRequirementsViews
		{
			get
			{
				if (_LM_SalesRepRequirementsViews == null) _LM_SalesRepRequirementsViews = new LM_SalesRepRequirementsViewController();
				return _LM_SalesRepRequirementsViews;
			}
		}

		RequirementLocationsViewController _RequirementLocationsViews;
		public RequirementLocationsViewController RequirementLocationsViews
		{
			get
			{
				if (_RequirementLocationsViews == null) _RequirementLocationsViews = new RequirementLocationsViewController();
				return _RequirementLocationsViews;
			}
		}

		#endregion //View Controllers Properties
	}
	
	#region Controllers
	
	public class LM_AgencyController : BaseTableController<LM_Agency, LM_AgencyCollection> { }
	public class LM_AttachmentController : BaseTableController<LM_Attachment, LM_AttachmentCollection> { }
	public class LM_AttachmentTypeController : BaseTableController<LM_AttachmentType, LM_AttachmentTypeCollection> { }
	public class LM_LicenseItemController : BaseTableController<LM_LicenseItem, LM_LicenseItemCollection> { }
	public class LM_LicenseController : BaseTableController<LM_License, LM_LicenseCollection> { }
	public class LM_LicenseStatusController : BaseTableController<LM_LicenseStatus, LM_LicenseStatusCollection> { }
	public class LM_LocationController : BaseTableController<LM_Location, LM_LocationCollection> { }
	public class LM_LocationTypeController : BaseTableController<LM_LocationType, LM_LocationTypeCollection> { }
	public class LM_LockController : BaseTableController<LM_Lock, LM_LockCollection> { }
	public class LM_MessageTokenController : BaseTableController<LM_MessageToken, LM_MessageTokenCollection> { }
	public class LM_NoteController : BaseTableController<LM_Note, LM_NoteCollection> { }
	public class LM_NoteTypeController : BaseTableController<LM_NoteType, LM_NoteTypeCollection> { }
	public class LM_RequirementItemController : BaseTableController<LM_RequirementItem, LM_RequirementItemCollection> { }
	public class LM_RequirementController : BaseTableController<LM_Requirement, LM_RequirementCollection> { }
	public class LM_RequirementTypeController : BaseTableController<LM_RequirementType, LM_RequirementTypeCollection> { }
	public class SAE_LicenseIndexController : BaseTableController<SAE_LicenseIndex, SAE_LicenseIndexCollection> { }

	#endregion //Controllers
	
	#region View Controllers
	
	public class AllRequirementsPerLocationViewController : BaseViewController<AllRequirementsPerLocationView, AllRequirementsPerLocationViewCollection> { }
	public class LicenseLocationsViewController : BaseViewController<LicenseLocationsView, LicenseLocationsViewCollection> { }
	public class LM_RequirementsViewController : BaseViewController<LM_RequirementsView, LM_RequirementsViewCollection> { }
	public class LM_SalesRepRequirementsViewController : BaseViewController<LM_SalesRepRequirementsView, LM_SalesRepRequirementsViewCollection> { }
	public class RequirementLocationsViewController : BaseViewController<RequirementLocationsView, RequirementLocationsViewCollection> { }

	#endregion //View Controllers
}
