using System;
using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;

namespace SOS.FunctionalServices.Models.HumanResource
{
	public class FnsRuSalesRep : IFnsRuSalesRep
	{
		#region .ctor

		public FnsRuSalesRep(RU_SalesRepsView ruSalesRep)
		{
			SalesRepId = ruSalesRep.SalesRepId;
            FullName = ruSalesRep.FullName;
            RepFirstName = ruSalesRep.RepFirstName;
            RepLastName = ruSalesRep.RepLastName;
            RepBirthDate = ruSalesRep.RepBirthDate;
            RepSeasonId = ruSalesRep.RepSeasonId;
            RepSeasonName = ruSalesRep.RepSeasonName;
			
		}
		#endregion .ctor

		#region Properties

        public string SalesRepId { get; set; }
        public string FullName { get; set; }
        public string RepFirstName { get; set; }
        public string RepLastName { get; set; }
        public DateTime? RepBirthDate { get; set; }
        public int RepSeasonId { get; set; }
        public string RepSeasonName { get; set; }

		#endregion Properties
	}
}
