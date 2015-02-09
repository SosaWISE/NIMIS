/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 08/08/12
 * Time: 07:22 am
 * 
 * Description:  Describes an client of our system.
 *********************************************************************************************************************/

using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models
{
	[DataContract]
	public class SosCustomer
	{
		#region Properties

		[DataMember]
		public long SessionID { get; set; }

		[DataMember]
		public long CustomerID { get; set; }

		[DataMember]
		public string CustomerTypeId { get; set; }

		[DataMember]
		public long CustomerMasterFileId { get; set; }

		[DataMember]
		public int DealerId { get; set; }

		[DataMember]
		public string DealerName { get; set; }

		[DataMember]
		public string LocalizationId { get; set; }

		[DataMember]
		public string LocalizationName { get; set; }

		[DataMember]
		public string Prefix { get; set; }

		[DataMember]
		public string Firstname { get; set; }

		[DataMember]
		public string MiddleName { get; set; }

		[DataMember]
		public string Lastname { get; set; }

		[DataMember]
		public string Postfix { get; set; }

		[DataMember]
		public string Gender { get; set; }

		[DataMember]
		public string PhoneHome { get; set; }

		[DataMember]
		public string PhoneWork { get; set; }

		[DataMember]
		public string PhoneCell { get; set; }

		[DataMember]
		public string Email { get; set; }

		[DataMember]
		public DateTime? DOB { get; set; }

		[DataMember]
		public string SSN { get; set; }

		[DataMember]
		public string Username { get; set; }

		[DataMember]
		public DateTime? LastLogin { get; set; }

        [DataMember]
        public string StateId { get; set; }

        [DataMember]
        public string CountryId { get; set; }

        [DataMember]
        public string TimezoneId { get; set; }

        [DataMember]
        public string StreetAddress { get; set; }

        [DataMember]
        public string StreetAddress2 { get; set; }

        [DataMember]
        public string County { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string PlusFour { get; set; }

        [DataMember]
        public string Phone { get; set; }

		#endregion Properties


	}
}
