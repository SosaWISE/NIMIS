
namespace SOS.Data.SosCrm
{
	public partial class MC_AccountNoteCat1
	{
		public static class LeadAccess
		{
			public static int ID { get { return 1; } }

			public static int AccessViaSearch { get { return 1; } }
			public static int AccessViaLink { get { return 2; } }
			public static int LeadCreated { get { return 5; } }
		}

		public static class LeadGeneration
		{
			public static int ID { get { return 6; } }

			/// <summary>
			/// Lead Generated Via Corp Site Brochure Download
			/// </summary>
			public static int LeadGeneratedViaCorpSiteBrochureDownload { get { return 10; } }
			/// <summary>
			/// Lead Generated Via Corp Site Information Request
			/// </summary>
			public static int LeadGeneratedViaCorpSiteInfomrationRequest { get { return 11; } }
		}

		public static int GetCat2IdFromSourceId(int nSourceId)
		{
			switch (nSourceId)
			{
				case 10: // Internet Brochure Request
					return LeadGeneration.LeadGeneratedViaCorpSiteBrochureDownload;
				case 11: // Internet Info Request
				default:
					return LeadGeneration.LeadGeneratedViaCorpSiteInfomrationRequest;  // 

			}
		}
	}
}
