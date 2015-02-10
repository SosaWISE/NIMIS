using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RestService
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRestServiceImpl" in both code and config file together.
	[ServiceContract]
	public interface IRestServiceImpl
	{
		[OperationContract]
		[WebInvoke(Method = "POST",
			//ResponseFormat = WebMessageFormat.Xml,
			//RequestFormat = WebMessageFormat.Xml,
			BodyStyle = WebMessageBodyStyle.Bare,
			UriTemplate = "auth")]
		ResponseData Auth(RequestData oRequestData);
	}


//	[DataContract(Namespace = "http://www.freedomsos.com/receiver")]
	[DataContract(Namespace = "http://www.freedomsos.com/receiver")]
	public class RequestData
	{
		[DataMember]
		public string Details { get; set; }
	}

	[DataContract]
	public class ResponseData
	{
		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Age { get; set; }

		[DataMember]
		public string Exp { get; set; }

		[DataMember]
		public string Technology { get; set; }
	}
}
