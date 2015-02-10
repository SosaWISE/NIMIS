using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.Lib.RestCake.Attributes;
using SOS.Services.Interfaces;
using SOS.Services.Interfaces.Models;
//using SOS.services.Wcf.Signals.Support;

namespace SOS.services.Wcf.Signals
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	[RestService(Namespace = "SOS", ServiceContract = typeof(IExecuteSvc))]
	public class ExecuteSvc : IExecuteSvc
	{
		#region .ctor
//		public ExecuteSvc() : base(false){}
		#endregion .ctor

		#region Implementation of IExecuteSvc

		public string Receive(string szReceiverToken)
		{
			/** Locals. */
			var oRequest = HttpContext.Current.Request;

			/** Check that there is a request. */
			System.Diagnostics.Debug.WriteLine("QueryString: {0}", oRequest.QueryString);

			/** Save request. */
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IReceiverEngineService>();

			var oResult = oService.SaveRequest(HttpContext.Current);
			var szResponse = string.Format("QS: {0} | Result: {1}"
				, oRequest.QueryString
				, oResult.Message);

			/** Return message. */
			return szResponse;
		}

		public TxtWireResponse TxtWireReceivePost(TxtWirePostInfo oParams)
		{
			/** Locals. */
			var oRequest = HttpContext.Current.Request;

			/** Check that there is a request. */
			System.Diagnostics.Debug.WriteLine("QueryString: {0}", oRequest.QueryString);

			return new TxtWireResponse{ResponseString = "Yeah we made it here."};
		}

		public string TxtWireReceive(string szTitle)
		{
			System.Diagnostics.Debug.WriteLine(szTitle);
			return "Andres";
		}

		#endregion
	}
}
