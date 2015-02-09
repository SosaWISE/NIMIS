using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Swing
{
    public class CustomerSwingEmergencyContact : ICustomerSwingEmergencyContact
    {
        #region Properties
        
        public string FirstName { get; set; }
        public string MiddleInit { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public string PhoneNumber1 { get; set; }
        #endregion Properties
    }

    #region Interface

    public interface ICustomerSwingEmergencyContact
    {

        [DataMember]
        string FirstName { get; set; }

        [DataMember]
        string MiddleInit { get; set; }

        [DataMember]
        string LastName { get; set; }

        [DataMember]
        string Relationship { get; set; }

        [DataMember]
        string PhoneNumber1 { get; set; }

    }

    #endregion Interface
}
