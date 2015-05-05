using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class MsHoldCatg2
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public int Catg1Id { get; set; }
		public string CatgDescription { get; set; }
		public bool IsRepFrontEndHold { get; set; }
		public bool IsRepBackEndHold { get; set; }

		internal static MsHoldCatg2 FromDb(MS_AccountHoldCatg2 item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("hold catg2 is null");
			}

			var result = new MsHoldCatg2();
			result.ID = item.Catg2ID;
			result.Name = item.CatgName;
			result.Catg1Id = item.Catg1Id;
			result.CatgDescription = item.CatgDescription;
			result.IsRepFrontEndHold = item.IsRepFrontEndHold;
			result.IsRepBackEndHold = item.IsRepBackEndHold;
			return result;
		}
	}
}
