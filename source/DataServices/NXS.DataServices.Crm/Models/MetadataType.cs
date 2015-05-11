using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class MetadataType
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

		internal static MetadataType FromFriendsAndFamilyType(MC_FriendsAndFamilyType item, bool nullable = false)
		{
			if (NullCheck("FriendsAndFamilyType", item, nullable))
				return null;
			var result = new MetadataType();
			result.ID = item.ID;
			result.Name = item.FriendsAndFamilyType;
			return result;
		}

		internal static MetadataType FromAccountCancelReason(MC_AccountCancelReason item, bool nullable = false)
		{
			if (NullCheck("AccountCancelReason", item, nullable))
				return null;
			var result = new MetadataType();
			result.ID = item.ID;
			result.Name = item.AccountCancelReason;
			return result;
		}

		internal static MetadataType FromLocationType(IE_LocationType item, bool nullable = false)
		{
			if (NullCheck("LocationType", item, nullable))
				return null;
			var result = new MetadataType();
			result.ID = item.ID;
			result.Name = item.LocationTypeName;
			return result;
		}
		internal static MetadataType FromLocation(Location item, bool nullable = false)
		{
			if (NullCheck("Location", item, nullable))
				return null;
			var result = new MetadataType();
			result.ID = item.ID;
			result.Name = item.Name;
			return result;
		}
	}
}
