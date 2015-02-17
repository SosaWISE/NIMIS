using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Reporting
{
	public interface IFnsMsAccountOnlineStatusInfo
	{
		[DataMember]
		string KeyName { get; set; }

		[DataMember]
		string Text { get; set; }

		[DataMember]
		string Value { get; set; }

		[DataMember]
		string Status { get; set; }
	}
}