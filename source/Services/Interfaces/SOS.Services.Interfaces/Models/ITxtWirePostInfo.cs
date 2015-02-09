using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models
{
	public interface ITxtWirePostInfo
	{
		// ReSharper disable InconsistentNaming
		[DataMember]
		string title { get; set; }
		[DataMember]
		string code { get; set; }
		[DataMember]
		string shortcode { get; set; }
		[DataMember]
		string message { get; set; }
		[DataMember]
		string phone { get; set; }
		[DataMember]
		string carrier { get; set; }
		[DataMember]
		string keyword { get; set; }
		[DataMember]
		string group { get; set; }
		[DataMember]
		string custom_ticket { get; set; }
		[DataMember]
		string default_keyword { get; set; }

		[DataMember]
		string user_name { get; set; }
		[DataMember]
		string password { get; set; }
		[DataMember]
		string api_key { get; set; }
		// ReSharper restore InconsistentNaming
	}
}
