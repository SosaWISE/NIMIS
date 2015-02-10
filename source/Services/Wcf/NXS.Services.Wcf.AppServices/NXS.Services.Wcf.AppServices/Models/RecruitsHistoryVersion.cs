using System;
using System.Runtime.Serialization;
using SOS.Data.HumanResource;

namespace NXS.Services.Wcf.AppServices.Models
{
	[DataContract]
	public class RecruitsHistoryVersion
	{
		public RecruitsHistoryVersion(HumanResourceDataContext recruitDB, RU_RecruitsHistory item)
		{
			RecruitHistoryID = item.RecruitHistoryID;
			HistoryDate = item.HistoryDate;
			ModifiedBy = HistoryHelper.GetUserEditorDisplayName(recruitDB, item.ModifiedBy);
		}

		[DataMember]
		public long RecruitHistoryID { get; set; }

		[DataMember]
		public DateTime HistoryDate { get; set; }

		[DataMember]
		public string ModifiedBy { get; set; }
	}
}
