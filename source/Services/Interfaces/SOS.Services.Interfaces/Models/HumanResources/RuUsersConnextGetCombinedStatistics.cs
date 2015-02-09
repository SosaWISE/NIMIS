using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
    public class RuUsersConnextGetCombinedStatistics : IRuUsersGetCombinedStatisticsConnext
    {
        public List<RuUsersConnextGetDetailedStatistics> OfficeStats { get; set; }
        public List<RuUsersConnextGetDetailedStatistics> RepStats { get; set; }

    }

    public interface IRuUsersGetCombinedStatisticsConnext
	{
        [DataMember]
        List<RuUsersConnextGetDetailedStatistics> OfficeStats { get; set; }

        [DataMember]
        List<RuUsersConnextGetDetailedStatistics> RepStats { get; set; }
        
    }
}
