using System;
using System.Web;
using System.Web.Mvc;

namespace SOS.Framework.Mvc.ActionResults
{
	public class FileAttachmentResult : ActionResult
	{
		#region .ctor

		public FileAttachmentResult(byte[] data, string contentType, string filename)
		{
			Data = data;
			ContentType = contentType;
			Filename = filename;
		}

		#endregion .ctor

		#region Properties

		public byte[] Data { get; private set; }
		public string ContentType { get; private set; }
		public string Filename { get; private set; }

		#endregion Properties

		#region Methods

		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");

			HttpResponseBase response = context.HttpContext.Response;

			if (!string.IsNullOrWhiteSpace(Filename))
			{
				response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", Filename.Replace(" ", "_")));
			}

			response.ContentType = ContentType;
			if (Data.Length > 0)
			{
				response.OutputStream.Write(Data, 0, Data.Length);
				response.OutputStream.Flush();
			}
		}

		#endregion Methods
	}
}