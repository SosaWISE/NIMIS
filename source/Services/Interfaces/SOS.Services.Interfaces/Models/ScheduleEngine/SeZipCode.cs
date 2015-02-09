using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.ScheduleEngine
{
    public class SeZipCode : ISeZipCode
	{
        public string ZipCode { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string PrimaryCity { get; set; }

        public string State { get; set; }

     


	}

    public interface ISeZipCode
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
