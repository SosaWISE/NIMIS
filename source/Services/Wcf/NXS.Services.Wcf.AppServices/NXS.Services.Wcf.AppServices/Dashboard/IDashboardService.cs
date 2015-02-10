using System.Collections.Generic;
using System.ServiceModel;
using NXS.Services.Wcf.AppServices.Dashboard.Models;

namespace NXS.Services.Wcf.AppServices.Dashboard
{
	[ServiceContract]
	public interface IDashboardService
	{
		[OperationContract]
		IList<DashboardApplication> GetApplications(List<ApplicationPermission> oPermissions);

		[OperationContract]
		IList<ActionApplictionMapping> GetActionApplicationMappings(List<ApplicationPermission> olPermissions);

		[OperationContract]
		byte[] GetSmallIconFile(int nApplicationID);

		[OperationContract]
		byte[] GetIconFile(int nApplicationID);

		[OperationContract]
		byte[] GetDeployedFile(int nApplicationID);

		[OperationContract]
		IList<Message> GetMessagesForUser(string szUsername, bool bIncludeRead, bool bIncludeDeleted);

		[OperationContract]
		void MarkMessageRead(int nMessageID);

		[OperationContract]
		void DeleteMessage(int nMessageID);
	}
}