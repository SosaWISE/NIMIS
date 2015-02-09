using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SOS.Lib.Util.Extensions;

namespace NXS.Framework.Wpf.Controls
{
	public class MaskedTextBox : TextBox
	{
		#region Properties

		/// <summary>
		///     Dependency property to store the mask to apply to the textbox
		/// </summary>
		public static readonly DependencyProperty MaskProperty =
			DependencyProperty.Register("Mask", typeof (string), typeof (MaskedTextBox),
				new UIPropertyMetadata(null, MaskPropertyChanged));

		/// <summary>
		///     Gets the MaskTextProvider for the specified Mask
		/// </summary>
		public MaskedTextProvider MaskProvider
		{
			get
			{
				MaskedTextProvider maskProvider = null;
				if (Mask != null)
				{
					maskProvider = new MaskedTextProvider(Mask);
					if (Text != null)
					{
						maskProvider.Set(Text);
					}
				}
				return maskProvider;
			}
		}

		/// <summary>
		///     Gets or sets the mask to apply to the textbox
		/// </summary>
		public string Mask
		{
			get { return (string) GetValue(MaskProperty); }
			set { SetValue(MaskProperty, value); }
		}

		private static void MaskPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			//make sure to update the text if the mask changes

			var textBox = (MaskedTextBox) sender;

			MaskedTextProvider provider = textBox.MaskProvider;

			textBox.SelectAll();
			int position;
			if (provider != null)
			{
				provider.SetTextInProvider(out position, textBox.SelectionStart, textBox.SelectionLength, textBox.Text);

				textBox.RefreshText(provider, 0);
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		///     Static Constructor
		/// </summary>
		static MaskedTextBox()
		{
			TextProperty.OverrideMetadata(typeof (MaskedTextBox), new FrameworkPropertyMetadata(null, Text_CoerceValue));
		}

		/// <summary>
		///     Default  constructor
		/// </summary>
		public MaskedTextBox()
		{
			//cancel the paste and cut command
			CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, null, PasteCommand));
			CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, null, CutCommand));

			//this.TextChanged += MaskedTextBox_TextChanged;
		}

		private static object Text_CoerceValue(DependencyObject obj, object value)
		{
			//Technically this is only needed when the value is set programmatically,
			//	but I couldn't find a way to have it called only at that time,
			//	so it's made to be called anytime the text changes
			//	It feels like double the work, ma che ci possa fare?

			var textBox = (MaskedTextBox) obj;

			var text = (string) value;

			MaskedTextProvider provider = textBox.MaskProvider;
			if (provider != null)
			{
				//save selection start
				int startPosition = textBox.SelectionStart;
				//set selection start to beginning
				textBox.SelectionStart = 0;

				if (!string.IsNullOrEmpty(text))
				{
					int position;
					provider.SetTextInProvider(out position, textBox.SelectionStart, textBox.SelectionLength, text);
				}
				else
				{
					provider.Clear();
				}
				text = provider.ToDisplayString();

				//reset selection start
				textBox.SelectionStart = startPosition;
			}

			return text;
		}

		#endregion //Constructor

		#region ApplicationCommands

		private void CutCommand(object sender, CanExecuteRoutedEventArgs e)
		{
			if (SelectionLength > 0)
			{
				string text = Text.Substring(SelectionStart, SelectionLength);

				MaskedTextProvider provider = MaskProvider;
				int position;
				if (provider.DeleteSelectedText(out position, SelectionStart, SelectionLength))
				{
					RefreshText(provider, position);
				}

				Clipboard.SetText(text);
			}

			e.CanExecute = false;
			e.Handled = true;
		}

		private void PasteCommand(object sender, CanExecuteRoutedEventArgs e)
		{
			MaskedTextProvider provider = MaskProvider;
			int position;
			if (provider.SetTextInProvider(out position, SelectionStart, SelectionLength, Clipboard.GetText()))
			{
				RefreshText(provider, position);
			}

			e.CanExecute = false;
			e.Handled = true;
		}

		#endregion //ApplicationCommands

		#region Overrides

		/// <summary>
		///     override this method to replace the characters enetered with the mask
		/// </summary>
		/// <param name="e">Arguments for event</param>
		protected override void OnPreviewTextInput(TextCompositionEventArgs e)
		{
			//if the text is readonly do not add the text
			if (IsReadOnly)
			{
				e.Handled = true;
				return;
			}

			MaskedTextProvider provider = MaskProvider;
			int position;
			if (provider.SetTextInProvider(out position, SelectionStart, SelectionLength, e.Text))
			{
				RefreshText(provider, position);
			}

			e.Handled = true;

			base.OnPreviewTextInput(e);
		}

		/// <summary>
		///     override the key down to handle delete of a character
		/// </summary>
		/// <param name="e">Arguments for the event</param>
		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			base.OnPreviewKeyDown(e);

			bool refresh = false;

			MaskedTextProvider provider = MaskProvider;
			int position = SelectionStart;

			if (e.Key == Key.Delete && SelectionLength > 0)
			{
//handle delete selection

				refresh = provider.DeleteSelectedText(out position, SelectionStart, SelectionLength);

				e.Handled = true;
			}
			else if (e.Key == Key.Space || e.Key == Key.Delete)
			{
//handle space and delete

				//delete and space end up doing the same thing
				//	so why not call the same method
				refresh = provider.SetTextInProvider(out position, SelectionStart, SelectionLength, " ");

				e.Handled = true;
			}
			else if (e.Key == Key.Back)
			{
//handle backspace

				if (SelectionLength > 0)
				{
					refresh = provider.DeleteSelectedText(out position, SelectionStart, SelectionLength);
				}
				else if (position > 0)
				{
					position--;
					refresh = provider.DeleteText(out position, position);
				}

				e.Handled = true;
			}

			if (refresh)
			{
				RefreshText(provider, position);
			}
		}

		#endregion

		#region Helper Methods

		//refreshes the text of the textbox
		protected virtual void RefreshText(MaskedTextProvider provider, int position)
		{
			provider.IncludeLiterals = !provider.IncludeLiterals;
			Text = provider.ToDisplayString();
			SelectionStart = position;
		}

		#endregion

		#region Public

		public string GetText(bool includePrompt, bool includeLiterals)
		{
			MaskedTextProvider provider = MaskProvider;
			return provider.ToString(includePrompt, includeLiterals);
		}

		#endregion //Public
	}
}