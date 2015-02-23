using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models.QualifyLead;
using SSE.Services.CmsCORS.Helpers;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;
using SOS.Lib.Core;

namespace SSE.Services.CmsCORS.Controllers.Qualify
{
	[RoutePrefix("QualifySrv")]
	public class CustomerMasterFileController : ApiController
	{
		private IWiseCrmService Service
		{
			get
			{
				return SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
			}
		}

		[Route("CustomerMasterFiles/{id}/HasCustomer")]
		[HttpGet]
		[HttpOptions]
		public Result<bool> HasCustomer(long id)
		{
			return CORSSecurity.Authorize("HasCustomer", null, null, user =>
			{
				var result = new Result<bool>();
				if (id == 0)
				{
					result.Code = -1;
					result.Message = "Invalid CustomerMasterFileID";
					return result;
				}

				var fnsResult = Service.MasterFileHasCustomer(id);
				return result.FromFnsResult(fnsResult);
			});
		}

		[Route("CustomerMasterFiles/{id}/Leads")]
		[HttpGet]
		[HttpOptions]
		public Result<List<IFnsQlLead>> Leads(long id)
		{
			return CORSSecurity.Authorize("Leads", null, null, user =>
			{
				var result = new Result<List<IFnsQlLead>>();
				if (id == 0)
				{
					result.Code = -1;
					result.Message = "Invalid CustomerMasterFileID";
					return result;
				}

				var fnsResult = Service.MasterFileLeads(id);
				return result.FromFnsResult(fnsResult);
			});
		}

		[Route("CustomerMasterFiles/{id}/Customers")]
		[HttpGet]
		[HttpOptions]
		public Result<List<IFnsAeCustomerCardInfo>> Customers(long id)
		{
			return CORSSecurity.Authorize("Customers", null, null, user =>
			{
				var result = new Result<List<IFnsAeCustomerCardInfo>>();
				if (id == 0)
				{
					result.Code = -1;
					result.Message = "Invalid CustomerMasterFileID";
					return result;
				}

				var fnsResult = Service.MasterFileCustomers(id);
				return result.FromFnsResult(fnsResult);
			});
		}
	}
}