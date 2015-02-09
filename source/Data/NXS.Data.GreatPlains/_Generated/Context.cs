


using System;
using SOS.Data;
using SubSonic;

namespace NXS.Data.GreatPlains
{
	public partial class GreatPlainsDataContext
	{
		#region Internal Instance
		
		private static GreatPlainsContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();
		
		public static GreatPlainsContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new GreatPlainsContext();
						}
					}
				}
				return _contextInstance;
			}
		}
		
		#endregion // Internal Instance
		
		#region Controllers Properties

		#endregion //Controllers Properties
		
		#region View Controllers Properties

		BillingContractsViewController _BillingContractsViews;
		public BillingContractsViewController BillingContractsViews
		{
			get
			{
				if (_BillingContractsViews == null) _BillingContractsViews = new BillingContractsViewController();
				return _BillingContractsViews;
			}
		}

		CustomerInformationViewController _CustomerInformationViews;
		public CustomerInformationViewController CustomerInformationViews
		{
			get
			{
				if (_CustomerInformationViews == null) _CustomerInformationViews = new CustomerInformationViewController();
				return _CustomerInformationViews;
			}
		}

		CustomerInvoicesViewController _CustomerInvoicesViews;
		public CustomerInvoicesViewController CustomerInvoicesViews
		{
			get
			{
				if (_CustomerInvoicesViews == null) _CustomerInvoicesViews = new CustomerInvoicesViewController();
				return _CustomerInvoicesViews;
			}
		}

		CustomerOutstandingInvoicesViewController _CustomerOutstandingInvoicesViews;
		public CustomerOutstandingInvoicesViewController CustomerOutstandingInvoicesViews
		{
			get
			{
				if (_CustomerOutstandingInvoicesViews == null) _CustomerOutstandingInvoicesViews = new CustomerOutstandingInvoicesViewController();
				return _CustomerOutstandingInvoicesViews;
			}
		}

		MonitoringContractsViewController _MonitoringContractsViews;
		public MonitoringContractsViewController MonitoringContractsViews
		{
			get
			{
				if (_MonitoringContractsViews == null) _MonitoringContractsViews = new MonitoringContractsViewController();
				return _MonitoringContractsViews;
			}
		}

		OpenPaymentsAndCreditsViewController _OpenPaymentsAndCreditsViews;
		public OpenPaymentsAndCreditsViewController OpenPaymentsAndCreditsViews
		{
			get
			{
				if (_OpenPaymentsAndCreditsViews == null) _OpenPaymentsAndCreditsViews = new OpenPaymentsAndCreditsViewController();
				return _OpenPaymentsAndCreditsViews;
			}
		}

		#endregion //View Controllers Properties
	}
	
	#region Controllers
	

	#endregion //Controllers
	
	#region View Controllers
	
	public class BillingContractsViewController : BaseViewController<BillingContractsView, BillingContractsViewCollection> { }
	public class CustomerInformationViewController : BaseViewController<CustomerInformationView, CustomerInformationViewCollection> { }
	public class CustomerInvoicesViewController : BaseViewController<CustomerInvoicesView, CustomerInvoicesViewCollection> { }
	public class CustomerOutstandingInvoicesViewController : BaseViewController<CustomerOutstandingInvoicesView, CustomerOutstandingInvoicesViewCollection> { }
	public class MonitoringContractsViewController : BaseViewController<MonitoringContractsView, MonitoringContractsViewCollection> { }
	public class OpenPaymentsAndCreditsViewController : BaseViewController<OpenPaymentsAndCreditsView, OpenPaymentsAndCreditsViewCollection> { }

	#endregion //View Controllers
}
