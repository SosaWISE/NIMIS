using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models
{
	public interface ISosGeneralAuthenticationInfo
	{
		#region Properties

		[DataMember]
		string AuthenticationToken { get; set; }

		[DataMember]
		string Url { get; set; }

		#endregion Properties
	}
}