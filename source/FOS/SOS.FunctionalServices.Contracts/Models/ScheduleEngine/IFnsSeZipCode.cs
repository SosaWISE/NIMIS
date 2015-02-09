using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.ScheduleEngine
{
	public interface IFnsSeZipCode
    {
        [DataMember]
        string ZipCode { get; set; }

        [DataMember]
        double? Latitude { get; set; }

        [DataMember]
        double? Longitude { get; set; }


        [DataMember]
        string PrimaryCity { get; set; }

        [DataMember]
        string State { get; set; }


	}
}