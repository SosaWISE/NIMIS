using AR = SOS.Data.SosCrm.AE_CustomerMasterFile;

namespace SOS.Data.SosCrm
{
	public partial class AE_CustomerMasterFile
	{
		#region Methods
		public static AR CreateNew(int nDealerId, string szUserId)
		{
			/** Initialize. */
			var oCMF = new AR { DealerId = nDealerId };
			oCMF.Save(szUserId);

			/** Return result. */
			return oCMF;
		}

		#endregion Methods
	}
}
