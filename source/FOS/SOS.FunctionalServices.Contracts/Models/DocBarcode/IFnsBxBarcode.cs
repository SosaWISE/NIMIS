using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.DocBarcode
{
    public interface IFnsBxBarcode
    {
        [DataMember]
        long BarcodeID { get; set; }

        [DataMember]
        string BarcodeTypeId { get; set; }

        [DataMember]
        string ForeignKey { get; set; }

        [DataMember]
        string BarcodeNumber { get; set; }
    }
}
