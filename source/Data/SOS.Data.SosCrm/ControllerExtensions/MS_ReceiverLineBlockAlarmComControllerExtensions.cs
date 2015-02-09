using System;
using AR = SOS.Data.SosCrm.MS_ReceiverLineBlockAlarmCom;
using ARCollection = SOS.Data.SosCrm.MS_ReceiverLineBlockAlarmComCollection;
using ARController = SOS.Data.SosCrm.MS_ReceiverLineBlockAlarmComController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class MS_ReceiverLineBlockAlarmComControllerExtensions
	{
		public static AR EnsureLoadByReceiverLineBlockID(this ARController controller, string receiverLineBlockID)
		{
			var result = controller.LoadByPrimaryKey(receiverLineBlockID);
			if (result == null)
			{
				result = new AR()
				{
					ReceiverLineBlockID = receiverLineBlockID,
				};
			}
			return result;
		}
	}
}
