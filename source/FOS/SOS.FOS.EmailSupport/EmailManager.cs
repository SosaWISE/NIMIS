using System;
using System.IO;
using SOS.Data.Logging;
using SOS.Lib.Core.ErrorHandling;
using SOS.Lib.RazorEngine;
using SOS.Lib.Util;
using NXS.Lib;

namespace SOS.FOS.EmailSupport
{
	public class EmailManager
	{
		private Templater<EmailModel> _templater;
		private string _templatePath;
		private string _smtpServer;

		#region .ctor

		public EmailManager()
		{
			Init();
		}

		#endregion .ctor

		#region Member Properties

		#endregion Member Properties

		#region Memeber Functions
		#region Private

		private void Init()
		{
			/** Initialize. */
			_templater = new Templater<EmailModel>();
			_templatePath = WebConfig.Instance.GetConfig("EmailTemplatePath");
			_smtpServer = WebConfig.Instance.GetConfig("SmtpServer");

			/** Validate values. */
			if (_templatePath.IsNullOrEmpty() )
			{
				throw new EmailManagerNoTemplatePathException("No template path was provided.");
			}
			if (_smtpServer.IsNullOrEmpty())
			{
				throw new EmailManagerSmtpServerNotSpeciedException("No smtp server provided.");
			}
			if (!Directory.Exists(_templatePath))
			{
				throw new EmailManagerTemplatePathMissingException(string.Format("The path '{0}' does not exists in this configuration."
					, _templatePath));
			}
		}

		#endregion Private

		#region Public

		public bool SendEmail (string szTemplateFile, EmailModel oModel, string szToAddresses)
		{
			try
			{
				/** Initialize. */
				var szTemplatePath = Path.Combine(_templatePath, szTemplateFile);
				var oTemplateDefin = new EmailTemplateDefinition(szTemplatePath);
				var szSubject = _templater.Generate(oModel, oTemplateDefin.SubjectTemplate);
				string szHtmlBody = null;
				string szTextBody = null;

				/** Validate data. */
				if (!oTemplateDefin.HtmlBodyTemplate.IsNullOrEmpty())
				{
					szHtmlBody = _templater.Generate(oModel, oTemplateDefin.HtmlBodyTemplate);
				}
				if (!oTemplateDefin.PlainTextBodyTemplate.IsNullOrEmpty())
				{
					szTextBody = _templater.Generate(oModel, oTemplateDefin.PlainTextBodyTemplate);
				}

				/** Execute. */
				var smtpSender = new SmtpSender(_smtpServer, 1, 0, szToAddresses);
				smtpSender.SendMessage(szSubject, szHtmlBody, szTextBody);

				/** Return a successfull result. */
				return true;
			}
			catch (Exception oEx)
			{
				DBErrorManager.Instance.AddMessage(ErrorMessageType.Exception, oEx
					, "Error Sending Email"
					, string.Format("The following error message was captured: {0}", oEx.Message));
			}

			/** Default path of execution. */
			return false;
		}

		#endregion Public

		#endregion Memeber Functions

		#region Exceptions

		public class EmailManagerNoTemplatePathException : Exception
		{
			public EmailManagerNoTemplatePathException(){}
			public EmailManagerNoTemplatePathException(string szMessage) : base(szMessage) {}
		}

		public class EmailManagerTemplatePathMissingException : Exception
		{
			public EmailManagerTemplatePathMissingException(){}
			public EmailManagerTemplatePathMissingException(string szMessage) : base(szMessage){}
		}

		public class EmailManagerSmtpServerNotSpeciedException : Exception
		{
			public EmailManagerSmtpServerNotSpeciedException(){}
			public EmailManagerSmtpServerNotSpeciedException(string szMessage) : base(szMessage) { }
		}

		#endregion Exceptions
	}
}