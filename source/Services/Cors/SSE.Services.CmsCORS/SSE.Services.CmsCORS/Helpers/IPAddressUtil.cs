using SOS.Lib.Util;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;

namespace SSE.Services.CmsCORS.Helpers
{
	public static class IPAddressUtil
	{
		public static string ClientIPAddress()
		{
			if (OperationContext.Current != null)
			{
				/** Initialize. */
				OperationContext oContext = OperationContext.Current;
				MessageProperties oMsgProp = oContext.IncomingMessageProperties;

				/** Acquire IP Addresses. */
				var oEndPointProp = oMsgProp[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

				/** Return result. */
				if (oEndPointProp != null) return oEndPointProp.Address;
				return null;
			}

			return HttpContext.Current.ClientIPAddress();
		}
	}
}