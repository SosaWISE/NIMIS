// ReSharper disable once CheckNamespace
namespace SOS.Data.HumanResource
{
// ReSharper disable once InconsistentNaming
	public partial class RU_User
	{
		public string StringSex
		{
			get
			{
				return (Sex == 1) ? "Male" : "Female";
			}
		}

		public string StringShirtSize
		{
			get
			{
				switch (ShirtSize)
				{
					case 0:
						return "x-small";
					case 1:
						return "Small";
					case 2:
						return "Medium";
					case 3:
						return "Large";
					case 4:
						return "x-large";
					case 5:
						return "xx-large";
					default:
						return null;
				}
			}
		}

		public string StringHatSize
		{
			get
			{
				switch (HatSize)
				{
					case 0:
						return "x-small";
					case 1:
						return "Small";
					case 2:
						return "Medium";
					case 3:
						return "Large";
					case 4:
						return "x-large";
					case 5:
						return "xx-large";
					default:
						return null;
				}
			}
		}

		public string SSNMask
		{
			get
			{
				if (string.IsNullOrEmpty(SSN)) return null;

				// ** Decrypt the SSN number
				var ssnRaw = Lib.Util.Cryptography.TripleDES.DecryptString(SSN, null);
				ssnRaw = Lib.Util.StringUtility.FormatSsn(ssnRaw);
				ssnRaw = Lib.Util.StringUtility.FormatSsnWithBlanks(ssnRaw);

				return ssnRaw;
			}
		}
	}
}
