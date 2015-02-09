using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
	public class RuTeamLocation : IRuTeamLocation
	{
		public int TeamLocationID { get; set; }
		public string City { get; set; }
	}

    public interface IRuTeamLocation
	{
		[DataMember]
        int TeamLocationID { get; set; }

		[DataMember]
        string City { get; set; }
	}
}
