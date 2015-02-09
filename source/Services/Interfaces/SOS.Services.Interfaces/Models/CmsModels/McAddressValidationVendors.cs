using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.CmsModels
{

	#region McAddressValidationVendors

	public interface IMcAddressValidationVendors
	{
		string ValidationVendorID { get; set; }
		string ValidationVendorName { get; set; }
	}

	public class McAddressValidationVendors : IMcAddressValidationVendors
	{
		#region Implementation of IMcAddressValidationVendors

		[DataMember]
		public string ValidationVendorID { get; set; }
		[DataMember]
		public string ValidationVendorName { get; set; }

		#endregion Implementation of IMcAddressValidationVendors
	}

	#endregion McAddressValidationVendors
}
