using NXS.Data.AuthenticationControl;
using System;

namespace NXS.DataServices.AuthenticationControl.Models
{
	public class GroupActionItem
	{
		public int ID { get; set; }
		public string GroupName { get; set; }
		public string RefType { get; set; }
		public string RefId { get; set; }
		//public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }

		internal static GroupActionItem FromDb(AC_GroupAction item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("GroupAction is null");
			}

			var result = new GroupActionItem();
			result.ID = item.ID;
			result.GroupName = item.GroupName;
			result.RefType = "Actions";
			result.RefId = item.ActionId;
			//result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			result.CreatedBy = item.CreatedBy;
			result.CreatedOn = item.CreatedOn;
			result.ModifiedBy = item.ModifiedBy;
			result.ModifiedOn = item.ModifiedOn;
			return result;
		}
		internal static GroupActionItem FromDb(AC_GroupApplication item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("GroupApplication is null");
			}

			var result = new GroupActionItem();
			result.ID = item.ID;
			result.GroupName = item.GroupName;
			result.RefType = "Applications";
			result.RefId = item.ApplicationId;
			//result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			result.CreatedBy = item.CreatedBy;
			result.CreatedOn = item.CreatedOn;
			result.ModifiedBy = item.ModifiedBy;
			result.ModifiedOn = item.ModifiedOn;
			return result;
		}


		internal void ToDb(AC_GroupAction item)
		{
			if (item.ID != this.ID)
				throw new Exception("IDs don't match");
			if (this.RefType != "Actions")
				throw new Exception("Unexpected RefType");

			item.ID = this.ID;
			item.GroupName = this.GroupName;
			item.ActionId = this.RefId;
			//item.IsActive = this.IsActive;
			item.IsDeleted = this.IsDeleted;
			//item.ModifiedOn = this.ModifiedOn;
			//item.ModifiedBy = this.ModifiedBy;
			//item.CreatedOn = this.CreatedOn;
			//item.CreatedBy = this.CreatedBy;
		}
		internal void ToDb(AC_GroupApplication item)
		{
			if (item.ID != this.ID)
				throw new Exception("IDs don't match");
			if (this.RefType != "Applications")
				throw new Exception("Unexpected RefType");

			item.ID = this.ID;
			item.GroupName = this.GroupName;
			item.ApplicationId = this.RefId;
			//item.IsActive = this.IsActive;
			item.IsDeleted = this.IsDeleted;
			//item.ModifiedOn = this.ModifiedOn;
			//item.ModifiedBy = this.ModifiedBy;
			//item.CreatedOn = this.CreatedOn;
			//item.CreatedBy = this.CreatedBy;
		}
	}
}
