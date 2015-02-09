using AR = SOS.Data.SosCrm.IE_LocationView;
using ARCollection = SOS.Data.SosCrm.IE_LocationViewCollection;
using ARController = SOS.Data.SosCrm.IE_LocationViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class IE_LocationViewControllerExtensions
	{

        public static ARCollection LocationGetByLocationTypeID(this ARController cntlr, string locationTypeId)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.IE_LocationGetByLocationTypeID(locationTypeId));
		}

      
	}
}