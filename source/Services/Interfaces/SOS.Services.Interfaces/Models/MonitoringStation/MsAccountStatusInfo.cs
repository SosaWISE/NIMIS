using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsAccountStatusInfo : IMsAccountStatusInfo
	{
		#region .ctor
		#endregion .ctor

		#region Properties
	
		public string KeyName { get; set; }

		public string Value { get; set; }

		public string Status { get; set; }

		#endregion Properties

	}


	public interface IMsAccountStatusInfo
	{
		[DataMember]
		string KeyName { get; set; }

		[DataMember]
		string Value { get; set; }

		[DataMember]
		string Status { get; set; }
	}
}
