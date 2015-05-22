using NXS.Data.AuthenticationControl;
using System;

namespace NXS.DataServices.AuthenticationControl.Models
{
	public class EnumType
	{
		public int ID { get; set; }
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

		internal static EnumType FromDb(AC_RequestReason item, bool nullable = false)
		{
			if (NullCheck("RequestReason", item, nullable))
				return null;
			var result = new EnumType();
			result.ID = item.ID;
			result.Name = item.Name;
			return result;
		}
	}
}
