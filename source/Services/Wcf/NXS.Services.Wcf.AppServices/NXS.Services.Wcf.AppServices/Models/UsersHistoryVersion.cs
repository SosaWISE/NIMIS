using System;
using System.Runtime.Serialization;
using SOS.Data.HumanResource;

namespace NXS.Services.Wcf.AppServices.Models
{
	[DataContract]
	public class UsersHistoryVersion
	{
		public UsersHistoryVersion(HumanResourceDataContext recruitDB, RU_UsersHistory item)
		{
			UserHistoryID = item.UserHistoryID;
			HistoryDate = item.HistoryDate;
			ModifiedBy = HistoryHelper.GetUserEditorDisplayName(recruitDB, item.ModifiedBy);
		}

		[DataMember]
		public long UserHistoryID { get; set; }

		[DataMember]
		public DateTime HistoryDate { get; set; }

		[DataMember]
		public string ModifiedBy { get; set; }
	}
}
