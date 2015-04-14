using NXS.Data.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm.Models
{
	public class AeContract
	{
		public int ID { get; set; }
		public int ContractTemplateId { get; set; }
		//public long? AccountId { get; set; }
		//public string ContractName { get; set; }
		//public short ContractLength { get; set; }
		//public DateTime EffectiveDate { get; set; }
		//public decimal MonthlyFee { get; set; }
		//public string ShortDesc { get; set; }
		//public bool IsActive { get; set; }
		//public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		//public DateTime CreatedOn { get; set; }
		//public string CreatedBy { get; set; }

		internal static AeContract FromDb(AE_Contract item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("invoice item is null");
			}

			var result = new AeContract();
			result.ID = item.ID;
			result.ContractTemplateId = item.ContractTemplateId;
			//result.AccountId = item.AccountId;
			//result.ContractName = item.ContractName;
			//result.ContractLength = item.ContractLength;
			//result.EffectiveDate = item.EffectiveDate;
			//result.MonthlyFee = item.MonthlyFee;
			//result.ShortDesc = item.ShortDesc;
			//result.IsActive = item.IsActive;
			//result.IsDeleted = item.IsDeleted;
			result.ModifiedOn = item.ModifiedOn;
			result.ModifiedBy = item.ModifiedBy;
			//result.CreatedOn = item.CreatedOn;
			//result.CreatedBy = item.CreatedBy;
			return result;
		}

		//internal void ToDb(AE_Contract item)
		//{
		//	if (item.ID != this.ID)
		//		throw new Exception("IDs don't match");
		//	NXS.Data.VersionException.CheckModifiedOn(item.ModifiedOn, this.ModifiedOn);
		//
		//	item.ID = this.ID;
		//	item.ContractTemplateId = this.ContractTemplateId;
		//	//item.AccountId = this.AccountId;
		//	//item.ContractName = this.ContractName;
		//	//item.ContractLength = this.ContractLength;
		//	//item.EffectiveDate = this.EffectiveDate;
		//	//item.MonthlyFee = this.MonthlyFee;
		//	//item.ShortDesc = this.ShortDesc;
		//	//item.IsActive = this.IsActive;
		//	//item.IsDeleted = this.IsDeleted;
		//	//item.ModifiedOn = this.ModifiedOn;
		//	//item.ModifiedBy = this.ModifiedBy;
		//	//item.CreatedOn = this.CreatedOn;
		//	//item.CreatedBy = this.CreatedBy;
		//}
	}
}
