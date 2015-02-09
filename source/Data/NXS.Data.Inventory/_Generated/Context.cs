


using System;
using SubSonic;
using SOS.Data;

namespace NXS.Data.Inventory
{
	public partial class InventoryDataContext
	{
		#region Internal Instance
		
		private static InventoryDataContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();
		
		public static InventoryDataContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new InventoryDataContext();
						}
					}
				}
				return _contextInstance;
			}
		}
		
		#endregion // Internal Instance
		
		#region Controllers Properties

		EP_DeviceController _EP_Devices;
		public EP_DeviceController EP_Devices
		{
			get
			{
				if (_EP_Devices == null) _EP_Devices = new EP_DeviceController();
				return _EP_Devices;
			}
		}

		EP_EquipmentItemTypeController _EP_EquipmentItemTypes;
		public EP_EquipmentItemTypeController EP_EquipmentItemTypes
		{
			get
			{
				if (_EP_EquipmentItemTypes == null) _EP_EquipmentItemTypes = new EP_EquipmentItemTypeController();
				return _EP_EquipmentItemTypes;
			}
		}

		EP_EquipmentManufacturerController _EP_EquipmentManufacturers;
		public EP_EquipmentManufacturerController EP_EquipmentManufacturers
		{
			get
			{
				if (_EP_EquipmentManufacturers == null) _EP_EquipmentManufacturers = new EP_EquipmentManufacturerController();
				return _EP_EquipmentManufacturers;
			}
		}

		EP_EquipmentController _EP_Equipments;
		public EP_EquipmentController EP_Equipments
		{
			get
			{
				if (_EP_Equipments == null) _EP_Equipments = new EP_EquipmentController();
				return _EP_Equipments;
			}
		}

		EP_EquipmentTypeController _EP_EquipmentTypes;
		public EP_EquipmentTypeController EP_EquipmentTypes
		{
			get
			{
				if (_EP_EquipmentTypes == null) _EP_EquipmentTypes = new EP_EquipmentTypeController();
				return _EP_EquipmentTypes;
			}
		}

		IV_AssetController _IV_Assets;
		public IV_AssetController IV_Assets
		{
			get
			{
				if (_IV_Assets == null) _IV_Assets = new IV_AssetController();
				return _IV_Assets;
			}
		}

		IV_AssetTagController _IV_AssetTags;
		public IV_AssetTagController IV_AssetTags
		{
			get
			{
				if (_IV_AssetTags == null) _IV_AssetTags = new IV_AssetTagController();
				return _IV_AssetTags;
			}
		}

		IV_AssetTrackingController _IV_AssetTrackings;
		public IV_AssetTrackingController IV_AssetTrackings
		{
			get
			{
				if (_IV_AssetTrackings == null) _IV_AssetTrackings = new IV_AssetTrackingController();
				return _IV_AssetTrackings;
			}
		}

		IV_AssetTrackingTypeController _IV_AssetTrackingTypes;
		public IV_AssetTrackingTypeController IV_AssetTrackingTypes
		{
			get
			{
				if (_IV_AssetTrackingTypes == null) _IV_AssetTrackingTypes = new IV_AssetTrackingTypeController();
				return _IV_AssetTrackingTypes;
			}
		}

		IV_OfficeAuditController _IV_OfficeAudits;
		public IV_OfficeAuditController IV_OfficeAudits
		{
			get
			{
				if (_IV_OfficeAudits == null) _IV_OfficeAudits = new IV_OfficeAuditController();
				return _IV_OfficeAudits;
			}
		}

		IV_OfficeController _IV_Offices;
		public IV_OfficeController IV_Offices
		{
			get
			{
				if (_IV_Offices == null) _IV_Offices = new IV_OfficeController();
				return _IV_Offices;
			}
		}

		IV_OfficeSafetyStockController _IV_OfficeSafetyStocks;
		public IV_OfficeSafetyStockController IV_OfficeSafetyStocks
		{
			get
			{
				if (_IV_OfficeSafetyStocks == null) _IV_OfficeSafetyStocks = new IV_OfficeSafetyStockController();
				return _IV_OfficeSafetyStocks;
			}
		}

		IV_OfficeTransferController _IV_OfficeTransfers;
		public IV_OfficeTransferController IV_OfficeTransfers
		{
			get
			{
				if (_IV_OfficeTransfers == null) _IV_OfficeTransfers = new IV_OfficeTransferController();
				return _IV_OfficeTransfers;
			}
		}

		IV_PurchaseOrderItemController _IV_PurchaseOrderItems;
		public IV_PurchaseOrderItemController IV_PurchaseOrderItems
		{
			get
			{
				if (_IV_PurchaseOrderItems == null) _IV_PurchaseOrderItems = new IV_PurchaseOrderItemController();
				return _IV_PurchaseOrderItems;
			}
		}

		IV_PurchaseOrderController _IV_PurchaseOrders;
		public IV_PurchaseOrderController IV_PurchaseOrders
		{
			get
			{
				if (_IV_PurchaseOrders == null) _IV_PurchaseOrders = new IV_PurchaseOrderController();
				return _IV_PurchaseOrders;
			}
		}

		IV_SupplierController _IV_Suppliers;
		public IV_SupplierController IV_Suppliers
		{
			get
			{
				if (_IV_Suppliers == null) _IV_Suppliers = new IV_SupplierController();
				return _IV_Suppliers;
			}
		}

		IV_TechAuditController _IV_TechAudits;
		public IV_TechAuditController IV_TechAudits
		{
			get
			{
				if (_IV_TechAudits == null) _IV_TechAudits = new IV_TechAuditController();
				return _IV_TechAudits;
			}
		}

		IV_TechSafetyStockController _IV_TechSafetyStocks;
		public IV_TechSafetyStockController IV_TechSafetyStocks
		{
			get
			{
				if (_IV_TechSafetyStocks == null) _IV_TechSafetyStocks = new IV_TechSafetyStockController();
				return _IV_TechSafetyStocks;
			}
		}

		#endregion //Controllers Properties
		
		#region View Controllers Properties

		#endregion //View Controllers Properties
	}
	
	#region Controllers
	
	public class EP_DeviceController : BaseTableController<EP_Device, EP_DeviceCollection> { }
	public class EP_EquipmentItemTypeController : BaseTableController<EP_EquipmentItemType, EP_EquipmentItemTypeCollection> { }
	public class EP_EquipmentManufacturerController : BaseTableController<EP_EquipmentManufacturer, EP_EquipmentManufacturerCollection> { }
	public class EP_EquipmentController : BaseTableController<EP_Equipment, EP_EquipmentCollection> { }
	public class EP_EquipmentTypeController : BaseTableController<EP_EquipmentType, EP_EquipmentTypeCollection> { }
	public class IV_AssetController : BaseTableController<IV_Asset, IV_AssetCollection> { }
	public class IV_AssetTagController : BaseTableController<IV_AssetTag, IV_AssetTagCollection> { }
	public class IV_AssetTrackingController : BaseTableController<IV_AssetTracking, IV_AssetTrackingCollection> { }
	public class IV_AssetTrackingTypeController : BaseTableController<IV_AssetTrackingType, IV_AssetTrackingTypeCollection> { }
	public class IV_OfficeAuditController : BaseTableController<IV_OfficeAudit, IV_OfficeAuditCollection> { }
	public class IV_OfficeController : BaseTableController<IV_Office, IV_OfficeCollection> { }
	public class IV_OfficeSafetyStockController : BaseTableController<IV_OfficeSafetyStock, IV_OfficeSafetyStockCollection> { }
	public class IV_OfficeTransferController : BaseTableController<IV_OfficeTransfer, IV_OfficeTransferCollection> { }
	public class IV_PurchaseOrderItemController : BaseTableController<IV_PurchaseOrderItem, IV_PurchaseOrderItemCollection> { }
	public class IV_PurchaseOrderController : BaseTableController<IV_PurchaseOrder, IV_PurchaseOrderCollection> { }
	public class IV_SupplierController : BaseTableController<IV_Supplier, IV_SupplierCollection> { }
	public class IV_TechAuditController : BaseTableController<IV_TechAudit, IV_TechAuditCollection> { }
	public class IV_TechSafetyStockController : BaseTableController<IV_TechSafetyStock, IV_TechSafetyStockCollection> { }

	#endregion //Controllers
	
	#region View Controllers
	

	#endregion //View Controllers
}
