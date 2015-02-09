using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models
{
	public static partial class McModels
	{
		public interface IDealerUser
		{
			[DataMember]
			int DealerUserID { get; set; }

			[DataMember]
			byte DealerUserTypeId { get; set; }

			[DataMember]
			string DealerUserType { get; set; }
			
			[DataMember]
			int DealerId { get; set; }

			[DataMember]
			int? AuthUserId { get; set; }

			[DataMember]
			string UserID { get; set; }

			[DataMember]
			string Firstname { get; set; }

			[DataMember]
			string Middlename { get; set; }

			[DataMember]
			string Lastname { get; set; }

			[DataMember]
			string FullName { get; set; }

			[DataMember]
			string Email { get; set; }

			[DataMember]
			string PhoneWork { get; set; }

			[DataMember]
			string PhoneCell { get; set; }

			[DataMember]
			string ADUsername { get; set; }

			[DataMember]
			string Username { get; set; }

			[DataMember]
			string Password { get; set; }

			[DataMember]
			DateTime? LastLoginOn { get; set; }

			[DataMember]
			bool IsActive { get; set; }

			[DataMember]
			bool IsDeleted { get; set; }

			[DataMember]
			DateTime ModifiedOn { get; set; }

			[DataMember]
			string ModifiedBy { get; set; }

			[DataMember]
			DateTime CreatedOn { get; set; }

			[DataMember]
			string CreatedBy { get; set; }
		}

		public class DealerUser : IDealerUser
		{
			#region Implementation of IDealerUser

			public int DealerUserID { get; set; }
			public byte DealerUserTypeId { get; set; }
			public string DealerUserType { get; set; }
			public int DealerId { get; set; }
			public int? AuthUserId { get; set; }
			public string UserID { get; set; }
			public string Firstname { get; set; }
			public string Middlename { get; set; }
			public string Lastname { get; set; }
			public string FullName { get; set; }
			public string Email { get; set; }
			public string PhoneWork { get; set; }
			public string PhoneCell { get; set; }
			public string ADUsername { get; set; }
			public string Username { get; set; }
			public string Password { get; set; }
			public DateTime? LastLoginOn { get; set; }
			public bool IsActive { get; set; }
			public bool IsDeleted { get; set; }
			public DateTime ModifiedOn { get; set; }
			public string ModifiedBy { get; set; }
			public DateTime CreatedOn { get; set; }
			public string CreatedBy { get; set; }

			#endregion Implementation of IDealerUser
		}
	}
}