using SOS.Lib.KW621API.ExceptionHandling;

namespace SOS.Lib.KW621API.Commands
{
	public class Requests
	{
		#region .ctor
		public Requests(string sPassword)
		{
			if (sPassword.Length != PASSWORD_LENGTH)
				throw new KW621ExceptionsFixedLengthException(string.Format(INVALID_LENGTH_MSG));
			Password = sPassword;
		}

		#endregion .ctor

		#region Memeber Properties

		public string Password { get; private set; }

		#endregion Memeber Properties

		#region Commands

		internal const string INVALID_LENGTH_MSG =
			"The password is invalid";

		internal const int PASSWORD_LENGTH = 4;
		internal const string DEFAULT_BLANK_PASSWRD = "XXXX";

		#endregion Commands
	}
}
