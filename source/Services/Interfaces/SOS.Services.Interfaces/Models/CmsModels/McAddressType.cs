using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.CmsModels
{
	#region McAddressType

	public interface IMcAddressType
	{
		string McAddressTypeID { get; set; }
		string McAddressTypeName { get; set; }
	}

	public class McAddressType : IMcAddressType
	{
		#region Implementation of IMcAddressType

		[DataMember]
		public string McAddressTypeID { get; set; }
		[DataMember]
		public string McAddressTypeName { get; set; }

		#endregion Implementation of IMcAddressType
	}

	#endregion McAddressType
}
