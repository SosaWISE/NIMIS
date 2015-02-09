
namespace SOS.Services.Interfaces.Models.CmsModels
{
	#region McLocalization

	public interface IMcLocalization
	{
		string LocalizationID { get; set; }
		int MSLocalId { get; set; }
		string LocalizationName { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
	}

	public class McLocalization : IMcLocalization
	{
		#region Implementation of IMcLocalization

		public string LocalizationID { get; set; }
		public int MSLocalId { get; set; }
		public string LocalizationName { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }

		#endregion Implementation of IMcLocalization
	}

	#endregion McLocalization
}
