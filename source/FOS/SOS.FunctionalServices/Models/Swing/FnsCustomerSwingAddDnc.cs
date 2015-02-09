using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Swing;

namespace SOS.FunctionalServices.Models.Swing
{
    public class FnsCustomerSwingAddDnc : IFnsCustomerSwingAddDnc
    {
        #region .ctor

        public FnsCustomerSwingAddDnc() { }

        public FnsCustomerSwingAddDnc(Action<IFnsCustomerSwingAddDnc, object> fxBindData, object value)
        {
            if (fxBindData == null)
                throw new NotImplementedException();

            fxBindData(this, value);
        }

        public FnsCustomerSwingAddDnc(AE_CustomerSWINGAdd_DncView item)
        {
            Dnc_Status = item.Dnc_Status;
            
        }

        #endregion .ctor

        #region Properties
        public string Dnc_Status { get; set; }        
        #endregion Properties

    }
}
