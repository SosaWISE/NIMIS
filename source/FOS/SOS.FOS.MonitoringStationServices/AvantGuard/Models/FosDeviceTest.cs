using System;
using NSE.FOS.Contracts.Models;
using SOS.FOS.MonitoringStationServices.AGSiteService;

namespace SOS.FOS.MonitoringStationServices.AvantGuard.Models
{
	public class FosDeviceTest : IFosDeviceTest
	{
		public int? TestNum { get; set; }
		public string TestCategory { get; set; }
		public string TestCategoryDescription { get; set; }
		public DateTime? EffectiveOn { get; set; }
		public DateTime? ExpiresOn { get; set; }
		public string TestType { get; set; }
	}
}
