using System.IO;
using SOS.Lib.RazorEngine.Templating;

namespace SOS.Lib.RazorEngine
{
	public class Templater<TModelType> : Templater
	{
		public Templater()
		{
			SetTemplateBase(typeof(TemplateBase<TModelType>));
		}

		public string Generate(TModelType model, string defaultTemplateContent)
		{
			return Generate(model, defaultTemplateContent, null);
		}

		public string Generate(TModelType model, string defaultTemplateContent, string templatePath)
		{
			string template;
			var fileName = Path.GetFileName(templatePath);

			if (!string.IsNullOrEmpty(templatePath))
			{
				//check to see if there is an override template file
				if (File.Exists(templatePath))
				{
					template = File.ReadAllText(templatePath);
				}
				else
				{
					//if there isn't use default content
					template = defaultTemplateContent;
				}
			}
			else
			{
				template = defaultTemplateContent;
			}

			var generatedContent = Parse(template, model, fileName, fileName);

			return generatedContent;
		}
	}
}
