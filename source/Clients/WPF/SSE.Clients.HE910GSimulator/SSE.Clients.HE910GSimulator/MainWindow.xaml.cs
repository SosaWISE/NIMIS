
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading;
using SSE.Lib.SseGpsDeviceAPI.Commands;
using SSE.Lib.SseGpsDeviceAPI.Commands.Interface;
using SSE.Lib.SseGpsDeviceAPI.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;
using SSE.Lib.SseGpsDeviceAPI.Processor;
using ConfigurationSettings = SOS.Lib.Util.Configuration.ConfigurationSettings;

namespace SSE.Clients.HE910GSimulator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			InitEnvironment();
			InitServerPortAndAddress();
		}

		#region Properties

		private readonly TcpClient _clientSocket = new TcpClient();
		private IPAddress _ipAddress;
		private int _portNumber;
		private Thread _serviceThread;

		#endregion Properties

		#region Methods

		private static void InitEnvironment()
		{
			/** Load configuration. */
			string environment = ConfigurationManager.AppSettings["Environment"]
								 ?? Environment.MachineName;
			ConfigurationSettings.Current.SetProperties("Preferences", environment);
		}

		private void AddCommandToMainConsole(string commandStr)
		{
			if (TbMainConsole.Text.Length != 0) TbMainConsole.Text += "\r\n";

			TbMainConsole.Text += string.Format("-->{0}", commandStr);
		}

		private bool FieldValidation()
		{
			/** Inititalize. */
			var messageStr = new List<string>();
			var result = true;

			if (CbEw.SelectedIndex == -1)
			{
				messageStr.Add(string.Format("Please select a East or West for the Longitude."));
				result = false;
			}
			if (CbNs.SelectedIndex == -1)
			{
				messageStr.Add(string.Format("Please select a North or South for the Latitude."));
				result = false;
			}

			/** SHow message. */
			var largeMsg = messageStr.Aggregate(string.Empty, (current, message) => current + string.Format("{0}\r\n", message));

			if (!result)
				MessageBox.Show(largeMsg, "Field Validation", MessageBoxButton.OK, MessageBoxImage.Error);


			/** Return result. */
			return result;
		}

		private void InitServerPortAndAddress()
		{
			/** Initialize. */
			string sPortNmbr = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("CSS_PORT_NUMBR"), null);
			string sIPAdress = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("CSS_IP_ADDRESS"), null);

			// ** Get IP Address. */
			if (!IPAddress.TryParse(sIPAdress, out _ipAddress))
			{
				_ipAddress = IPAddress.Any;
				MessageBox.Show("* Missing config Settings", string.Format("The configuration setting \"CSS_IP_ADDRESS\" is either missing or invalid.  Listener is working on IP Address: {0}"
						, _ipAddress), MessageBoxButton.OK, MessageBoxImage.Error);
			}

			// ** Get Port number. */
			if (!int.TryParse(sPortNmbr, out _portNumber))
			{
				_portNumber = 2055;
				MessageBox.Show("* Missing Config Settings"
					, string.Format("The configuration setting \"CSS_PORT_NUMBR\" is either missing or invalid.  Listener working on Port: {0}"
						, _portNumber), MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void ServiceEntryPoint()
		{
			/** Initialize connection. */
			_clientSocket.Connect(_ipAddress.ToString(), _portNumber);
			NetworkStream s = _clientSocket.GetStream();

			/** Get Server commands. */
			while (true)
			{
				/** Read input. */
				var sr = new StreamReader(s);
				var sw = new StreamWriter(s) { AutoFlush = true };
				string sSerReq = sr.ReadLine();

				Dispatcher.Invoke((Action)(() =>
				{
					TbMainConsole.Text += string.Format("<--{0}\r\n", sSerReq);
					long accountId;
					if (!long.TryParse(TbDeviceId.Text, out accountId))
					{
						MessageBox.Show("Invalid Device ID", string.Format("The Device ID '{0}' is invalid.", TbDeviceId.Text));
					}

					var device = new Device(accountId, TbImeiNumber.Text, TbSimNumber.Text, Convert.ToInt16(TbPingInterval.Text),
											SldrLowBatteryAlert.Value, TbPassword.Text, Convert.ToDouble(TbSpeedAlert.Text), Convert.ToInt16(TbGForceAlert.Text));

					// Process the server request.

					var clientRequestPar = new ClientRequestParser(sSerReq, device);
					var clientResponse = clientRequestPar.GetResponseSentence();

					TbMainConsole.Text += string.Format("-->{0}\r\n", clientResponse);

					// Return a response to server.
					sw.WriteLine(clientResponse);
				}));
			}
		}

		#endregion Methods

		#region Events

		private void tbSentence_KeyUp(object sender, KeyEventArgs e)
		{
			/** Initialize. */
			var chkSum = SentenceParser.GetCheckSum(TbSentence.Text);
			TbChkSum.Text = chkSum;
			TbSentenceLength.Text = TbSentence.Text.Length.ToString(CultureInfo.InvariantCulture);
		}

		private void btConnectToSS_Click(object sender, RoutedEventArgs e)
		{
			if (_clientSocket.Connected) return;

			_serviceThread = new Thread(ServiceEntryPoint);
			_serviceThread.Start();

			BtConnectToSs.IsEnabled = false;
		}

		private void BindData(IResponsePDE objectPDE)
		{
			var utcDateTime = DateTime.UtcNow;

			/** Field Validation. */
			if (!FieldValidation()) { return; }

			/** Bind data. */
			objectPDE.DeviceID = TbDeviceId.Text;
			objectPDE.Lattitude = TbLatitude.Text;
			objectPDE.NSIndicator = CbNs.SelectedValue.ToString().Equals("North")
										? GPSIndicator.InstanceN
										: GPSIndicator.InstanceS;
			objectPDE.Longitude = TbLongitude.Text;
			objectPDE.EWIndicator = CbEw.SelectedValue.ToString().Equals("East")
										? GPSIndicator.InstanceE
										: GPSIndicator.InstanceW;
			objectPDE.HDOP = "2.25";
			objectPDE.Altitude = TbAltitude.Text;  // In Feet
			objectPDE.Fix = "3";
			objectPDE.COG = "0.0";
			objectPDE.SpKm = TbSpeed.Text;
			objectPDE.SpKn = Conversion.Instance.KmToKt(TbSpeed.Text).ToString("F2");
			objectPDE.UTCEventTime = utcDateTime; // 240613
			objectPDE.NSat = "04";
			objectPDE.Battery = SldBattery.Value.ToString("F2");
			objectPDE.GForce = SldGForce.Value.ToString("F2");
			objectPDE.CellStrength = SldCellStrength.Value.ToString("F2");
			objectPDE.GpsStrength = SldGpsStregth.Value.ToString("F2");
			objectPDE.MessageState = MessageState.RealTime;

		}

		private void btPDE_Click(object sender, RoutedEventArgs e)
		{
			/** Initialize. */
			var objectPDE = new ClientResponsePDE(string.Empty);

			BindData(objectPDE);
			try
			{
				var sentencePDE = objectPDE.GetSentence();
				AddCommandToMainConsole(sentencePDE);
			}
			catch (Exception oEx)
			{
				MessageBox.Show(string.Format("The following error occurred: \r\n{0}", oEx.Message));
			}
		}

		private void btnSOSAlert_Click(object sender, RoutedEventArgs e)
		{
			/** Initialize. */
			var objectSOS = new ClientResponseSOS();

			BindData(objectSOS);
			try
			{
				var sentence = objectSOS.GetSentence();
				AddCommandToMainConsole(sentence);
			}
			catch (Exception oEx)
			{
				MessageBox.Show(string.Format("The follwong error occurred: \r\n{0}", oEx.Message));
			}

		}

		private void BtTamperAlert_Click(object sender, RoutedEventArgs e)
		{
			/** Initialize. */
			var objectBTA = new ClientResponseBTA();

			BindData(objectBTA);
			try
			{
				var sentence = objectBTA.GetSentence();
				AddCommandToMainConsole(sentence);
			}
			catch (Exception oEx)
			{
				MessageBox.Show(string.Format("The follwong error occurred: \r\n{0}", oEx.Message));
			}
		}

		private void BtLowBatteryAlert_Click(object sender, RoutedEventArgs e)
		{
			/** Initialize. */
			var objectBTA = new ClientResponseLBA();

			BindData(objectBTA);
			try
			{
				var sentence = objectBTA.GetSentence();
				AddCommandToMainConsole(sentence);
			}
			catch (Exception oEx)
			{
				MessageBox.Show(string.Format("The follwong error occurred: \r\n{0}", oEx.Message));
			}
		}
		
		private void BtFallAlertClick(object sender, RoutedEventArgs e)
		{
			/** Initialize. */
			var objectBTA = new ClientResponseFDA();

			BindData(objectBTA);
			try
			{
				var sentence = objectBTA.GetSentence();
				AddCommandToMainConsole(sentence);
			}
			catch (Exception oEx)
			{
				MessageBox.Show(string.Format("The follwong error occurred: \r\n{0}", oEx.Message));
			}

		}

		private void BtFenceExitAlertClick(object sender, RoutedEventArgs e)
		{
			/** Initialize. */
			var objectBTA = new ClientResponseIZB();

			BindData(objectBTA);
			try
			{
				var sentence = objectBTA.GetSentence();
				AddCommandToMainConsole(sentence);
			}
			catch (Exception oEx)
			{
				MessageBox.Show(string.Format("The follwong error occurred: \r\n{0}", oEx.Message));
			}
		}

		private void BtFenceEnterAlert_Click(object sender, RoutedEventArgs e)
		{
			/** Initialize. */
			var objectBTA = new ClientResponseEZB();

			BindData(objectBTA);
			try
			{
				var sentence = objectBTA.GetSentence();
				AddCommandToMainConsole(sentence);
			}
			catch (Exception oEx)
			{
				MessageBox.Show(string.Format("The follwong error occurred: \r\n{0}", oEx.Message));
			}
		}

		#endregion Events
	}
}
