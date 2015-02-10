using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SSE.Services.CORS.Helpers
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

			// Default path of execution
			/** Initialize. */
			var oWebContext = System.Web.HttpContext.Current;
			return oWebContext.Request.ServerVariables["REMOTE_ADDR"];
		}
	}
}