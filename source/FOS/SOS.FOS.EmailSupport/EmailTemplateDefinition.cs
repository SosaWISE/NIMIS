using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Linq;
using SOS.Lib.Util;

namespace SOS.FOS.EmailSupport
{
	public class EmailTemplateDefinition
	{
		#region .ctor
		
		public EmailTemplateDefinition(string szTemplatePath)
		{
			var rootEl = XElement.Load(szTemplatePath);

			if (rootEl == null)
			{
				throw new InvalidOperationException("Missing EmailTemplateDefinition element");
			}

			var definitions = (string)rootEl.Element(_templateDefinitonsName);
			var subjectTemplate = (string)rootEl.Element(_subjectTemplateName);
			var htmlBodyTemplate = (string)rootEl.Element(_htmlBodyTemplateName);
			var plainTextBodyTemplate = (string)rootEl.Element(_plainTextTemplateName);

			if (definitions.IsNullOrEmpty())
			{
				throw new InvalidOperationException("Missing template definitions");
			}

			var dparts = definitions.Split('\n');

			Definitions = string.Join(Environment.NewLine, dparts.Select(s => _trimmer.Replace(s, ""))).Trim();

			if (subjectTemplate.IsNullOrEmpty())
			{
				throw new InvalidOperationException("Missing subject template");
			}

			if (htmlBodyTemplate.IsNullOrEmpty() && plainTextBodyTemplate.IsNullOrEmpty())
			{
				throw new InvalidOperationException("No body template defined");
			}

			SubjectTemplate = Definitions + Environment.NewLine + _trimmer.Replace(subjectTemplate, "");

			if (!htmlBodyTemplate.IsNullOrEmpty())
			{
				HtmlBodyTemplate = Definitions + Environment.NewLine + _trimmer.Replace(htmlBodyTemplate, "");
			}

			if (!plainTextBodyTemplate.IsNullOrEmpty())
			{
				PlainTextBodyTemplate = Definitions + Environment.NewLine + _trimmer.Replace(plainTextBodyTemplate, "");
			}
		}

		#endregion .ctor

		#region Memeber Properties
		#region Private

		//private static readonly XName _emailTemplateName = "EmailTemplateDefinition";
		private static readonly XName _subjectTemplateName = "SubjectTemplate";
		private static readonly XName _htmlBodyTemplateName = "HtmlBodyTemplate";
		private static readonly XName _plainTextTemplateName = "TextBodyTemplate";
		private static readonly XName _templateDefinitonsName = "TemplateDefinitions";

		private static readonly Regex _trimmer = new Regex(@"^\s+|\s+$", RegexOptions.Compiled);

		#endregion Private

		public string Definitions { get; set; }
		public string SubjectTemplate { get; private set; }
		public string HtmlBodyTemplate { get; private set; }
		public string PlainTextBodyTemplate { get; private set; }

		#region Public


		#endregion Public

		#endregion Memeber Properties
	}
}
