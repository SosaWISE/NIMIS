using NXS.Data;
using NXS.Data.Crm;
using NXS.DataServices.Crm.Models;
using SOS.Lib.Core;
using SOS.Lib.Core.ErrorHandling;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm
{
	public class ContractAdminService
	{
		string _gpEmployeeId;
		public ContractAdminService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		//public Dictionary<string, int> CustomerTypesPrecedenceMap()
		//{
		//	return precedenceMap;
		//}
		//private static readonly Dictionary<string, int> precedenceMap = new Dictionary<string, int>(5, StringComparer.OrdinalIgnoreCase) {
		//	{"PRI", 1},
		//	{"LEAD", 1},
		//	{"SEC", 2},
		//	{"BILL", 3},
		//	{"SHIP", 4},
		//};
		//private static int getPrecedence(string customerTypeId)
		//{
		//	if (precedenceMap.ContainsKey(customerTypeId))
		//		return precedenceMap[customerTypeId];
		//	else
		//		return 99;
		//}
		//private static int PrecedenceComparison(AE_CustomerAccount a, AE_CustomerAccount b)
		//{
		//	return getPrecedence(a.CustomerTypeId) - getPrecedence(b.CustomerTypeId);
		//}

		//public async Task<Result<AeCustomer>> CustomerByTypeAsync(long accountId, string customerTypeId)
		//{
		//	using (var db = CrmDb.Connect())
		//	{
		//		var item = await db.AE_Customers.OneByTypeAsync(accountId, customerTypeId).ConfigureAwait(false);
		//		var result = new Result<AeCustomer>(value: AeCustomer.FromDb(item, nullable: true));
		//		return result;
		//	}
		//}
		//public async Task<Result<McAddress>> CustomerAddressByTypeAsync(long accountId, string customerTypeId)
		//{
		//	using (var db = CrmDb.Connect())
		//	{
		//		var item = await db.MC_Addresses.OneByTypeAsync(accountId, customerTypeId).ConfigureAwait(false);
		//		var result = new Result<McAddress>(value: McAddress.FromDb(item, nullable: true));
		//		return result;
		//	}
		//}

		public async Task<Result<AeCustomerAccount>> CustomerAccountByTypeAsync(long accountId, string customerTypeId)
		{
			using (var db = CrmDb.Connect())
			{
				var item = (await db.AE_CustomerAccounts.ByAccountIdAndTypeAsync(accountId, customerTypeId).ConfigureAwait(false)).FirstOrDefault();
				var result = new Result<AeCustomerAccount>(value: AeCustomerAccount.FromDb(item, nullable: true));
				return result;
			}
		}
		public async Task<Result<AeCustomer>> SetCustomerAccountAsync(long accountId, string customerTypeId, long leadId)
		{
			using (var db = CrmDb.Connect())
			{
				var result = new Result<AeCustomer>();
				var lead = await db.QL_Leads.ByIdAsync(leadId).ConfigureAwait(false);
				if (lead == null)
					return result.Fail(-1, "Invalid LeadID");
				var qlAddress = (await db.QL_Addresses.ByIdAsync(lead.AddressId).ConfigureAwait(false));

				//@TODO: validate lead's account matches accountId ???

				AE_Customer customer = null;
				await db.TransactionAsync(async () =>
				{
					var tbl = db.AE_CustomerAccounts;
					var custAccounts = (await tbl.ByAccountIdAndTypeAsync(accountId, customerTypeId).ConfigureAwait(false)).ToList();
					var custAcct = custAccounts.FirstOrDefault();
					if (custAcct != null)
						custAccounts.Remove(custAcct); // don't delete this one
					// delete existing customer accounts except current (there should only be one but the db schema allows more than one)
					foreach (var item in custAccounts)
						await tbl.DeleteAsync(item.CustomerAccountID).ConfigureAwait(false);

					// ensure MC_Address
					var mcAddress = (await UpdateOrCreateMcAddress(_gpEmployeeId, db, qlAddress).ConfigureAwait(false));
					// ensure AE_Customer
					customer = (await CreateAeCustomer(_gpEmployeeId, db, lead, mcAddress).ConfigureAwait(false));
					// add new customer account
					if (custAcct == null)
					{
						custAcct = new AE_CustomerAccount();
						CopyFrom(custAcct, accountId, customerTypeId, lead, customer, mcAddress);
						await tbl.InsertAsync(custAcct, _gpEmployeeId).ConfigureAwait(false);
					}
					else
					{
						// update customer account
						var snapShot = Snapshotter.Start(custAcct);
						CopyFrom(custAcct, accountId, customerTypeId, lead, customer, mcAddress);
						await tbl.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);
					}

					// commit transaction
					return true;
				}).ConfigureAwait(false);

				result.Value = AeCustomer.FromDb(customer);
				return result;
			}
		}
		private static void CopyFrom(AE_CustomerAccount custAcct, long accountId, string customerTypeId, QL_Lead lead, AE_Customer customer, MC_Address mcAddress)
		{
			custAcct.AccountId = accountId;
			custAcct.CustomerTypeId = customerTypeId;
			custAcct.LeadId = lead.LeadID;
			custAcct.CustomerId = customer.CustomerID;
			custAcct.AddressId = mcAddress.AddressID;
		}

		public async Task<Result<bool>> DeleteCustomerAccountAsync(long accountId, string customerTypeId)
		{
			using (var db = CrmDb.Connect())
			{
				var result = new Result<bool>();

				await db.TransactionAsync(async () =>
				{
					// delete existing customer accounts (there should only be one but the db schema allows more than one)
					var custAccounts = (await db.AE_CustomerAccounts.ByAccountIdAndTypeAsync(accountId, customerTypeId).ConfigureAwait(false)).ToList();
					foreach (var custAcct in custAccounts)
						await db.AE_CustomerAccounts.DeleteAsync(custAcct.CustomerAccountID).ConfigureAwait(false);

					// commit transaction
					return true;
				}).ConfigureAwait(false);

				result.Value = true;
				return result;
			}
		}

		//private static int PrecedenceComparison(AeCustomerAccountInput a, AeCustomerAccountInput b)
		//{
		//	return getPrecedence(a.CustomerTypeId) - getPrecedence(b.CustomerTypeId);
		//}
		//public async Task<Result<List<AeCustomerAccount>>> SetCustomerAccountsAsync(long accountId, List<AeCustomerAccountInput> customerAccountInputs)
		//{
		//	using (var db = CrmDb.Connect())
		//	{
		//		await Task.Delay(1);
		//		var result = new Result<List<AeCustomerAccount>>();

		//		var custAccts = (await db.AE_CustomerAccounts.ManyByAccountIdAsync(accountId).ConfigureAwait(false)).ToList();
		//		custAccts.Sort(PrecedenceComparison);

		//		// loop in order of precendence
		//		customerAccountInputs.Sort(PrecedenceComparison);
		//		foreach (var customerAccountInput in customerAccountInputs)
		//		{
		//			if (IsTopQlAddress(custAccts, customerAccountInput))
		//			{
		//				// highest precedence QL_Address update MC_Address
		//			}
		//			else
		//			{
		//				// NOT highest precedence QL_Address - create new MC_Address
		//			}

		//			if (IsTopLead(custAccts, customerAccountInput))
		//			{
		//				// highest precedence QL_Lead - update AE_Customer
		//			}
		//			else
		//			{
		//				// NOT highest precedence QL_Lead - create new AE_Customer
		//			}
		//		}

		//		//var lead = await db.QL_Leads.ByIdAsync(leadId).ConfigureAwait(false);
		//		//if (lead == null)
		//		//	return result.Fail(-1, "Invalid LeadID");
		//		//var qlAddress = (await db.QL_Addresses.ByIdAsync(lead.AddressId).ConfigureAwait(false));

		//		////@TODO: validate lead's account matches accountId ???

		//		//AE_Customer customer = null;
		//		//await db.TransactionAsync(async () =>
		//		//{
		//		//	// delete existing customer accounts (there should only be one but the db schema allows more than one)
		//		//	var custAccounts = (await db.AE_CustomerAccounts.ManyByAccountIdAndTypeAsync(accountId, customerTypeId).ConfigureAwait(false)).ToList();
		//		//	foreach (var custAcct in custAccounts)
		//		//		await db.AE_CustomerAccounts.DeleteAsync(custAcct.CustomerAccountID).ConfigureAwait(false);

		//		//	// ensure MC_Address
		//		//	var mcAddress = (await UpdateOrCreateMcAddress(db, qlAddress).ConfigureAwait(false));
		//		//	// ensure AE_Customer
		//		//	customer = (await UpdateOrCreateAeCustomer(db, lead, mcAddress).ConfigureAwait(false));
		//		//	// add new customer account
		//		//	{
		//		//		var custAcct = new AE_CustomerAccount();
		//		//		//custAcct.CustomerAccountID 
		//		//		custAcct.LeadId = leadId;
		//		//		custAcct.AccountId = accountId;
		//		//		custAcct.CustomerId = customer.CustomerID;
		//		//		custAcct.CustomerTypeId = customerTypeId;
		//		//		custAcct.AddressId = mcAddress.AddressID;
		//		//		custAcct.CreatedOn = DateTime.UtcNow;
		//		//		custAcct.CreatedBy = _gpEmployeeId;
		//		//		await db.AE_CustomerAccounts.InsertAsync(custAcct).ConfigureAwait(false);
		//		//	}

		//		//	// commit transaction
		//		//	return true;
		//		//}).ConfigureAwait(false);

		//		//result.Value = AeCustomer.FromDb(customer);
		//		return result;
		//	}
		//}
		//private static bool IsTopQlAddress(List<AE_CustomerAccount> custAccts, AeCustomerAccountInput customerAccountInput)
		//{
		//	foreach (var ca in custAccts)
		//		if (ca.Address.QlAddressId == customerAccountInput.QlAddressId)
		//			return ca.CustomerTypeId == customerAccountInput.CustomerTypeId;
		//	// default to top if none were found
		//	return true;
		//}
		//private static bool IsTopLead(List<AE_CustomerAccount> custAccts, AeCustomerAccountInput customerAccountInput)
		//{
		//	foreach (var ca in custAccts)
		//		if (ca.Customer.LeadId == customerAccountInput.LeadId)
		//			return ca.CustomerTypeId == customerAccountInput.CustomerTypeId;
		//	// default to top if none were found
		//	return true;
		//}

		//public async Task<Result<AeCustomer>> SetCustomerAsync(long accountId, string customerTypeId, long leadId)
		//{
		//	using (var db = CrmDb.Connect())
		//	{
		//		var result = new Result<AeCustomer>();
		//		var lead = await db.QL_Leads.ByIdAsync(leadId).ConfigureAwait(false);
		//		if (lead == null)
		//			return result.Fail(-1, "Invalid LeadID");
		//		var qlAddress = (await db.QL_Addresses.ByIdAsync(lead.AddressId).ConfigureAwait(false));

		//		//@TODO: validate lead's account matches accountId ???

		//		AE_Customer customer = null;
		//		await db.TransactionAsync(async () =>
		//		{
		//			// delete existing customer accounts (there should only be one but the db schema allows more than one)
		//			var custAccounts = (await db.AE_CustomerAccounts.ManyByAccountIdAndTypeAsync(accountId, customerTypeId).ConfigureAwait(false)).ToList();
		//			foreach (var custAcct in custAccounts)
		//				await db.AE_CustomerAccounts.DeleteAsync(custAcct.CustomerAccountID).ConfigureAwait(false);

		//			// ensure MC_Address
		//			var mcAddress = (await UpdateOrCreateMcAddress(_gpEmployeeId, db, qlAddress).ConfigureAwait(false));
		//			// ensure AE_Customer
		//			customer = (await CreateAeCustomer(_gpEmployeeId, db, lead, mcAddress).ConfigureAwait(false));
		//			// add new customer account
		//			{
		//				var custAcct = new AE_CustomerAccount();
		//				custAcct.LeadId = leadId;
		//				custAcct.AccountId = accountId;
		//				custAcct.CustomerId = customer.CustomerID;
		//				custAcct.CustomerTypeId = customerTypeId;
		//				custAcct.AddressId = mcAddress.AddressID;
		//				custAcct.CreatedOn = DateTime.UtcNow;
		//				custAcct.CreatedBy = _gpEmployeeId;
		//				await db.AE_CustomerAccounts.InsertAsync(custAcct).ConfigureAwait(false);
		//			}

		//			// commit transaction
		//			return true;
		//		}).ConfigureAwait(false);

		//		result.Value = AeCustomer.FromDb(customer);
		//		return result;
		//	}
		//}
		//public async Task<Result<bool>> SetCustomerAccountAddressAsync(long accountId, string customerTypeId, long qlAddressId)
		//{
		//	using (var db = CrmDb.Connect())
		//	{
		//		var result = new Result<bool>();
		//		var qlAddress = (await db.QL_Addresses.ByIdAsync(qlAddressId).ConfigureAwait(false));
		//		if (qlAddress == null)
		//			return result.Fail(-1, "Invalid AddressID");
		//
		//		//@TODO: validate address's account matches accountId ???
		//
		//		await db.TransactionAsync(async () =>
		//		{
		//			// ensure MC_Address
		//			var mcAddress = (await UpdateOrCreateMcAddress(_gpEmployeeId, db, qlAddress).ConfigureAwait(false));
		//
		//			// update existing customer accounts (there should only be one but the db schema allows more than one)
		//			var custAccounts = (await db.AE_CustomerAccounts.ManyByAccountIdAndTypeAsync(accountId, customerTypeId).ConfigureAwait(false)).ToList();
		//			foreach (var custAcct in custAccounts)
		//			{
		//				await db.AE_CustomerAccounts.DeleteAsync(custAcct.CustomerAccountID).ConfigureAwait(false);
		//			}
		//
		//			// commit transaction
		//			return true;
		//		}).ConfigureAwait(false);
		//
		//		result.Value = true;
		//		return result;
		//	}
		//}

		private static async Task<MC_Address> UpdateOrCreateMcAddress(string gpEmployeeId, CrmDb db, QL_Address qlAddress)
		{
			var tbl = db.MC_Addresses;
			// map ql_address to mc_address 1 to 1
			// check if an address already exists for the lead qlAddress
			var mcAddress = (await tbl.ByQlAddressIdAsync(qlAddress.AddressID).ConfigureAwait(false));
			if (mcAddress == null)
			{
				mcAddress = new MC_Address();
				CopyFromLead(mcAddress, qlAddress);
				await tbl.InsertAsync(mcAddress, gpEmployeeId).ConfigureAwait(false);
			}
			else
			{
				var snapShot = Snapshotter.Start(mcAddress);
				CopyFromLead(mcAddress, qlAddress);
				await tbl.UpdateAsync(snapShot, gpEmployeeId).ConfigureAwait(false);
			}
			return mcAddress;
		}
		private static void CopyFromLead(MC_Address mcAddress, QL_Address qlAddress)
		{
			mcAddress.QlAddressId = qlAddress.AddressID;
			mcAddress.DealerId = qlAddress.DealerId;
			mcAddress.ValidationVendorId = qlAddress.ValidationVendorId;
			mcAddress.AddressValidationStateId = qlAddress.AddressValidationStateId;
			mcAddress.StateId = qlAddress.StateId;
			mcAddress.CountryId = qlAddress.CountryId;
			mcAddress.TimeZoneId = qlAddress.TimeZoneId;
			mcAddress.AddressTypeId = qlAddress.AddressTypeId;
			mcAddress.StreetAddress = qlAddress.StreetAddress;
			mcAddress.StreetAddress2 = qlAddress.StreetAddress2;
			mcAddress.StreetNumber = qlAddress.StreetNumber;
			mcAddress.StreetName = qlAddress.StreetName;
			mcAddress.StreetType = qlAddress.StreetType;
			mcAddress.PreDirectional = qlAddress.PreDirectional;
			mcAddress.PostDirectional = qlAddress.PostDirectional;
			mcAddress.Extension = qlAddress.Extension;
			mcAddress.ExtensionNumber = qlAddress.ExtensionNumber;
			mcAddress.County = qlAddress.County;
			mcAddress.CountyCode = qlAddress.CountyCode;
			mcAddress.Urbanization = qlAddress.Urbanization;
			mcAddress.UrbanizationCode = qlAddress.UrbanizationCode;
			mcAddress.City = qlAddress.City;
			mcAddress.PostalCode = qlAddress.PostalCode;
			mcAddress.PlusFour = qlAddress.PlusFour;
			mcAddress.Phone = qlAddress.Phone;
			mcAddress.DeliveryPoint = qlAddress.DeliveryPoint;
			mcAddress.CrossStreet = qlAddress.CrossStreet;
			mcAddress.Latitude = qlAddress.Latitude;
			mcAddress.Longitude = qlAddress.Longitude;
			mcAddress.CongressionalDistric = qlAddress.CongressionalDistric;
			mcAddress.DPV = qlAddress.DPV;
			mcAddress.DPVResponse = qlAddress.DPVResponse;
			mcAddress.DPVFootNote = qlAddress.DPVFootnote;
			mcAddress.CarrierRoute = qlAddress.CarrierRoute;
			// make sure address is active even if the lead address is not
			mcAddress.IsActive = true;// qlAddress.IsActive;
			mcAddress.IsDeleted = false;// qlAddress.IsDeleted;
		}

		private static async Task<AE_Customer> CreateAeCustomer(string gpEmployeeId, CrmDb db, QL_Lead lead, MC_Address mcAddress)
		{
			var tbl = db.AE_Customers;
			// map lead to customer 1 to 1
			// check if a customer already exists for the lead
			var customer = (await tbl.OneByLeadIdAsync(lead.LeadID).ConfigureAwait(false));
			if (customer == null)
			{
				customer = new AE_Customer();
				CopyFromLead(customer, lead, mcAddress);
				await tbl.InsertAsync(customer, gpEmployeeId).ConfigureAwait(false);
			}
			// lead data should not change but customer data can, so updating the customer should be unnecessary and may overwrite changed data
			//@REVIEW: it may be possible to add data to a lead, but not modify, e.g.: change a null value to a non-null value
			//else
			//{
			//	var snapShot = Snapshotter.Start(customer);
			//	CopyFromLead(customer, lead, mcAddress);
			//	await tbl.UpdateAsync(snapShot, gpEmployeeId).ConfigureAwait(false);
			//}

			return customer;
		}
		private static void CopyFromLead(AE_Customer customer, QL_Lead lead, MC_Address mcAddress)
		{
			customer.CustomerTypeId = lead.CustomerTypeId;
			customer.CustomerMasterFileId = lead.CustomerMasterFileId;
			customer.DealerId = lead.DealerId;
			customer.AddressId = mcAddress.AddressID;
			customer.LeadId = lead.LeadID;
			customer.LocalizationId = lead.LocalizationId;
			customer.Prefix = lead.Salutation;
			customer.FirstName = lead.FirstName;
			customer.MiddleName = lead.MiddleName;
			customer.LastName = lead.LastName;
			customer.Postfix = lead.Suffix;
			//customer.BusinessName = lead.BusinessName;
			customer.Gender = lead.Gender;
			customer.PhoneHome = lead.PhoneHome ?? mcAddress.Phone; // This is needed because this PhoneHome maps to the panel phone.
			customer.PhoneWork = lead.PhoneWork;
			customer.PhoneMobile = lead.PhoneMobile;
			customer.Email = lead.Email;
			customer.DOB = lead.DOB;
			customer.SSN = lead.SSN;
			//customer.Username = lead.Username;
			//customer.Password = lead.Password;
			customer.IsActive = lead.IsActive;
			customer.IsDeleted = lead.IsDeleted;
		}

		//List<AE_CustomerAccount> existingCustAccounts = null;
		//// check for existing customer
		//var customer = await db.AE_Customers.OneByLeadIdAsync(leadId).ConfigureAwait(false);
		//if (customer != null)
		//{
		//	//var custAccts = (await db.AE_CustomerAccounts.ManyByCustomerIdAsync(customer.CustomerID).ConfigureAwait(false)).ToList();
		//	//// if there are any customer types(excluding the current customerTypeId) using this customer, create a new customer
		//	//if (custAccts.Count(ca => !AreEqual(ca.CustomerTypeId, customerTypeId)) > 0)
		//	//	customer = null;
		//	//else
		//	//{
		//	//	// delete these later
		//	//	existingCustAccounts = custAccts.Where(ca => AreEqual(ca.CustomerTypeId, customerTypeId)).ToList();
		//	//}
		//}
		//
		//private static Func<AE_CustomerAccount, bool> MatchesCustomerTypeId(string customerTypeId)
		//{
		//	return (ca) =>
		//	{
		//		return string.Equals(ca.CustomerTypeId, customerTypeId, StringComparison.OrdinalIgnoreCase);
		//	};
		//}
		//private static bool AreEqual(string a, string b)
		//{
		//	return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
		//}

		//public async Task<Result<MsAccountSalesInformationExtras>> SaveAccountSalesInformationExtras(long accountId, MsAccountSalesInformationExtras inputItem)
		//{
		//	using (var db = CrmDb.Connect())
		//	{
		//		var result = new Result<MsAccountSalesInformationExtras>();
		//		var tbl = db.MS_AccountSalesInformations;

		//		var item = (await tbl.ByIdAsync(accountId).ConfigureAwait(false));
		//		if (item == null)
		//			return result.Fail(-1, "Invalid AccountID");
		//		if (!string.IsNullOrEmpty((result.Message = VersionException.ModifiedOnErrMsg(item.ModifiedOn, inputItem.ModifiedOn))))
		//		{
		//			result.Value = MsAccountSalesInformationExtras.FromDb(item);
		//			return result.Fail((int)BaseErrorCodes.ErrorCodes.InvalidModifiedOn, result.Message);
		//		}

		//		var snapShot = Snapshotter.Start(item);
		//		inputItem.ToDb(item);
		//		// save
		//		await tbl.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);

		//		result.Value = MsAccountSalesInformationExtras.FromDb(item);
		//		return result;
		//	}
		//}

		//public async Task<Result<MsAccountSalesInformation>> AccountSalesInformation(long accountId)
		//{
		//	using (var db = CrmDb.Connect())
		//	{
		//		var result = new Result<MsAccountSalesInformation>();
		//		var tbl = db.MS_AccountSalesInformations;
		//
		//		var msAcctSalesInfo = (await tbl.ByIdAsync(accountId).ConfigureAwait(false));
		//		result.Value = MsAccountSalesInformation.FromDb(msAcctSalesInfo, true);
		//		return result;
		//	}
		//}

		public async Task<Result<Noc>> NocDate(DateTime startDate)
		{
			using (var db = CrmDb.Connect())
			{
				var result = new Result<Noc>();
				const string sql = "SELECT WISE_CRM.dbo.fxGetLastNOCDate(@startDate)";
				var nocDate = (await db.QueryAsync<DateTime>(sql, new { startDate }).ConfigureAwait(false)).First();
				result.Value = new Noc() { NOCDate = nocDate };
				return result;
			}
		}
	}
}
