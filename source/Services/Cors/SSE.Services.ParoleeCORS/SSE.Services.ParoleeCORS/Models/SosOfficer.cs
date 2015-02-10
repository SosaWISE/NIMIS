using System.Runtime.Serialization;

namespace SSE.Services.ParoleeCORS.Models
{
	public class SosOfficer
	{
		#region Officer Properties

		[DataMember]
		public long SessionID { get; set; }

		[DataMember]
		public int OfficerID { get; set; }


		[DataMember]
		public string LocalizationId { get; set; }


		[DataMember]
		public string UserName { get; set; }


		[DataMember]
		public string Password { get; set; }


		[DataMember]
		public string Title	 { get; set; }


		[DataMember]
		public string Department { get; set; }


		[DataMember]
		public string Salutation { get; set; }


		[DataMember]
		public string FirstName { get; set; }


		[DataMember]
		public string MiddleName { get; set; }


		[DataMember]
		public string LastName { get; set; }


		[DataMember]
		public string Suffix { get; set; }


		[DataMember]
		public string OfficePhone { get; set; }



		[DataMember]
		public string MobilePhone { get; set; }


		[DataMember]
		public string HomePhone { get; set; }


		[DataMember]
		public string Pager { get; set; }


		[DataMember]
		public string Fax { get; set; }


		[DataMember]
		public string Email1 { get; set; }


		[DataMember]
		public string Email2 { get; set; }


		[DataMember]
		public string EmailPasswordReset { get; set; }


		[DataMember]
		public string SmsGateway { get; set; }


		[DataMember]
		public string SmsAddress { get; set; }


		[DataMember]
		public string SessionTimeOut { get; set; }


		#endregion Officer Properties
	}
}