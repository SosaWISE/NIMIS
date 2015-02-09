using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.InventoryEngine
{
	public interface IFnsIeLocationType
	{
        [DataMember]
        string LocationTypeID { get; set; }

        [DataMember]
        string LocationTypeName { get; set; }

        [DataMember]
        string TableName { get; set; }


        [DataMember]
        string FieldName { get; set; }

        [DataMember]
        string Comment { get; set; }

        [DataMember]
        bool IsDeleted { get; set; }

       
	}
}