using NXS.Data.AuthenticationControl;
using System;

namespace NXS.DataServices.AuthenticationControl.Models
{
	public class AcActionRequestNew
	{
		//public int ID { get; set; }
		public string ApplicationId { get; set; }
		public string ActionId { get; set; }
		public int UserId { get; set; }
		public int RequestReasonId { get; set; }
		public string RequestReason { get; set; }
		public string OnBehalfOf { get; set; }
		public string ActionKey { get; set; }

		internal void ToDb(AC_ActionRequest item)
		{
			//if (item.ID != this.ID) throw new Exception("IDs don't match");
			//item.ID = this.ID;
			item.ApplicationId = this.ApplicationId;
			item.ActionId = this.ActionId;
			item.UserId = this.UserId;
			item.RequestReasonId = this.RequestReasonId;
			item.RequestReason = this.RequestReason;
			item.OnBehalfOf = this.OnBehalfOf;
			item.ActionKey = this.ActionKey;
		}
	}

	public class AcActionRequest
	{
		public static class Statuses
		{
			public const string Pending = "pending";
			public const string Denied = "denied";
			public const string Approved = "approved";
			public const string Expired = "expired";
			public const string Used = "used";
		}

		public string Status { get; set; } // calculated field
		public string Token { get; set; } // unhashed ActionKey, should only be set after creation
		//
		//
		//
		public int ID { get; set; }
		public string ApplicationId { get; set; }
		public string ActionId { get; set; }
		public int UserId { get; set; }
		public int RequestReasonId { get; set; }
		public string RequestReason { get; set; }
		public string OnBehalfOf { get; set; }
		public string ActionKey { get; set; } 
		public DateTime? SignedOn { get; set; }
		public string SignedBy { get; set; }
		public int? DeniedReasonId { get; set; }
		public string DeniedReason { get; set; }
		public DateTime? UsedOn { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		public string Username { get; set; }

		// should not be exposed in api
		public bool ShouldSerializeID() { return false; }
		public bool ShouldSerializeUserId() { return false; }
		public bool ShouldSerializeActionKey() { return false; }

		internal static AcActionRequest FromDb(AC_ActionRequest item, string status, string token = null)
		{
			if (item == null) throw new Exception("item is null");

			var result = new AcActionRequest();
			result.Status = status;
			result.Token = token;
			//
			//
			//
			result.ID = item.ID;
			result.ApplicationId = item.ApplicationId;
			result.ActionId = item.ActionId;
			result.UserId = item.UserId;
			result.RequestReasonId = item.RequestReasonId;
			result.RequestReason = item.RequestReason;
			result.OnBehalfOf = item.OnBehalfOf;
			result.ActionKey = item.ActionKey;
			result.SignedOn = item.SignedOn;
			result.SignedBy = item.SignedBy;
			result.DeniedReasonId = item.DeniedReasonId;
			result.DeniedReason = item.DeniedReason;
			result.UsedOn = item.UsedOn;
			result.ModifiedOn = item.ModifiedOn;
			result.ModifiedBy = item.ModifiedBy;
			result.CreatedOn = item.CreatedOn;
			result.CreatedBy = item.CreatedBy;

			result.Username = item.Username;

			return result;
		}
	}
}
