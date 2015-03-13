namespace SOS.Lib.Core.CreditReportService
{
	public static class CreditHelper
	{
		public static CreditScoreGroup GetCreditScoreGroup(int score, bool reportFound,
			int excellentCreditScore, int passCreditScore, int subCreditScore)
		{
			var result = CreditScoreGroup.NotFound;
			if (reportFound)
			{
				if (score >= excellentCreditScore)
				{
					result = CreditScoreGroup.Excellent;
				}
				else if (score >= passCreditScore)
				{
					result = CreditScoreGroup.Good;
				}
				else if (score >= subCreditScore)
				{
					result = CreditScoreGroup.Sub;
				}
				else
				{
					result = CreditScoreGroup.Poor;
				}
			}
			return result;
		}
	}
}
