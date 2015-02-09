using AR = SLS.Data.Cms.XN_LeadCreditReportScore;
using ARCollection = SLS.Data.Cms.XN_LeadCreditReportScoreCollection;
using ARController = SLS.Data.Cms.XN_LeadCreditReportScoreController;

namespace SLS.Data.Cms.ControllerExtensions
{
	public static class XN_LeadCreditReportScoreControllerExtentions
	{
		public static AR CreateByList(this ARController oCntlr, QL_CreditReport[] oList, long lLeadId, string szVendorId, int nSeasonID, GetScoreStatusBySeasonID mGetScoreStatusBySeasonID)
		{
			// Locals
			var oResult = new AR { LeadId = lLeadId, CreditReportVendorId = szVendorId, SeasonId = nSeasonID };

			// Get the rest of the bureaus
			foreach (var oReport in oList)
			{
				AssignCorrectScoreAndBureau(oResult, oReport.CreditReportVendorEasyAccess, nSeasonID, mGetScoreStatusBySeasonID);
			}

			// Return result
			return oResult;
		}

		public static void AssignCorrectScoreAndBureau(AR oNdx, ICreditReportVendors oReport, int nSeasonID, GetScoreStatusBySeasonID mGetScoreStatusBySeasonID)
		{
			switch (oReport.BureauId)
			{
				case QL_CreditReportBureau.MetaData.EquifaxID:
					oNdx.EquifaxCrId = oReport.CreditReportID;
					oNdx.EquifaxScore = oReport.Score;
					oNdx.EquifaxStatus = mGetScoreStatusBySeasonID(oReport.Score, nSeasonID);
					break;
				case QL_CreditReportBureau.MetaData.TransUnionID:
					oNdx.TransUnionCrId = oReport.CreditReportID;
					oNdx.TransUnionScore = oReport.Score;
					oNdx.TransUnionStatus = mGetScoreStatusBySeasonID(oReport.Score, nSeasonID);
					break;
				case QL_CreditReportBureau.MetaData.ExperianID:
					oNdx.ExperianCrId = oReport.CreditReportID;
					oNdx.ExperianScore = oReport.Score;
					oNdx.ExperianStatus = mGetScoreStatusBySeasonID(oReport.Score, nSeasonID);
					break;
				case QL_CreditReportBureau.MetaData.ManualCreditID:
					oNdx.ManualCrId = oReport.CreditReportID;
					oNdx.ManualScore = oReport.Score;
					oNdx.ManualStatus = mGetScoreStatusBySeasonID(oReport.Score, nSeasonID);
					break;
			}
		}

		public delegate string GetScoreStatusBySeasonID(short nScore, int nSeasonID);
	}
}
