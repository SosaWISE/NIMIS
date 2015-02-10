using System.ServiceModel;
using NXS.Services.Wcf.AppServices.SecurityManager.Model;

namespace NXS.Services.Wcf.AppServices.SecurityManager
{
	[ServiceContract]
	public interface ISecurityService
	{
		[OperationContract]
		UserInfoModel Authenticate(string szUsername, string szPassword, string szDomainName);

		[OperationContract]
		UserInfoModel Impersonate(string szUsername);
	}
}