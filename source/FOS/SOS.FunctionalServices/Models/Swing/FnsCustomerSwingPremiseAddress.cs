using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Swing;

namespace SOS.FunctionalServices.Models.Swing
{
	public class FnsCustomerSwingPremiseAddress : IFnsCustomerSwingPremiseAddress
	{
		#region .ctor

		public FnsCustomerSwingPremiseAddress() {}

        public FnsCustomerSwingPremiseAddress(Action<IFnsCustomerSwingPremiseAddress, object> fxBindData, object value)
		{
			if (fxBindData == null)
				throw new NotImplementedException();

			fxBindData(this, value);
		}

        public FnsCustomerSwingPremiseAddress(AE_CustomerSWINGPremiseAddressView item)
		{
           StreetAddress1 = item.StreetAddress1;
           StreetAddress2 = item.StreetAddress2;
	       City = item.City;
           County= item.County;
           PostalCode = item.PostalCode;
           State = item.State;

		}
        
		#endregion .ctor

		#region Properties
      	public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
	    public string City { get; set; }
	    public string County { get; set; }
	    public string PostalCode { get; set; }
	    public string State { get; set; }

		#endregion Properties
      
	}
}
