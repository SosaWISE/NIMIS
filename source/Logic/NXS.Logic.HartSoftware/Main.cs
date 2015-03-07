using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using SOS.Lib.Core.CreditReportService;
using SOS.Lib.Core.ErrorHandling;

namespace NXS.Logic.HartSoftware
{
	public class Main
	{
		#region .ctor

		#endregion .ctor

		#region Properties
		const string _METHOD = "POST";
		const string _TYPE = "application/xml"; //REST XML

		#endregion Properties

		#region Methods

		public bool RunService(IWSLead oWSLead, IWSAddress oWSAddress, string[] szaBureaus, IWSSeason oSeason, string szAdUsername, ref List<WSMessage> oMessageList, out IWSCreditReportInfo oRepInfo)
		{
			#region Initialize

			var result = false;
			var sb = new StringBuilder();
			oRepInfo = null;
			var fullname = oWSLead.FirstName;
			fullname = string.IsNullOrEmpty(oWSLead.MiddleName)
				? string.Format("{0} {1}", fullname, oWSLead.LastName)
				: string.Format("{0} {1} {2}", fullname, oWSLead.MiddleName, oWSLead.LastName);
			#endregion Initialize

			#region Execute Call To Service

			try
			{
				sb.AppendFormat("ACCOUNT={0}", Credentials.CrUsername);
				sb.AppendFormat("\r\nPASSWD={0}", Credentials.CrPassword);
				sb.AppendFormat("\r\nPASS={0}", 2);
				sb.AppendFormat("\r\nPROCESS={0}", "PCCREDIT");
				sb.AppendFormat("\r\nBUREAU={0}", "TU");
				sb.AppendFormat("\r\nPRODUCT={0}", "CREDIT");
				sb.AppendFormat("\r\nNAME={0}", fullname);
				if (!string.IsNullOrEmpty(oWSLead.SocialSecurity))
					sb.AppendFormat("\r\nSSN={0}", oWSLead.SocialSecurity);
				sb.AppendFormat("\r\nADDRESS={0}", oWSAddress.Address1);
				sb.AppendFormat("\r\nCITY={0}", oWSAddress.City);
				sb.AppendFormat("\r\nSTATE={0}", oWSAddress.State);
				sb.AppendFormat("\r\nZIP={0}", oWSAddress.PostalCode);

				// Build Web Post
				var webRequest = (HttpWebRequest)WebRequest.Create(Credentials.EndPointUri);
				webRequest.Method = _METHOD;
				webRequest.ContentType = _TYPE;

				// write and send request data 
				using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
				{
					streamWriter.Write(sb.ToString());
				}

				using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
				{
					Stream getResponseStream = webResponse.GetResponseStream();
					if (getResponseStream == null) throw new Exception("GetResponseStream returned a null.");
					using (var responseStream = new StreamReader(getResponseStream))
					{
						var responseString = responseStream.ReadToEnd();
						using (TextReader reader = new StringReader(responseString))
						{
							//var serializer = new XmlSerializer(typeof(HX5));
							//var responseCls = (HX5)serializer.Deserialize(reader);
							//Debug.WriteLine(responseCls.version);
						}
					}
				}

				result = true;
			}
			catch (Exception ex)
			{
				oMessageList.Add(new WSMessage
				{
					DisplayMessage = string.Format("The following error occurred:\r\n{0}", ex.Message),
					Ex = ex,
					MessageType = ErrorMessageType.Critical,
					Title = "Exception thrown running Credit on Hart."
				});
			}
			#endregion Execute Call To Service

			// ** Return result
			return result;
		}

		#endregion Methods
	}

}
