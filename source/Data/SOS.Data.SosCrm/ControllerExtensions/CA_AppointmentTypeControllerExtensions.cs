using AR = SOS.Data.SosCrm.CA_AppointmentType;
using ARCollection = SOS.Data.SosCrm.CA_AppointmentTypeCollection;
using ARController = SOS.Data.SosCrm.CA_AppointmentTypeController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class CA_AppointmentTypeControllerExtensions
	{
		public static ARCollection GetOptionsList(this ARController oCntlr)
		{
			return oCntlr.LoadAll();
		}
	}
}
