using System.Runtime.Serialization;
using SOS.Services.Interfaces.Models;

namespace SOS.services.Wcf.Signals.Models
{
	[DataContract(Name = "TxtWireResult")]
	public class TxtWirePostInfo : ITxtWirePostInfo
	{
		// ReSharper disable InconsistentNaming
		[DataMember]
		public string title { get; set; }
		[DataMember]
		public string code { get; set; }
		[DataMember]
		public string shortcode { get; set; }
		[DataMember]
		public string message { get; set; }
		[DataMember]
		public string phone { get; set; }
		[DataMember]
		public string carrier { get; set; }
		[DataMember]
		public string keyword { get; set; }
		[DataMember]
		public string group { get; set; }
		[DataMember]
		public string custom_ticket { get; set; }
		[DataMember]
		public string default_keyword { get; set; }

		[DataMember]
		public string user_name { get; set; }
		[DataMember]
		public string password { get; set; }
		[DataMember]
		public string api_key { get; set; }
		// ReSharper restore InconsistentNaming
	}
}