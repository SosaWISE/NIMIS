using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.DocBarcode;
using System;

namespace SOS.FunctionalServices.Models.DocBarcode
{
    public class FnsBxBarcode : IFnsBxBarcode
    {
        #region .ctor

		public FnsBxBarcode()
		{
		}

        public FnsBxBarcode(BX_Barcode barcodeInfo)
		{
            BarcodeID = barcodeInfo.BarcodeID;
            BarcodeTypeId = barcodeInfo.BarcodeTypeId;
            ForeignKey = barcodeInfo.ForeignKey;
            BarcodeNumber = barcodeInfo.BarcodeNumber;
		}

		#endregion .ctor

        #region Properties
        public long BarcodeID { get; set; }
        public string BarcodeTypeId { get; set; }
        public string ForeignKey { get; set; }
        public string BarcodeNumber { get; set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedOn { get; private set; }
        #endregion Properties
    }
}
