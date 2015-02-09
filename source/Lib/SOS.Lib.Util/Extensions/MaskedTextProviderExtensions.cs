using System.ComponentModel;

namespace SOS.Lib.Util.Extensions
{
	public static class MaskedTextProviderExtensions
	{
		/// <summary>
		/// Inserts the text at the current position
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="newPosition"></param>
		/// <param name="selectionStart"></param>
		/// <param name="selectionLength"></param>
		/// <param name="text"></param>
		public static bool SetTextInProvider(this MaskedTextProvider provider, out int newPosition, int selectionStart, int selectionLength, string text)
		{
			if (text != null)
			{
				if (selectionLength > 0)
				{
					provider.DeleteSelectedText(out newPosition, selectionStart, selectionLength);
				}

				newPosition = selectionStart;

				foreach (char c in text)
				{
					if (newPosition < provider.Length)
					{
						newPosition = provider.GetNextCharacterPosition(newPosition);

						if (provider.Replace(c, newPosition))
							newPosition++;

						newPosition = provider.GetNextCharacterPosition(newPosition);
					}
				}

				return true;
			}
			newPosition = 0;
			return false;
		}

		public static int GetNextCharacterPosition(this MaskedTextProvider provider, int startPosition)
		{
			int position = provider.FindEditPositionFrom(startPosition, true);

			if (position == -1)
			{
				return startPosition;
			}

			// Default path
			return position;
		}

		public static bool DeleteSelectedText(this MaskedTextProvider provider, out int newPosition, int selectionStart, int selectionLength)
		{
			int startIndex = selectionStart;
			int endIndex = (selectionStart + selectionLength) - 1;

			return DeleteText(provider, out newPosition, startIndex, endIndex);
		}

		public static bool DeleteText(this MaskedTextProvider provider, out int newPosition, int position)
		{
			return DeleteText(provider, out newPosition, position, position);
		}

		public static bool DeleteText(this MaskedTextProvider provider, out int newPosition, int startIndex, int endIndex)
		{
			bool refresh = false;
			while (startIndex <= endIndex)
			{
				if (provider.Replace(' ', endIndex))
				{
					refresh = true;
				}
				endIndex--;
			}

			newPosition = startIndex;
			return refresh;
		}
	}
}
