using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SOS.Clients.MVC.DealerSite.Support
{
	public class TemplateModule : IHttpModule
	{
		#region Member Properties
		
		private static readonly Regex _idParser = new Regex("\\<!--\\s*TemplateId\\:\\s*(?<type>[a-z]+)\\s*(?<id>[a-zA-Z0-9-_.]+)\\s*--\\>"
			, RegexOptions.Compiled);

		private List<string> _templatePaths = new List<string>();

		private static readonly Regex _browserVersionParser = 
			new Regex(@"(?<browser>Chrome|Firefox|chromeframe)\/(?<majver>[0-9]+)",
			RegexOptions.Compiled);

		private static readonly Regex _ieVersionParser =
			new Regex(@"MSIE\s+(?<majver>[0-9]+)",
			RegexOptions.Compiled);

		#endregion Member Properties

		#region Member Functions
		private void OnEndRequest(object sender, EventArgs e){}
		private void OnBeginRequest(object sender, EventArgs e)
		{
			/** Initialize. */
			var oApp = (HttpApplication) sender;
			var oContext = oApp.Context;

			/** Execute. */
			if (oContext.Request.HttpMethod == "GET")
			{
				/** Initialize. */
				var szFilePath = oContext.Request.FilePath;
				var szRawFilename = VirtualPathUtility.GetFileName(szFilePath);
				/** Check result. */
				if (szRawFilename != null)
				{
					var szFilename = szRawFilename.ToLower();

					/** Bypass index.html. */
					if (szFilename.Equals("index.html"))
					{
						/** First make sure the clien is running a new enough browser. */
						if (CheckBrowserVersion(oContext))
						{
							AppendTemplates(oContext);
						}
						oContext.Response.End();
					}
					else if (szFilename.EndsWith(".html"))
					{
						/** IIS 6 hack. */
						var iis6Hack = ConfigurationManager.AppSettings["iis6StaticContextHack"];
						if (!string.IsNullOrWhiteSpace(iis6Hack) && iis6Hack.ToLower() == "true")
						{
							oContext.Response.WriteFile(oContext.Request.PhysicalPath);
							oContext.Response.End();
						}
					}
				}
			}
		}

		/// <summary>
		/// Validate client browser version, and if not correct,
		/// return appropriate page with upgrade information.
		/// 
		/// Note that for now if we can't detect the browser version
		/// we'll assume it is ok.
		/// </summary>
		private bool CheckBrowserVersion(HttpContext context)
		{
			/** Initialize. */
			var ua = context.Request.UserAgent;

			/** Check that ua is present. */
			if (ua == null) return false;

			var matches = _browserVersionParser.Matches(ua);
			var ieMatch = _ieVersionParser.Match(ua);

			var ieVersion = 0;
			var chromeVersion = 0;
			var firefoxVersion = 0;
			var chromeFrameVersion = 0;
			var otherBrower = 0;

			foreach (Match match in matches)
			{
				var browserType = match.Groups["browser"].Value;
				var version = int.Parse(match.Groups["majver"].Value);

				switch (browserType)
				{
					case "Chrome":
						chromeVersion = version;
						break;

					case "Firefox":
						firefoxVersion = version;
						break;

					case "chromeframe":
						chromeFrameVersion = version;
						break;

					default:
						otherBrower = version;
						break;
				}
			}

			if (ieMatch.Success)
			{
				ieVersion = int.Parse(ieMatch.Groups["majver"].Value);
			}

			//check IE first
			if (ieVersion > 0 && ieVersion < 9)
			{
				//we're ok if chromeframe v15 or later is installed
				if (chromeFrameVersion > 0)
				{
					if (chromeFrameVersion >= 15)
					{
						return true;
					}

					//chromeframe is installed but it needs upgrading
					SendPage(context, "UpgradeChromeFrame.html");
					return false;
				}

				//no good - return ie upgrade page
				SendPage(context, "UpgradeIe.html");
				return false;
			}

			//kin2 will probably work with chrome versions older than 15
			//but for now thats what we're goign with
			if (chromeVersion > 0 && chromeVersion < 15)
			{
				SendPage(context, "UpgradeChrome.html");
				return false;
			}

			//verify this
			if (firefoxVersion > 0 && firefoxVersion < 6)
			{
				SendPage(context, "UpgradeFirefox.html");
				return false;
			}

			/** Verify that there is some weired browser. */
			if (otherBrower >= 0)
			{
				SendPage(context, "OtherBrowser.html");
			}

			return true;
		}

		private void SendPage(HttpContext context, string fileName)
		{
			var filePath = context.Server.MapPath("/" + fileName);
			if (!File.Exists(filePath))
			{
				return;
			}

			var content = File.ReadAllText(filePath);
			context.Response.Write(content);
		}

		private void AppendTemplates(HttpContext oContext)
		{
			/** Initialize. */
			const string insertionPoint = @"<!-- TEMPLATE INSERTION POINT - DO NOT ALTER THIS LINE -->";
			var indexPath = oContext.Server.MapPath(oContext.Request.FilePath);
			var indexText = File.ReadAllText(indexPath);
			var insertionStart = indexText.IndexOf(insertionPoint, StringComparison.Ordinal);
			var insertionEnd = insertionStart + insertionPoint.Length;

			var sbCompositePage = new StringBuilder();
			sbCompositePage.Append(indexText.Substring(0, insertionStart));
			sbCompositePage.AppendLine();

			var oTemplatesList = new List<string>();
			foreach (var szPath in _templatePaths)
			{
				oTemplatesList.AddRange(Directory.GetFiles(szPath, "*.html"));
			}

			/** Check that there is something to process. */
			if (oTemplatesList.Count > 0)
			{
				foreach (var szTemplatePath in oTemplatesList)
				{
					/** Initialize. */
					var szTemplate = File.ReadAllText(szTemplatePath);
					var idMatch = _idParser.Match(szTemplate);

					/** Continue if there is no match. */
					if (!idMatch.Success) continue;
					
					/** Default path of execution. */
					var szType = idMatch.Groups["type"].Value;
					var szId = idMatch.Groups["id"].Value;
					sbCompositePage.Append("\r\n<script type=\"text/" + szType + "-template\" id=\"" + szId + "\">" +
					                       szTemplate + "</script>\r\n");
				}
			}

			sbCompositePage.Append(indexText.Substring(insertionEnd));

			oContext.Response.Write(sbCompositePage.ToString());
		}

		#endregion Member Functions

		#region Implementation of IHttpModule

		/// <summary>
		/// Initializes a module and prepares it to handle requests.
		/// </summary>
		/// <param name="oApp">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application </param>
		public void Init(HttpApplication oApp)
		{
			/** Initialize. */
			var szTemplateLocations = ConfigurationManager.AppSettings["TEMPLATE_DIRECTORIES"];

			oApp.BeginRequest += OnBeginRequest;
			oApp.EndRequest += OnEndRequest;

			if (!string.IsNullOrEmpty(szTemplateLocations))
			{
				var szaParts = szTemplateLocations.Split(';');
				_templatePaths = szaParts.Select(szPath => oApp.Context.Server.MapPath(szPath)).ToList();
			}
		}

		/// <summary>
		/// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
		/// </summary>
		public void Dispose()
		{
		}

		#endregion
	}
}