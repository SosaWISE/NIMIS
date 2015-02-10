using System;
using System.Collections.Generic;
using SOS.Data.HumanResource;
using SOS.Lib.Util.Cryptography;

namespace NXS.Services.Wcf.AppServices
{
	public static class HistoryHelper
	{
		public const string UNKNOWN = "[Unknown]";
		private static Dictionary<byte, string> _hatSizeDict;
		private static readonly object HatSizeLockObj = new object();
		private static Dictionary<byte, string> _shirtSizeDict;
		private static readonly object ShirtSizeLockObj = new object();
		private static Dictionary<byte, string> _sexDict;
		private static readonly object SexLockObj = new object();

		public static string GetMaritalStatusDisplayText(bool? maritalStatus)
		{
			string result;

			if (maritalStatus == null)
			{
				result = UNKNOWN;
			}
			else
			{
				result = maritalStatus.Value ? "Married" : "Single";
			}

			return result;
		}

		public static string GetDecryptedText(string encryptedValue)
		{
			return DecryptString(encryptedValue);
		}

		public static string DecryptString(string value)
		{
			// Locals 
			string szResult = string.Empty;

			// Check to see that the object is not null
			if (value != null && !value.Equals(string.Empty))
			{
				// Check to see if the string is encrypted.
				try
				{
					szResult = TripleDES.DecryptString(value, null);

					// Check that there was no other error that was handled
					if (szResult.IndexOf("Error") == 0)
					{
						szResult = value;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("An error occured when trying to decrypt value.  {0}", ex.Message);

					szResult = value;
				}
			}
			return szResult;
		}

		public static string GetUserEditorDisplayName(HumanResourceDataContext recruitDB, string editorID)
		{
			string sxResult;

			int userID;
			if (!int.TryParse(editorID, out userID))
			{
				//return text if not an int
				//TODO: format in a uniform manner
				sxResult = editorID;
			}
			else
			{
				RU_User item = recruitDB.RU_Users.LoadByPrimaryKey(userID);
				return item == null ? UNKNOWN : string.Format("{0} ({1})", item.FullName, GetUserID(item.UserID));
			}

			return sxResult;
		}

		public static string GetCellCarrierDisplayName(HumanResourceDataContext recruitDB, short? id)
		{
			string szResult;

			if (id == null)
			{
				szResult = string.Empty;
			}
			else
			{
				RU_PhoneCellCarrier item = recruitDB.RU_PhoneCellCarriers.LoadByPrimaryKey(id.Value);
				return item == null ? UNKNOWN : item.Description;
			}

			return szResult;
		}

		public static string GetByteValue(Dictionary<byte, string> dict, byte? id)
		{
			string szResult;

			if (id == null)
			{
				szResult = string.Empty;
			}
			else
			{
				string text;
				szResult = !dict.TryGetValue(id.Value, out text) ? UNKNOWN : text;
			}

			return szResult;
		}

		public static string GetDisplayHatSize(byte? hatSizeID)
		{
			if (_hatSizeDict == null)
			{
				lock (HatSizeLockObj)
				{
					if (_hatSizeDict == null)
					{
						_hatSizeDict = new Dictionary<byte, string> { { 1, "S" }, { 2, "M" }, { 3, "L" } };
					}
				}
			}
			return GetByteValue(_hatSizeDict, hatSizeID);
		}

		public static string GetDisplayShirtSize(byte? shirtSizeID)
		{
			if (_shirtSizeDict == null)
			{
				lock (ShirtSizeLockObj)
				{
					if (_shirtSizeDict == null)
					{
						_shirtSizeDict = new Dictionary<byte, string>
						                 	{
						                 		{1, "XXS"},
						                 		{2, "XS"},
						                 		{3, "S"},
						                 		{4, "M"},
						                 		{5, "L"},
						                 		{6, "XL"},
						                 		{7, "XXL"},
						                 		{8, "XXXL"}
						                 	};
					}
				}
			}
			return GetByteValue(_shirtSizeDict, shirtSizeID);
		}

		public static string GetDisplaySex(byte? sexID)
		{
			if (_sexDict == null)
			{
				lock (SexLockObj)
				{
					if (_sexDict == null)
					{
						_sexDict = new Dictionary<byte, string> { { 1, "Male" }, { 2, "Female" } };
					}
				}
			}
			return GetByteValue(_sexDict, sexID);
		}

		public static string GetDateOnlyText(DateTime? date)
		{
			var szResult = date == null ? string.Empty : date.Value.ToString("MM/dd/yyyy");

			return szResult;
		}

		public static string GetDateAndTimeText(DateTime? date)
		{
			var szResult = date == null ? string.Empty : date.Value.ToString("MM/dd/yyyy hh:mm:ss tt");

			return szResult;
		}

		public static string GetUserFullName(HumanResourceDataContext recruitDB, int? id)
		{
			string szResult;

			if (id == null)
			{
				szResult = string.Empty;
			}
			else
			{
				RU_User item = recruitDB.RU_Users.LoadByPrimaryKey(id.Value);
				return item == null ? UNKNOWN : string.Format("{0} ({1})", item.FullName, GetUserID(item.UserID));
			}

			return szResult;
		}

		public static string GetSeasonDisplayName(HumanResourceDataContext recruitDB, int id)
		{
			RU_Season item = recruitDB.RU_Seasons.LoadByPrimaryKey(id);
			var szResult = item == null ? UNKNOWN : string.Format("{0} ({1})", item.SeasonName, item.SeasonID);

			return szResult;
		}

		public static string GetRecruitCohabbitTypeDisplayName(HumanResourceDataContext recruitDB, int? id)
		{
			string szResult;

			if (id == null)
			{
				szResult = string.Empty;
			}
			else
			{
				RU_RecruitCohabbitType item = recruitDB.RU_RecruitCohabbitTypes.LoadByPrimaryKey(id.Value);
				szResult = item == null ? UNKNOWN : item.RecruitCohabbitType;
			}

			return szResult;
		}

		public static string GetRecruitFullName(HumanResourceDataContext recruitDB, int? id)
		{
			string result;

			if (id == null)
			{
				result = string.Empty;
			}
			else
			{
				RU_Recruit item = recruitDB.RU_Recruits.LoadByPrimaryKey(id.Value);
				if (item == null)
				{
					result = UNKNOWN;
				}
				else
				{
					//RU_User user = recruitDB.RU_Users.LoadByPrimaryKey(item.UserID);
					RU_User user = item.User;
					result = string.Format("{0} ({1})", user.FullName, GetRecruitID(item.RecruitID));
				}
			}

			return result;
		}

		public static string GetUserID(int userID)
		{
			return string.Concat("U" + userID.ToString().PadLeft(5, '0'));
		}

		public static string GetRecruitID(int recruitID)
		{
			return string.Concat("R" + recruitID.ToString().PadLeft(5, '0'));
		}

		public static string GetUserTypeDisplayName(HumanResourceDataContext recruitDB, short id)
		{
			RU_UserType item = recruitDB.RU_UserTypes.LoadByPrimaryKey(id);
			var szResult = item == null ? UNKNOWN : string.Format("{0} ({1})", item.Description, item.UserTypeID);

			return szResult;
		}

		public static string GetDocStatusEnumDisplayName(int? id)
		{
			string result = id == null ? string.Empty : "";

			return result;
		}

		public static string GetMoneyDisplayValue(decimal? value)
		{
			string result = value == null ? string.Empty : string.Format("{0:C}", value.Value);

			return result;
		}

		public static string GetPayScaleDisplayName(HumanResourceDataContext recruitDB, int? id)
		{
			string result;

			if (id == null)
			{
				result = string.Empty;
			}
			else
			{
				RU_Payscale item = recruitDB.RU_Payscales.LoadByPrimaryKey(id.Value);
				result = item == null ? UNKNOWN : string.Format("{0} ({1})", item.Name, item.PayscaleID);
			}

			return result;
		}


		public static string GetTeamDisplayName(HumanResourceDataContext recruitDB, int? id)
		{
			string result;

			if (id == null)
			{
				result = string.Empty;
			}
			else
			{
				RU_Team item = recruitDB.RU_Teams.LoadByPrimaryKey(id.Value);
				if (item == null)
				{
					result = UNKNOWN;
				}
				else
				{
					RU_TeamLocation teamLoc = item.TeamLocation;
					result = string.Format("{0} ({1}) in {2} Office ({3})", item.Description, item.TeamID, teamLoc.Description,
										   teamLoc.TeamLocationID);
				}
			}

			return result;
		}

		public static string GetSchoolDisplayName(HumanResourceDataContext recruitDB, short? id)
		{
			string result;

			if (id == null)
			{
				result = string.Empty;
			}
			else
			{
				RU_School item = recruitDB.RU_Schools.LoadByPrimaryKey(id.Value);
				result = item == null ? UNKNOWN : string.Format("{0} ({1})", item.SchoolName, item.SchoolId);
			}

			return result;
		}

		public static string GetDocStatusDisplayName(HumanResourceDataContext recruitDB, int? id)
		{
			string result;

			if (id == null)
			{
				result = string.Empty;
			}
			else
			{
				RU_DocStatus item = recruitDB.RU_DocStatuses.LoadByPrimaryKey(id.Value);
				result = item == null ? UNKNOWN : item.Description;
			}

			return result;
		}

		public static string GetUserEmployeeTypeNameFromID(HumanResourceDataContext recruitDB, string userEmployeeTypeID)
		{
			RU_UserEmployeeType userEmployeeType = recruitDB.RU_UserEmployeeTypes.LoadByPrimaryKey(userEmployeeTypeID);
			return userEmployeeType != null ? userEmployeeType.UserEmployeeTypeName : null;
		}
	}

}
