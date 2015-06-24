using System;
using NSE.FOS.Contracts.Models;

namespace SOS.FOS.MonitoringStationServices.Monitronics.Models
{
	public class FosDeviceTest : IFosDeviceTest
	{
		public string TestNum { get; set; }
		public string TestCategory { get; set; }
		public string TestCategoryDescription { get; set; }
		public DateTime? EffectiveOn { get; set; }
		public DateTime? ExpiresOn { get; set; }
		public string TestType { get; set; }
	}
}
