using System.Web;
using SOS.Lib.Util.Configuration;

namespace SOS.Clients.MVC.CorpSite2
{
	/// <summary>
	/// Summary description for DownloadBrochure
	/// </summary>
	public class DownloadBrochure : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			/** Initialize. */
			string szOutputFileName;
			string szInputSourcePath;
			context.Response.Clear();
			switch (context.Request.QueryString["brochureName"])
			{
				case "FreedomSOS":
					szOutputFileName = "ClientBrochureFileName";
					szInputSourcePath = "ClientBrochurePath";
					//context.Response.AddHeader("Content-Disposition", "attachment; filename=BrochureV1.PDF");
					//context.Response.WriteFile(context.Server.MapPath("~/Content/FreedomSOSBrochure.pdf"));
					break;

				case "DealerOpp":
					szOutputFileName = "DealerBrochureFileName";
					szInputSourcePath = "DealerBrochurePath";
					//context.Response.AddHeader("Content-Disposition", "attachment; filename=DealerOppBrochureV1.PDF");
					//context.Response.WriteFile(context.Server.MapPath("~/Content/OppDealerV001.pdf"));
					break;

				case "SalesOpp":
					szOutputFileName = "SalesBrochureFileName";
					szInputSourcePath = "SalesBrochurePath";
					//context.Response.AddHeader("Content-Disposition", "attachment; filename=SalesOppBrochureV1.PDF");
					//context.Response.WriteFile(context.Server.MapPath("~/Content/OppSalesV001.pdf"));
					break;

				default:
					szOutputFileName = "ClientBrochureFileName";
					szInputSourcePath = "ClientBrochurePath";
					//context.Response.AddHeader("Content-Disposition", "attachment; filename=BrochureV1.PDF");
					//context.Response.WriteFile(context.Server.MapPath("~/Content/FreedomSOSBrochure.pdf"));
					break;
			}

			szOutputFileName = ConfigurationSettings.Current.GetConfig(szOutputFileName);
			szOutputFileName = string.Format("attachment; filename={0}", szOutputFileName);
			szInputSourcePath = ConfigurationSettings.Current.GetConfig(szInputSourcePath);

			context.Response.AddHeader("Content-Disposition", szOutputFileName);
			context.Response.WriteFile(context.Server.MapPath(szInputSourcePath));
			context.Response.ContentType = "application/octet-stream";
			context.Response.End();
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}