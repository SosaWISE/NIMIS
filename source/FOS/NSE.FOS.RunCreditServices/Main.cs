using System;
using System.Collections.Generic;
using NSE.FOS.RunCreditServices.Interfaces;
using NSE.FOS.RunCreditServices.Vendors;
using SOS.Data.SosCrm;
using SOS.Lib.Core.CreditReportService;
using SOS.Lib.Util.Configuration;

namespace NSE.FOS.RunCreditServices
{
    public class Main
	{
		#region .ctor
		public Main()
	    {
			_vendorID = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("CreditReport_Vendor"), null);
			Initialize();
	    }
		#endregion .ctor

		#region Properties

		private readonly string _vendorID;
		public string VendorID
		{
			get { return _vendorID; }
		}

		private IVendor _avVendor;

		public static class Vendors
		{
			public const string ABARA = "ABARA";
			public const string MICROBILT = "MICROBILT";
		}

		#endregion Properties
		#region Methods

		private void Initialize()
		{
			switch (VendorID)
			{
				case Vendors.ABARA:
					_avVendor = new Abara();
					break;
				case Vendors.MICROBILT:
					_avVendor = new Microbilt();
					break;
				default:
					throw new Exception(string.Format("The vendor set in config file ('{0}') is not supported.", VendorID));
			}
		}

	    public bool RunCredit(IWSLead wsLead, IWSAddress wsAddress, string[] bureausList, IWSSeason season, string username,
		    ref List<WSMessage> messageList, out IWSCreditReportInfo crInfo)
	    {
			/** Initialize. */
			var result = _avVendor.RunCredit(wsLead, wsAddress, bureausList, season, username, ref messageList, out crInfo);

			/** Calculate the ScoreGroup. */
			crInfo.ScoreGroup = CreditHelper.GetCreditScoreGroup(crInfo.Score, crInfo.ReportFound,
				excellentCreditScore: season.ExcellentCreditScoreThreshold,
				passCreditScore: season.PassCreditScoreThreshold,
				subCreditScore: season.SubCreditScoreThreshold);

			/** Return result. */
		    return result;
	    }

	    public static string[] GetBureausList()
	    {
			// Check to see if there is a list in the configuration file.
			if (!string.IsNullOrEmpty(ConfigurationSettings.Current.GetConfig("CreditService_BureauListOrder")))
				return ConfigurationSettings.Current.GetConfig("CreditService_BureauListOrder").Split(',');

			// ** Default path of execution.
		    string[] result =
		    {
			    QL_CreditReportBureau.MetaData.TransUnionID,
			    QL_CreditReportBureau.MetaData.EquafaxID,
			    QL_CreditReportBureau.MetaData.ExperianID
		    };

		    return result;
	    }

	    #endregion Methods
	}
}
