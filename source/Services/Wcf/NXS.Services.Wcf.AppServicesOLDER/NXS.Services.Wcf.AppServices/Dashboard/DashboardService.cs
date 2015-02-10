using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using SOS.Data;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using NXS.Data.FileStore;
using SOS.Lib.Util;
using NXS.Services.Wcf.AppServices.Dashboard.Models;
using ConfigurationSettings = SOS.Lib.Util.Configuration.ConfigurationSettings;

namespace NXS.Services.Wcf.AppServices.Dashboard
{
	public class DashboardService : IDashboardService
	{
		#region .ctor

		public DashboardService()
		{
			if (!IsSetup) SetupSystem();

			_cmsDB = SosCrmDataContext.Instance;
			_fileStoreDB = FileStoreDataContext.Instance;
		}

		private static void SetupSystem()
		{
			Debug.WriteLine("Hopefully this is never called.");
		}

		#endregion //.ctor

		#region Fields

		private static readonly object _syncSetup = new object();
		private static object _setup;
		private static bool _isSetup;
		private readonly FileStoreDataContext _fileStoreDB;
		private readonly SosCrmDataContext _cmsDB;

		public static bool IsSetup
		{
			get
			{
				if (_setup == null)
					lock (_syncSetup)
					{
						if (_setup == null)
						{
							// Initialize connection strings
							ConfigurationSettings.Current.SetProperties("Preferences",
																		ConfigurationManager.
																			AppSettings["Environment"]);
							SubSonicConfigHelper.SetupConnectionStrings();

							_setup = new object();
							_isSetup = true;
						}
					}
				// Return value
				return _isSetup;
			}
		}

		#endregion //Fields

		#region Methods

		private static DashboardApplication BuildDashboardApplication(UI_ApplicationVersionsView app)
		{
			return new DashboardApplication
			{
				ApplicationID = app.ApplicationID,
				ApplicationName = app.ApplicationName,
				ExecutableFileName = app.ExecutableFileName,
				FriendlyName = app.FriendlyName,
				VersionNumber = app.VersionNumber
			};
		}

		private byte[] LoadFileData(int? fileID)
		{
			byte[] result = null;
			if (fileID != null)
			{
				FS_File file = _fileStoreDB.FS_Files.LoadByPrimaryKey(fileID.Value);
				if (file != null && file.IsLoaded)
					result = file.FileData;
			}
			return result;
		}

		#region Interface Items

		public IList<DashboardApplication> GetApplications(List<ApplicationPermission> oPermissions)
		{
			var result = new Dictionary<int, DashboardApplication>();

			foreach (ApplicationPermission curr in oPermissions)
			{
				object typeValue = Enum.Parse(typeof(UI_PermissionType.PermissionTypeEnum), curr.TypeID.ToString());
				UI_PermissionType.PermissionTypeEnum permissionType;
				if (typeValue != null && typeValue is UI_PermissionType.PermissionTypeEnum)
					permissionType = (UI_PermissionType.PermissionTypeEnum)typeValue;
				else
					permissionType = UI_PermissionType.PermissionTypeEnum.Ad_User; // Use this as default

				foreach (UI_ApplicationVersionsView currApp in
					_cmsDB.UI_ApplicationVersionsViews.GetApplicationsByPermission(permissionType, curr.PrincipalName))
				{
					if (!result.ContainsKey(currApp.ApplicationID))
					{
						result.Add(currApp.ApplicationID, BuildDashboardApplication(currApp));
					}
				}
			}

			return new List<DashboardApplication>(result.Values);
		}

