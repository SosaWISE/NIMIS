using SOS.Data.Extensions;
using SubSonic;
using AR = SOS.Data.HumanResource.RU_PhoneCellCarrier;
using ARCollection = SOS.Data.HumanResource.RU_PhoneCellCarrierCollection;
using ARController = SOS.Data.HumanResource.RU_PhoneCellCarrierController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
	public static class RU_PhoneCellCarrierControllerExtensions
	{
		public static ARCollection LoadAllActive(this ARController controller)
		{
			Query qry = AR.Query().Active();
			return controller.LoadCollection(qry);
		}
	}
}
