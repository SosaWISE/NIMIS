using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SOS.Framework.Mvc.ActionResults
{
	public class JsonpResult : ActionResult
	{
		#region .ctor
		//public JsonpResult () {}
		#endregion .ctor

		#region Properties

		public Encoding ContentEncoding { get; set; }
		public string ContentType { get; set; }
		public object Data { get; set; }
		public string JsonCallback { get; set; }
		public JsonRequestBehavior JsonRequestBehavior { get; set; }

		#endregion Properties

		#region Overrides of ActionResult

		/// <summary>
		/// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult"/> class.
		/// </summary>
		/// <param name="oContext">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
		public override void ExecuteResult(ControllerContext oContext)
		{
			/** Check if context is null. */
			if (oContext == null) { throw new ArgumentNullException("oContext"); }

			/** Acquire callback. */
			JsonCallback = oContext.HttpContext.Request["jsoncallback"];

			if (string.IsNullOrEmpty(JsonCallback))
				JsonCallback = oContext.HttpContext.Request["callback"];

			/** Check callback is empty. */
			if (string.IsNullOrEmpty(JsonCallback))
				throw new Exception("JsonCallback required for JSONP response.");

			/** Initialize. */
			var oResponse = oContext.HttpContext.Response;

			/** Get Content Type. */
			oResponse.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

			/** Get Content encoding. */
			if (ContentEncoding != null)
				oResponse.ContentEncoding = ContentEncoding;

			/** Get Data. */
			if (Data == null) return;
			// Default path of execution.
			var oSerializer = new JavaScriptSerializer();
			oResponse.Write(string.Format("{0}({1});", JsonCallback, oSerializer.Serialize(Data)));
		}

		#endregion Overrides of ActionResult
	}
}
