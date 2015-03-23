using NXS.DataServices.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.ModelBinding;
using NXS.DataServices.Crm.Models;

namespace WebModules.Crm.ContractAdmin
{
	public class AccountsModule : BaseModule
	{
		public AccountsModule()
			: base("/ContractAdmin/Accounts")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			//Get["/{id:long}/Customers/{customerTypeId}", true] = async (x, ct) =>
			//{
			//	var srv = new ContractAdminService(this.User.GPEmployeeID);
			//	return await srv.CustomerByTypeAsync((long)x.id, (string)x.customerTypeId).ConfigureAwait(false);
			//};
			//Post["/{id:long}/Customers/{customerTypeId}", true] = async (x, ct) =>
			//{
			//	var srv = new ContractAdminService(this.User.GPEmployeeID);
			//	var leadId = this.Bind<long>();
			//	return await srv.SetCustomerAsync((long)x.id, (string)x.customerTypeId, leadId).ConfigureAwait(false);
			//};

			Get["/{id:long}/CustomerAccounts/{customerTypeId}", true] = async (x, ct) =>
			{
				var srv = new ContractAdminService(this.User.GPEmployeeID);
				return await srv.CustomerAccountByTypeAsync((long)x.id, (string)x.customerTypeId).ConfigureAwait(false);
			};
			Post["/{id:long}/CustomerAccounts/{customerTypeId}", true] = async (x, ct) =>
			{
				var srv = new ContractAdminService(this.User.GPEmployeeID);
				var input = this.Bind<AeCustomerAccountInput>();
				return await srv.SetCustomerAccountAsync((long)x.id, (string)x.customerTypeId, input.LeadId).ConfigureAwait(false);
			};
			//Delete["/{id:long}/CustomerAccounts/{customerTypeId}", true] = async (x, ct) =>
			//{
			//	var srv = new ContractAdminService(this.User.GPEmployeeID);
			//	var input = this.Bind<AeCustomerAccountInput>();
			//	return await srv.DeleteCustomerAccountAsync((long)x.id, (string)x.customerTypeId).ConfigureAwait(false);
			//};


			//Get["/{id:long}/Customers/{customerTypeId}/Address", true] = async (x, ct) =>
			//{
			//	var srv = new ContractAdminService(this.User.GPEmployeeID);
			//	return await srv.CustomerAddressByTypeAsync((long)x.id, (string)x.customerTypeId).ConfigureAwait(false);
			//};
			//Post["/{id:long}/Customers/{customerTypeId}/Address/{addressId:long}", true] = async (x, ct) =>
			//{
			//	var srv = new ContractAdminService(this.User.GPEmployeeID);
			//	return await srv.SetCustomerAccountAddressAsync((long)x.id, (string)x.customerTypeId, (long)x.addressId).ConfigureAwait(false);
			//};

			//Get["/{id:long}/CustomerAccounts", true] = async (x, ct) =>
			//{
			//	var srv = new ContractAdminService(this.User.GPEmployeeID);
			//	return await srv.CustomerAccountsAsync((long)x.id).ConfigureAwait(false);
			//};
			//Post["/{id:long}/CustomerAccounts", true] = async (x, ct) =>
			//{
			//	var list = this.Bind<List<AeCustomerAccountInput>>();
			//	var srv = new ContractAdminService(this.User.GPEmployeeID);
			//	return await srv.SetCustomerAccountsAsync((long)x.id, list).ConfigureAwait(false);
			//};
		}
	}
}
