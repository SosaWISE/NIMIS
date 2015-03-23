using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Connext;

namespace SOS.FunctionalServices.Contracts
{
	public interface IConnextService : IFunctionalService
	{
		IFnsResult<IFnsCxContact> ContactCreate(IFnsCxContact contact, string gpEmployeeId);
		IFnsResult<List<IFnsCxContact>> ContactReadAll(string gpEmployeeId);
		IFnsResult<IFnsCxContact> ContactRead(long contactId, string gpEmployeeId);
		IFnsResult<IFnsCxContact> ContactUpdate(IFnsCxContact contact, string gpEmployeeId);
		IFnsResult<IFnsCxContact> ContactDelete(long contactId, string gpEmployeeId);


		IFnsResult<IFnsCxAddress> AddressCreate(IFnsCxAddress address, string gpEmployeeId);
		IFnsResult<List<IFnsCxAddress>> AddressReadAll(string gpEmployeeId);
		IFnsResult<IFnsCxAddress> AddressRead(long addressId, string gpEmployeeId);
		IFnsResult<IFnsCxAddress> AddressUpdate(IFnsCxAddress address, string gpEmployeeId);
		IFnsResult<IFnsCxAddress> AddressDelete(long addressId, string gpEmployeeId);


	}
}