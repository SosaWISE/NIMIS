using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using NXS.Logic.HartSoftware.Schemas.TU;
using SOS.Data.SosCrm;
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

			var cr = new QL_CreditReport
			{
				LeadId = oWSLead.LeadID,
				AddressId = oWSAddress.AddressID,
				BureauId = QL_CreditReportBureau.MetaData.TransUnionID,
				SeasonId = oSeason.SeasonID,
				CreditReportVendorId = QL_CreditReportVendor.MetaData.Hart_SoftwareID,
				Prefix = oWSLead.Prefix,
				FirstName = oWSLead.FirstName,
				MiddleName = oWSLead.MiddleName,
				LastName = oWSLead.LastName,
				Suffix = oWSLead.Suffix
			};
			if (!string.IsNullOrEmpty(oWSLead.SocialSecurity))
				cr.SSN = SOS.Lib.Util.Cryptography.TripleDES.EncryptString(oWSLead.SocialSecurity, null);
			if (!string.IsNullOrEmpty(oWSLead.DOB))
				cr.DOB = DateTime.Parse(oWSLead.DOB);
			cr.CreatedOn = DateTime.UtcNow;
			cr.Save();

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
							var serializer = new XmlSerializer(typeof(THX5));
							var responseCls = (THX5)serializer.Deserialize(reader);
							var crHart = new QL_CreditReportVendorHartSoftware();
							crHart.CreditReportId = cr.CreditReportID;
							crHart.BureauId = cr.BureauId;
							crHart.Version = responseCls.version;
							crHart.Token = responseCls.HX5_transaction_information.Token;
							crHart.TransactionId = responseCls.HX5_transaction_information.Transid;
							crHart.XmlResponse = responseString;
							crHart.ReportHtml = responseCls.HTML_Reports[0].Value;
							crHart.IsHit = responseCls.bureau_xml_data[0].subject_segments[0].subject_header[0].file_hit.code.Equals("Y");
//							crHart.IsHit = responseCls.bureau_xml_data[0].subject_segments[0].scoring_segments != null;
							if (responseCls.bureau_xml_data[0].subject_segments[0].scoring_segments != null)
							{
								int score;
								crHart.IsScored = int.TryParse(responseCls.bureau_xml_data[0].subject_segments[0].scoring_segments[0].scoring.score, out score);
								crHart.Score = (crHart.IsScored) ? score : 0;
								crHart.ProductCode = responseCls.bureau_xml_data[0].subject_segments[0].scoring_segments[0].scoring.product_code.code;
								crHart.ProductCodeValue = responseCls.bureau_xml_data[0].subject_segments[0].scoring_segments[0].scoring.product_code.Value;
								crHart.IndicatorFlagCode = responseCls.bureau_xml_data[0].subject_segments[0].scoring_segments[0].scoring.indicator_flag.code;
								crHart.IndicatorFlagValue = responseCls.bureau_xml_data[0].subject_segments[0].scoring_segments[0].scoring.indicator_flag.Value;
							}
							
							crHart.Save();

							Debug.WriteLine(responseCls.version);
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
