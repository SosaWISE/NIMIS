using NXS.DataServices.Crm;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.ContractAdmin
{
	public class AccountsModule : BaseModule
	{
		ContractAdminService Srv { get { return new ContractAdminService(this.User.GPEmployeeID); } }

		public AccountsModule()
			: base("/ContractAdmin/Accounts")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			//Get["/{id:long}/Customers/{customerTypeId}", true] = async (x, ct) =>
			//{
			//	return await Srv.CustomerByTypeAsync((long)x.id, (string)x.customerTypeId).ConfigureAwait(false);
			//};
			//Post["/{id:long}/Customers/{customerTypeId}", true] = async (x, ct) =>
			//{
			//	var leadId = this.Bind<long>();
			//	return await Srv.SetCustomerAsync((long)x.id, (string)x.customerTypeId, leadId).ConfigureAwait(false);
			//};

			Get["/{id:long}/CustomerAccounts/{customerTypeId}", true] = async (x, ct) =>
			{
				return await Srv.CustomerAccountByTypeAsync((long)x.id, (string)x.customerTypeId).ConfigureAwait(false);
			};
			Post["/{id:long}/CustomerAccounts/{customerTypeId}", true] = async (x, ct) =>
			{
				var input = this.BindBody<AeCustomerAccountInput>();
				return await Srv.SetCustomerAccountAsync((long)x.id, (string)x.customerTypeId, input.LeadId).ConfigureAwait(false);
			};
			Delete["/{id:long}/CustomerAccounts/{customerTypeId}", true] = async (x, ct) =>
			{
				return await Srv.DeleteCustomerAccountAsync((long)x.id, (string)x.customerTypeId).ConfigureAwait(false);
			};


			//Get["/{id:long}/Customers/{customerTypeId}/Address", true] = async (x, ct) =>
			//{
			//	return await Srv.CustomerAddressByTypeAsync((long)x.id, (string)x.customerTypeId).ConfigureAwait(false);
			//};
			//Post["/{id:long}/Customers/{customerTypeId}/Address/{addressId:long}", true] = async (x, ct) =>
			//{
			//	return await Srv.SetCustomerAccountAddressAsync((long)x.id, (string)x.customerTypeId, (long)x.addressId).ConfigureAwait(false);
			//};

			//Get["/{id:long}/CustomerAccounts", true] = async (x, ct) =>
			//{
			//	return await Srv.CustomerAccountsAsync((long)x.id).ConfigureAwait(false);
			//};
			//Post["/{id:long}/CustomerAccounts", true] = async (x, ct) =>
			//{
			//	var list = this.Bind<List<AeCustomerAccountInput>>();
			//	return await Srv.SetCustomerAccountsAsync((long)x.id, list).ConfigureAwait(false);
			//};
		}
	}
}
