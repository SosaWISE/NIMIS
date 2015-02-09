using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
    public class MsIndustryAccount : IMsIndustryAccount
    {
        #region Properties
        public long IndustryAccountID { get; set; }

        public long AccountId { get; set; }

        public string ReceiverLineId { get; set; }

        public string ReceiverLineBlockId { get; set; }

	    public string IndustryAccount { get; set; }

	    public string Designator { get; set; }

	    public string SubscriberNumber { get; set; }

	    public string ReceiverNumber { get; set; }

	    public bool IsActive { get; set; }

	    #endregion Properties
    }

    public interface IMsIndustryAccount
    {
        #region Properties

        [DataMember]
        long IndustryAccountID { get; set; }

        [DataMember]
        long AccountId { get; set; }

        [DataMember]
        string ReceiverLineId { get; set; }

        [DataMember]
        string ReceiverLineBlockId { get; set; }

		[DataMember]
		string IndustryAccount { get; set; }

		[DataMember]
		string Designator { get; set; }

		[DataMember]
		string SubscriberNumber { get; set; }

		[DataMember]
		string ReceiverNumber { get; set; }

		[DataMember]
		bool IsActive { get; set; }
		
		#endregion Properties
    }
}
