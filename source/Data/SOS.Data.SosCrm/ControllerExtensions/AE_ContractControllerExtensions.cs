using AR = SOS.Data.SosCrm.AE_Contract;
using ARCollection = SOS.Data.SosCrm.AE_ContractCollection;
using ARController = SOS.Data.SosCrm.AE_ContractController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class AE_ContractControllerExtensions
	{
		public static AR CreateContractTemplate(this ARController oCntlr, AE_ContractTemplate oContTemplate, string sUserId)
		{
			/** Initialize. */
			AR oResult = new AE_Contract
				{
							ContractTemplateId = oContTemplate.ContractTemplateID
							, ContractName = oContTemplate.ContractName
							, ContractLength = oContTemplate.ContractLength
							, MonthlyFee = oContTemplate.MonthlyFee
							, ShortDesc = oContTemplate.ShortDesc
				};
			oResult.Save(sUserId);

			/** Return result. */
			return oResult;
		}
	}
}
