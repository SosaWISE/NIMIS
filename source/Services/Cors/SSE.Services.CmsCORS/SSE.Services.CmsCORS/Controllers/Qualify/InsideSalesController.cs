using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace SSE.Services.CmsCORS.Controllers.Qualify
{
	[RoutePrefix("QualifySrv")]
	public class InsideSalesController : ApiController
	{
		private IInsideSalesService Service
		{
			get
			{
				return SosServiceEngine.Instance.FunctionalServices.Instance<IInsideSalesService>();
			}
		}
		private static void SetOnCorsResult(CmsCORSResult<object> cors, IFnsResult fns)
		{
			cors.Code = fns.Code;
			cors.Message = fns.Message;
			cors.Value = fns.GetValue();
		}

		[HttpPost]
		[Route("InsideSales/{leadId}")]
		public CmsCORSResult<object> Post(long leadId)
        {
			return CORSSecurity.AuthenticationWrapper("Post", user =>
			{
				CmsCORSResult<object> result;
				// do validation
				var valid = CORSArg.ArgumentValidation(new List<CORSArg>() {
                    new CORSArg("", (leadId == 0), "Missing LeadID."),
                }, out result);
				// always set session
				result.SessionId = user.SessionID;

				// check if passed validation
				if (valid)
				{
					SetOnCorsResult(result, Service.Send(leadId, user.GPEmployeeID));
				}
				return result;
			});
        }
    }

	//class InsideSales
	//{
	//	public string Json
	//}
}
