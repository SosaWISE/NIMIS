using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.DocBarcode
{
    public class BxBarcode
    {
        [DataMember]
        public long BarcodeID { get; set; }

        [DataMember]
        public string BarcodeTypeId { get; set; }

        [DataMember]
        public string ForeignKey { get; set; }

        [DataMember]
        public string BarcodeNumber { get; set; }
    }
}
