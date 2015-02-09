


using SOS.Data;
using System;
using SubSonic;

namespace NXS.Data.Accounting
{
	public partial class AccountingDataContext
	{
		#region Internal Instance
		
		private static AccountingDataContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();
		
		public static AccountingDataContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new AccountingDataContext();
						}
					}
				}
				return _contextInstance;
			}
		}
		
		#endregion // Internal Instance
		
		#region Controllers Properties

		AE_OfficeController _AE_Offices;
		public AE_OfficeController AE_Offices
		{
			get
			{
				if (_AE_Offices == null) _AE_Offices = new AE_OfficeController();
				return _AE_Offices;
			}
		}

		UPR40700Controller _UPR40700S;
		public UPR40700Controller UPR40700S
		{
			get
			{
				if (_UPR40700S == null) _UPR40700S = new UPR40700Controller();
				return _UPR40700S;
			}
		}

		#endregion //Controllers Properties
		
		#region View Controllers Properties

		#endregion //View Controllers Properties
	}
	
	#region Controllers
	
	public class AE_OfficeController : BaseTableController<AE_Office, AE_OfficeCollection> { }
	public class UPR40700Controller : BaseTableController<UPR40700, UPR40700Collection> { }

	#endregion //Controllers
	
	#region View Controllers
	

	#endregion //View Controllers
}