		public IList<ActionApplictionMapping> GetActionApplicationMappings(List<ApplicationPermission> olPermissions)
		{
			var result = new List<ActionApplictionMapping>();

			string username = null;
			var adGroups = new List<string>();
			foreach (ApplicationPermission curr in olPermissions)
			{
				if (curr.TypeID == (int)UI_PermissionType.PermissionTypeEnum.Ad_Group)
				{
					adGroups.Add(curr.PrincipalName);
				}
				else if (curr.TypeID == (int)UI_PermissionType.PermissionTypeEnum.Ad_User)
				{
					username = curr.PrincipalName;
				}
			}

			using (
				DataSet dsMappings =
					SosCrmDataStoredProcedureManager.UI_MenuItemsGetActionApplicationMappingsAD(username,
																							 StringUtility.Join(adGroups, ",")).
						GetDataSet())
			{
				foreach (DataRow currRow in dsMappings.Tables[0].Rows)
				{
					var actionName = (string)currRow[UI_ApplicationMenuView.Columns.ActionName];
					var appID = (int)currRow[UI_ApplicationMenuView.Columns.ApplicationID];

					result.Add(
						new ActionApplictionMapping
						{
							ActionName = actionName,
							ApplicationID = appID
						});
				}
			}

			return result;
		}

		public byte[] GetSmallIconFile(int nApplicationID)
		{
			byte[] btResult = null;

			UI_ApplicationVersionsView oAppVersion =
				_cmsDB.UI_ApplicationVersionsViews.GetApplicationVersionView(nApplicationID);
			if (oAppVersion != null && oAppVersion.IsLoaded)
				btResult = LoadFileData(oAppVersion.SmallIconFileId);

			return btResult;
		}

		public byte[] GetIconFile(int nApplicationID)
		{
			byte[] btResult = null;

			UI_ApplicationVersionsView oAppVersion =
				_cmsDB.UI_ApplicationVersionsViews.GetApplicationVersionView(nApplicationID);
			if (oAppVersion != null && oAppVersion.IsLoaded)
				btResult = LoadFileData(oAppVersion.IconFileId);

			return btResult;
		}

		public byte[] GetDeployedFile(int nApplicationID)
		{
			byte[] btResult = null;

			UI_ApplicationVersionsView oAppVersion =
				_cmsDB.UI_ApplicationVersionsViews.GetApplicationVersionView(nApplicationID);
			if (oAppVersion != null && oAppVersion.IsLoaded)
				btResult = LoadFileData(oAppVersion.DeployedFileID);

			return btResult;
		}

		public IList<Message> GetMessagesForUser(string szUsername, bool bIncludeRead, bool bIncludeDeleted)
		{
			var result = new List<Message>();

			foreach (
				UI_Message oCurrMessage in _cmsDB.UI_Messages.GetMessagesByRecipient(szUsername, bIncludeRead, bIncludeDeleted))
			{
				var message = new Message
				{
					MessageID = oCurrMessage.MessageID,
					RecipientID = oCurrMessage.RecipeintID,
					Subject = oCurrMessage.Subject,
					MessageText = oCurrMessage.Message,
					ReadOn = oCurrMessage.ReadOn,
					CreatedBy = oCurrMessage.CreatedBy,
					CreatedOn = oCurrMessage.CreatedOn,
					IsDeleted = oCurrMessage.IsDeleted,
					Actions = new List<MessageAction>()
				};

				foreach (UI_MessageAction currAction in oCurrMessage.UI_MessageActions)
				{
					var action = new MessageAction
					{
						ActionName = currAction.Action.ActionName,
						Label = currAction.Label,
						Parameters = new List<MessageActionParameter>()
					};

					foreach (UI_MessageActionParameter currParam in currAction.UI_MessageActionParameters)
					{
						action.Parameters.Add(
							new MessageActionParameter
							{
								Name = currParam.ParameterName,
								Value = currParam.ParemterValue
							});
					}

					message.Actions.Add(action);
				}

				result.Add(message);
			}

			return result;
		}

		public void MarkMessageRead(int nMessageID)
		{
			UI_Message oMsg = _cmsDB.UI_Messages.LoadByPrimaryKey(nMessageID);
			if (oMsg != null)
			{
				oMsg.ReadOn = DateTime.Now;
				oMsg.Save();
			}
		}

		public void DeleteMessage(int nMessageID)
		{
			UI_Message oMsg = _cmsDB.UI_Messages.LoadByPrimaryKey(nMessageID);
			if (oMsg != null)
			{
				oMsg.IsDeleted = true;
				oMsg.Save();
			}
		}

		#endregion Interface Items

		#endregion Methods
	}
}