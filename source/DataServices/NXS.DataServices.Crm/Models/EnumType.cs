using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class EnumType
	{
		public string ID { get; set; }
		public string Name { get; set; }

		static bool NullCheck(string name, object item, bool nullable)
		{
			if (item == null)
			{
				if (nullable)
					return true;
				else
					throw new Exception(name + " is null");
			}
			return false;
		}

		internal static EnumType FromFriendsAndFamilyType(MC_FriendsAndFamilyType item, bool nullable = false)
		{
			if (NullCheck("FriendsAndFamilyType", item, nullable))
				return null;
			var result = new EnumType();
			result.ID = item.ID;
			result.Name = item.FriendsAndFamilyType;
			return result;
		}
	}
}
