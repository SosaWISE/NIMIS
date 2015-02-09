// ReSharper disable once CheckNamespace
namespace SOS.Data.SosCrm
{
// ReSharper disable once InconsistentNaming
	public partial class MS_AccountSiteType
	{
		public MS_MonitronicsEntitySiteType GetMoniSiteTypeId()
		{
			return
				SosCrmDataContext.Instance.MS_MonitronicsEntitySiteTypes.LoadSingle(
					SosCrmDataStoredProcedureManager.MS_MonitronicsEntitySiteTypeGetByAccountSiteTypeId(SiteTypeID));
		}
	}
}
