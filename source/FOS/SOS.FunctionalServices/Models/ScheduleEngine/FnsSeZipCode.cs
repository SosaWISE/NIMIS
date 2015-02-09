using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.ScheduleEngine;

namespace SOS.FunctionalServices.Models.ScheduleEngine
{
	public class FnsSeZipCode : IFnsSeZipCode
	{
		#region .ctor
        public FnsSeZipCode(SE_ZipCode zipCode)
		{
            ZipCode = zipCode.ZipCode;
            Latitude = zipCode.Latitude;
            Longitude = zipCode.Longitude;
            PrimaryCity = zipCode.PrimaryCity;
            State = zipCode.State;
       
		}




		#endregion .ctor

		#region Properties
        public string ZipCode { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string PrimaryCity { get; set; }

        public string State { get; set; }


		#endregion Properties
	}
}
