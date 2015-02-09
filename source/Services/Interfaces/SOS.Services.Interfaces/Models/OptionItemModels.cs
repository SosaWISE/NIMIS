namespace SOS.Services.Interfaces.Models
{
	public interface IOptionItemModel
	{
		string Value { get; set; }
		string Text { get; set; }
	}

	public class OptionItemModel : IOptionItemModel
	{
		#region .ctor

		public OptionItemModel(string sValue, string sText)
		{
			Value = sValue;
			Text = sText;
		}

		#endregion .ctor

		#region Implementation of IOptionItemModel

		public string Value { get; set; }
		public string Text { get; set; }

		#endregion Implementation of IOptionItemModel
	}
}