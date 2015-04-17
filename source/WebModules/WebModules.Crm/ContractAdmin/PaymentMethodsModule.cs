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
			this.RequiresPermission(applicationID: null, actionID: null);

			// get payment method on an account
			Get["/{id:long}/PaymentMethod", true] = async (x, ct) =>
			{
				long accountId = x.id;
				return await Srv.AccountPaymentMethod(accountId, false).ConfigureAwait(false);
			};
			Post["/{id:long}/PaymentMethod", true] = async (x, ct) =>
			{
				long accountId = x.id;
				var input = this.BindBody<AePaymentMethod>();
				return await Srv.SavePaymentMethod(accountId, false, input).ConfigureAwait(false);
			};
			// get initial payment method on an account
			Get["/{id:long}/InitialPaymentMethod", true] = async (x, ct) =>
			{
				long accountId = x.id;
				return await Srv.AccountPaymentMethod(accountId, true).ConfigureAwait(false);
			};
			Post["/{id:long}/InitialPaymentMethod", true] = async (x, ct) =>
			{
				long accountId = x.id;
				var input = this.BindBody<AePaymentMethod>();
				return await Srv.SavePaymentMethod(accountId, true, input).ConfigureAwait(false);
			};
		}
	}
}
