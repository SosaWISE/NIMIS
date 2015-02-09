using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.InventoryEngine
{
    public class IeLocationType : IIeLocationType
	{
        public string LocationTypeID { get; set; }

        public string LocationTypeName { get; set; }

        public string TableName { get; set; }

        public string FieldName { get; set; }

        public string Comment { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
	}

    public interface IIeLocationType
	{

        [DataMember]
        string LocationTypeID { get; set; }

        [DataMember]
        string LocationTypeName { get; set; }

    
        [DataMember]
        bool IsDeleted { get; set; }

        [DataMember]
        DateTime ModifiedOn { get; set; }

        [DataMember]
        string ModifiedBy { get; set; }

        [DataMember]
        DateTime CreatedOn { get; set; }

        [DataMember]
        string CreatedBy { get; set; }
	}
}
