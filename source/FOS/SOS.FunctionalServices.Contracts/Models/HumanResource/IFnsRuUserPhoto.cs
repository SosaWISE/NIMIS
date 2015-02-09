using System;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
{
	public interface IFnsRuUserPhoto
	{
		int UserID { get; set; }
		byte[] PhotoFile { get; set; }
		string MimeType { get; set; }
	}
}
