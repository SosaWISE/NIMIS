using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using PPro.Lib.Data.Licensing;
using PPro.Lib.Data.Recruiting;
using PPro.Lib.Util.Cryptography;
using System.IO;
using PPro.Lib.Data.Letters;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Diagnostics;
using PPro.Lib.Windows.Forms.ErrorHandling;
using PPro.Lib.Pdf;
using System.Collections;
using PPro.Lib.Windows.LicenseManagement;

namespace PPro.WindowsClients.LicenseManagement
{
    public partial class UploadExcelFile : ManagedErrorForm
    {
        #region Properties

        public UploadExcelFile()
        {
            InitializeComponent();
        }

        private DataTable DefaultTable(string SheetName)
        {

            DataTable oDt = new DataTable();
            if (ofdExcelFile.ShowDialog() == DialogResult.OK)
            {
                string excelConnectionString =
                        string.Format(
                            @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;""",
                            ofdExcelFile.FileName);
                OleDbConnection oCon = new OleDbConnection(excelConnectionString);
                oCon.Open();
                OleDbCommand oCmd = new OleDbCommand(string.Format("SELECT * FROM [{0}$]", SheetName), oCon);
                OleDbDataAdapter Dataadapt = new OleDbDataAdapter();
                DataSet oDs = new DataSet();
                Dataadapt.SelectCommand = oCmd;
                Dataadapt.Fill(oDs);
                oDt = oDs.Tables[0];
            }
            return oDt;

        }

        private DataTable _licensetable = null;

        private DataTable LicenseTable
        {
            get
            {
                if (_licensetable == null)
                {
                    if (ofdExcelFile.ShowDialog() == DialogResult.OK)
                    {
                        string excelConnectionString =
                                string.Format(
                                    @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;""",
                                    ofdExcelFile.FileName);
                        OleDbConnection oCon = new OleDbConnection(excelConnectionString);
                        oCon.Open();
                        OleDbCommand oCmd = new OleDbCommand("SELECT * FROM [Locations$]", oCon);
                        OleDbDataAdapter Dataadapt = new OleDbDataAdapter();
                        DataSet oDs = new DataSet();
                        Dataadapt.SelectCommand = oCmd;
                        Dataadapt.Fill(oDs);
                        _licensetable = new DataTable();
                        _licensetable = oDs.Tables[0];
                    }
                }
                return _licensetable;
            }
        }

        private DataTable _CityCountyTable = null;

        private DataTable CityCountytable
        {
            get
            {
                if (_CityCountyTable == null)
                {
                    if (ofdExcelFile.ShowDialog() == DialogResult.OK)
                    {
                        string excelConnectionString =
                                string.Format(
                                    @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;""",
                                    ofdExcelFile.FileName);
                        OleDbConnection oCon = new OleDbConnection(excelConnectionString);
                        oCon.Open();
                        OleDbCommand oCmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oCon);
                        OleDbDataAdapter Dataadapt = new OleDbDataAdapter();
                        DataSet oDs = new DataSet();
                        Dataadapt.SelectCommand = oCmd;
                        Dataadapt.Fill(oDs);
                        _CityCountyTable = new DataTable();
                        _CityCountyTable = oDs.Tables[0];

                        foreach (DataRow currRow in _CityCountyTable.Rows)
                        {
                            if (currRow["County"].ToString() != currRow["County"].ToString().ToUpper() || currRow["City"].ToString() != currRow["City"].ToString().ToUpper())
                            {
                                currRow.Delete();
                            }
                        }
                    }
                }
                return _CityCountyTable;
            }
        }

        private DataTable _stateLicensingAndRegistration = null;

        private DataTable StateLicensingAndRegistration
        {
            get
            {
                if (_stateLicensingAndRegistration == null)
                {
                    if (ofdExcelFile.ShowDialog() == DialogResult.OK)
                    {
                        string excelConnectionString =
                                string.Format(
                                    @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;""",
                                    ofdExcelFile.FileName);
                        OleDbConnection oCon = new OleDbConnection(excelConnectionString);
                        oCon.Open();
                        OleDbCommand oCmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oCon);
                        OleDbDataAdapter Dataadapt = new OleDbDataAdapter();
                        DataSet oDs = new DataSet();
                        Dataadapt.SelectCommand = oCmd;
                        Dataadapt.Fill(oDs);
                        _stateLicensingAndRegistration = new DataTable();
                        _stateLicensingAndRegistration = oDs.Tables[0];
                    }
                }
                return _stateLicensingAndRegistration;
            }
        }

        #endregion

        #region Upload Methods

        private void GetPlatCertificationStuff()
        {
            DataTable oDt = DefaultTable("Sheet2");
            foreach (DataRow currRow in oDt.Rows)
            {
                string GPEmpID = currRow["GPID"].ToString();
                string Score = currRow["SCORE"].ToString();
                int RequirementID = 83;

                LM_License oLicense = new LM_License();
                oLicense.RequirementID = RequirementID;
                oLicense.GPEmployeeID = GPEmpID;
                oLicense.IssueDate = DateTime.Parse("09/01/2008");
                oLicense.ExpirationDate = DateTime.Parse("12/31/2008");
                oLicense.RequirementsAreMet = true;
                oLicense.IsActive = true;
                oLicense.IsDeleted = false;
                oLicense.ModifiedByDate = DateTime.Now;
				oLicense.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                oLicense.CreatedByDate = DateTime.Now;
				oLicense.CreatedByID = LicenseManagementHelper.LoggedInUser;
                oLicense.Save();

                LM_Requirement oRequirement = LM_Requirement.LoadByPrimaryKey(RequirementID);

                foreach (LM_RequirementItem oItem in oRequirement.LM_RequirementItems)
                {
                    AddLicenseItem(oLicense.LicenseID, true, oItem.Name, oItem.Description);
                }

                if (Score != "")
                {
                    AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Score: %" + Score);
                }
            }
        }

        private void GetRepOrTechByGPEmpID()
        {
            DataTable oDt = DefaultTable("Sheet1");
            DataTable Datasource = new DataTable();
            Datasource.Columns.Add("Full Name");
            Datasource.Columns.Add("Rep or Tech");
            Datasource.Columns.Add("State");
            foreach (DataRow currRow in oDt.Rows)
            {
                string GPEmployeeID = currRow["GPEMPID"].ToString();
                string FullName = currRow["FIRST"].ToString() + " " + currRow["LAST"].ToString();
                string State = currRow["STATE"].ToString();
                string RepOrTech = "";
                if (GPEmployeeID != "NULL")
                {
                    RU_User oUser = new RU_User();
                    oUser.LoadAndCloseReader(RU_User.Query().WHERE(RU_User.Columns.GPEmployeeID, GPEmployeeID).ExecuteReader());
                    if (oUser.IsLoaded)
                    {
                        RU_Recruit oRecruit = new RU_Recruit();
                        oRecruit.LoadAndCloseReader(RU_Recruit.Query().WHERE(RU_Recruit.Columns.UserID, oUser.UserID).
                                                                        AND(RU_Recruit.Columns.SeasonID, 7).ExecuteReader());
                        if (oRecruit.IsLoaded)
                        {
                            switch (oRecruit.UserTypeId)
                            {
                                case 6:
                                case 7:
                                case 8:
                                case 10:
                                    RepOrTech = "TECH";
                                    break;
                                default:
                                    RepOrTech = "REP";
                                    break;
                            }

                        }
                        Datasource.Rows.Add(oUser.FullName, RepOrTech, State);
                    }
                    else
                    {
                        Datasource.Rows.Add(FullName, RepOrTech, State);
                    }
                }
                else
                {
                    Datasource.Rows.Add(FullName, RepOrTech, State);
                }
            }
            gcStateCounty.DataSource = Datasource;
        }

        private void GetGPEmpIDs()
        {
            DataTable oDt = DefaultTable("Sheet1");
            DataTable DataSource = new DataTable();
            DataSource.Columns.Add("FullName");
            DataSource.Columns.Add("GPEmployee ID");
            DataSource.Columns.Add("Tech/Rep");
            DataSource.Columns.Add("Source");
            foreach (DataRow currRow in oDt.Rows)
            {
                string SSN = (currRow["SOCIAL"].ToString() != "") ? TripleDES.EncryptString(currRow["SOCIAL"].ToString().Replace("-", ""), null) : "NONE";
                string FirstName = currRow["FIRST"].ToString();
                string LastName = currRow["LAST"].ToString();
                string Source = "";


                RU_User oUser = new RU_User();
                oUser.LoadAndCloseReader(RU_User.Query().WHERE(RU_User.Columns.Ssn, SSN).ExecuteReader());
                if (oUser.IsLoaded)
                    Source = "SSN";

                if (!oUser.IsLoaded)
                {
                    oUser.LoadAndCloseReader(RU_User.Query().WHERE(RU_User.Columns.FirstName, FirstName).
                                                                AND(RU_User.Columns.LastName, LastName).ExecuteReader());
                    if (oUser.IsLoaded)
                        Source = "Name";
                }

                if (oUser.IsLoaded)
                {
                    //get recruit from season 3 - summer 2007
                    RU_Recruit oRecruit = new RU_Recruit();
                    oRecruit.LoadAndCloseReader(RU_Recruit.Query().WHERE(RU_Recruit.Columns.UserID, oUser.UserID).
                                                                    AND(RU_Recruit.Columns.SeasonID, 3).ExecuteReader());

                    if (!oRecruit.IsLoaded)
                        oRecruit.LoadAndCloseReader(RU_Recruit.Query().WHERE(RU_Recruit.Columns.UserID, oUser.UserID).
                                                                    AND(RU_Recruit.Columns.SeasonID, 4).ExecuteReader());
                    if (!oRecruit.IsLoaded)
                        oRecruit.LoadAndCloseReader(RU_Recruit.Query().WHERE(RU_Recruit.Columns.UserID, oUser.UserID).
                                                                    AND(RU_Recruit.Columns.SeasonID, 6).ExecuteReader());
                    if (!oRecruit.IsLoaded)
                        oRecruit.LoadAndCloseReader(RU_Recruit.Query().WHERE(RU_Recruit.Columns.UserID, oUser.UserID).
                                                                    AND(RU_Recruit.Columns.SeasonID, 7).ExecuteReader());
                    if (oRecruit.IsLoaded)
                    {
                        switch (oRecruit.UserTypeId)
                        {
                            case 6:
                            case 7:
                            case 8:
                            case 10:
                                DataSource.Rows.Add(oUser.FullName, oUser.GPEmployeeID, "TECH", Source);
                                break;
                            default:
                                DataSource.Rows.Add(oUser.FullName, oUser.GPEmployeeID, "REP", Source);
                                break;

                        }

                    }
                }
                else
                {
                    DataSource.Rows.Add(FirstName + " " + LastName, "NULL", "", "None");
                }
            }

            gcStateCounty.DataSource = DataSource;
        }

        private void UploadEmployeeLicenses()
        {
            DataTable oDt = DefaultTable("UserTable");
            int icount = 1;
            foreach (DataRow currRow in oDt.Rows)
            {
                icount = icount + 1;
                string GPEmployeeID = currRow["GPEMPID"].ToString();
                string LicenseNumber = currRow["LICENSE#"].ToString();
                string ExpDate = currRow["EXP DATE"].ToString();
                string IssueDate = currRow["ACTIVE DATE"].ToString();
                string PWCompleted = currRow["PW COMPLETED"].ToString();
                string MissingItems = currRow["MISSING ITEMS"].ToString();
                string CheckRequested = currRow["CHECK REQUESTED"].ToString();
                string PWSent = currRow["PW SENT"].ToString();
                string CheckNumber = currRow["CHECK#"].ToString();
                string Application = currRow["APPLICATION"].ToString();
                string StateAB = currRow["STATE"].ToString();
                string Notes = currRow["NOTES"].ToString();
                string Type = currRow["TYPE"].ToString();
                bool IsComplete = (Application.ToUpper() == "APPROVED" || Application.ToUpper() == "COMPLETE") ? true : false;
                int RequirementID = 0;

                LM_Location oState = new LM_Location();
                oState.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.State).
                                                                    AND(LM_Location.Columns.Abbreviation, StateAB).
                                                                    AND(LM_Location.Columns.IsDeleted, false).
                                                                    AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                switch (Type)
                {
                    case "REP":
                        LM_Requirement oRepReq = new LM_Requirement();
                        oRepReq.LoadAndCloseReader(LM_Requirement.Query().WHERE(LM_Requirement.Columns.IsActive, true).
                                                                            AND(LM_Requirement.Columns.IsDeleted, false).
                                                                            AND(LM_Requirement.Columns.LocationID, oState.LocationID).
                                                                            AND(LM_Requirement.Columns.RequirementName, "State Employee License").
                                                                            AND(LM_Requirement.Columns.RequirementTypeID, (int)LM_RequirementType.EnumRequirementType.SalesRep).ExecuteReader());
                        RequirementID = oRepReq.RequirementID;
                        break;
                    case "TECH":
                        LM_Requirement oTechReq = new LM_Requirement();
                        oTechReq.LoadAndCloseReader(LM_Requirement.Query().WHERE(LM_Requirement.Columns.IsActive, true).
                                                                            AND(LM_Requirement.Columns.IsDeleted, false).
                                                                            AND(LM_Requirement.Columns.LocationID, oState.LocationID).
                                                                            AND(LM_Requirement.Columns.RequirementName, "State Employee License").
                                                                            AND(LM_Requirement.Columns.RequirementTypeID, (int)LM_RequirementType.EnumRequirementType.Technician).ExecuteReader());
                        RequirementID = oTechReq.RequirementID;
                        break;
                    //default:
                    //    MessageBox.Show("Could not read type on line " + icount.ToString());
                    //    break;
                }

                LM_License oCheckLicense = new LM_License();
                oCheckLicense.LoadAndCloseReader(LM_License.Query().WHERE(LM_License.Columns.IsActive, true).
                                                                    AND(LM_License.Columns.IsDeleted, false).
                                                                    AND(LM_License.Columns.GPEmployeeID, GPEmployeeID).
                                                                    AND(LM_License.Columns.RequirementID, RequirementID).ExecuteReader());

                if (!oCheckLicense.IsLoaded)
                {
                    if (GPEmployeeID != "NULL" && GPEmployeeID != "" && RequirementID != 0)
                    {

                        LM_License oLicense = new LM_License();
                        oLicense.RequirementID = RequirementID;
                        oLicense.GPEmployeeID = GPEmployeeID;
                        DateTime ExpirDate = new DateTime();
                        if (DateTime.TryParse(ExpDate, out ExpirDate))
                            oLicense.ExpirationDate = ExpirDate;
                        DateTime IssDate = new DateTime();
                        if (DateTime.TryParse(IssueDate, out IssDate))
                            oLicense.IssueDate = IssDate;
                        oLicense.LicenseNumber = LicenseNumber;
                        oLicense.RequirementsAreMet = IsComplete;
                        oLicense.IsActive = true;
                        oLicense.IsDeleted = false;
                        oLicense.CreatedByDate = DateTime.Now;
                        oLicense.ModifiedByDate = DateTime.Now;
						oLicense.ModifiedByID = LicenseManagementHelper.LoggedInUser;
						oLicense.CreatedByID = LicenseManagementHelper.LoggedInUser;
                        oLicense.Save();

                        LM_Requirement oRequirement = LM_Requirement.LoadByPrimaryKey(RequirementID);

                        foreach (LM_RequirementItem ReqItem in oRequirement.LM_RequirementItems)
                        {
                            AddLicenseItem(oLicense.LicenseID, IsComplete, ReqItem.Name, ReqItem.Description);
                        }

                        if (PWCompleted != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Paperwork completed: " + PWCompleted);
                        if (MissingItems != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Missing Items: " + MissingItems);
                        if (CheckRequested != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Check Requested: " + CheckRequested);
                        if (PWSent != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Paperwork sent: " + PWSent);
                        if (CheckNumber != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Check #: " + CheckNumber);
                        if (!IsComplete && Application != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Application status: " + Application);
                        if (Notes != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, Notes);
                    }
                }
                else
                {
                    DateTime ExpirDate = new DateTime();
                    if (DateTime.TryParse(ExpDate, out ExpirDate))
                        oCheckLicense.ExpirationDate = ExpirDate;
                    DateTime IssDate = new DateTime();
                    if (DateTime.TryParse(IssueDate, out IssDate))
                        oCheckLicense.IssueDate = IssDate;
                    oCheckLicense.LicenseNumber = LicenseNumber;
                    oCheckLicense.RequirementsAreMet = IsComplete;
                    oCheckLicense.IsActive = true;
                    oCheckLicense.IsDeleted = false;
                    oCheckLicense.CreatedByDate = DateTime.Now;
                    oCheckLicense.ModifiedByDate = DateTime.Now;
					oCheckLicense.ModifiedByID = LicenseManagementHelper.LoggedInUser;
					oCheckLicense.CreatedByID = LicenseManagementHelper.LoggedInUser;
                    oCheckLicense.Save();

                    foreach (LM_LicenseItem oItem in oCheckLicense.LM_LicenseItems)
                    {
                        oItem.IsCompleted = IsComplete;
                        oItem.Save();
                    }

                    if (PWCompleted != "")
                        AddNote(LM_NoteType.EnumNoteType.License, oCheckLicense.LicenseID, "Paperwork completed: " + PWCompleted);
                    if (MissingItems != "")
                        AddNote(LM_NoteType.EnumNoteType.License, oCheckLicense.LicenseID, "Missing Items: " + MissingItems);
                    if (CheckRequested != "")
                        AddNote(LM_NoteType.EnumNoteType.License, oCheckLicense.LicenseID, "Check Requested: " + CheckRequested);
                    if (PWSent != "")
                        AddNote(LM_NoteType.EnumNoteType.License, oCheckLicense.LicenseID, "Paperwork sent: " + PWSent);
                    if (CheckNumber != "")
                        AddNote(LM_NoteType.EnumNoteType.License, oCheckLicense.LicenseID, "Check #: " + CheckNumber);
                    if (!IsComplete && Application != "")
                        AddNote(LM_NoteType.EnumNoteType.License, oCheckLicense.LicenseID, "Application status: " + Application);
                    if (Notes != "")
                        AddNote(LM_NoteType.EnumNoteType.License, oCheckLicense.LicenseID, Notes);
                }
            }
        }

        private void AddNote(LM_NoteType.EnumNoteType NoteType, int ForeignKeyID, string Note)
        {
            LM_Note oNote = new LM_Note();
            oNote.NoteTypeID = (int)NoteType;
            oNote.ForeignKeyID = ForeignKeyID;
            oNote.Note = Note;
			oNote.CreatedByID = LicenseManagementHelper.LoggedInUser;
            oNote.CreatedByDate = DateTime.Now;
            oNote.Save();
        }

        private void AddLicenseItem(int LicenseID, bool IsComplete, string Name, string Description)
        {
            LM_LicenseItem oItem = new LM_LicenseItem();
            oItem.LicenseID = LicenseID;
            oItem.Name = Name;
            oItem.Description = Description;
            oItem.IsCompleted = IsComplete;
            oItem.IsDeleted = false;
            oItem.CreatedByDate = DateTime.Now;
            oItem.ModifiedByDate = DateTime.Now;
            oItem.ModifiedByID = LicenseManagementHelper.LoggedInUser;
            oItem.CreatedByID = LicenseManagementHelper.LoggedInUser;
            oItem.Save();
        }

        private void UploadStateTechLicensing()
        {
            DataTable oDt = DefaultTable("Sheet1");
            foreach (DataRow currRow in oDt.Rows)
            {
                string State = currRow["State"].ToString();
                string FingerPrint = currRow["Fingerprint"].ToString();
                string Livescan = currRow["Livescan"].ToString();
                string App = currRow["App"].ToString();
                string PhotoRequired = currRow["Photo"].ToString();
                string LicenseFee = currRow["License Fee"].ToString();
                string LiveScanFee = currRow["Live Scan Fee"].ToString();
                string Training = currRow["Training"].ToString();
                string Agency = currRow["Agency"].ToString();
                int AgencyID = int.Parse(currRow["AgencyID"].ToString());

                //get the state object
                LM_Location oState = new LM_Location();
                oState.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.State).
                                                                    AND(LM_Location.Columns.LocationName, State).
                                                                    AND(LM_Location.Columns.IsDeleted, false).
                                                                    AND(LM_Location.Columns.IsActive, true).ExecuteReader());


                if (oState.IsLoaded)
                {
                    LM_Requirement oRequirement = new LM_Requirement();
                    oRequirement.LocationID = oState.LocationID;
                    oRequirement.RequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Technician;
                    oRequirement.AgencyID = AgencyID;
                    oRequirement.LockID = (int)LM_Lock.EnumLock.HardLock;
                    oRequirement.RequirementName = "State Employee License";
                    oRequirement.ApplicationDescription = App;
                    oRequirement.IsActive = true;
                    oRequirement.IsDeleted = false;
                    oRequirement.CreatedByDate = DateTime.Now;
                    oRequirement.ModifiedByDate = DateTime.Now;
					oRequirement.ModifiedByID = LicenseManagementHelper.LoggedInUser;
					oRequirement.CreatedByID = LicenseManagementHelper.LoggedInUser;
                    oRequirement.Save();

                    if (FingerPrint.Contains("YES"))
                    {
                        AddRequirementItem(oRequirement.RequirementID, "Fingerprint", FingerPrint.Replace("YES ", ""));
                    }
                    if (Livescan.Contains("YES"))
                    {
                        AddRequirementItem(oRequirement.RequirementID, "Live Scan", "Fee: " + LiveScanFee);
                    }
                    if (PhotoRequired.Contains("YES"))
                    {
                        AddRequirementItem(oRequirement.RequirementID, "Photo", PhotoRequired.Replace("YES- ", ""));
                    }
                    if (LicenseFee.Contains("$"))
                    {
                        AddRequirementItem(oRequirement.RequirementID, "License Fee", LicenseFee);
                    }
                    if (Training != "None")
                    {
                        AddRequirementItem(oRequirement.RequirementID, "Training", Training);
                    }
                }
                else
                {
                    //error
                    MessageBox.Show("Could not load state: " + State);
                }
            }
        }

        private void UploadStateRepLicensing()
        {
            DataTable oDt = DefaultTable("Sheet1");
            foreach (DataRow currRow in oDt.Rows)
            {
                string State = currRow["State"].ToString();
                string FingerPrint = currRow["Fingerprint"].ToString();
                string Livescan = currRow["Livescan"].ToString();
                string App = currRow["App"].ToString();
                string PhotoRequired = currRow["Photo Required"].ToString();
                string LicenseFee = currRow["License Fee"].ToString();
                string LiveScanFee = currRow["Live Scan Fee"].ToString();
                string Training = currRow["Training"].ToString();
                string Agency = currRow["Agency"].ToString();
                int AgencyID = int.Parse(currRow["AgencyID"].ToString());

                //get the state object
                LM_Location oState = new LM_Location();
                oState.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.State).
                                                                    AND(LM_Location.Columns.LocationName, State).
                                                                    AND(LM_Location.Columns.IsDeleted, false).
                                                                    AND(LM_Location.Columns.IsActive, true).ExecuteReader());


                if (oState.IsLoaded)
                {
                    LM_Requirement oRequirement = new LM_Requirement();
                    oRequirement.LocationID = oState.LocationID;
                    oRequirement.RequirementTypeID = (int)LM_RequirementType.EnumRequirementType.SalesRep;
                    oRequirement.AgencyID = AgencyID;
                    oRequirement.LockID = (int)LM_Lock.EnumLock.HardLock;
                    oRequirement.RequirementName = "State Employee License";
                    oRequirement.ApplicationDescription = App;
                    oRequirement.IsActive = true;
                    oRequirement.IsDeleted = false;
                    oRequirement.CreatedByDate = DateTime.Now;
                    oRequirement.ModifiedByDate = DateTime.Now;
					oRequirement.ModifiedByID = LicenseManagementHelper.LoggedInUser;
					oRequirement.CreatedByID = LicenseManagementHelper.LoggedInUser;
                    oRequirement.Save();

                    if (FingerPrint.Contains("YES"))
                    {
                        AddRequirementItem(oRequirement.RequirementID, "Fingerprint", FingerPrint.Replace("YES ", ""));
                    }
                    if (Livescan.Contains("YES"))
                    {
                        AddRequirementItem(oRequirement.RequirementID, "Live Scan", "Fee: " + LiveScanFee);
                    }
                    if (PhotoRequired.Contains("YES"))
                    {
                        AddRequirementItem(oRequirement.RequirementID, "Photo", PhotoRequired.Replace("YES- ", ""));
                    }
                    if (LicenseFee.Contains("$"))
                    {
                        AddRequirementItem(oRequirement.RequirementID, "License Fee", LicenseFee);
                    }
                    if (Training != "None")
                    {
                        AddRequirementItem(oRequirement.RequirementID, "Training", Training);
                    }
                }
                else
                {
                    //error
                    MessageBox.Show("Could not load state: " + State);
                }
            }
        }

        private void AddRequirementItem(int nRequirementID, string Name, string Description)
        {
            LM_RequirementItem oItem = new LM_RequirementItem();
            oItem.RequirementID = nRequirementID;
            oItem.Name = Name;
            oItem.Description = Description;
            oItem.IsActive = true;
            oItem.IsDeleted = false;
            oItem.CreatedByDate = DateTime.Now;
            oItem.ModifiedByDate = DateTime.Now;
			oItem.ModifiedByID = LicenseManagementHelper.LoggedInUser;
			oItem.CreatedByID = LicenseManagementHelper.LoggedInUser;
            oItem.Save();
        }

        private void UploadCountyStateGrid()
        {
            DataTable oDt = DefaultTable("Sheet1");
            foreach (DataRow currRow in oDt.Rows)
            {
                string StateAB = currRow["STATEAB"].ToString();
                string County = currRow["COUNTY"].ToString();
                string City = currRow["CITY"].ToString();

                if (StateAB != StateAB.ToUpper() || County != County.ToUpper() || City != City.ToUpper() || County == "NULL")
                {
                    currRow.Delete();
                }
                else
                {

                    LM_Location oState = new LM_Location();
                    oState.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.State).
                                                                        AND(LM_Location.Columns.Abbreviation, StateAB).
                                                                        AND(LM_Location.Columns.IsDeleted, false).
                                                                        AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                    LM_Location oExistingCounty = new LM_Location();
                    oExistingCounty.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.ParentLocationID, oState.LocationID).
                                                                            AND(LM_Location.Columns.IsActive, true).
                                                                            AND(LM_Location.Columns.IsDeleted, false).
                                                                            AND(LM_Location.Columns.LocationName, County + " COUNTY").ExecuteReader());

                    if (oExistingCounty.IsLoaded)
                    {
                        LM_Location oExistingCity = new LM_Location();
                        oExistingCity.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.ParentLocationID, oExistingCounty.LocationID).
                                                                                AND(LM_Location.Columns.IsDeleted, false).
                                                                                AND(LM_Location.Columns.IsActive, true).
                                                                                AND(LM_Location.Columns.LocationName, City).ExecuteReader());

                        if (!oExistingCity.IsLoaded)
                        {
                            LM_Location oCity = new LM_Location();
                            oCity.LocationTypeID = (int)LM_LocationType.EnumLocationType.City;
                            oCity.ParentLocationID = oExistingCounty.LocationID;
                            oCity.ParentCountryID = oExistingCounty.ParentCountryID;
                            oCity.ParentStateID = oExistingCounty.ParentStateID;
                            oCity.ParentCountyID = oExistingCounty.LocationID;
                            oCity.ParentCityID = null;
                            oCity.LocationName = City;
                            oCity.CanSolicit = true;
                            oCity.IsActive = true;
                            oCity.IsDeleted = false;
                            oCity.CreatedByDate = DateTime.Now;
                            oCity.ModifiedByDate = DateTime.Now;
                            oCity.CreatedByID = LicenseManagementHelper.LoggedInUser;
                            oCity.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                            oCity.Save();
                        }
                    }
                    else
                    {
                        LM_Location oCounty = new LM_Location();
                        oCounty.LocationTypeID = (int)LM_LocationType.EnumLocationType.County;
                        oCounty.ParentLocationID = oState.LocationID;
                        oCounty.ParentCountryID = oState.ParentCountryID;
                        oCounty.ParentStateID = oState.LocationID;
                        oCounty.ParentCountyID = oCounty.ParentCityID = null;
                        oCounty.LocationName = County + " COUNTY";
                        oCounty.CanSolicit = true;
                        oCounty.IsActive = true;
                        oCounty.IsDeleted = false;
                        oCounty.CreatedByDate = DateTime.Now;
                        oCounty.ModifiedByDate = DateTime.Now;
						oCounty.CreatedByID = LicenseManagementHelper.LoggedInUser;
						oCounty.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                        oCounty.Save();

                        LM_Location oCity = new LM_Location();
                        oCity.LocationTypeID = (int)LM_LocationType.EnumLocationType.City;
                        oCity.ParentLocationID = oCounty.LocationID;
                        oCity.ParentCountryID = oCounty.ParentCountryID;
                        oCity.ParentStateID = oCounty.ParentStateID;
                        oCity.ParentCountyID = oCounty.LocationID;
                        oCity.ParentCityID = null;
                        oCity.LocationName = City;
                        oCity.CanSolicit = true;
                        oCity.IsActive = true;
                        oCity.IsDeleted = false;
                        oCity.CreatedByDate = DateTime.Now;
                        oCity.ModifiedByDate = DateTime.Now;
						oCity.CreatedByID = LicenseManagementHelper.LoggedInUser;
						oCity.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                        oCity.Save();
                    }
                }
            }
            //gcStateCounty.DataSource = oDt;
        }

        private void UploadStateLicensing()
        {
            if (ofdExcelFile.ShowDialog() == DialogResult.OK)
            {
                string excelConnectionString =
                        string.Format(
                            @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;""",
                            ofdExcelFile.FileName);
                OleDbConnection oCon = new OleDbConnection(excelConnectionString);
                oCon.Open();
                OleDbCommand oCmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oCon);
                OleDbDataAdapter Dataadapt = new OleDbDataAdapter();
                DataSet oDs = new DataSet();
                Dataadapt.SelectCommand = oCmd;
                Dataadapt.Fill(oDs);
                DataTable oDt = new DataTable();
                oDt = oDs.Tables[0];

                foreach (DataRow currRow in oDt.Rows)
                {
                    string StateAB = currRow["STATE AB"].ToString();
                    string License = currRow["LICENSE"].ToString();
                    string Dept = currRow["DEPT"].ToString();
                    string Website = currRow["WEBSITE"].ToString();
                    string StreetAddress = currRow["DEPT STREET ADDRESS"].ToString();
                    string City = currRow["CITY"].ToString();
                    string State = currRow["STATE"].ToString();
                    string Zip = currRow["ZIP"].ToString();
                    string Phone = currRow["DEPT PHONE"].ToString();
                    string Fax = currRow["DEPT FAX"].ToString();
                    string Contact = currRow["CONTACT"].ToString();
                    string LicenseNumber = currRow["LICENSE NUMBER"].ToString();
                    string ExpDate = currRow["EXP DATE"].ToString();
                    string LicenseQualifier = currRow["LICENSE QUALIFIER"].ToString();
                    string Renewal = currRow["RENEWAL"].ToString();

                    //Get State Object
                    LM_Location oState = new LM_Location();
                    oState.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.State).
                                                                        AND(LM_Location.Columns.Abbreviation, StateAB).
                                                                        AND(LM_Location.Columns.IsDeleted, false).
                                                                        AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                    LM_Agency CheckAgency = new LM_Agency();
                    CheckAgency.LoadAndCloseReader(LM_Agency.Query().WHERE(LM_Agency.Columns.LocationID, oState.LocationID).
                                                                        AND(LM_Agency.Columns.AgencyName, Dept).
                                                                        AND(LM_Agency.Columns.IsActive, true).
                                                                        AND(LM_Agency.Columns.IsDeleted, false).ExecuteReader());

                    if (CheckAgency.IsLoaded)
                    {
                        CheckAgency.StreetAddress2 = StreetAddress;
                        CheckAgency.City2 = City;
                        CheckAgency.StateProvince2 = State;
                        CheckAgency.ZipCode2 = Zip;
                        CheckAgency.Save();
                    }
                    else
                    {
                        //create Agency
                        LM_Agency oAgency = new LM_Agency();
                        oAgency.LocationID = oState.LocationID;
                        oAgency.AgencyName = Dept;
                        oAgency.Contact = Contact;
                        oAgency.Website = Website;
                        oAgency.Phone1 = Phone;
                        oAgency.Fax = Fax;
                        oAgency.StreetAddress = StreetAddress;
                        oAgency.City = City;
                        oAgency.StateProvince = State;
                        oAgency.ZipCode = Zip;
                        oAgency.Country = "USA";
                        oAgency.IsActive = true;
                        oAgency.IsDeleted = false;
                        oAgency.CreatedbyDate = DateTime.Now;
                        oAgency.ModifiedByDate = DateTime.Now;
                        oAgency.CreatedByID = LicenseManagementHelper.LoggedInUser;
                        oAgency.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                        oAgency.Save();

                        //check for existing requirement
                        LM_Requirement CheckRequirement = new LM_Requirement();
                        CheckRequirement.LoadAndCloseReader(LM_Requirement.Query().WHERE(LM_Requirement.Columns.RequirementName, License).
                                                                                    AND(LM_Requirement.Columns.LocationID, oState.LocationID).
                                                                                    AND(LM_Requirement.Columns.IsActive, true).
                                                                                    AND(LM_Requirement.Columns.IsDeleted, false).ExecuteReader());
                        if (!CheckRequirement.IsLoaded)
                        {
                            //create new requirement
                            LM_Requirement oRequirement = new LM_Requirement();
                            oRequirement.LocationID = oState.LocationID;
                            oRequirement.RequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Company;
                            oRequirement.AgencyID = oAgency.AgencyID;
                            oRequirement.LockID = (int)LM_Lock.EnumLock.HardLock;
                            oRequirement.RequirementName = License;
                            oRequirement.IsActive = true;
                            oRequirement.IsDeleted = false;
                            oRequirement.ModifiedByDate = DateTime.Now;
                            oRequirement.CreatedByDate = DateTime.Now;
							oRequirement.ModifiedByID = LicenseManagementHelper.LoggedInUser;
							oRequirement.CreatedByID = LicenseManagementHelper.LoggedInUser;
                            oRequirement.Save();

                            //create license, can't exist without requirement so no check is necessary
                            LM_License oLicense = new LM_License();
                            oLicense.RequirementID = oRequirement.RequirementID;
                            DateTime ExpirationDate = new DateTime();
                            if (DateTime.TryParse(ExpDate, out ExpirationDate))
                                oLicense.ExpirationDate = ExpirationDate;
                            oLicense.LicenseNumber = LicenseNumber;
                            oLicense.RequirementsAreMet = true;
                            oLicense.IsActive = true;
                            oLicense.IsDeleted = false;
                            oLicense.ModifiedByDate = DateTime.Now;
                            oLicense.CreatedByDate = DateTime.Now;
                            oLicense.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                            oLicense.CreatedByID = LicenseManagementHelper.LoggedInUser;
                            oLicense.Save();

                            if (LicenseQualifier != "" && LicenseQualifier != "N/A")
                            {
                                LM_Note.AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "License Qualifier: " + LicenseQualifier, LicenseManagementHelper.LoggedInUser);
                            }
                            if (Renewal != "")
                            {
                                LM_Note.AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Renewal: " + Renewal, LicenseManagementHelper.LoggedInUser);
                            }
                        }
                        else
                        {
                            LM_License CheckLicense = new LM_License();
                            CheckLicense.LoadAndCloseReader(LM_License.Query().WHERE(LM_License.Columns.RequirementID, CheckRequirement.RequirementID).
                                                                                AND(LM_License.Columns.IsDeleted, false).
                                                                                AND(LM_License.Columns.IsActive, true).ExecuteReader());
                            if (!CheckLicense.IsLoaded)
                            {
                                //create license, can't exist without requirement so no check is necessary
                                LM_License oLicense = new LM_License();
                                oLicense.RequirementID = CheckRequirement.RequirementID;
                                DateTime ExpirationDate = new DateTime();
                                if (DateTime.TryParse(ExpDate, out ExpirationDate))
                                    oLicense.ExpirationDate = ExpirationDate;
                                oLicense.LicenseNumber = LicenseNumber;
                                oLicense.RequirementsAreMet = true;
                                oLicense.IsActive = true;
                                oLicense.IsDeleted = false;
                                oLicense.ModifiedByDate = DateTime.Now;
                                oLicense.CreatedByDate = DateTime.Now;
                                oLicense.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                                oLicense.CreatedByID = LicenseManagementHelper.LoggedInUser;
                                oLicense.Save();

                                if (LicenseQualifier != "" && LicenseQualifier != "N/A")
                                {
                                    LM_Note.AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "License Qualifier: " + LicenseQualifier, LicenseManagementHelper.LoggedInUser);
                                }
                                if (Renewal != "")
                                {
                                    LM_Note.AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Renewal: " + Renewal, LicenseManagementHelper.LoggedInUser);
                                }
                            }
                        }

                    }
                }
            }
            MessageBox.Show("Complete!");
        }

        private void UploadCounties()
        {
            if (ofdExcelFile.ShowDialog() == DialogResult.OK)
            {
                string excelConnectionString =
                        string.Format(
                            @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;""",
                            ofdExcelFile.FileName);
                OleDbConnection oCon = new OleDbConnection(excelConnectionString);
                oCon.Open();
                OleDbCommand oCmd = new OleDbCommand("SELECT * FROM [ALL$]", oCon);
                OleDbDataAdapter Dataadapt = new OleDbDataAdapter();
                DataSet oDs = new DataSet();
                Dataadapt.SelectCommand = oCmd;
                Dataadapt.Fill(oDs);
                DataTable oDt = new DataTable();
                oDt = oDs.Tables[0];

                foreach (DataRow currRow in oDt.Rows)
                {
                    LM_Location oParentState = new LM_Location();//TODO: get not deleted states
                    oParentState.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.State).
                                                                        AND(LM_Location.Columns.LocationName, currRow["STATE"]).
                                                                        AND(LM_Location.Columns.IsDeleted, false).
                                                                        AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                    DataTable ExistingCounties = new DataTable();
                    ExistingCounties.Load(LM_Location.Query().WHERE(LM_Location.Columns.IsActive, true).
                                                                AND(LM_Location.Columns.IsDeleted, false).
                                                                AND(LM_Location.Columns.ParentLocationID, oParentState.LocationID).ExecuteReader());

                    List<string> ExCountyNames = new List<string>();
                    foreach (DataRow County in ExistingCounties.Rows)
                    {
                        ExCountyNames.Add(County[LM_Location.Columns.LocationName].ToString());
                    }

                    if (!ExCountyNames.Contains(currRow["COUNTY"].ToString() + " COUNTY") && currRow["COUNTY"].ToString() != "")
                    {
                        LM_Location oCounty = new LM_Location();
                        oCounty.LocationTypeID = (int)LM_LocationType.EnumLocationType.County;
                        oCounty.ParentLocationID = oParentState.LocationID;
                        oCounty.ParentCountryID = oParentState.ParentCountryID;
                        oCounty.ParentStateID = oParentState.LocationID;
                        oCounty.ParentCountyID = oCounty.ParentCityID = null;
                        oCounty.LocationName = currRow["COUNTY"].ToString() + " COUNTY";
                        oCounty.CanSolicit = true;
                        oCounty.IsActive = true;
                        oCounty.IsDeleted = false;
                        oCounty.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                        oCounty.CreatedByID = LicenseManagementHelper.LoggedInUser;
                        oCounty.ModifiedByDate = DateTime.Now;
                        oCounty.CreatedByDate = DateTime.Now;
                        oCounty.Save();
                    }
                }
            }
        }

        private void UploadStateLicensingAndRegistration()
        {
            foreach (DataRow currRow in StateLicensingAndRegistration.Rows)
            {
                LM_Location oState = new LM_Location();
                oState.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.Abbreviation, currRow["STATE"].ToString()).
                                                                AND(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.State).ExecuteReader());
                DataTable ocheck = new DataTable();
                ocheck.Load(LM_Agency.Query().WHERE(LM_Agency.Columns.LocationID, oState.LocationID).
                                                AND(LM_Agency.Columns.AgencyName, currRow["DEPT"].ToString()).ExecuteReader());
                if (ocheck.Rows.Count <= 0)
                {
                    LM_Agency oAgency = new LM_Agency();
                    oAgency.LocationID = oState.LocationID;
                    oAgency.AgencyName = (currRow["DEPT"].ToString() != "") ? currRow["DEPT"].ToString() : "State Agency";
                    //oAgency.
                }
            }
        }

        private void LoadCities()
        {
            //manipulate table
            DataTable States = new DataTable();
            States.Load(LM_Location.Query().WHERE(LM_Location.Columns.IsActive, true).
                                            AND(LM_Location.Columns.IsDeleted, false).
                                            AND(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.State).ExecuteReader());

            foreach (DataRow currRow in LicenseTable.Rows)
            {
                if (currRow["City"] != null && currRow["City"].ToString() != "")
                {
                    //locals
                    DataRow[] ParentStateRows = States.Select("Abbreviation = '" + currRow["State"].ToString() + "'");
                    if (ParentStateRows.Length > 0)
                    {
                        DataRow ParentStateRow = ParentStateRows[0];
                        LM_Location ParentState = LM_Location.LoadByPrimaryKey((int)ParentStateRow[LM_Location.Columns.LocationID]);
                        DataRow[] ParentCountyRow = CityCountytable.Select(string.Format("StateAB = '{0}' AND City = '{1}'", ParentState.Abbreviation, currRow["City"].ToString()));
                        if (ParentCountyRow.Length > 0)
                        {
                            string ParentCountyName = ParentCountyRow[0]["County"].ToString();

                            if (ParentCountyName != "NULL")
                            {
                                DataTable Counties = new DataTable();
                                Counties.Load(LM_Location.Query().WHERE(LM_Location.Columns.IsActive, true).
                                                                AND(LM_Location.Columns.IsDeleted, false).
                                                                AND(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.County).
                                                                AND(LM_Location.Columns.ParentLocationID, ParentState.LocationID).
                                                                AND(LM_Location.Columns.LocationName, ParentCountyName).ExecuteReader());
                                LM_Location ParentLocation;
                                if (Counties.Rows.Count > 0)
                                {
                                    ParentLocation = LM_Location.LoadByPrimaryKey((int)Counties.Rows[0][LM_Location.Columns.LocationID]);
                                }
                                else
                                {
                                    ParentLocation = new LM_Location();
                                    ParentLocation.LocationTypeID = (int)LM_LocationType.EnumLocationType.County;
                                    ParentLocation.ParentLocationID = ParentState.LocationID;
                                    ParentLocation.ParentCountryID = ParentState.ParentCountryID;
                                    ParentLocation.ParentStateID = ParentState.LocationID;
                                    ParentLocation.ParentCountyID = ParentLocation.ParentCityID = null;
                                    ParentLocation.LocationName = ParentCountyName;
                                    ParentLocation.CanSolicit = true;
                                    ParentLocation.IsActive = true;
                                    ParentLocation.IsDeleted = false;
                                    ParentLocation.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                                    ParentLocation.CreatedByID = LicenseManagementHelper.LoggedInUser;
                                    ParentLocation.ModifiedByDate = DateTime.Now;
                                    ParentLocation.CreatedByDate = DateTime.Now;
                                    ParentLocation.Save();
                                }

                                #region Add City

                                DataTable CityCheck = new DataTable();
                                Counties.Load(LM_Location.Query().WHERE(LM_Location.Columns.IsActive, true).
                                                                AND(LM_Location.Columns.IsDeleted, false).
                                                                AND(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.City).
                                                                AND(LM_Location.Columns.ParentLocationID, ParentLocation.LocationID).
                                                                AND(LM_Location.Columns.LocationName, currRow["City"].ToString()).ExecuteReader());

                                if (CityCheck.Rows.Count <= 0)
                                {
                                    LM_Location oLocation = new LM_Location();
                                    oLocation.LocationTypeID = (int)LM_LocationType.EnumLocationType.City;
                                    oLocation.ParentLocationID = ParentLocation.LocationID;
                                    oLocation.ParentCountryID = ParentLocation.ParentCountryID;
                                    oLocation.ParentStateID = ParentLocation.ParentStateID;
                                    oLocation.ParentCountyID = ParentLocation.LocationID;
                                    oLocation.ParentCityID = null;
                                    oLocation.LocationName = currRow["City"].ToString();
                                    oLocation.CanSolicit = true;
                                    oLocation.IsActive = true;
                                    oLocation.IsDeleted = false;
                                    oLocation.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                                    oLocation.CreatedByID = LicenseManagementHelper.LoggedInUser;
                                    oLocation.ModifiedByDate = DateTime.Now;
                                    oLocation.CreatedByDate = DateTime.Now;
                                    oLocation.Save();
                                }

                                #endregion
                            }

                        }
                    }
                }
            }
        }

        private void GetCountiesByStateABAndCity()
        {
            DataTable oDt = DefaultTable("All Permits");
            DataTable DataSource = new DataTable();
            DataSource.Columns.Add("State AB");
            DataSource.Columns.Add("County");
            DataSource.Columns.Add("City");

            foreach (DataRow currRow in oDt.Rows)
            {
                string StateAB = currRow[0].ToString();
                string City = currRow[2].ToString();
                LM_Location oState = new LM_Location();
                oState.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.State).
                                                                    AND(LM_Location.Columns.Abbreviation, StateAB).
                                                                    AND(LM_Location.Columns.IsDeleted, false).
                                                                    AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                LM_Location oCity = new LM_Location();
                oCity.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.IsActive, true).
                                                                AND(LM_Location.Columns.IsDeleted, false).
                                                                AND(LM_Location.Columns.LocationName, City).
                                                                AND(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.City).
                                                                AND(LM_Location.Columns.ParentStateID, oState.LocationID).ExecuteReader());

                if (oCity.IsLoaded)
                {
                    DataSource.Rows.Add(StateAB, oCity.ParentCounty.LocationName, City);
                }
                else
                {
                    DataSource.Rows.Add(StateAB, "", City);
                }
            }

            gcStateCounty.DataSource = DataSource;
        }

        private void LoadCountyAndCityBusinessLicenseReqs()
        {
            DataTable oDt = DefaultTable("Sheet2");
            int CityReqs = 0;
            int CountyReqs = 0;
            foreach (DataRow currRow in oDt.Rows)
            {
                string StateAB = currRow["State"].ToString();
                string CountyName = currRow["County"].ToString();
                string CityName = currRow["City"].ToString();
                string AgencyName = currRow["Dept"].ToString();
                string Phone = currRow["Phone #"].ToString();
                string Contact = currRow["Contact"].ToString();
                string BusinessLicenseDetails = currRow["Business License Required"].ToString().ToUpper();
                string ApplicationInfo = currRow["Application"].ToString();
                //string Fee = currRow["Fee"].ToString();
                //string ProcessTime = currRow["Processing Time"].ToString();

                LM_Location oState = new LM_Location();
                oState.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.State).
                                                                    AND(LM_Location.Columns.Abbreviation, StateAB).
                                                                    AND(LM_Location.Columns.IsDeleted, false).
                                                                    AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                LM_Location oCounty = new LM_Location();
                oCounty.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.County).
                                                                AND(LM_Location.Columns.LocationName, CountyName).
                                                                AND(LM_Location.Columns.ParentStateID, oState.LocationID).
                                                                AND(LM_Location.Columns.IsDeleted, false).
                                                                AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                LM_Location oCity = new LM_Location();
                oCity.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.City).
                                                                AND(LM_Location.Columns.LocationName, CityName).
                                                                AND(LM_Location.Columns.ParentStateID, oState.LocationID).
                                                                AND(LM_Location.Columns.ParentCountyID, oCounty.LocationID).
                                                                AND(LM_Location.Columns.IsDeleted, false).
                                                                AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                if (oCity.IsLoaded)
                {
                    LM_Agency oCheckAgency = new LM_Agency();
                    oCheckAgency.LoadAndCloseReader(LM_Agency.Query().WHERE(LM_Agency.Columns.IsDeleted, false).
                                                                        AND(LM_Agency.Columns.IsActive, true).
                                                                        AND(LM_Agency.Columns.LocationID, oCity.LocationID).
                                                                        AND(LM_Agency.Columns.AgencyName, AgencyName).ExecuteReader());
                    if (!oCheckAgency.IsLoaded)
                    {
                        LM_Agency oAgency = new LM_Agency();
                        oAgency.LocationID = oCity.LocationID;
                        oAgency.AgencyName = AgencyName;
                        oAgency.Contact = Contact;
                        oAgency.Phone1 = Phone;
                        oAgency.IsActive = true;
                        oAgency.IsDeleted = false;
                        oAgency.ModifiedByDate = DateTime.Now;
                        oAgency.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                        oAgency.CreatedbyDate = DateTime.Now;
                        oAgency.CreatedByID = LicenseManagementHelper.LoggedInUser;
                        oAgency.Save();

                        //LM_Requirement oRequirement = new LM_Requirement();
                        //oRequirement.LocationID = oCity.LocationID;
                        //oRequirement.AgencyID = oAgency.AgencyID;
                        //oRequirement.RequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Company;
                        //oRequirement.LockID = (int)LM_Lock.EnumLock.SoftLock;
                        //oRequirement.RequirementName = "Business License";
                        //oRequirement.ApplicationDescription = BusinessLicenseDetails.Replace("YES", "").Replace("-", "");
                        //oRequirement.CallCenterMessage = "Platinum protection is not licensed to sell in this city";
                        //oRequirement.IsActive = true;
                        //oRequirement.IsDeleted = false;
                        //oRequirement.ModifiedByDate = DateTime.Now;
                        //oRequirement.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                        //oRequirement.CreatedByDate = DateTime.Now;
                        //oRequirement.CreatedByID = LicenseManagementHelper.LoggedInUser;
                        //oRequirement.Save();

                        if (ApplicationInfo != "")
                        {
                            AddNote(LM_NoteType.EnumNoteType.Agency, oAgency.AgencyID, ApplicationInfo);
                        }
                        if (BusinessLicenseDetails != "")
                        {
                            AddNote(LM_NoteType.EnumNoteType.Agency, oAgency.AgencyID, BusinessLicenseDetails);
                        }
                        //if (ProcessTime != "")
                        //    AddNote(LM_NoteType.EnumNoteType.Requirement, oRequirement.RequirementID, "Process Time: " + ProcessTime);

                        CityReqs = CityReqs + 1;

                    }
                    else
                    {
                        //LM_Requirement oRequirement = new LM_Requirement();
                        //oRequirement.LocationID = oCity.LocationID;
                        //oRequirement.AgencyID = oCheckAgency.AgencyID;
                        //oRequirement.RequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Company;
                        //oRequirement.LockID = (int)LM_Lock.EnumLock.SoftLock;
                        //oRequirement.RequirementName = "Business License";
                        //oRequirement.ApplicationDescription = BusinessLicenseDetails.Replace("YES", "").Replace("-", "");
                        //oRequirement.CallCenterMessage = "Platinum protection is not licensed to sell in this city";
                        //oRequirement.IsActive = true;
                        //oRequirement.IsDeleted = false;
                        //oRequirement.ModifiedByDate = DateTime.Now;
                        //oRequirement.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                        //oRequirement.CreatedByDate = DateTime.Now;
                        //oRequirement.CreatedByID = LicenseManagementHelper.LoggedInUser;
                        //oRequirement.Save();

                        if (ApplicationInfo != "")
                        {
                            AddNote(LM_NoteType.EnumNoteType.Agency, oCheckAgency.AgencyID, ApplicationInfo);
                        }
                        if (BusinessLicenseDetails != "")
                        {
                            AddNote(LM_NoteType.EnumNoteType.Agency, oCheckAgency.AgencyID, BusinessLicenseDetails);
                        }
                        //if (ProcessTime != "")
                        //    AddNote(LM_NoteType.EnumNoteType.Requirement, oRequirement.RequirementID, "Process Time: " + ProcessTime);

                        CityReqs = CityReqs + 1;
                    }
                }
                else if (oCounty.IsLoaded)
                {
                    LM_Agency oCheckAgency = new LM_Agency();
                    oCheckAgency.LoadAndCloseReader(LM_Agency.Query().WHERE(LM_Agency.Columns.IsDeleted, false).
                                                                        AND(LM_Agency.Columns.IsActive, true).
                                                                        AND(LM_Agency.Columns.LocationID, oCounty.LocationID).
                                                                        AND(LM_Agency.Columns.AgencyName, AgencyName).ExecuteReader());
                    if (!oCheckAgency.IsLoaded)
                    {
                        LM_Agency oAgency = new LM_Agency();
                        oAgency.LocationID = oCounty.LocationID;
                        oAgency.AgencyName = AgencyName;
                        oAgency.Contact = Contact;
                        oAgency.Phone1 = Phone;
                        oAgency.IsActive = true;
                        oAgency.IsDeleted = false;
                        oAgency.ModifiedByDate = DateTime.Now;
                        oAgency.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                        oAgency.CreatedbyDate = DateTime.Now;
                        oAgency.CreatedByID = LicenseManagementHelper.LoggedInUser;
                        oAgency.Save();

                        //LM_Requirement oRequirement = new LM_Requirement();
                        //oRequirement.LocationID = oCounty.LocationID;
                        //oRequirement.AgencyID = oAgency.AgencyID;
                        //oRequirement.RequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Company;
                        //oRequirement.LockID = (int)LM_Lock.EnumLock.SoftLock;
                        //oRequirement.RequirementName = "Business License";
                        //oRequirement.ApplicationDescription = BusinessLicenseDetails.Replace("YES", "").Replace("-", "");
                        //oRequirement.CallCenterMessage = "Platinum protection is not licensed to sell in this city";
                        //oRequirement.IsActive = true;
                        //oRequirement.IsDeleted = false;
                        //oRequirement.ModifiedByDate = DateTime.Now;
                        //oRequirement.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                        //oRequirement.CreatedByDate = DateTime.Now;
                        //oRequirement.CreatedByID = LicenseManagementHelper.LoggedInUser;
                        //oRequirement.Save();

                        if (ApplicationInfo != "")
                        {
                            AddNote(LM_NoteType.EnumNoteType.Agency, oAgency.AgencyID, ApplicationInfo);
                        }
                        if (BusinessLicenseDetails != "")
                        {
                            AddNote(LM_NoteType.EnumNoteType.Agency, oAgency.AgencyID, BusinessLicenseDetails);
                        }
                        //if (ProcessTime != "")
                        //    AddNote(LM_NoteType.EnumNoteType.Requirement, oRequirement.RequirementID, "Process Time: " + ProcessTime);

                        CountyReqs = CountyReqs + 1;

                    }
                    else
                    {
                        //LM_Requirement oRequirement = new LM_Requirement();
                        //oRequirement.LocationID = oCounty.LocationID;
                        //oRequirement.AgencyID = oCheckAgency.AgencyID;
                        //oRequirement.RequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Company;
                        //oRequirement.LockID = (int)LM_Lock.EnumLock.SoftLock;
                        //oRequirement.RequirementName = "Business License";
                        //oRequirement.ApplicationDescription = BusinessLicenseDetails.Replace("YES", "").Replace("-", "");
                        //oRequirement.CallCenterMessage = "Platinum protection is not licensed to sell in this city";
                        //oRequirement.IsActive = true;
                        //oRequirement.IsDeleted = false;
                        //oRequirement.ModifiedByDate = DateTime.Now;
                        //oRequirement.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                        //oRequirement.CreatedByDate = DateTime.Now;
                        //oRequirement.CreatedByID = LicenseManagementHelper.LoggedInUser;
                        //oRequirement.Save();

                        if (ApplicationInfo != "")
                        {
                            AddNote(LM_NoteType.EnumNoteType.Agency, oCheckAgency.AgencyID, ApplicationInfo);
                        }
                        if (BusinessLicenseDetails != "")
                        {
                            AddNote(LM_NoteType.EnumNoteType.Agency, oCheckAgency.AgencyID, BusinessLicenseDetails);
                        }
                        //if (ProcessTime != "")
                        //    AddNote(LM_NoteType.EnumNoteType.Requirement, oRequirement.RequirementID, "Process Time: " + ProcessTime);

                        CountyReqs = CountyReqs + 1;
                    }
                }

            }

            MessageBox.Show(string.Format("{0} City requirements uploaded and {1} County requirements uploaded", CityReqs, CountyReqs));

        }

        private void CheckIfLoaded()
        {
            DataTable oDt = DefaultTable("Sheet2");
            string szMessage = "";
            foreach (DataRow currRow in oDt.Rows)
            {
                string StateAB = currRow["State"].ToString();
                string CountyName = currRow["County"].ToString();
                string CityName = currRow["City"].ToString();

                LM_Location oState = new LM_Location();
                oState.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.State).
                                                                    AND(LM_Location.Columns.Abbreviation, StateAB).
                                                                    AND(LM_Location.Columns.IsDeleted, false).
                                                                    AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                LM_Location oCounty = new LM_Location();
                oCounty.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.County).
                                                                AND(LM_Location.Columns.LocationName, CountyName).
                                                                AND(LM_Location.Columns.ParentStateID, oState.LocationID).
                                                                AND(LM_Location.Columns.IsDeleted, false).
                                                                AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                LM_Location oCity = new LM_Location();
                oCity.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.City).
                                                                AND(LM_Location.Columns.LocationName, CityName).
                                                                AND(LM_Location.Columns.ParentStateID, oState.LocationID).
                                                                AND(LM_Location.Columns.ParentCountyID, oCounty.LocationID).
                                                                AND(LM_Location.Columns.IsDeleted, false).
                                                                AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                if (!oCity.IsLoaded)
                {
                    szMessage += CityName + ", " + CountyName + " was not loaded \r\n";
                }
            }
            MessageBox.Show(szMessage);
        }

        private LM_License CreateCompanyLicense(LM_Requirement oRequirement, string IssueDate, string ExpirationDate, string LicenseNumber, bool IsComplete)
        {
            LM_License oLicense = new LM_License();
            oLicense.RequirementID = oRequirement.RequirementID;
            DateTime Issue = new DateTime();
            if (DateTime.TryParse(IssueDate, out Issue))
                oLicense.IssueDate = Issue;
            DateTime Exp = new DateTime();
            if (DateTime.TryParse(ExpirationDate, out Exp))
                oLicense.ExpirationDate = Exp;
            oLicense.LicenseNumber = LicenseNumber;
            oLicense.RequirementsAreMet = IsComplete;
            oLicense.IsActive = true;
            oLicense.IsDeleted = false;
            oLicense.ModifiedByDate = DateTime.Now;
            oLicense.CreatedByDate = DateTime.Now;
            oLicense.ModifiedByID = LicenseManagementHelper.LoggedInUser;
            oLicense.CreatedByID = LicenseManagementHelper.LoggedInUser;
            oLicense.Save();

            foreach (LM_RequirementItem ReqItem in oRequirement.LM_RequirementItems)
            {
                AddLicenseItem(oLicense.LicenseID, IsComplete, ReqItem.Name, ReqItem.Description);
            }

            return oLicense;
        }

        private void LoadCityBusinessLicenses()
        {
            DataTable oDt = DefaultTable("Sheet3");

            //reporting tools
            string NotLoaded = "";
            int CityLicensesLoaded = 0;
            int CountyLicensesLoaded = 0;
            int CountyRequirementsExisted = 0;
            int CityRequirementsExisted = 0;

            foreach (DataRow currRow in oDt.Rows)
            {
                string StateAB = currRow["STATE"].ToString();
                string CountyName = currRow["COUNTY"].ToString();
                string CityName = currRow["CITY"].ToString();
                string Application = currRow["APPLICATION"].ToString();
                string CheckNumber = currRow["CHECK #"].ToString();
                string Fee = currRow["Fee"].ToString();
                string LicenseNumber = currRow["LICENSE NUMBER"].ToString();
                string ExpDate = currRow["EXPIRES"].ToString();
                string Renewal = currRow["RENEWAL"].ToString();
                string Note = currRow["NOTE"].ToString();
                bool IsCompleted = (!LicenseNumber.ToUpper().Contains("PENDING")) ? true : false;



                LM_Location oState = new LM_Location();
                oState.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.State).
                                                                    AND(LM_Location.Columns.Abbreviation, StateAB).
                                                                    AND(LM_Location.Columns.IsDeleted, false).
                                                                    AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                LM_Location oCounty = new LM_Location();
                oCounty.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.County).
                                                                AND(LM_Location.Columns.LocationName, CountyName).
                                                                AND(LM_Location.Columns.ParentStateID, oState.LocationID).
                                                                AND(LM_Location.Columns.IsDeleted, false).
                                                                AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                LM_Location oCity = new LM_Location();
                oCity.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.City).
                                                                AND(LM_Location.Columns.LocationName, CityName).
                                                                AND(LM_Location.Columns.ParentStateID, oState.LocationID).
                                                                AND(LM_Location.Columns.ParentCountyID, oCounty.LocationID).
                                                                AND(LM_Location.Columns.IsDeleted, false).
                                                                AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                if (oCity.IsLoaded)
                {
                    //load requirements if any
                    LM_Requirement oCheckRequirement = new LM_Requirement();
                    oCheckRequirement.LoadAndCloseReader(LM_Requirement.Query().WHERE(LM_Requirement.Columns.IsActive, true).
                                                                                AND(LM_Requirement.Columns.IsDeleted, false).
                                                                                AND(LM_Requirement.Columns.LocationID, oCity.LocationID).
                                                                                AND(LM_Requirement.Columns.RequirementName, "Business License").ExecuteReader());

                    if (oCheckRequirement.IsLoaded)
                    {
                        LM_License oLicense = CreateCompanyLicense(oCheckRequirement, "", ExpDate, LicenseNumber, IsCompleted);
                        if (Application != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Application Info: " + Application);

                        if (CheckNumber != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Check Number: " + CheckNumber);

                        if (Renewal != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Renewal: " + Renewal);

                        if (Note != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, Note);

                        CityRequirementsExisted = CityRequirementsExisted + 1;
                    }
                    else
                    {
                        //create new requirement
                        LM_Requirement oRequirement = new LM_Requirement();
                        oRequirement.LocationID = oCity.LocationID;
                        oRequirement.RequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Company;
                        oRequirement.LockID = (int)LM_Lock.EnumLock.SoftLock;
                        oRequirement.RequirementName = "Business License";
                        oRequirement.CallCenterMessage = "Platinum Protection is not authorized to sell in this city";
                        oRequirement.IsActive = true;
                        oRequirement.IsDeleted = false;
                        oRequirement.ModifiedByDate = DateTime.Now;
                        oRequirement.CreatedByDate = DateTime.Now;
                        oRequirement.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                        oRequirement.CreatedByID = LicenseManagementHelper.LoggedInUser;
                        oRequirement.Save();

                        AddRequirementItem(oRequirement.RequirementID, "Application", "Application Needed");

                        if (Fee != "")
                            AddRequirementItem(oRequirement.RequirementID, "Fee", Fee);

                        LM_License oLicense = CreateCompanyLicense(oRequirement, "", ExpDate, LicenseNumber, IsCompleted);

                        if (Application != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Application Info: " + Application);

                        if (CheckNumber != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Check Number: " + CheckNumber);

                        if (Renewal != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Renewal: " + Renewal);

                        if (Note != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, Note);

                    }
                    CityLicensesLoaded = CityLicensesLoaded + 1;
                }
                else if (oCounty.IsLoaded)
                {
                    //load requirements if any
                    LM_Requirement oCheckRequirement = new LM_Requirement();
                    oCheckRequirement.LoadAndCloseReader(LM_Requirement.Query().WHERE(LM_Requirement.Columns.IsActive, true).
                                                                                AND(LM_Requirement.Columns.IsDeleted, false).
                                                                                AND(LM_Requirement.Columns.LocationID, oCounty.LocationID).
                                                                                AND(LM_Requirement.Columns.RequirementName, "Business License").ExecuteReader());

                    if (oCheckRequirement.IsLoaded)
                    {
                        LM_License oLicense = CreateCompanyLicense(oCheckRequirement, "", ExpDate, LicenseNumber, IsCompleted);

                        if (Application != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Application Info: " + Application);

                        if (CheckNumber != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Check Number: " + CheckNumber);

                        if (Renewal != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Renewal: " + Renewal);

                        if (Note != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, Note);

                        CountyRequirementsExisted = CountyRequirementsExisted + 1;
                    }
                    else
                    {
                        //create new requirement
                        LM_Requirement oRequirement = new LM_Requirement();
                        oRequirement.LocationID = oCounty.LocationID;
                        oRequirement.RequirementTypeID = (int)LM_RequirementType.EnumRequirementType.Company;
                        oRequirement.LockID = (int)LM_Lock.EnumLock.SoftLock;
                        oRequirement.RequirementName = "Business License";
                        oRequirement.CallCenterMessage = "Platinum Protection is not authorized to sell in this county";
                        oRequirement.IsActive = true;
                        oRequirement.IsDeleted = false;
                        oRequirement.ModifiedByDate = DateTime.Now;
                        oRequirement.CreatedByDate = DateTime.Now;
                        oRequirement.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                        oRequirement.CreatedByID = LicenseManagementHelper.LoggedInUser;
                        oRequirement.Save();

                        AddRequirementItem(oRequirement.RequirementID, "Application", "Application Needed");
                        if (Fee != "")
                            AddRequirementItem(oRequirement.RequirementID, "Fee", Fee);

                        LM_License oLicense = CreateCompanyLicense(oRequirement, "", ExpDate, LicenseNumber, IsCompleted);

                        if (Application != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Application Info: " + Application);

                        if (CheckNumber != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Check Number: " + CheckNumber);

                        if (Renewal != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, "Renewal: " + Renewal);

                        if (Note != "")
                            AddNote(LM_NoteType.EnumNoteType.License, oLicense.LicenseID, Note);
                    }
                    CountyLicensesLoaded = CountyLicensesLoaded + 1;
                }
                else
                {
                    NotLoaded += string.Format("{0}, {1}, {2}\r\n", CityName, CountyName, StateAB);
                }
            }

            string szNotLoadedMessage = "";
            if (NotLoaded != "")
                szNotLoadedMessage += string.Format("Locations not loaded: {0}", NotLoaded);

            MessageBox.Show(string.Format("{0} City Licenses Loaded\r\n{1} city requirements already existed\r\n{2} County Licenses Loaded\r\n{3} county requirements already existed\r\n{4}", CityLicensesLoaded, CityRequirementsExisted, CountyLicensesLoaded, CountyRequirementsExisted, szNotLoadedMessage));
        }

        private void LoadCustomerCityRequirements()
        {
            DataTable oDt = DefaultTable("All Permits");
            int CityNoRequired = 0;
            int CountyNoRequired = 0;
            int CityRequired = 0;
            int CountyRequired = 0;
            foreach (DataRow currRow in oDt.Rows)
            {
                string StateAB = currRow["State"].ToString();
                string CountyName = currRow["County"].ToString();
                string CityName = currRow["City"].ToString();
                string AgencyName = currRow["Department"].ToString();
                string Phone = currRow["Phone"].ToString();
                string Contact = currRow["Contact"].ToString();
                bool AlarmRegRequired = (currRow["Alarm Registration Reqd"].ToString().ToUpper() == "YES") ? true : false;
                string AppDescription = currRow["Application Process"].ToString();
                string Fee = currRow["Fee"].ToString();
                string Notes = currRow["Notes"].ToString();
                string RequirementName = currRow["Requirement Name"].ToString();
                string Website = currRow["Website"].ToString();

                LM_Location oState = new LM_Location();
                oState.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.State).
                                                                    AND(LM_Location.Columns.Abbreviation, StateAB).
                                                                    AND(LM_Location.Columns.IsDeleted, false).
                                                                    AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                LM_Location oCounty = new LM_Location();
                oCounty.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.County).
                                                                AND(LM_Location.Columns.LocationName, CountyName).
                                                                AND(LM_Location.Columns.ParentStateID, oState.LocationID).
                                                                AND(LM_Location.Columns.IsDeleted, false).
                                                                AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                LM_Location oCity = new LM_Location();
                oCity.LoadAndCloseReader(LM_Location.Query().WHERE(LM_Location.Columns.LocationTypeID, (int)LM_LocationType.EnumLocationType.City).
                                                                AND(LM_Location.Columns.LocationName, CityName).
                                                                AND(LM_Location.Columns.ParentStateID, oState.LocationID).
                                                                AND(LM_Location.Columns.ParentCountyID, oCounty.LocationID).
                                                                AND(LM_Location.Columns.IsDeleted, false).
                                                                AND(LM_Location.Columns.IsActive, true).ExecuteReader());

                if (oCity.IsLoaded)
                {
                    if (!AlarmRegRequired && AgencyName != "")
                    {
                        //create agency and add appropriate notes including that no requirement type is needed
                        LM_Agency oCheckAgency = new LM_Agency();
                        oCheckAgency.LoadAndCloseReader(LM_Agency.Query().WHERE(LM_Agency.Columns.IsDeleted, false).
                                                                        AND(LM_Agency.Columns.IsActive, true).
                                                                        AND(LM_Agency.Columns.LocationID, oCity.LocationID).
                                                                        AND(LM_Agency.Columns.AgencyName, AgencyName).ExecuteReader());

                        if (oCheckAgency.IsLoaded)
                        {
                            AddNote(LM_NoteType.EnumNoteType.Agency, oCheckAgency.AgencyID, string.Format("{0} is not required in this city", RequirementName));
                        }
                        else
                        {
                            LM_Agency oAgency = new LM_Agency();
                            oAgency.LocationID = oCity.LocationID;
                            oAgency.AgencyName = AgencyName;
                            oAgency.Contact = Contact;
                            oAgency.Phone1 = Phone;
                            oAgency.Website = Website;
                            oAgency.IsActive = true;
                            oAgency.IsDeleted = false;
                            oAgency.ModifiedByDate = DateTime.Now;
                            oAgency.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                            oAgency.CreatedbyDate = DateTime.Now;
                            oAgency.CreatedByID = LicenseManagementHelper.LoggedInUser;
                            oAgency.Save();

                            AddNote(LM_NoteType.EnumNoteType.Agency, oAgency.AgencyID, string.Format("{0} is not required in this city", RequirementName));
                        }

                        CityNoRequired = CityNoRequired + 1;
                    }
                    else if (AlarmRegRequired)
                    {
                        //add agency if exists, add requirement with agency ID and items w/ application being one by default
                        LM_Agency oAgency = new LM_Agency();
                        if (AgencyName != "")
                        {
                            oAgency.LoadAndCloseReader(LM_Agency.Query().WHERE(LM_Agency.Columns.IsDeleted, false).
                                                                        AND(LM_Agency.Columns.IsActive, true).
                                                                        AND(LM_Agency.Columns.LocationID, oCity.LocationID).
                                                                        AND(LM_Agency.Columns.AgencyName, AgencyName).ExecuteReader());
                            if (!oAgency.IsLoaded)
                            {
                                oAgency.LocationID = oCity.LocationID;
                                oAgency.AgencyName = AgencyName;
                                oAgency.Contact = Contact;
                                oAgency.Phone1 = Phone;
                                oAgency.Website = Website;
                                oAgency.IsActive = true;
                                oAgency.IsDeleted = false;
                                oAgency.ModifiedByDate = DateTime.Now;
                                oAgency.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                                oAgency.CreatedbyDate = DateTime.Now;
                                oAgency.CreatedByID = LicenseManagementHelper.LoggedInUser;
                                oAgency.Save();
                            }
                        }

                        LM_Requirement oRequirement = new LM_Requirement();
                        oRequirement.LocationID = oCity.LocationID;
                        if (AgencyName != "")
                            oRequirement.AgencyID = oAgency.AgencyID;
                        oRequirement.RequirementTypeID = (int)LM_RequirementType.EnumRequirementType.CustomerPermit;
                        oRequirement.LockID = (int)LM_Lock.EnumLock.SoftLock;
                        oRequirement.RequirementName = RequirementName;
                        oRequirement.ApplicationDescription = AppDescription;
                        oRequirement.CallCenterMessage = "The customer is required to comply with the following: " + RequirementName;
                        oRequirement.IsActive = true;
                        oRequirement.IsDeleted = false;
                        oRequirement.ModifiedByDate = DateTime.Now;
                        oRequirement.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                        oRequirement.CreatedByDate = DateTime.Now;
                        oRequirement.CreatedByID = LicenseManagementHelper.LoggedInUser;
                        oRequirement.Save();

                        AddRequirementItem(oRequirement.RequirementID, "Application", "Application must be submitted");

                        if (Fee != "")
                            AddRequirementItem(oRequirement.RequirementID, "Fee", Fee);
                        if (Notes != "")
                            AddNote(LM_NoteType.EnumNoteType.Requirement, oRequirement.RequirementID, Notes);

                        CityRequired = CityRequired + 1;
                    }
                }
                else if (oCounty.IsLoaded)
                {
                    if (!AlarmRegRequired && AgencyName != "")
                    {
                        //create agency and add appropriate notes including that no requirement type is needed
                        LM_Agency oCheckAgency = new LM_Agency();
                        oCheckAgency.LoadAndCloseReader(LM_Agency.Query().WHERE(LM_Agency.Columns.IsDeleted, false).
                                                                        AND(LM_Agency.Columns.IsActive, true).
                                                                        AND(LM_Agency.Columns.LocationID, oCounty.LocationID).
                                                                        AND(LM_Agency.Columns.AgencyName, AgencyName).ExecuteReader());

                        if (oCheckAgency.IsLoaded)
                        {
                            AddNote(LM_NoteType.EnumNoteType.Agency, oCheckAgency.AgencyID, string.Format("{0} is not required in this county", RequirementName));
                        }
                        else
                        {
                            LM_Agency oAgency = new LM_Agency();
                            oAgency.LocationID = oCounty.LocationID;
                            oAgency.AgencyName = AgencyName;
                            oAgency.Contact = Contact;
                            oAgency.Phone1 = Phone;
                            oAgency.Website = Website;
                            oAgency.IsActive = true;
                            oAgency.IsDeleted = false;
                            oAgency.ModifiedByDate = DateTime.Now;
                            oAgency.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                            oAgency.CreatedbyDate = DateTime.Now;
                            oAgency.CreatedByID = LicenseManagementHelper.LoggedInUser;
                            oAgency.Save();

                            AddNote(LM_NoteType.EnumNoteType.Agency, oAgency.AgencyID, string.Format("{0} is not required in this county", RequirementName));
                        }

                        CountyNoRequired = CountyNoRequired + 1;
                    }
                    else if (AlarmRegRequired)
                    {
                        //add agency if exists, add requirement with agency ID and items w/ application being one by default
                        LM_Agency oAgency = new LM_Agency();
                        if (AgencyName != "")
                        {
                            oAgency.LoadAndCloseReader(LM_Agency.Query().WHERE(LM_Agency.Columns.IsDeleted, false).
                                                                        AND(LM_Agency.Columns.IsActive, true).
                                                                        AND(LM_Agency.Columns.LocationID, oCounty.LocationID).
                                                                        AND(LM_Agency.Columns.AgencyName, AgencyName).ExecuteReader());
                            if (!oAgency.IsLoaded)
                            {
                                oAgency.LocationID = oCounty.LocationID;
                                oAgency.AgencyName = AgencyName;
                                oAgency.Contact = Contact;
                                oAgency.Phone1 = Phone;
                                oAgency.Website = Website;
                                oAgency.IsActive = true;
                                oAgency.IsDeleted = false;
                                oAgency.ModifiedByDate = DateTime.Now;
                                oAgency.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                                oAgency.CreatedbyDate = DateTime.Now;
                                oAgency.CreatedByID = LicenseManagementHelper.LoggedInUser;
                                oAgency.Save();
                            }
                        }

                        LM_Requirement oRequirement = new LM_Requirement();
                        oRequirement.LocationID = oCounty.LocationID;
                        if (AgencyName != "")
                            oRequirement.AgencyID = oAgency.AgencyID;
                        oRequirement.RequirementTypeID = (int)LM_RequirementType.EnumRequirementType.CustomerPermit;
                        oRequirement.LockID = (int)LM_Lock.EnumLock.SoftLock;
                        oRequirement.RequirementName = RequirementName;
                        oRequirement.ApplicationDescription = AppDescription;
                        oRequirement.CallCenterMessage = "The customer is required to comply with the following: " + RequirementName;
                        oRequirement.IsActive = true;
                        oRequirement.IsDeleted = false;
                        oRequirement.ModifiedByDate = DateTime.Now;
                        oRequirement.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                        oRequirement.CreatedByDate = DateTime.Now;
                        oRequirement.CreatedByID = LicenseManagementHelper.LoggedInUser;
                        oRequirement.Save();

                        AddRequirementItem(oRequirement.RequirementID, "Application", "Application must be submitted");

                        if (Fee != "")
                            AddRequirementItem(oRequirement.RequirementID, "Fee", Fee);
                        if (Notes != "")
                            AddNote(LM_NoteType.EnumNoteType.Requirement, oRequirement.RequirementID, Notes);

                        CountyRequired = CountyRequired + 1;
                    }
                }

            }

            MessageBox.Show(string.Format("{0} Cities and {1} counties had no requirements\r\n{2} cities and {3} counties had requirements", CityNoRequired, CountyNoRequired, CityRequired, CountyNoRequired));
        }

        private void LoadCityCustomerPermits(int RequirementID, string AccountIDCol, string PermitCol, string SubmitDateCol)
        {
            LM_License oCheckLicense = new LM_License();
            oCheckLicense.LoadAndCloseReader(LM_License.Query().WHERE(LM_License.Columns.RequirementID, RequirementID).ExecuteReader());
            if (!oCheckLicense.IsLoaded)
            {
                DataTable oDt = DefaultTable("Sheet1");
                int Count = 0;
                try
                {
                    foreach (DataRow currRow in oDt.Rows)
                    {
                        int AccountID = 0;
                        bool AccountIDExists = int.TryParse(currRow[AccountIDCol].ToString(), out AccountID);
                        string PermitNumber = currRow[PermitCol].ToString();
                        DateTime SubmitDate = new DateTime();
                        bool SubmitDateExists = false;
                        if (SubmitDateCol != "")
                            SubmitDateExists = DateTime.TryParse(currRow[SubmitDateCol].ToString(), out SubmitDate);


                        if (AccountIDExists)
                        {
                            LM_Requirement oRequirement = LM_Requirement.LoadByPrimaryKey(RequirementID);

                            LM_License oLicense = new LM_License();
                            oLicense.RequirementID = oRequirement.RequirementID;
                            oLicense.LicenseNumber = PermitNumber;
                            oLicense.RequirementsAreMet = true;
                            oLicense.AccountID = AccountID;
                            if (SubmitDateExists)
                                oLicense.SubmissionDate = SubmitDate;
                            oLicense.IsActive = true;
                            oLicense.IsDeleted = false;
                            oLicense.ModifiedByDate = DateTime.Now;
                            oLicense.CreatedByDate = DateTime.Now;
                            oLicense.ModifiedByID = LicenseManagementHelper.LoggedInUser;
                            oLicense.CreatedByID = LicenseManagementHelper.LoggedInUser;
                            oLicense.Save();

                            foreach (LM_RequirementItem ReqItem in oRequirement.LM_RequirementItems)
                            {
                                AddLicenseItem(oLicense.LicenseID, true, ReqItem.Name, ReqItem.Description);
                            }

                            Count = Count + 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                MessageBox.Show(Count + " Licenses created");
            }
            else
            {
                MessageBox.Show("Licenses already exist for this requirement");
            }
        }

        private void DecryptSSNs()
        {
            DataTable oDt = DefaultTable("Sheet1");
            DataTable oGrid = new DataTable();
            oGrid.Columns.Add("SSN");
            foreach (DataRow currRow in oDt.Rows)
            {
                string SSN = currRow["SSN"].ToString();
                try
                {
                    oGrid.Rows.Add(TripleDES.DecryptString(SSN, null).Insert(3, "-").Insert(6, "-"));
                }
                catch
                {
                    oGrid.Rows.Add(SSN);
                }
            }

            gcStateCounty.DataSource = oGrid;
        }

        #endregion

        #region Checks

        private void CheckLicenses()
        {
            DataTable oDt = DefaultTable("Sheet1");
            DataTable DataSource = new DataTable();
            DataSource.Columns.Add("License Loaded");
            foreach (DataRow currRow in oDt.Rows)
            {
                string LicenseNumber = currRow["LICENSE"].ToString();
                DataSource.Rows.Add((LicenseNumber != "") ? "Yes" : "No");
            }
            gcStateCounty.DataSource = DataSource;
        }

        private void LoadPDFDoc()
        {
            //LD_Template oTemplate = LD_Template.LoadByPrimaryKey(1);
            DataTable oDt = DefaultTable("Sheet1");
            //List<string> FileList = new List<string>();
            //string testFile = Path.Combine(Application.StartupPath, "Test.pdf");
            //using (FileStream oStr = new FileStream(testFile, FileMode.Create, FileAccess.ReadWrite))
            //{
            //    Document oDoc = new Document(PageSize.LETTER, 72, 72, 36, 36);
            //    PdfWriter writer = PdfWriter.GetInstance(oDoc, oStr);
            //    oDoc.Open();

            //    foreach (DataRow oRow in oDt.Rows)
            //    {
            //        string FileName = Path.Combine(Application.StartupPath, string.Format("{0}.pdf", Guid.NewGuid()));
            //        FileList.Add(FileName);
            //        int AccountID = int.Parse(oRow[0].ToString());
            //        TemplateUtility.CreatePDFFromTemplate(1, AccountID, writer, oDoc, FileName, null);
            //    }
            //    oDoc.Close();

            //    Process.Start(testFile);
            //}
            //foreach (string filname in FileList)
            //    if (File.Exists(filname))
            //        File.Delete(filname);
            foreach (DataRow oRow in oDt.Rows)
            {
                int AccountID = int.Parse(oRow[0].ToString());
                LD_LettersToPrint oLetter = new LD_LettersToPrint();
                oLetter.AccountID = AccountID;
                oLetter.LetterName = "Sandy Permit Application";
                oLetter.Letter = "Sandy Permit Application";
                oLetter.Priority = "Regular";
                oLetter.CorrespondanceNotes = "3 Pages - Instructional letter with application";
                oLetter.IsPaymentAuthorization = false;
                oLetter.IsInsuranceLetter = false;
                oLetter.IsPrinted = false;
                oLetter.TemplateID = 1;
                oLetter.IsDeleted = false;
                oLetter.CreatedByDate = DateTime.Now;
                oLetter.Save(LicenseManagementHelper.LoggedInUser);
            }


        }

        private void DocTypeLoadQueries()
        {
            LD_DocType dAccount = LD_DocType.LoadByPrimaryKey(1);
            dAccount.Query = @"SELECT TOP 1 *
FROM MS_Account MSA
	LEFT OUTER JOIN MC_Lead Customer1
	ON
		MSA.Customer1ID = Customer1.LeadID
	LEFT OUTER JOIN MC_Address MCA
	ON
		MSA.PremiseAddressID = MCA.AddressID
	LEFT OUTER JOIN MC_PoliticalState MPS
	ON
		MCA.StateID = MPS.StateID
	LEFT OUTER JOIN MC_PoliticalCountry MPC
	ON
		MCA.CountryID = MPC.CountryID
WHERE MSA.AccountID = {0}";
            dAccount.Save();
            LD_DocType dRecruit = LD_DocType.LoadByPrimaryKey(2);
            dRecruit.Query = @"SELECT TOP 1 RUR.*, RUU.*, PermAdd.StreetAddress AS PermStreetAddress, PermAdd.City AS PermCity, PermAdd.PostalCode AS PermPostalCode
			, PermState.StateAB AS PermStateAB, PermState.StateName AS PermStateName, PermCountry.CountryName AS PermCountryName, CurrAdd.StreetAddress AS CurrStreetAddress, CurrAdd.City AS CurrCity, CurrAdd.PostalCode AS CurrPostalCode
			, CurrState.StateAB AS CurrStateAB, CurrState.StateName AS CurrStateName, CurrCountry.CountryName AS CurrCountryName
			, UManager.FullName AS ManagerName
			, RecruitedBy.FullName AS RecruitedBy
FROM RU_Recruits RUR
	LEFT OUTER JOIN RU_Users RUU
	ON
		RUR.UserID = RUU.UserID
	LEFT OUTER JOIN RU_RecruitAddresses PermAdd
	ON
		RUU.PermanentAddressID = PermAdd.AddressID
	LEFT OUTER JOIN Platinum_Protection_InterimCRM.dbo.MC_PoliticalState PermState
	ON
		PermAdd.StateID = PermState.StateID
	LEFT OUTER JOIN Platinum_Protection_InterimCRM.dbo.MC_PoliticalCountry PermCountry
	ON
		PermAdd.CountryID = PermCountry.CountryID
	LEFT OUTER JOIN RU_RecruitAddresses CurrAdd
	ON
		RUR.CurrentAddressID = CurrAdd.AddressID
	LEFT OUTER JOIN Platinum_Protection_InterimCRM.dbo.MC_PoliticalState CurrState
	ON
		CurrAdd.StateID = CurrState.StateID
	LEFT OUTER JOIN Platinum_Protection_InterimCRM.dbo.MC_PoliticalCountry CurrCountry
	ON
		CurrAdd.CountryID = CurrCountry.CountryID
	LEFT OUTER JOIN RU_Recruits RManager
	ON
		RManager.RecruitID = RUR.ReportsToID
	LEFT OUTER JOIN RU_User UManager
	ON
		UManager.UserID = Manager.UserID
	LEFT OUTER JOIN RU_User RecruitedBy
	ON
		RUR.RecruitedByID = RecruitedBy.UserID
	WHERE RUR.RecruitID = {0}";
            dRecruit.Save();

            LD_DocType dReg = LD_DocType.LoadByPrimaryKey(4);
            dReg.Query = @"SELECT TOP 1 REG.*, PermAdd.StreetAddress AS PermStreetAddress, PermAdd.City AS PermCity, PermAdd.PostalCode AS PermPostalCode
			, PermState.StateAB AS PermStateAB, PermState.StateName AS PermStateName, PermCountry.CountryName AS PermCountryName, CurrAdd.StreetAddress AS CurrStreetAddress, CurrAdd.City AS CurrCity, CurrAdd.PostalCode AS CurrPostalCode
			, CurrState.StateAB AS CurrStateAB, CurrState.StateName AS CurrStateName, CurrCountry.CountryName AS CurrCountryName
			, UManager.FullName AS ManagerName
			, RecruitedBy.FullName AS RecruitedBy
FROM RU_RecruitRegistration REG
	LEFT OUTER JOIN RU_RecruitAddresses PermAdd
	ON
		REG.PermanentAddressID = PermAdd.AddressID
	LEFT OUTER JOIN Platinum_Protection_InterimCRM.dbo.MC_PoliticalState PermState
	ON
		PermAdd.StateID = PermState.StateID
	LEFT OUTER JOIN Platinum_Protection_InterimCRM.dbo.MC_PoliticalCountry PermCountry
	ON
		PermAdd.CountryID = PermCountry.CountryID
	LEFT OUTER JOIN RU_RecruitAddresses CurrAdd
	ON
		REG.CurrentAddressID = CurrAdd.AddressID
	LEFT OUTER JOIN Platinum_Protection_InterimCRM.dbo.MC_PoliticalState CurrState
	ON
		CurrAdd.StateID = CurrState.StateID
	LEFT OUTER JOIN Platinum_Protection_InterimCRM.dbo.MC_PoliticalCountry CurrCountry
	ON
		CurrAdd.CountryID = CurrCountry.CountryID
	LEFT OUTER JOIN RU_Recruits RManager
	ON
		RManager.RecruitID = REG.ReportsToID
	LEFT OUTER JOIN RU_User UManager
	ON
		UManager.UserID = Manager.UserID
	LEFT OUTER JOIN RU_User RecruitedBy
	ON
		REG.RecruitedByID = RecruitedBy.UserID
	WHERE oReg.RecruitRegistrationID = {0}";
            dReg.Save();
        }

        #endregion

        private bool HasFullName(LD_FieldCollection oFields)
        {
            string FullName = "FullName";
            foreach (LD_Field oField in oFields)
            {
                if (oField.FieldName == FullName)
                    return true;
            }
            return false;
        }

        private bool HasStreetAddress(LD_FieldCollection oFields)
        {
            string StreetAddress = "StreetAddress";
            foreach (LD_Field oField in oFields)
            {
                if (oField.FieldName == StreetAddress)
                    return true;
            }
            return false;
        }

        private bool HasCityStateZip(LD_FieldCollection oFields)
        {
            string CityStateZip = "CityStateZip";
            foreach (LD_Field oField in oFields)
            {
                if (oField.FieldName == CityStateZip)
                    return true;
            }
            return false;
        }

        private LD_Field CreateField(int TemplateID, string FieldName, string ColumnName, string FormatString)
        {
            LD_Field oField = new LD_Field();
            oField.TemplateID = TemplateID;
            oField.FieldName = FieldName;
            oField.DBColumnName = ColumnName;
            oField.FormatString = FormatString;
            oField.IsEncrypted = false;
            oField.IsSubstring = false;
            oField.IsDeleted = false;
            oField.Save();

            return oField;
        }

        private void AddFullNameField(int TemplateID)
        {
            LD_Field FirstName = CreateField(TemplateID, "FullName", "FirstName", "{0} {1} {2}");
            LD_Field MiddleName = CreateField(TemplateID, "", "MiddleName", "{0}");
            LD_Field LastName = CreateField(TemplateID, "", "LastName", "{0}");

            //add subsequent fields
            FirstName.SubsequentFieldID = MiddleName.FieldID;
            FirstName.Save();
            MiddleName.SubsequentFieldID = LastName.FieldID;
            MiddleName.Save();
        }

        private void AddStreetAddressField(int TemplateID)
        {
            LD_Field oStreetAddress = CreateField(TemplateID, "StreetAddress", "StreetAddress", "{0}");
        }

        private void AddCityStateZipField(int TemplateID)
        {
            LD_Field City = CreateField(TemplateID, "CityStateZip", "City", "{0}, {1} {2}");
            LD_Field StateAB = CreateField(TemplateID, "", "StateAB", "{0}");
            LD_Field Zip = CreateField(TemplateID, "", "PostalCode", "{0}");

            City.SubsequentFieldID = StateAB.FieldID;
            City.Save();

            StateAB.SubsequentFieldID = Zip.FieldID;
            StateAB.Save();
        }

        private void ReformatCustomerPermits()
        {
            LD_TemplateCollection oTemplates = new LD_TemplateCollection();
            oTemplates.LoadAndCloseReader(LD_Template.Query().WHERE(LD_Template.Columns.DocTypeID, (int)LD_DocType.enumDocType.Account).AND(LD_Template.Columns.IsDeleted, false).ExecuteReader());

            foreach (LD_Template PermitLetter in oTemplates)
            {
                if (!HasFullName(PermitLetter.LD_Fields))
                    AddFullNameField(PermitLetter.TemplateID);

                if (!HasStreetAddress(PermitLetter.LD_Fields))
                    AddStreetAddressField(PermitLetter.TemplateID);

                if (!HasCityStateZip(PermitLetter.LD_Fields))
                    AddCityStateZipField(PermitLetter.TemplateID);
            }
        }

        private void TestCustomerPermits()
        {
            List<string> FileList = new List<string>();
            LD_TemplateCollection oTemplates = new LD_TemplateCollection();
            oTemplates.LoadAndCloseReader(LD_Template.Query().WHERE(LD_Template.Columns.DocTypeID, (int)LD_DocType.enumDocType.Account).AND(LD_Template.Columns.IsDeleted, false).ExecuteReader());
            string szMessage = "";
            string testFile = Path.Combine(Application.StartupPath, "Test.pdf");
            using (FileStream oStr = new FileStream(testFile, FileMode.Create, FileAccess.ReadWrite))
            {
                Document oDoc = new Document(PageSize.LETTER, 72, 72, 36, 36);
                PdfWriter writer = PdfWriter.GetInstance(oDoc, oStr);
                oDoc.Open();


                foreach (LD_Template oTemplate in oTemplates)
                {
                    try
                    {
                        string FileName = Path.Combine(Application.StartupPath, string.Format("{0}.pdf", Guid.NewGuid()));
                        FileList.Add(FileName);
                        TemplateUtility.CreatePDFFromTemplate(oTemplate.TemplateID, 100000, writer, oDoc, FileName, null);
                    }
                    catch (Exception ex)
                    {
                        szMessage += oTemplate.TemplateID;
                    }
                }

                oDoc.Close();

                //Process.Start(testFile);
            }

            foreach (string filname in FileList)
                if (File.Exists(filname))
                    File.Delete(filname);

            if (szMessage != "")
                MessageBox.Show(szMessage);
        }

        private void CreateCustomerPermitPDFFromFile(string szFileName)
        {
            if (ofdExcelFile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream oStr = new FileStream(ofdExcelFile.FileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    Document oDoc = new Document(PageSize.LETTER, 72, 72, 36, 36);
                    PdfWriter writer = PdfWriter.GetInstance(oDoc, oStr);
                    oDoc.Open();

                    BaseFont oFont = BaseFont.CreateFont(BaseFont.HELVETICA, "", false);
                    PdfAcroForm acroform = writer.AcroForm;
                    acroform.AddSingleLineTextField("FullName", "", oFont, 14, 50, 620, 600, 650);
                    acroform.AddSingleLineTextField("StreetAddress", "", oFont, 14, 50, 590, 600, 620);
                    acroform.AddSingleLineTextField("CityStateZip", "", oFont, 14, 50, 560, 600, 590);

                    oDoc.Close();
                }

                Process.Start(szFileName);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            CreateCustomerPermitPDFFromFile(Path.Combine(Application.StartupPath, "Test.pdf"));
            //MessageBox.Show(TripleDES.DecryptString("ixqisHJtuyftC2ElRHCYa3YjRwh9qU0LqX+jjQDc+iRXAg0gOKD2ijmMRSKxR7kqjMbvjuk7HmecVheNiqtzBguvW0nTpGw+", null));
        }
    }
}

