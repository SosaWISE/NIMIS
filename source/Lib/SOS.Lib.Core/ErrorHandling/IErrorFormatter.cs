using System.Collections.Generic;

namespace SOS.Lib.Core.ErrorHandling
{
	public interface IErrorFormatter<RetT>
	{
		RetT FormatErrorMessage(IErrorMessage message);

		ICollection<RetT> FormatErrorMessageCollection(ICollection<IErrorMessage> messages);

		string FormatLineBreaks(string input);
	}
}