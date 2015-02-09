using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.DoNotCall;

namespace SOS.FunctionalServices.Models.DoNotCall
{
    public class FnsDncPhoneNumber : IFnsDncPhoneNumber
    {
	    public FnsDncPhoneNumber()
	    {
	    }

	    public FnsDncPhoneNumber(DC_PhoneNumber phoneNumber)
	    {
		    PhoneNumberID = phoneNumber.PhoneNumberID;
		    AreaCodeId = phoneNumber.AreaCodeId;
            PhoneNumber = phoneNumber.PhoneNumber;
		}

        #region Properties
		public string PhoneNumberID { get; set; }
        public string AreaCodeId { get; set; }
        public string PhoneNumber { get; set; }

	    #endregion Properties
    }
}
