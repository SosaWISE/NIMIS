using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using System;
using System.Collections.Generic;

namespace SOS.FunctionalServices.Models.HumanResource
{
	public class FnsRuTechnician : IFnsRuTechnician
	{
		#region .ctor

        public FnsRuTechnician(RU_TechniciansView ruTechnician)
		{
            TechnicianId = ruTechnician.TechnicianId;
            FullName = ruTechnician.FullName;
            TechFirstName = ruTechnician.TechFirstName;
            TechLastName = ruTechnician.TechLastName;
            TechBirthDate = ruTechnician.TechBirthDate;
            TechSeasonId = ruTechnician.TechSeasonId;
            TechSeasonName = ruTechnician.TechSeasonName;
			
		}
		#endregion .ctor

		#region Properties

        public string TechnicianId { get; set; }
        public string FullName { get; set; }
        public string TechFirstName { get; set; }
        public string TechLastName { get; set; }
        public DateTime? TechBirthDate { get; set; }
        public int TechSeasonId { get; set; }
        public string TechSeasonName { get; set; }

		#endregion Properties
	}
}
