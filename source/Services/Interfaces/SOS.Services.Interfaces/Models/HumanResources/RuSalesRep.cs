using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
	public class RuSalesRep : IRuSalesRep
	{
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

	public interface IRuSalesRep
	{
		[DataMember]
		string SalesRepId { get; set; }
		[DataMember]
		string FullName { get; set; }  // Used to post a SalesRep to an account.
		[DataMember]
		string RepFirstName { get; set; }
		[DataMember]
		string RepLastName { get; set; }
		[DataMember]
		DateTime? RepBirthDate { get; set; }
		[DataMember]
		int RepSeasonId { get; set; }
		[DataMember]
		string RepSeasonName { get; set; }
		
	}
}
