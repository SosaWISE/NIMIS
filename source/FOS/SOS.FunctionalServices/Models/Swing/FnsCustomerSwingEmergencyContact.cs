using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Swing;

namespace SOS.FunctionalServices.Models.Swing
{
    class FnsCustomerSwingEmergencyContact : IFnsCustomerSwingEmergencyContact
    {

        #region .ctor

		public FnsCustomerSwingEmergencyContact() {}

        public FnsCustomerSwingEmergencyContact(Action<IFnsCustomerSwingEmergencyContact, object> fxBindData, object value)
		{
			if (fxBindData == null)
				throw new NotImplementedException();

			fxBindData(this, value);
		}
        
        public FnsCustomerSwingEmergencyContact(AE_CustomerSWINGEmergencyContactView item)
		{
            FirstName = item.FirstName;
			MiddleInit = item.MiddleInit;
            LastName = item.LastName;
            Relationship = item.Relationship;
			PhoneNumber1 = item.PhoneNumber1;
  
		}
        
		#endregion .ctor

		#region Properties

        public string FirstName { get; set; }
        public string MiddleInit { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public string PhoneNumber1 { get; set; }
	    	
		#endregion Properties

    }
}
