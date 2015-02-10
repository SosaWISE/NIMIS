using System.Collections.Generic;
using System.ServiceModel;
using NXS.Services.Wcf.AppServices.ErrorHandling.Models;

namespace NXS.Services.Wcf.AppServices.ErrorHandling
{
	[ServiceContract]
	public interface IErrorHandlingService
	{
		[OperationContract]
		void PersistMessages(List<ErrorMessage> messages, int logSourceID);
	}
}