using NSE.FOS.Contracts.Models;
using SOS.Data.SosCrm;
using SOS.FOS.MonitoringStationServices.Contracts.Models;
using System;
using System.Collections.Generic;

namespace SOS.FOS.MonitoringStationServices
{
	public class Main
	{
		#region .ctor

		public Main() : this(MonitoringStations.AutoSelect) { }

		public Main(MonitoringStations msToUse)
		{
			MsToUse = msToUse;

			switch (msToUse)
			{
				case MonitoringStations.AvantGuard:
					MsStation = new AvantGuard.CentralStation();
					break;
				case MonitoringStations.Monitronics:
					MsStation = new Monitronics.CentralStation();
					break;
				default:
					MsStation = new AvantGuard.CentralStation();
					break;
			}
		}

		#endregion .ctor

		#region Properties

		public enum MonitoringStations
		{
			AutoSelect = 0,
			AvantGuard = 1,
			Monitronics = 2
		}

		public MonitoringStations MsToUse { get; private set; }

		public IMonitoringStation MsStation { get; private set; }

		#endregion Properties

		#region Methods

		public FosResult<MS_AccountSubmit> AccountCreate(MS_AccountSubmit msAccountSubmit, string gpEmployeeId)
		{
			return MsStation.AccountOnboard(msAccountSubmit, gpEmployeeId) as FosResult<MS_AccountSubmit>;
		}

		public FosResult<MS_AccountSubmit> AccountShell(MS_AccountSubmit msAccountSubmit, string gpEmployeeId)
		{
			return MsStation.AccountShell(msAccountSubmit, gpEmployeeId) as FosResult<MS_AccountSubmit>;
		}

		public FosResult<MS_AccountSubmit> AccountUpdate(long accountID, string gpEmployeeId)
		{
			return MsStation.AccountUpdate(accountID, gpEmployeeId) as FosResult<MS_AccountSubmit>;
		}

		public FosResult<List<IFosSignalHistoryItem>> GetSignalHistory(DateTime startDate, DateTime endDate, string transmitterCode)
		{
			return MsStation.GetSignalHistory(startDate, endDate, transmitterCode);
		}

		public FosResult<bool> UpdateContacts(long accountId)
		{
			return MsStation.UpdateContacts(accountId);
		}

		public FosResult<object> TwoWayTestData(long accountId)
		{
			return MsStation.TwoWayTestData(accountId);
		}
		public FosResult<object> InitTwoWayTest(long accountId, string gpEmployeeId)
		{
			return MsStation.InitTwoWayTest(accountId, gpEmployeeId);
		}
		public FosResult<object> CompleteTwoWayTest(long accountId, string confirmedBy, string gpEmployeeId)
		{
			return MsStation.CompleteTwoWayTest(accountId, confirmedBy, gpEmployeeId);
		}
		public FosResult<List<IFosDeviceTest>> ActiveTests(long accountId)
		{
			return MsStation.ActiveTests(accountId);
		}
		public FosResult<bool> ClearActiveTests(long accountId)
		{
			return MsStation.ClearActiveTests(accountId);
		}
		public FosResult<bool> ClearTest(long accountId, int testNum)
		{
			return MsStation.ClearTest(accountId, testNum);
		}
		public FosResult<string> ServiceStatus(long accountId)
		{
			return MsStation.ServiceStatus(accountId);
		}
		public FosResult<string> SetServiceStatus(long accountId, string oosCat, DateTime startDate, string comment, string gpEmployeeId)
		{
			return MsStation.SetServiceStatus(accountId, oosCat, startDate, comment, gpEmployeeId);
		}

		public static MonitoringStations GetMsChoice(MS_ReceiverLine receiverLine, out bool shellAccount)
		{
			var msChoice = Main.MonitoringStations.AutoSelect;
			shellAccount = true;

			switch (receiverLine.MonitoringStationOSId)
			{
				case MS_MonitoringStationOss.MetaDataStatic.MI_MASTER:
					msChoice = Main.MonitoringStations.Monitronics;
					shellAccount = false;
					break;
				case MS_MonitoringStationOss.MetaDataStatic.AG_ALARMSYS:
					msChoice = Main.MonitoringStations.AvantGuard;
					break;
			}
			// ** Return result
			return msChoice;
		}

		#endregion Methods
	}
}