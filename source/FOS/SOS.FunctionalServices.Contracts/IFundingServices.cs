/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 03/24/15
 * Time: 10:08
 * 
 * Description:  Implementes the Funding Services Engine
 *********************************************************************************************************************/

using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Funding;

namespace SOS.FunctionalServices.Contracts
{
	public interface IFundingServices
	{
		IFnsResult<List<IFnsFeCriteria>> CriteriaReadAll(string gpEmployeeId);

		IFnsResult<List<IFnsFePacketView>> PacketReadAll(string gpEmployeeId);

		IFnsResult<List<IFnsFePacketItemView>> PacketItemsRead(int packetId, string gpEmployeeId);

		IFnsResult<List<IFnsFeBundle>> BundleReadAll(string gpEmployeeId);

		IFnsResult<IFnsFeBundle> BundleRead(int bundleID, string gpEmployeeId);

		IFnsResult<List<IFnsFeBundleItem>> BundleItemsRead(int bundleId, string gpEmployeeId);
	}
}