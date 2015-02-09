using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsVendorAlarmComPackage : IMsVendorAlarmComPackage
	{
		#region Properties
		public string AlarmComPackageID { get; set; }
		public string PackageName { get; set; }
		public bool DefaultValue { get; set; }
		#endregion Properties
	}

	#region Interface

	public interface IMsVendorAlarmComPackage
	{
		[DataMember]
		string AlarmComPackageID { get; set; }
		[DataMember]
		string PackageName { get; set; }
		[DataMember]
		bool DefaultValue { get; set; }
	}

	#endregion Interface
}
