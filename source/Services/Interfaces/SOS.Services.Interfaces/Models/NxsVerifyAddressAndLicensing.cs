using System.Collections.Generic;
using SOS.Services.Interfaces.Models.Licensing;

namespace SOS.Services.Interfaces.Models
{
	public class NxsVerifyAddressAndLicensing
	{
		public QualifyLead.QlAddress VerifiedAddress { get; set; }
		public List<LmSalesRepRequirement> SalesLicReq { get; set; } 
	}
}
