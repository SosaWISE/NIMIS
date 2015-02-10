using SOS.Lib.Util.Configuration;

namespace SSE.Services.Console.SocketServerV1.Servers
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

		public const int ATTEMP_NUMBER = 5;
		private int _attemptNumberPerCmd;
		public int AttemptNumberPerCmd
		{
			get
			{
				if (_attemptNumberPerCmd == 0)
				{
					/** Initialize. */
					int result;
					_attemptNumberPerCmd =
						!int.TryParse(ConfigurationSettings.Current.GetConfig("PUSH_SERVER.AttemptNumberPerCmd"), out result)
							? ATTEMP_NUMBER
							: result;
				}

				/** Return result. */
				return _attemptNumberPerCmd;
			}
		}

		#endregion Member Variables
	}
}
