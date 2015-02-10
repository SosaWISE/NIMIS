using SOS.Lib.Util.Configuration;

namespace SOS.Clients.Console.KW621SocketServer.Servers
{
	public class PushServerBase
	{
		#region Member Variables

		public const int PAUSE_PERIOD = 30;
		private int _pausePeriodInSeconds;
		public int PausePeriodInSeconds
		{
			get
			{
				if (_pausePeriodInSeconds == 0)
				{
					/** Initialize. */
					int result;
					_pausePeriodInSeconds = !int.TryParse(ConfigurationSettings.Current.GetConfig("PUSH_SERVER.PausePeriodInSeconds"), out result)
						? PAUSE_PERIOD
						: result;
				}

				/** Return result. */
				return _pausePeriodInSeconds;
			}
		}

		//<add key="PUSH_SERVER.AttemptNumberPerCmd" value="5" />
		public const int ATTEMP_NUMBER = 5;
		private int _attempNumberPerCmd;
		public int AttemptNumberPerCmd
		{
			get
			{
				if (_attempNumberPerCmd == 0)
				{
					/** Initialize. */
					int result;
					_attempNumberPerCmd = !int.TryParse(ConfigurationSettings.Current.GetConfig("PUSH_SERVER.AttemptNumberPerCmd"), out result)
						? ATTEMP_NUMBER
						: result;
				}
				/** Return result. */
				return _attempNumberPerCmd;
			}
		}

		#endregion Member Variables
	}
}
