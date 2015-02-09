using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
	public class RuTechInfo : IRuTechInfo
	{
		#region Properties
		public int UserID { get; set; }
		public long MsAccountId { get; set; }
		public string CompanyID { get; set; }
		public int TeamLocationId { get; set; }
		public string DefaultOfficeName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string CompanyName { get; set; }
		public string UserName { get; set; }
		public DateTime? BirthDate { get; set; }
		public string HomeTown { get; set; }
		public string Sex { get; set; }
		public string ShirtSize { get; set; }
		public string HatSize { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneCell { get; set; }
		public string Email { get; set; }
		public string SSN { get; set; }
		public string ImagePath { get; set; }
		public List<IRuSeason> Seasons { get; set; }

		#endregion Properties
	}

	public interface IRuTechInfo
	{
		[DataMember]
		int UserID { get; set; }
		[DataMember]
		long MsAccountId { get; set; }  // Used to post a Tech to an account.
		[DataMember]
		string CompanyID { get; set; }
		[DataMember]
		int TeamLocationId { get; set; }
		[DataMember]
		string DefaultOfficeName { get; set; }
		[DataMember]
		string FirstName { get; set; }
		[DataMember]
		string LastName { get; set; }
		[DataMember]
		string CompanyName { get; set; }
		[DataMember]
		string UserName { get; set; }
		[DataMember]
		DateTime? BirthDate { get; set; }
		[DataMember]
		string HomeTown { get; set; }
		[DataMember]
		string Sex { get; set; }
		[DataMember]
		string ShirtSize { get; set; }
		[DataMember]
		string HatSize { get; set; }
		[DataMember]
		string PhoneHome { get; set; }
		[DataMember]
		string PhoneCell { get; set; }
		[DataMember]
		string Email { get; set; }
		[DataMember]
		string SSN { get; set; }
		[DataMember]
		string ImagePath { get; set; }
		[DataMember]
		List<IRuSeason> Seasons { get; set; }
	}
}
