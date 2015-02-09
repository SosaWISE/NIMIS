using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SOS.Framework.Mvc.ActionResults
{
	public class HttpErrorResult : ActionResult
	{
		#region .ctor

		public HttpErrorResult(HttpStatusCode oCode) : this(oCode, null) {}

		public HttpErrorResult (HttpStatusCode oCode, string szMessage)
		{
			StatusCode = oCode;
			Message = szMessage;
		}
		#endregion .ctor

		#region Properties

		public HttpStatusCode StatusCode { get; set; }
		public string Message { get; set; }

		#endregion Properties

		#region Methods

		public override void ExecuteResult(ControllerContext context)
		{
			throw new HttpException((int) StatusCode, Message);
		}

		#endregion Methods

	}
}
