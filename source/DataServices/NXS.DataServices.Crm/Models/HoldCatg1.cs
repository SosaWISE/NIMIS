using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class HoldCatg1
	{
		public int Catg1ID { get; set; }
		public string CatgName { get; set; }
		public string CatgDescription { get; set; }

		internal static HoldCatg1 FromDb(MS_AccountHoldCatg1 item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("hold catg1 is null");
			}

			var result = new HoldCatg1();
			result.Catg1ID = item.Catg1ID;
			result.CatgName = item.CatgName;
			result.CatgDescription = item.CatgDescription;
			return result;
		}
	}
}
