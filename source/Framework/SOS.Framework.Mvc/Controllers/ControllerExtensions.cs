using System.Text;
using System.Web.Mvc;
using SOS.Framework.Mvc.ActionResults;

namespace SOS.Framework.Mvc.Controllers
{
	public static class ControllerExtensions
	{
		#region Method Extensions

		public static JsonpResult Jsonp(this Controller oCntlr, object oData)
		{
			return Jsonp(oCntlr, oData, null /* oContentType */, null /* oContentEncoding */, JsonRequestBehavior.DenyGet);
		}

		public static JsonpResult Jsonp(this Controller oCntlr, object oData, string oContentType)
		{
			return Jsonp(oCntlr, oData, oContentType, null /* oContentEncoding */, JsonRequestBehavior.DenyGet);
		}

		public static JsonpResult Jsonp(this Controller oCntlr, object oData, string oContentType, Encoding oContentEncoding)
		{
			return Jsonp(oCntlr, oData, oContentType, oContentEncoding, JsonRequestBehavior.DenyGet);
		}

		public static JsonpResult Jsonp(this Controller oCntlr, object oData, JsonRequestBehavior behavior)
		{
			return Jsonp(oCntlr, oData, null /* oContentType */, null /* oContentEncoding */, behavior);
		}

		public static JsonpResult Jsonp(this Controller oCntlr, object oData, string oContentType, JsonRequestBehavior behavior)
		{
			return Jsonp(oCntlr, oData, oContentType, null /* oContentEncoding */, behavior);
		}

		public static JsonpResult Jsonp(this Controller oCntlr, object oData, string oContentType, Encoding oContentEncoding, JsonRequestBehavior behavior)
		{
			/** Initialize. */
			var oResult = new JsonpResult
			{
				Data = oData,
				ContentType = oContentType,
				ContentEncoding = oContentEncoding,
				JsonRequestBehavior = behavior
			};

			/** Execute Result. */
			//oResult.ExecuteResult(oCntlr.ControllerContext);

			/** Return result. */
			return oResult;
		} 

		#endregion Method Extensions
	}
}
