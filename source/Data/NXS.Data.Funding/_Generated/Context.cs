


using System;
using SubSonic;
using SOS.Data;

namespace NXS.Data.Funding
{
	public partial class NxseFundingDataContext
	{
		#region Internal Instance

		private static NxseFundingDataContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();

		public static NxseFundingDataContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new NxseFundingDataContext();
						}
					}
				}
				return _contextInstance;
			}
		}

		#endregion // Internal Instance

		#region Controllers Properties

		FE_AccountFundingStatusController _FE_AccountFundingStatuses;
		public FE_AccountFundingStatusController FE_AccountFundingStatuses
		{
			get
			{
				if (_FE_AccountFundingStatuses == null) _FE_AccountFundingStatuses = new FE_AccountFundingStatusController();
				return _FE_AccountFundingStatuses;
			}
		}

		FE_AccountFundingStatusTypeController _FE_AccountFundingStatusTypes;
		public FE_AccountFundingStatusTypeController FE_AccountFundingStatusTypes
		{
			get
			{
				if (_FE_AccountFundingStatusTypes == null) _FE_AccountFundingStatusTypes = new FE_AccountFundingStatusTypeController();
				return _FE_AccountFundingStatusTypes;
			}
		}

		FE_BundleItemController _FE_BundleItems;
		public FE_BundleItemController FE_BundleItems
		{
			get
			{
				if (_FE_BundleItems == null) _FE_BundleItems = new FE_BundleItemController();
				return _FE_BundleItems;
			}
		}

		FE_BundleController _FE_Bundles;
		public FE_BundleController FE_Bundles
		{
			get
			{
				if (_FE_Bundles == null) _FE_Bundles = new FE_BundleController();
				return _FE_Bundles;
			}
		}

		FE_CriteriaItemController _FE_CriteriaItems;
		public FE_CriteriaItemController FE_CriteriaItems
		{
			get
			{
				if (_FE_CriteriaItems == null) _FE_CriteriaItems = new FE_CriteriaItemController();
				return _FE_CriteriaItems;
			}
		}

		FE_CriteriaController _FE_Criterias;
		public FE_CriteriaController FE_Criterias
		{
			get
			{
				if (_FE_Criterias == null) _FE_Criterias = new FE_CriteriaController();
				return _FE_Criterias;
			}
		}

		FE_FundingHistoryController _FE_FundingHistories;
		public FE_FundingHistoryController FE_FundingHistories
		{
			get
			{
				if (_FE_FundingHistories == null) _FE_FundingHistories = new FE_FundingHistoryController();
				return _FE_FundingHistories;
			}
		}

		FE_PacketItemController _FE_PacketItems;
		public FE_PacketItemController FE_PacketItems
		{
			get
			{
				if (_FE_PacketItems == null) _FE_PacketItems = new FE_PacketItemController();
				return _FE_PacketItems;
			}
		}

		FE_PacketController _FE_Packets;
		public FE_PacketController FE_Packets
		{
			get
			{
				if (_FE_Packets == null) _FE_Packets = new FE_PacketController();
				return _FE_Packets;
			}
		}

		FE_PurchaseContractMonitronicController _FE_PurchaseContractMonitronics;
		public FE_PurchaseContractMonitronicController FE_PurchaseContractMonitronics
		{
			get
			{
				if (_FE_PurchaseContractMonitronics == null) _FE_PurchaseContractMonitronics = new FE_PurchaseContractMonitronicController();
				return _FE_PurchaseContractMonitronics;
			}
		}

		FE_PurchaseContractController _FE_PurchaseContracts;
		public FE_PurchaseContractController FE_PurchaseContracts
		{
			get
			{
				if (_FE_PurchaseContracts == null) _FE_PurchaseContracts = new FE_PurchaseContractController();
				return _FE_PurchaseContracts;
			}
		}

		FE_PurchasedAccountController _FE_PurchasedAccounts;
		public FE_PurchasedAccountController FE_PurchasedAccounts
		{
			get
			{
				if (_FE_PurchasedAccounts == null) _FE_PurchasedAccounts = new FE_PurchasedAccountController();
				return _FE_PurchasedAccounts;
			}
		}

		FE_PurchasedAccountMonitronicController _FE_PurchasedAccountMonitronics;
		public FE_PurchasedAccountMonitronicController FE_PurchasedAccountMonitronics
		{
			get
			{
				if (_FE_PurchasedAccountMonitronics == null) _FE_PurchasedAccountMonitronics = new FE_PurchasedAccountMonitronicController();
				return _FE_PurchasedAccountMonitronics;
			}
		}

		FE_PurchaserController _FE_Purchasers;
		public FE_PurchaserController FE_Purchasers
		{
			get
			{
				if (_FE_Purchasers == null) _FE_Purchasers = new FE_PurchaserController();
				return _FE_Purchasers;
			}
		}

		FE_RejectedAccountController _FE_RejectedAccounts;
		public FE_RejectedAccountController FE_RejectedAccounts
		{
			get
			{
				if (_FE_RejectedAccounts == null) _FE_RejectedAccounts = new FE_RejectedAccountController();
				return _FE_RejectedAccounts;
			}
		}

		FE_RejectionController _FE_Rejections;
		public FE_RejectionController FE_Rejections
		{
			get
			{
				if (_FE_Rejections == null) _FE_Rejections = new FE_RejectionController();
				return _FE_Rejections;
			}
		}

		FE_RejectionTypeController _FE_RejectionTypes;
		public FE_RejectionTypeController FE_RejectionTypes
		{
			get
			{
				if (_FE_RejectionTypes == null) _FE_RejectionTypes = new FE_RejectionTypeController();
				return _FE_RejectionTypes;
			}
		}

		FE_ReplacedAccountController _FE_ReplacedAccounts;
		public FE_ReplacedAccountController FE_ReplacedAccounts
		{
			get
			{
				if (_FE_ReplacedAccounts == null) _FE_ReplacedAccounts = new FE_ReplacedAccountController();
				return _FE_ReplacedAccounts;
			}
		}

		FE_ReturnActionController _FE_ReturnActions;
		public FE_ReturnActionController FE_ReturnActions
		{
			get
			{
				if (_FE_ReturnActions == null) _FE_ReturnActions = new FE_ReturnActionController();
				return _FE_ReturnActions;
			}
		}

		FE_ReturnManifestMonitronicsDetailController _FE_ReturnManifestMonitronicsDetails;
		public FE_ReturnManifestMonitronicsDetailController FE_ReturnManifestMonitronicsDetails
		{
			get
			{
				if (_FE_ReturnManifestMonitronicsDetails == null) _FE_ReturnManifestMonitronicsDetails = new FE_ReturnManifestMonitronicsDetailController();
				return _FE_ReturnManifestMonitronicsDetails;
			}
		}

		FE_ReturnManifestController _FE_ReturnManifests;
		public FE_ReturnManifestController FE_ReturnManifests
		{
			get
			{
				if (_FE_ReturnManifests == null) _FE_ReturnManifests = new FE_ReturnManifestController();
				return _FE_ReturnManifests;
			}
		}

		FE_SubmittedToPurchaserAccountController _FE_SubmittedToPurchaserAccounts;
		public FE_SubmittedToPurchaserAccountController FE_SubmittedToPurchaserAccounts
		{
			get
			{
				if (_FE_SubmittedToPurchaserAccounts == null) _FE_SubmittedToPurchaserAccounts = new FE_SubmittedToPurchaserAccountController();
				return _FE_SubmittedToPurchaserAccounts;
			}
		}

		FE_TrackingNumberController _FE_TrackingNumbers;
		public FE_TrackingNumberController FE_TrackingNumbers
		{
			get
			{
				if (_FE_TrackingNumbers == null) _FE_TrackingNumbers = new FE_TrackingNumberController();
				return _FE_TrackingNumbers;
			}
		}

		#endregion //Controllers Properties

		#region View Controllers Properties

		FE_CriteriasViewController _FE_CriteriasViews;
		public FE_CriteriasViewController FE_CriteriasViews
		{
			get
			{
				if (_FE_CriteriasViews == null) _FE_CriteriasViews = new FE_CriteriasViewController();
				return _FE_CriteriasViews;
			}
		}

		FE_PacketItemsViewController _FE_PacketItemsViews;
		public FE_PacketItemsViewController FE_PacketItemsViews
		{
			get
			{
				if (_FE_PacketItemsViews == null) _FE_PacketItemsViews = new FE_PacketItemsViewController();
				return _FE_PacketItemsViews;
			}
		}

		FE_PacketsViewController _FE_PacketsViews;
		public FE_PacketsViewController FE_PacketsViews
		{
			get
			{
				if (_FE_PacketsViews == null) _FE_PacketsViews = new FE_PacketsViewController();
				return _FE_PacketsViews;
			}
		}

		#endregion //View Controllers Properties
	}

	#region Controllers

	public class FE_AccountFundingStatusController : BaseTableController<FE_AccountFundingStatus, FE_AccountFundingStatusCollection> { }
	public class FE_AccountFundingStatusTypeController : BaseTableController<FE_AccountFundingStatusType, FE_AccountFundingStatusTypeCollection> { }
	public class FE_BundleItemController : BaseTableController<FE_BundleItem, FE_BundleItemCollection> { }
	public class FE_BundleController : BaseTableController<FE_Bundle, FE_BundleCollection> { }
	public class FE_CriteriaItemController : BaseTableController<FE_CriteriaItem, FE_CriteriaItemCollection> { }
	public class FE_CriteriaController : BaseTableController<FE_Criteria, FE_CriteriaCollection> { }
	public class FE_FundingHistoryController : BaseTableController<FE_FundingHistory, FE_FundingHistoryCollection> { }
	public class FE_PacketItemController : BaseTableController<FE_PacketItem, FE_PacketItemCollection> { }
	public class FE_PacketController : BaseTableController<FE_Packet, FE_PacketCollection> { }
	public class FE_PurchaseContractMonitronicController : BaseTableController<FE_PurchaseContractMonitronic, FE_PurchaseContractMonitronicCollection> { }
	public class FE_PurchaseContractController : BaseTableController<FE_PurchaseContract, FE_PurchaseContractCollection> { }
	public class FE_PurchasedAccountController : BaseTableController<FE_PurchasedAccount, FE_PurchasedAccountCollection> { }
	public class FE_PurchasedAccountMonitronicController : BaseTableController<FE_PurchasedAccountMonitronic, FE_PurchasedAccountMonitronicCollection> { }
	public class FE_PurchaserController : BaseTableController<FE_Purchaser, FE_PurchaserCollection> { }
	public class FE_RejectedAccountController : BaseTableController<FE_RejectedAccount, FE_RejectedAccountCollection> { }
	public class FE_RejectionController : BaseTableController<FE_Rejection, FE_RejectionCollection> { }
	public class FE_RejectionTypeController : BaseTableController<FE_RejectionType, FE_RejectionTypeCollection> { }
	public class FE_ReplacedAccountController : BaseTableController<FE_ReplacedAccount, FE_ReplacedAccountCollection> { }
	public class FE_ReturnActionController : BaseTableController<FE_ReturnAction, FE_ReturnActionCollection> { }
	public class FE_ReturnManifestMonitronicsDetailController : BaseTableController<FE_ReturnManifestMonitronicsDetail, FE_ReturnManifestMonitronicsDetailCollection> { }
	public class FE_ReturnManifestController : BaseTableController<FE_ReturnManifest, FE_ReturnManifestCollection> { }
	public class FE_SubmittedToPurchaserAccountController : BaseTableController<FE_SubmittedToPurchaserAccount, FE_SubmittedToPurchaserAccountCollection> { }
	public class FE_TrackingNumberController : BaseTableController<FE_TrackingNumber, FE_TrackingNumberCollection> { }

	#endregion //Controllers

	#region View Controllers

	public class FE_CriteriasViewController : BaseViewController<FE_CriteriasView, FE_CriteriasViewCollection> { }
	public class FE_PacketItemsViewController : BaseViewController<FE_PacketItemsView, FE_PacketItemsViewCollection> { }
	public class FE_PacketsViewController : BaseViewController<FE_PacketsView, FE_PacketsViewCollection> { }

	#endregion //View Controllers
}
