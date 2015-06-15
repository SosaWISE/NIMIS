using AR = SOS.Data.Logging.LG_MessageStackFrame;
using ARCollection = SOS.Data.Logging.LG_MessageStackFrameCollection;
using ARController = SOS.Data.Logging.LG_MessageStackFrameController;
using SubSonic;

namespace SOS.Data.Logging.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class LG_MessageStackFrameControllerExtensions
	{
		public static ARCollection LoadByMessageID(this ARController controller, int messageID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.MessageId, messageID);

			return controller.LoadCollection(qry);
		}
	}
}
