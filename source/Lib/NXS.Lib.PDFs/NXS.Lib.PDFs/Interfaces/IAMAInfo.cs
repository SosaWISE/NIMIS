using System;

namespace NXS.Lib.PDFs.Interfaces
{
	public interface IAMAInfo
	{
		#region Properties
		long CustomerNumber { get; }
		long AccountID { get; }
		string OwnerPreName { get; }
		string OwnerFirstName { get; }
		string OwnerMiddleName { get; }
		string OwnerLastName { get; }
		string OwnerPostName { get; }
		string SpousePreName { get; }
		string SpouseFirstName { get; }
		string SpouseMiddleName { get; }
		string SpouseLastName { get; }
		string SpousePostName { get; }
		DateTime EffectiveDate { get; }
		string NameOfBusiness { get; }
		string PremiseAddress { get; set; }

		#endregion Properties
	}
}
