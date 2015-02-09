using System;

namespace SOS.Services.Interfaces.Models.CmsModels
{
	#region QlLeadProductOffer

	public interface IQlLeadProductOffer
	{
		long LeadProductOfferedId { get; set; }
		long LeadId { get; set; }
		string ProductSkwId { get; set; }
		string ProductName { get; set; }
		string ShortName { get; set; }
		string ProductTypeName { get; set; }
		string ProductImageName { get; set; }
		string SalesRepId { get; set; }
		string SalesRepFullName { get; set; }
		DateTime OfferDate { get; set; }
	}

	public class QlLeadProductOffer : IQlLeadProductOffer
	{

		#region Implementation of IQlLeadProductOffer

		public long LeadProductOfferedId { get; set; }
		public long LeadId { get; set; }
		public string ProductSkwId { get; set; }
		public string ProductName { get; set; }
		public string ShortName { get; set; }
		public string ProductTypeName { get; set; }
		public string ProductImageName { get; set; }
		public string SalesRepId { get; set; }
		public string SalesRepFullName { get; set; }
		public DateTime OfferDate { get; set; }

		#endregion Implementation of IQlLeadProductOffer
	}

	#endregion QlLeadProductOffer
}
