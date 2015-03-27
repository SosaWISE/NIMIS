using System.Collections.Generic;
using System.Web.Http;

namespace SSE.Services.CmsCORS.Controllers.Funding
{
	[RoutePrefix("FundingSrv")]
	public class BundlesController : ApiController
    {
		[Route("Bundles")]
		[HttpGet]
		// GET api/bundles
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

		[Route("Bundles/{id}")]
		[HttpGet]
		// GET api/bundles/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
