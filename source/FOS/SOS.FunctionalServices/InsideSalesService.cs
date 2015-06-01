using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NXS.Lib;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace SOS.FunctionalServices
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class InsideSalesService : IInsideSalesService
	{
		readonly List<object> loginParameters;
		public InsideSalesService()
		{
			loginParameters = new List<object>()
			{
				WebConfig.Instance.GetConfig("InsideSales_Username"),
				WebConfig.Instance.GetConfig("InsideSales_Password"),
				WebConfig.Instance.GetConfig("InsideSales_SecurityToken"),
			};
		}

		public IFnsResult<bool> Send(long leadId, string gpEmployeeId)
		{
			var result = new FnsResult<bool>()
			{
				Code = 0,
				Message = "",
				Value = false,
			};

			// get data from db
			var lead = SosCrmDataContext.Instance.QL_Leads.LoadByPrimaryKey(leadId);
			if (lead == null)
			{
				result.Code = (int)ErrorCodes.SqlItemNotFound;
				result.Message = "Lead not found.";
				return result;
			}

			var cookies = new CookieContainer();
			// login to api
			if (!Login(cookies))
			{
				result.Message = "Failed to login to InsideSales.com API.";
				result.Code = -1;
				return result;
			}

			var update = lead.InsideSalesId.HasValue;
			var addr = lead.Address;
			var parameters = new List<object>()
				{
					new Dictionary<string,object> {
						//
						{"id",update ? lead.InsideSalesId.Value : (int?)null},
						//
						{"external_id",lead.LeadID},
						{"name_prefix",lead.Salutation},
						{"first_name",lead.FirstName},
						{"middle_name",lead.MiddleName},
						{"last_name",lead.LastName},
						{"birthdate",lead.DOB.HasValue ? lead.DOB.Value.ToString("yyyy-MM-dd") : null},
						{"phone",lead.PhoneHome},
						{"mobile_phone",lead.PhoneMobile},
						{"home_phone",lead.PhoneHome},
						{"other_phone",lead.PhoneWork},
						{"email",lead.Email},
						// addr
						{"addr1",addr.StreetAddress},
						{"addr2",addr.StreetAddress2},
						{"city",addr.City},
						{"state_abbrev",addr.StateId},
						{"zip",addr.PostalCode},
					},
				};
			var respText = SendData(cookies, (update ? "updateLead" : "addLead"), parameters);
			var resp = JsonConvert.DeserializeObject(respText);
			if (resp is long)
			{
				// save lead with InsideSalesId
				lead.InsideSalesId = (int)(long)resp;
				lead.Save(gpEmployeeId);

				// success
				result.Value = true;
				if (update)
				{
					result.Message = "Updated lead in InsideSales.com";
				}
				else
				{
					result.Message = "Pushed lead to InsideSales.com";
				}
			}
			else
			{
				var respObj = resp as JObject;
				if (respObj != null && respObj["exception"] != null)
				{
					// failed
					var ex = respObj["exception"];
					result.Message = string.Format("{0}: {1}{2}", ex["code"], ex["string"], ex["name"]);
					result.Code = -1;
				}
				else
				{
					// failed??
					result.Message = "Unexpected response: " + respText;
					result.Code = -1;
				}
			}
			return result;
		}

		private bool Login(CookieContainer cookies)
		{
			return "true" == PostData(cookies, "login", loginParameters);
		}
		private string SendData(CookieContainer cookies, string op, List<object> parameters)
		{
			return PostData(cookies, op, parameters);
		}

		// query parameters to get leads by id
		//var parameters = new List<object>()
		//{
		//	new List<Query>(){
		//		new Query() {
		//			field = "id",
		//			values = new List<object>(){5606},
		//			operatorValue = "IN",
		//		},
		//	},
		//	0,
		//	5,
		//};
		//var respText = SendData(cookies, "getLeads", parameters);

		private string PostData(CookieContainer cookies, string op, List<object> parameters)
		{
			string postContent = JsonConvert.SerializeObject(new Operation { operation = op, parameters = parameters });
			var request = CreateInsideSalesRequest(cookies);
			WriteData(request, postContent);
			return ProcessResponse((HttpWebResponse)request.GetResponse());
		}
		private HttpWebRequest CreateInsideSalesRequest(CookieContainer cookies)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://nexsense.insidesales.com/do=noauth/rest/service");
			request.Method = "POST";
			request.ContentType = "application/json";
			request.CookieContainer = cookies;
			request.Timeout = (30 * 1000);
			//request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.7) Gecko/20091221 Firefox/3.5.7 (.NET CLR 3.5.30729)";
			return request;
		}
		private void WriteData(HttpWebRequest request, string postContent)
		{
			if (!String.IsNullOrEmpty(postContent))
			{
				byte[] bytes = Encoding.Default.GetBytes(postContent);
				request.ContentLength = bytes.Length;
				using (Stream stream = request.GetRequestStream())
				{
					stream.Write(bytes, 0, bytes.Length);
				}
			}
		}
		private string ProcessResponse(HttpWebResponse response)
		{
			string result;
			using (Stream stream = response.GetResponseStream())
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					result = reader.ReadToEnd();
				}
			}
			return result;
		}
	}

	class Operation
	{
		public string operation { get; set; }
		public List<object> parameters { get; set; }
	}
	class Query
	{
		public string field { get; set; }
		public List<object> values { get; set; }
		[JsonProperty("operator")]
		public string operatorValue { get; set; }
	}
	class InsideSalesErrorResponse
	{
		public InsideSalesError exception { get; set; }
	}
	class InsideSalesError
	{
		public string code { get; set; }
		[JsonProperty("string")]
		public string stringText { get; set; }
		public string actor { get; set; }
		public string details { get; set; }
		public string name { get; set; }
	}
}
