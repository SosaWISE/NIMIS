using NXS.Data.AuthenticationControl;
using System;

namespace NXS.DataServices.AuthenticationControl.Models
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

		internal static MetadataType FromDb(AC_Action item, bool nullable = false)
		{
			if (NullCheck("Action", item, nullable))
				return null;
			var result = new MetadataType();
			result.ID = item.ID;
			result.Name = item.Name;
			return result;
		}

		internal static MetadataType FromDb(AC_Application item, bool nullable = false)
		{
			if (NullCheck("Application", item, nullable))
				return null;
			var result = new MetadataType();
			result.ID = item.ID;
			result.Name = item.ApplicationName;
			return result;
		}
	}
}
