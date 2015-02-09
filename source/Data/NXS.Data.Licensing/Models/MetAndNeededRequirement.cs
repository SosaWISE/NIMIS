using System.Runtime.Serialization;

namespace NXS.Data.Licensing.Models
{
	[DataContract]
	public class MetAndNeededRequirement
	{
		[DataMember]
		public int LicenseID { get; set; }
		[DataMember]
		public int RequirementID { get; set; }
		[DataMember]
		public string RequirementName { get; set; }
		[DataMember]
		public int LockID { get; set; }
		[DataMember]
		public string LicenseNumber { get; set; }
		[DataMember]
		public string ExpirationDate { get; set; }
		[DataMember]
		public string LocationName { get; set; }
		[DataMember]
		public bool RequirementsAreMet { get; set; }
	}
}
