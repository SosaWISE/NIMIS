using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;

namespace SOS.FunctionalServices.Models.HumanResource
{
	public class FnsRuUserPhoto : IFnsRuUserPhoto
	{
		public int UserID { get; set; }
		public byte[] PhotoFile { get; set; }
		public string MimeType { get; set; }

		public FnsRuUserPhoto(RU_UserPhoto userPhoto)
		{
			UserID = userPhoto.UserID;
			PhotoFile = userPhoto.PhotoFile;
			MimeType = userPhoto.MimeType;
		}
	}
}
