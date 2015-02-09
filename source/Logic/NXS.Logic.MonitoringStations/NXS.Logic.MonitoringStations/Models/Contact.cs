using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models
{
	public class Contact
	{
		#region Properties

		[XmlAttribute(DataType = "string", AttributeName = "last_name")]
		public string LastName { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "first_name")]
		public string FirstName { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "ctactype_id")]
		public string ContactTypeId { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "relation_id")]
		public string RelationId { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "auth_id")]
		public string AuthId { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "contract_signer_flag")]
		public string ContractSignerFlag { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "has_key_flag")]
		public string HasKeyFlag { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "phone1")]
		public string Phone1 { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "phone2")]
        public string Phone2 { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "phone3")]
        public string Phone3 { get; set; }

		[XmlAttribute(DataType = "string", AttributeName = "phonetype_id1")]
		public string PhoneTypeId1 { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "phonetype_id2")]
        public string PhoneTypeId2 { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "phonetype_id3")]
        public string PhoneTypeId3 { get; set; }


		[XmlAttribute(DataType = "string", AttributeName = "contltype_no")]
		public string ContlTypeNo { get; set; }
        
        [XmlAttribute(DataType = "string", AttributeName = "pin")]
        public string Pin { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "email_address")]
        public string EmailAddress { get; set; }
		#endregion Properties
	}
}
