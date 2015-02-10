using System.Runtime.Serialization;

namespace NXS.Services.Wcf.AppServices.Models
{
	[DataContract]
	public struct CountryMetaData
	{
		[DataMember]
		public const string USA_ID = "USA";

		[DataMember]
		public const string CAN_ID = "CAN";

		[DataMember]
		public const string UK_ID = "UK";
	}
}
