using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.DoNotCall
{
    public class DncPhoneNumber
    {
		[DataMember]
		public string PhoneNumberID { get; set; }

        [DataMember]
        public string AreaCodeId { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }
    }
}