namespace SSE.Lib.HE910API.Helper
{
	public class MessageState
	{
		#region .ctor

		private MessageState(string messageStateValue)
		{
			Value = messageStateValue;
		}

		#endregion .ctor

		#region Properties

		public string Value { get; private set; }

		public static MessageState RealTime
		{
			get
			{
				return new MessageState("R");
			}
		}

		public static MessageState DataLogged
		{
			get
			{
				return new MessageState("D");
			}
		}

		#endregion Properties
	}
}
