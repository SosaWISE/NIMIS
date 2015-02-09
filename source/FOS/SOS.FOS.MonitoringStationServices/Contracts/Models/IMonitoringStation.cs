using NSE.FOS.Contracts.Models;
using SOS.Data.SosCrm;
using System;
using System.Collections.Generic;

namespace SOS.FOS.MonitoringStationServices.Contracts.Models
{
	public interface IMonitoringStation
	{
		IFosResult<MS_AccountSubmit> AccountShell(MS_AccountSubmit msAccountSubmit, string gpEmployeeId);

		IFosResult<MS_AccountSubmit> AccountOnboard(MS_AccountSubmit msAccountSubmit, string gpEmployeeId);

		IFosResult<MS_AccountSubmit> AccountUpdate(long accountID, string gpEmployeeId);

		FosResult<List<IFosSignalHistoryItem>> GetSignalHistory(DateTime startDate, DateTime endDate, string transmitterCode);

		FosResult<bool> UpdateContacts(long accountId);

		FosResult<object> TwoWayTestData(long accountId);
		FosResult<object> InitTwoWayTest(long accountId, string gpEmployeeId);
		FosResult<object> CompleteTwoWayTest(long accountId, string confirmedBy, string gpEmployeeId);
		FosResult<List<IFosDeviceTest>> ActiveTests(long accountId);
		FosResult<bool> ClearActiveTests(long accountId);
		FosResult<bool> ClearTest(long accountId, int testNum);
		FosResult<string> ServiceStatus(long accountId);
		FosResult<string> SetServiceStatus(long accountId, string oosCat, DateTime startDate, string comment, string gpEmployeeId);

		FosResult<bool> GenerateMetaData(long? accountId = null, string username = "SYSTEM");

		FosResult<List<MS_DispatchAgency>> FindDispatchAgency(string agencyTypeId, string phone, string city, string state, string zip, string gpEmployeeId);
	}
}