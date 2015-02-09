using System.ComponentModel;
using SOS.Lib.Util.Extensions;

namespace NXS.Framework.Wpf.Validation
{
	public class MaskedValidatedInput : ValidatedInput<string>
	{
		public string Mask { get; set; }
		public string EmptyMask { get; private set; }

		public override void SetValue(string value)
		{
			//bug: if it's clean with text and all text is deleted it remains clean
			//bool wasClean = !this.IsDirty;

			//format text before setting
			MaskedTextProvider provider = GetProvider(value);

			base.SetValue(provider.ToDisplayString());

			//// If we started out as clean and the new value is the NullValueString, then keep it clean
			//if (wasClean && this.IsDirty && StringUtility.AreEqual(value, NullValueString, false))
			//{
			//    this.Clean();
			//}
		}
		private MaskedTextProvider GetProvider(string value)
		{
			//format text before setting
			MaskedTextProvider provider = new MaskedTextProvider(Mask);
			EmptyMask = provider.ToDisplayString();

			if (value == null) {
				value = "";
			}
			int position;
			provider.SetTextInProvider(out position, 0, value.Length, value);

			return provider;
		}

		public string GetText(bool includePrompt, bool includeLiterals)
		{
			MaskedTextProvider provider = GetProvider(Value);
			return provider.ToString(includePrompt, includeLiterals);
		}
	}
}
