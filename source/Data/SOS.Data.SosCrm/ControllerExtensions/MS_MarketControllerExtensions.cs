using SOS.Data.Extensions;
using SubSonic;
using AR = SOS.Data.SosCrm.MS_Market;
using ARCollection = SOS.Data.SosCrm.MS_MarketCollection;
using ARController = SOS.Data.SosCrm.MS_MarketController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_MarketControllerExtensions
	{
		public static ARCollection LoadAllActive(this ARController controller)
		{
			Query qry = AR.Query()
				.Active()//not deleted
				.AND(AR.Columns.IsActive, true);

			return controller.LoadCollection(qry);
		}

        public static ARCollection GetAllActiveWithNullFirstItem(this ARController oCntlr)
        {
            // Locals
            var oResult = LoadAllActive(oCntlr);

            oResult.Add(new MS_Market
                            {
                                MarketName = "[Select One]"
                            });
            
            // Return result
            oResult.Sort(AR.Columns.MarketName, true);

            return oResult;
        }
	}
}
