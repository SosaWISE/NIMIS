using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
{
	public interface IFnsSalesRepInfo
	{
		[DataMember]
		int UserID { get; set; }
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
		List<IFnsSeason> Seasons { get; set; }

	}
}