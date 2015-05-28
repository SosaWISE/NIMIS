using System;
using NXS.Data;

namespace NXS.Data.Sales
{
	public partial class DBase
	{
		HumanResource.DBase _hrDb;
		// This should only be used for cross db joins
		public HumanResource.DBase HrDb
		{
			get
			{
				if (_hrDb == null)
					_hrDb = HumanResource.DBase.Init(null);
				return _hrDb;
			}
		}
	}
}
