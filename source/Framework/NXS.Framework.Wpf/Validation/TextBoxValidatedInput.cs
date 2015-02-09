using System.Windows;

namespace NXS.Framework.Wpf.Validation
{
	public class TextBoxValidatedInput<T> : ValidatedInput<T>
	{
		public TextWrapping TextWrapping { get; set; }
		public bool AcceptsReturn { get; set; }
	}
}
