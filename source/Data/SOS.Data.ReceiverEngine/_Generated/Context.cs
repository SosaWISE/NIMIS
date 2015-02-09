


using System;
using SubSonic;
using SOS.Data;

namespace SOS.Data.ReceiverEngine
{
	public partial class SosReceiverEngineControlDataContext
	{
		#region Internal Instance

		private static SosReceiverEngineControlDataContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();

		public static SosReceiverEngineControlDataContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new SosReceiverEngineControlDataContext();
						}
					}
				}
				return _contextInstance;
			}
		}

		#endregion // Internal Instance

		#region Controllers Properties

		RE_RequestsRawController _RE_RequestsRaws;
		public RE_RequestsRawController RE_RequestsRaws
		{
			get
			{
				if (_RE_RequestsRaws == null) _RE_RequestsRaws = new RE_RequestsRawController();
				return _RE_RequestsRaws;
			}
		}

		RE_TxtWireRequestController _RE_TxtWireRequests;
		public RE_TxtWireRequestController RE_TxtWireRequests
		{
			get
			{
				if (_RE_TxtWireRequests == null) _RE_TxtWireRequests = new RE_TxtWireRequestController();
				return _RE_TxtWireRequests;
			}
		}

		#endregion //Controllers Properties

		#region View Controllers Properties

		#endregion //View Controllers Properties
	}

	#region Controllers

	public class RE_RequestsRawController : BaseTableController<RE_RequestsRaw, RE_RequestsRawCollection> { }
	public class RE_TxtWireRequestController : BaseTableController<RE_TxtWireRequest, RE_TxtWireRequestCollection> { }

	#endregion //Controllers

	#region View Controllers


	#endregion //View Controllers
}
