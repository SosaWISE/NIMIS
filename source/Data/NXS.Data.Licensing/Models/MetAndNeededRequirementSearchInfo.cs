using System.Runtime.Serialization;

namespace NXS.Data.Licensing.Models
{
	[DataContract]
	public class MetAndNeededRequirementSearchInfo
	{
		[DataMember]
		public string CompanyID { get; set; }
		[DataMember]
		public LM_RequirementType.RequirementTypeEnum RequirementType { get; set; }
		[DataMember]
		public string LocationName { get; set; }
	}
}
