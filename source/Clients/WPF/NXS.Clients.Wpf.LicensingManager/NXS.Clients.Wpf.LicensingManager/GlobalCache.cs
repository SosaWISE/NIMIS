using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NXS.Clients.Wpf.LicensingManager.Models;
using NXS.Framework.Wpf.ParentChildService;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.Lib.Util;

namespace NXS.Clients.Wpf.LicensingManager
{
	public static class GlobalCache
	{
		#region Parent Communicator
		private static readonly object _syncParentComm = new object();
		private static ParentCommunicator _parentComm;
		public static ParentCommunicator ParentComm
		{
			get
			{
				if (_parentComm == null)
				{
					lock (_syncParentComm)
					{
						if (_parentComm == null)
						{
							_parentComm = new ParentCommunicator(Application.Current.MainWindow);
						}
					}
				}
				return _parentComm;
			}
		}
		#endregion

		#region CachedLists
		public static readonly CachedList<GPDepartmentModel> GPCorpDepartments = new CachedList<GPDepartmentModel>(GPDepartmentModel.GetEmployeeDepartments);
		public static readonly CachedList<GeneralModel> ActiveReasons = new CachedList<GeneralModel>(GetActiveReasons);

		public static readonly CachedList<MC_PoliticalState> States = new CachedList<MC_PoliticalState>
			(() => DataContextController.Instance.InterimContext.MC_PoliticalStates.GetAllInCountry().ToList());
		#endregion CachedLists

		#region Methods
		public static List<GeneralModel> GetActiveReasons()
		{
			var models = new List<GeneralModel>
			{
				new GeneralModel(1, "Both (Active/Inactive)"),
				new GeneralModel(2, "Active"),
				new GeneralModel(3, "Inactive")
			};
			return models;
		}
		#endregion Methods
	}
}
