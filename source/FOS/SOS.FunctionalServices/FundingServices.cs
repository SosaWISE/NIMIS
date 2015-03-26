/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 03/24/15
 * Time: 10:08
 * 
 * Description:  Implementes the Funding Services Engine
 *********************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using NXS.Data.Funding;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Funding;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.Funding;
using SOS.Lib.Core.ErrorHandling;

namespace SOS.FunctionalServices
{
	public class FundingServices : IFundingServices
	{
		public IFnsResult<List<IFnsFeCriteria>> CriteriaReadAll(string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "CriteriaReadAll";
			var result = new FnsResult<List<IFnsFeCriteria>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var feCriterias = NxseFundingDataContext.Instance.FE_CriteriasViews.LoadCollection(FE_CriteriasView.Query());
				var fnsList = feCriterias.Select(feCriteria => new FnsFeCriteria(feCriteria)).Cast<IFnsFeCriteria>().ToList();

				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Value = fnsList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsFeCriteria>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsFePacketView>> PacketReadAll(string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "PacketReadAll";
			var result = new FnsResult<List<IFnsFePacketView>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var fePackets = NxseFundingDataContext.Instance.FE_PacketsViews.LoadCollection(NxseFundingDataStoredProcedureManager.FE_PacketsReadOpen());
				var fnsList = fePackets.Select(fePacket => new FnsFePacketView(fePacket)).Cast<IFnsFePacketView>().ToList();

				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Value = fnsList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsFePacketView>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		// PacketItemsRead
		public IFnsResult<List<IFnsFePacketItemView>> PacketItemsRead(int packetId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "PacketReadAll";
			var result = new FnsResult<List<IFnsFePacketItemView>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var fePacketItems = NxseFundingDataContext.Instance.FE_PacketItemsViews.LoadCollection(NxseFundingDataStoredProcedureManager.FE_PacketItemsViewByPacketID(packetId));
				var fnsList = fePacketItems.Select(fePacketItem => new FnsFePacketItemView(fePacketItem)).Cast<IFnsFePacketItemView>().ToList();

				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Value = fnsList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsFePacketItemView>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}
	}
}
