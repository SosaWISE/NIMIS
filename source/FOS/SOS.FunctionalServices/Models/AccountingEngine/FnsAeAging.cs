using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;

namespace SOS.FunctionalServices.Models.AccountingEngine
{
	public class FnsAeAging : IFnsAeAging
	{
		#region .ctor

		public FnsAeAging(AE_AgingView ageItem)
		{
			CustomerMasterFileID = ageItem.CustomerMasterFileID;
			AgingStepID = ageItem.AgingStepID;
			AgingStep = ageItem.AgingStep;
			Value = ageItem.ValueDue;
			StepOrder = ageItem.StepOrder;
		}

		#endregion .ctor

		#region Properties
		public long CustomerMasterFileID { get; private set; }
		public string AgingStepID { get; private set; }
		public string AgingStep { get; private set; }
		public decimal Value { get; private set; }
		public short StepOrder { get; private set; }
		#endregion Properties
	}
}
