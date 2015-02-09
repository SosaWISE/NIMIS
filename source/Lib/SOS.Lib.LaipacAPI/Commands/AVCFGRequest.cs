using System;
using SOS.Data.GpsTracking;
using SOS.Data.GpsTracking.ControllerExtensions;

namespace SOS.Lib.LaipacAPI.Commands
{
	/********************************************
	 * 18. Feature Flag Configuration
	 *******************************************/
	public class AVCFGRequest : Requests
	{
		#region .ctor

		public AVCFGRequest(string sPassword, LP_AVCFGCode.MetaDataEnum eConfig, LP_CommandMessageAVRMC oCommandMessageAVRMC)
			: base(sPassword)
		{
			CommandMessageAVRMC = oCommandMessageAVRMC;
			ConfigCode = eConfig;

			CreateCommandMessage();
		}

		#endregion .ctor

		#region Member Properties

		protected LP_AVCFGCode.MetaDataEnum ConfigCode { get; private set; }
		public LP_CommandMessageAVRMC CommandMessageAVRMC { get; private set; }
		public LP_CommandMessageAVCFGFF CommandMessageAVCFGFF { get; private set; }
		public LP_CommandMessage CommandMessage { get; private set; }

		#endregion Member Properties

		public void CreateCommandMessage()
		{
			/** Create Base Command Message. */
			CommandMessage = new LP_CommandMessage
			{
				CommandTypeId = LP_CommandType.MetaData.ConfigurationCommandsID,
				CommandNameId = LP_CommandName.MetaData.AVCFGFeatureFlagID,
				MessageDate = DateTime.Now,
				Sentence = GetRequestWrapper(string.Format(REQ_FEATURE_FLAG_CONFIG, Password, GpsTrackingDataContext.Instance.LP_AVCFGCodes.GetMetaData(ConfigCode))),
				CreatedOn = DateTime.Now,
				DEX_ROW_TS = DateTime.UtcNow
			};
			CommandMessage.Save();


			/** Save information */
			CommandMessageAVCFGFF = new LP_CommandMessageAVCFGFF
			                        	{
			                        		CommandMessageID = CommandMessage.CommandMessageID,
											ResponseToCommandMessageId = CommandMessageAVRMC.CommandMessageID,
											Password = Password,
											Code = GpsTrackingDataContext.Instance.LP_AVCFGCodes.GetMetaData(ConfigCode),
											CreatedOn = DateTime.Now
			                        	};
			CommandMessageAVCFGFF.Save();
		}

		public string GetRequest()
		{
			return GetRequestWrapper(string.Format(REQ_FEATURE_FLAG_CONFIG, Password, GpsTrackingDataContext.Instance.LP_AVCFGCodes.GetMetaData(ConfigCode)));
		}
	}
}
