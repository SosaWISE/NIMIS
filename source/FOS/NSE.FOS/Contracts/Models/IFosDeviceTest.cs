using System;

namespace NSE.FOS.Contracts.Models
{
	public interface IFosDeviceTest
	{
		int? TestNum { get; set; }
		string TestCategory { get; set; }
		string TestCategoryDescription { get; set; }
		DateTime? EffectiveOn { get; set; }
		DateTime? ExpiresOn { get; set; }
		string TestType { get; set; }
	}
}