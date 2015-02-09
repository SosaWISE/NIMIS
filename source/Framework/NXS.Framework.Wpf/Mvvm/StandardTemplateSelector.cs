using System.Windows.Controls;
using System.Windows;

namespace NXS.Framework.Wpf.Mvvm
{
	public enum StandardTemplateType
	{
		Display,
		Form
	}

	public interface IStandardTemplateHost
	{
		StandardTemplateType TemplateType { get; }
	}

	public class StandardTemplateSelector : DataTemplateSelector
	{
		public DataTemplate DisplayTemplate { get; set; }
		public DataTemplate FormTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			DataTemplate result = null;

			IStandardTemplateHost host = item as IStandardTemplateHost;
			if (host != null)
			{
				switch (host.TemplateType)
				{
					case StandardTemplateType.Display :
						result = this.DisplayTemplate;
						break;
					case StandardTemplateType.Form :
						result = this.FormTemplate;
						break;
				}
			}
			else
			{
				result = base.SelectTemplate(item, container);
			}

			return result;
		}
	}
}