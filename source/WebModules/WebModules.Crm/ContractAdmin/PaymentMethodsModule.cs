using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;
using AuthApplications = NXS.Data.AuthenticationControl.AC_Application.MetaData;
using AuthActions = NXS.Data.AuthenticationControl.AC_Action.MetaData;

namespace WebModules.Crm.Ms
{
	public class PaymentMethodsModule : BaseModule
	{
		PaymentService Srv { get { return new PaymentService(this.User.GPEmployeeID); } }

		public PaymentMethodsModule()
			: base("/Ms/Accounts")
		{
			this.RequiresPermission(applicationID: AuthApplications.ContractAdministrationID, actionID: null);

			// get payment method on an account
			Get["/{id:long}/PaymentMethod", true] = async (x, ct) =>
			{
				long accountId = x.id;
				return await Srv.AccountPaymentMethod(accountId).ConfigureAwait(false);
			};
			Post["/{id:long}/PaymentMethod", true] = async (x, ct) =>
			{
				long accountId = x.id;	
				var input = this.BindBody<AePaymentMethod>();
				return await Srv.SavePaymentMethod(accountId, input).ConfigureAwait(false);
			};
		}
	}
}
