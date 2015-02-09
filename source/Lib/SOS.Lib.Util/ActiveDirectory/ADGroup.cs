// Wrapper API for using Microsoft Active Directory Services version 1.0
// Copyright (c) 2004-2005
// by Syed Adnan Ahmed ( adnanahmed235@yahoo.com )
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace SOS.Lib.Util.ActiveDirectory
{
	[Serializable]
	public class ADGroup
	{
		#region Enums

		public struct GroupNames
		{
// ReSharper disable InconsistentNaming
			public const string AccountingControllers = "AccountingControllers";
			public const string AccountManagementFunding = "AccountManagementFunding";
			public const string AccountManagementScanning = "AccountManagementScanning";
			public const string AccountManagers = "AccountManagers";
			public const string CallCenterMgrs = "CallCenterMgrs";
			public const string Collections = "Collections";
			public const string CollectionsAccountDeactivation = "CollectionsAccountDeactivation";
			public const string CollectionsMgrs = "CollectionsMgrs";
			public const string CorporateInventoryManagers = "Inventory Corporate Managers";
			public const string CorpInventory = "CorpInventory";
			public const string CRMServiceTech = "CRMServiceTech";
			public const string CSRMove = "CSRMove";
			public const string DataEntry = "DataEntry";
			public const string DataEntryMgrs = "DataEntryMgrs";
			public const string Executive = "Executive";
			public const string HMAdmin = "HMAdmin";
			public const string HR_HR = "HR-HR";
			public const string HRGroup = "HRGroup";
			public const string InventoryManagers = "Inventory Managers";
			public const string ITShares = "ITShares";
			public const string Licensing = "Licensing";
			public const string LicensingAccess = "Licensing Access";
			public const string OfficeAssistants = "Office Assistants";
			public const string Payroll = "Payroll";
			public const string PayrollHistoryEntry = "Payroll History Entry";
			public const string PayrollMgr = "PayrollMgr";
			public const string Retention = "Retention";
			public const string ServiceGroup = "Service Group";
			public const string ServicePayrollAccess = "ServicePayrollAccess";
			public const string ServiceManagement = "Service Management";
			public const string LicenseBypass = "License Bypass";
			public const string CancelPage = "CancelPage";
			public const string HiringManagerAccess = "HiringManagerAccess";
			public const string HiringManagerReadOnly = "HiringManagerReadOnly";
			public const string SVCSchAdmin = "SVCSchAdmin";
			public const string SVCSchOverride = "SVCSchOverride";
			public const string SVCSchAccess = "SVCSchAccess";
			public const string CreditLink = "CreditLink";
			public const string RollCallMgr = "RollCallMgr";
// ReSharper restore InconsistentNaming
		}

		#endregion Enums

		#region Private Property Variables

		private string _description;
		private string _displayName;
		private string _distinguishedName;
		private string _name; //(cn ) in Active Directory
		private List<ADUser> _users;

		#endregion

		#region Public Properties

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public string DisplayName
		{
			get { return _displayName; }
			set { _displayName = value; }
		}

		public string DistinguishedName
		{
			get { return _distinguishedName; }
			set { _distinguishedName = value; }
		}

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		public List<ADUser> Users
		{
			get { return _users ?? (_users = ADUser.LoadByGroup(DistinguishedName)); }
			set { _users = value; }
		}

		#endregion

		#region Friend Functions

		internal static List<ADGroup> LoadByUser(string szDistinguishedName)
		{
			return GetGroups(szDistinguishedName);
		}

		public void Update()
		{
			try
			{
				DirectoryEntry de = ADUtility.GetGroup(Name);
				ADUtility.SetProperty(de, "DisplayName", DisplayName);
				ADUtility.SetProperty(de, "Description", Description);
				de.CommitChanges();
			}
			catch (Exception ex)
			{
				throw (new Exception("User cannot be updated" + ex.Message));
			}
		}

		#endregion

		#region Private Functions

		private static List<ADGroup> GetGroups(string szDistinguishedName)
		{
			DirectoryEntry lDe = ADUtility.GetDirectoryObjectByDistinguishedName(szDistinguishedName);
			int index;
			var list = new List<ADGroup>();
			for (index = 0; index <= lDe.Properties["memberOf"].Count - 1; index++)
			{
				// Locals
				DirectoryEntry oDe =
					ADUtility.GetDirectoryObjectByDistinguishedName(ADUtility.ADPath + "/" +
					                                                lDe.Properties["memberOf"][index]);
				list.Add(Load(oDe));
				GetNestedGroups(ADUtility.ADPath + "/" + ADUtility.GetProperty(oDe, "DistinguishedName"), ref list);
			}
			return list;
		}

		private static void GetNestedGroups(string szDistinguishedName, ref List<ADGroup> list)
		{
			// Locals
			DirectoryEntry lDe = ADUtility.GetDirectoryObjectByDistinguishedName(szDistinguishedName);
			int index;
			for (index = 0; index <= lDe.Properties["memberOf"].Count - 1; index++)
			{
				// Locals
				DirectoryEntry oDe =
					ADUtility.GetDirectoryObjectByDistinguishedName(ADUtility.ADPath + "/" +
					                                                lDe.Properties["memberOf"][index]);
				list.Add(Load(oDe));
				GetNestedGroups(ADUtility.ADPath + "/" + ADUtility.GetProperty(oDe, "DistinguishedName"), ref list);
			}
		}

		private static ADGroup Load(DirectoryEntry de)
		{
			var oADGroup = new ADGroup
			               	{
			               		Name = ADUtility.GetProperty(de, "cn"),
			               		DisplayName = ADUtility.GetProperty(de, "DisplayName"),
			               		DistinguishedName = ADUtility.ADPath + "/" + ADUtility.GetProperty(de, "DistinguishedName"),
			               		Description = ADUtility.GetProperty(de, "Description")
			               	};
			return oADGroup;
		}

		#endregion
	}
}