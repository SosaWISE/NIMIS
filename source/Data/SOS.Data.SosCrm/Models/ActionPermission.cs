using System.Runtime.Serialization;

namespace SOS.Data.SosCrm.Models
{
	[DataContract]
	public class ActionPermission
	{
		[DataMember]
		public int PermissionTypeID { get; set; }
		[DataMember]
		public string ActionName { get; set; }
		[DataMember]
		public string PrincipalName { get; set; }
		[DataMember]
		public bool AllowAccess { get; set; }
		[DataMember]
		public bool IsOverrideable { get; set; }

		public override string ToString()
		{
			return string.Format("{0}-{1}-{2}-{3}-{4}", ActionName, PermissionTypeID, PrincipalName, (AllowAccess ? "Allow" : "Deny"), (IsOverrideable ? "Overrideable" : "NoOverride"));
		}

	}
}