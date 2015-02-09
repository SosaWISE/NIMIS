using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using System.Web.Http;
using SOS.Data.Logging;
using System;

namespace SSE.Services.CmsCORS.Controllers
{
	public class HomeController : ApiController
	{
		[Route("Ping")]
		[HttpGet]
		public CmsCORSResult<int> Ping()
		{
			return CORSSecurity.AuthenticationWrapper("Ping", user =>
			{
				return new CmsCORSResult<int>(0, "", "");
			});
		}
		[Route("LogError")]
		[HttpPost]
		public CmsCORSResult<long> LogError([FromBody]ErrorMessage data)
		{
			string createdBy = "";
			var result = CORSSecurity.AuthenticationWrapper("LogError", user =>
			{
				createdBy = user.GPEmployeeID;
				return new CmsCORSResult<long>(0, "", "");
			});

			try
			{
				// try to save error, even when not logged in
				result.Code = 0;
				result.Message = "";

				var msg = new LG_Message
				{
					MessageTypeId = data.MessageTypeId,
					Header = TrimMaxLength(data.Header, LG_Message.MaxHeaderLength) ?? "",//can't be null
					Message = SOS.Lib.Util.StringHelper.NullIfWhiteSpace(data.Message) ?? "",//can't be null
					//@REVIEW: for now store Version in TargetSchema column
					TargetSchema = TrimMaxLength(data.Version, 128),
					MethodCall = TrimMaxLength(data.MethodCall, LG_Message.MaxMethodLength),
					ComputerName = TrimMaxLength(data.ComputerName, LG_Message.MaxComputerNameLength),
					SourceView = TrimMaxLength(data.SourceView, LG_Message.MaxSourceViewLength),

					CreatedBy = TrimMaxLength(createdBy, 50) ?? "[UNKNOWN]",//can't be null
					CreatedOn = DateTime.Now,
					LogSourceID = (int)SOS.Data.SubSonicConfigHelper.GetLogSourceFromConfig(),
				};
				msg.Save(msg.CreatedBy);

				// return new ID of message
				result.Value = msg.MessageID;
			}
			catch (Exception ex)
			{
				result.Code = (int)CmsResultCodes.ExceptionThrown;
				result.Message = string.Format("Failed to log error: {0}", ex.Message);
			}

			return result;
		}
		static string TrimMaxLength(string value, int maxLength)
		{
			value = SOS.Lib.Util.StringHelper.NullIfWhiteSpace(value);
			if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
			{
				value = value.Substring(0, maxLength);
			}
			return value;
		}
	}
	public class ErrorMessage
	{
		public int MessageTypeId { get; set; }
		public string Header { get; set; }
		public string Message { get; set; }
		public string Version { get; set; }
		public string MethodCall { get; set; }
		public string ComputerName { get; set; }
		public string SourceView { get; set; }
	}
}
