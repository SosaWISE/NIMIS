using System.Collections.Generic;

namespace NXS.Logic.MonitoringStations.Models
{
    public class Account : Base
	{
		#region .ctor
		#endregion .ctor

		#region Properties

		public List<SiteSystem> SiteSystems { get; set; }

		public List<Zone> Zones { get; set; }

		public List<SiteAgencyPermit> SiteAgencyPermits { get; set; }

		public List<Contact> Contacts { get; set; }

		public List<SiteOption> SiteOptions { get; set; }

		public List<SiteSystemOption> SiteSystemOptions { get; set; }

		public List<SiteGeneralDispatches> SiteGeneralDispatches { get; set; }

		#endregion Properties

		#region Methods

	    public string Serialize()
	    {
		    return Serialize<Account>();
	    }

	    #endregion Methods
	}
}
