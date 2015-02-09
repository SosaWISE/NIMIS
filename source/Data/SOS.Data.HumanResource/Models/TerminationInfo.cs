using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.Data.HumanResource.Models
{
	[DataContract]
	public class TerminationInfo
	{
		[DataMember]
		public RU_TerminationsWithStatusView TerminationStatusView { get; set; }

		[DataMember]
		public RU_Termination Termination { get; set; }
		[DataMember]
		public List<RU_TerminationNote> Notes { get; set; }

		[DataMember]
		public RU_TerminationStatus NewTerminationStatus { get; set; }
	}
}
