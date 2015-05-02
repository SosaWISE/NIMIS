using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class MsHoldCatg1
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string CatgDescription { get; set; }

		internal static MsHoldCatg1 FromDb(MS_AccountHoldCatg1 item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("hold catg1 is null");
			}

			var result = new MsHoldCatg1();
			result.ID = item.Catg1ID;
			result.Name = item.CatgName;
			result.CatgDescription = item.CatgDescription;
			return result;
		}
	}
}
