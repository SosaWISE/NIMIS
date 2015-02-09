using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.QualifyLead;

namespace SOS.FunctionalServices.Contracts
{
	public interface IQualifyLeadServices : IFunctionalService
	{
		#region QlAddress CRUD

		IFnsResult<IFnsQlAddress> QlAddressCreate(IFnsQlAddress address, string gpEmployeeId);
		IFnsResult<IFnsQlAddress> QlAddressRead(long addressId, string gpEmployeeId);
		IFnsResult<IFnsQlAddress> QlAddressUpdate(IFnsQlAddress fnsAddress, string gpEmployeeId);
		IFnsResult<IFnsQlAddress> QlAddressDelete(long addressId, string gpEmployeeId);

		#endregion QlAddress CRUD

		#region QlQualifyCustomerInfoRead
		IFnsResult<IFnsQlQualifyCustomerInfo> QlQualifyCustomerInfoReadByLeadId(long leadId, string gpEmployeeID);

		IFnsResult<IFnsQlQualifyCustomerInfo> QlQualifyCustomerInfoReadByCustomerId(long customerId, string gpEmployeeID);

		IFnsResult<IFnsQlQualifyCustomerInfo> QlQualifyCustomerInfoReadByAccountId(long accountId, string gpEmployeeID);

		#endregion QlQualifyCustomerInfoRead
	}
}