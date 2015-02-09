using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.CmsModels
{
	#region McAddressStatus

		public interface IMcAddressStatus
		{
			string McAddressStatusID { get; set; }
			string McAddressStatusName { get; set; }
		}

		public class McAddressStatus : IMcAddressStatus
		{
			#region Implementation of IMcAddressStatus

			[DataMember]
			public string McAddressStatusID { get; set; }
			[DataMember]
			public string McAddressStatusName { get; set; }

			#endregion Implementation of IMcAddressStatus
		}

		#endregion McAddressStatus
}
