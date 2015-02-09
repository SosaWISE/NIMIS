using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NXS.Data.Licensing.Models
{
	[DataContract]
	public class MetAndNeededRequirementSearchResults
	{
		[DataMember]
		public MetAndNeededRequirementSearchInfo SearchInfo { get; set; }
		[DataMember]
		public IList<MetAndNeededRequirement> ResultsList { get; set; }
	}
}
