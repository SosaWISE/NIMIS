using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NXS.Lib.PDFs.Interfaces;

namespace NXS.Lib.PDFsUT
{
	[TestClass]
	public class AMAUT
	{
		#region Properties

		private const string _PDF_AMA_INPUT_PATH = ".\\Res\\templates\\{0}.pdf";
		private const string _PDF_AMA_OUTPUT_PATH = ".\\Output\\{0}_{1}_{2}.pdf";

		private class CustInfo : IAMAInfo
		{
			#region .ctor

			public CustInfo(long accountId, long customerNumber, string ownerPreName, string ownerFirstName, string ownerMiddleName, string ownerLastName, string ownerPostName, string spousePreName, string spouseFirstName, string spouseMiddleName, string spouseLastName, string spousePostName, DateTime effectiveDate, string nameOfBusiness)
			{
				AccountID = accountId;
				CustomerNumber = customerNumber;
				OwnerPreName = ownerPreName;
				OwnerFirstName = ownerFirstName;
				OwnerMiddleName = ownerMiddleName;
				OwnerLastName = ownerLastName;
				OwnerPostName = ownerPostName;

				SpousePreName = spousePreName;
				SpouseFirstName = spouseFirstName;
				SpouseMiddleName = spouseMiddleName;
				SpouseLastName = spouseLastName;
				SpousePostName = spousePostName;

				EffectiveDate = effectiveDate;

				NameOfBusiness = nameOfBusiness;
			}

			#endregion .ctor

			#region Properties
			public long AccountID { get; private set; }
			public long CustomerNumber { get; private set; }
			public string OwnerPreName { get; private set; }
			public string OwnerFirstName { get; private set; }
			public string OwnerMiddleName { get; private set; }
			public string OwnerLastName { get; private set; }
			public string OwnerPostName { get; private set; }
			public string SpousePreName { get; private set; }
			public string SpouseFirstName { get; private set; }
			public string SpouseMiddleName { get; private set; }
			public string SpouseLastName { get; private set; }
			public string SpousePostName { get; private set; }
			public DateTime EffectiveDate { get; private set; }
			public string NameOfBusiness { get; private set; }
			public string PremiseAddress { get; set; }

			#endregion Properties
		}

		#endregion Properties

		[TestMethod]
		public void AMABindDataToIt()
		{
			// ** Init
			const long ACCOUNT_ID = 123456;
			const long CUSTOMER_NUMBER = 9876543;
			const string O_PRE_NAME = "Mr.";
			const string O_FIRST_NAME = "Andres";
			const string O_MIDDLE_NAME = "Efraim";
			const string O_LAST_NAME = "Sosa";
			const string O_POST_NAME = "II";
			const string S_PRE_NAME = "Mrs.";
			const string S_FIRST_NAME = "Sandra";
			const string S_MIDDLE_NAME = "Michelle";
			const string S_LAST_NAME = "Oxley";
			const string S_POST_NAME = "III";
			const string TEMPLATE_NAME = "Nxs_GEN_AMA_F_080514_dv_FORM";
			const string NAME_OF_BUSINESS = "Cimtech Corp";
			DateTime effectiveDate = DateTime.Now;
			var custInfo = new CustInfo(ACCOUNT_ID, CUSTOMER_NUMBER, O_PRE_NAME, O_FIRST_NAME, O_MIDDLE_NAME, O_LAST_NAME, O_POST_NAME, S_PRE_NAME, S_FIRST_NAME, S_MIDDLE_NAME, S_LAST_NAME, S_POST_NAME, effectiveDate, NAME_OF_BUSINESS);
			var amaObject = new PDFs.AMA(custInfo, string.Format(_PDF_AMA_INPUT_PATH, TEMPLATE_NAME), string.Format(_PDF_AMA_OUTPUT_PATH, TEMPLATE_NAME, CUSTOMER_NUMBER, ACCOUNT_ID));

			// ** Bind Data on the PDF
			string message;
			Assert.IsTrue(amaObject.TryBindData(out message), string.Format("The following error was returned: {0}", message));
		}

		[TestMethod]
		public void AMABindDataViaDict()
		{
			// ** Initialize. 
			const long ACCOUNT_ID = 123456;
			const long CUSTOMER_NUMBER = 9876543;
			const string TEMPLATE_NAME = "Nxs_GEN_AMA_F_080514_dv_FORM";
			const int TERM = 4;
			const double TERM_MONTHS = 12 * TERM;
			const double MMR = 89.97;
			var keyPairs = new Dictionary<string, object>
			{
				{"OwnerFirstName", "Mr. Sammy Test"},
				{"OwnerLastName", "Johnson III"},
				{"SpouseFirstName", "Mrs. Cindy Little"},
				{"SpouseLastName", "Test II"},
				{"EffDateMM", "02"},
				{"EffDateDD", "21"},
				{"EffDateYY", "2014"},
				{"NameOfBusiness", "WISE Architects Inc."},
				{"PremiseAddress", "722 E Technology AVE"},
				{"PremiseCity", "OREM"},
				{"PremiseState", "UT"},
				{"PremiseZip", "84097"},
				{"PremiseZip4", "6547"},
				{"BillingAddress", "1773 N Technology Way"},
				{"BillingCity", "OREM"},
				{"BillingState", "UT"},
				{"BillingZip", "84097"},
				{"BillingZip4", "8888"},
				{"SSO4", "3234"},
				{"SSR4", "4568"},
				{"ExtendedServiceOption_No", true},

				// ** ******************************
				{"InitialTerm", "four (4) years"},
				{"InitialTerm1", TERM},
				{"InitialTerm2", TERM_MONTHS},
				{"MMR", MMR},
				{"TotalPayments", string.Format("{0:F2}", TERM_MONTHS * MMR)},

			};
			var amaObject = new PDFs.AMA(keyPairs, string.Format(_PDF_AMA_INPUT_PATH, TEMPLATE_NAME), string.Format(_PDF_AMA_OUTPUT_PATH, TEMPLATE_NAME, CUSTOMER_NUMBER, ACCOUNT_ID));

			// ** Bind Data on the PDF
			string message;
			Assert.IsTrue(amaObject.TryBindData(out message), string.Format("The following error was returned: {0}", message));
		}
	}
}