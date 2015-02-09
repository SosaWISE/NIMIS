using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models
{
	public class SseCmsUser
	{
		#region Properties

		[DataMember]
		public int UserID { get; set; }

		[DataMember]
		public int? HRUserID { get; set; }

		[DataMember]
		public int DealerId { get; set; }

		[DataMember]
		public Guid? Ssid { get; set; }

		[DataMember]
		public long SessionID { get; set; }

		[DataMember]
		public string Username { get; set; }

		[DataMember]
		public string Firstname { get; set; }

		[DataMember]
		public string Lastname { get; set; }

		[DataMember]
		public string GPEmployeeID { get; set; }


        [DataMember]
        public string UserEmployeeTypeID { get; set; }
        [DataMember]
        public string UserEmployeeTypeName { get; set; }
        
        [DataMember]
		public byte? SecurityLevel { get; set; }

		[DataMember]
		public List<string> Apps { get; set; }
		[DataMember]
		public List<string> Actions { get; set; }


		#endregion Properties
	}
}