using SOS.Lib.Util.Cryptography;
using System;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace SOS.Wpf.CryptoManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		#region .ctor

		public MainWindow()
		{
			InitializeComponent();

			// Set the tab order of things
			var nIndex = 0;
			tbEncText.TabIndex = ++nIndex;
			btnEncrypt.TabIndex = ++nIndex;
			tbEncOutput.TabIndex = ++nIndex;

			tbDecText.TabIndex = ++nIndex;
			btnDecrypt.TabIndex = ++nIndex;
			tbDecOutput.TabIndex = ++nIndex;
		}

		#endregion .ctor

		#region Properties

		private const string _SZC_CONNSTRING_TEMPLATE = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Application Name={4}";
		private const string _SZC_XML_TEMPATE = "<add key=\"{0}\" value=\"{1}\"/>\r\n";
		private enum EnumCols
		{
			ConnStringName = 0,
			DBName = 1,
			Username = 2,
			Password = 3
		}

		#endregion Properties

		#region Event Handlers
		private void BtnEncryptClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(tbEncryptionKeyOverride.Text))
				tbEncOutput.Text = TripleDES.EncryptString(tbEncText.Text, null);
			else
				tbEncOutput.Text = TripleDES.EncryptString(tbEncText.Text, tbEncryptionKeyOverride.Text);
		}

        private void BtnDecryptClick(object sender, RoutedEventArgs e) {
            Func<string, string> decrypt;
            if (cbUseOld.IsChecked.HasValue && cbUseOld.IsChecked.Value) {
                decrypt = (line) => { return TripleDES.DecryptString(line); };
            } else {
                var key = SOS.Lib.Util.StringHelper.NullIfWhiteSpace(tbEncryptionKeyOverride.Text);
                decrypt = (line) => { return TripleDES.DecryptString(line, key); };
            }

            var lines = tbDecText.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length > 1) {
                // decrypt each line separately
                tbDecOutput.Text = "";
                // begin crappy threading code
                ThreadPool.QueueUserWorkItem(delegate {
                    StringBuilder sob = new StringBuilder();
                    //var count = 0;
                    //var total = 0;
                    foreach (var line in lines) {
                        var result = decrypt(line).Trim();// +" - " + line;
                        this.Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(() => {
                            //++total;
                            if (result.StartsWith("Error:")) {
                                sob.AppendLine(result);
                            } else {
                                //tbDecOutput.Text = string.Format("{0} of {1}", ++count, total);
                                tbDecOutput.Text += result + Environment.NewLine;
                                tbDecOutput.ScrollToEnd();
                            }
                            Thread.Sleep(5);
                        }));
                        Thread.Sleep(5);
                    }

                    this.Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(() => {
                        if (!string.IsNullOrWhiteSpace(tbDecOutput.Text)) {
                            tbDecOutput.Text += Environment.NewLine + Environment.NewLine;
                        }
                        tbDecOutput.Text += sob.ToString();
                    }));
                });
            } else {
                // decrypt only one
                tbDecOutput.Text = decrypt(tbDecText.Text);
            }
        }

		private void BtnGenerateClick(object sender, RoutedEventArgs e)
		{
			// Locals
			var sbResult = new StringBuilder();
			sbResult.Append(string.Format(_SZC_XML_TEMPATE, "Comments", tbEnvironmentComment.Text));

			var oFile = new System.IO.StreamReader("Assets\\Data\\Input.txt");
			string szLine;

			while ((szLine = oFile.ReadLine()) != null)
			{
				var szaData = szLine.Split('\t');

				sbResult.Append(string.Format(_SZC_XML_TEMPATE, szaData[(int)EnumCols.ConnStringName]
					, TripleDES.EncryptString(string.Format(_SZC_CONNSTRING_TEMPLATE
						, tbServerName.Text
						, szaData[(int)EnumCols.DBName]
						, szaData[(int)EnumCols.Username]
						, szaData[(int)EnumCols.Password]
						, tbApplicationName.Text), null)));
			}



			oFile.Close();

			// Print results
			tbResult.Text = sbResult.ToString();

		}
		#endregion Event Handlers
	}
}
