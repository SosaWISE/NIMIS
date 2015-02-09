using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Swing;

namespace SOS.FunctionalServices.Models.Swing
{
    public class FnsCustomerSwingSystemDetails : IFnsCustomerSwingSystemDetails
    {
        #region .ctor

        public FnsCustomerSwingSystemDetails() { }

        public FnsCustomerSwingSystemDetails(Action<IFnsCustomerSwingSystemDetails, object> fxBindData, object value)
        {
            if (fxBindData == null)
                throw new NotImplementedException();

            fxBindData(this, value);
        }

        public FnsCustomerSwingSystemDetails(AE_CustomerSWINGSystemDetailView item)
        {
            ServiceType = item.ServiceType;
            CellularType = item.CellularType;
            PassPhrase = item.PassPhrase;
            PanelType = item.PanelType;
            DslSeizure = item.DslSeizure;
            
        }

        #endregion .ctor

        #region Properties
        public string ServiceType { get; set; }
        public string CellularType { get; set; }
        public string PassPhrase { get; set; }
        public string PanelType { get; set; }
        public string DslSeizure { get; set; }
        
        #endregion Properties

    }
}
