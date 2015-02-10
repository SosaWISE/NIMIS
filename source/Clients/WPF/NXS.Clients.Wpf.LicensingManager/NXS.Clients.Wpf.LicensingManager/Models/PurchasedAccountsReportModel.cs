using System;
using NXS.Framework.Wpf.Mvvm;
using NXS.Framework.Wpf.Validation;
using SOS.Data.SosCrm;

namespace NXS.Clients.Wpf.LicensingManager.Models
{
	public class PurchasedAccountsReportModel : ModelBase
	{
		#region Properties
		public ValidatedInput<string> City { get; private set; }
		public ValidatedInput<string> County { get; private set; }
		public ValidatedListInput<string, MC_PoliticalState> State { get; private set; }
		public ValidatedInput<DateTime?> PurchaseStartDate { get; private set; }
		public ValidatedInput<DateTime?> PurchaseEndDate { get; private set; }
		#endregion Properties

		#region Constructors
		public PurchasedAccountsReportModel()
			: base()
		{
			AddInput(City = new ValidatedInput<string>()
			{
				Validator = StringLengthValidator.Create(0, 50)
			});
			AddInput(County = new ValidatedInput<string>()
			{
				Validator = StringLengthValidator.Create(0, 50)
			});
			AddInput(State = new ValidatedListInput<string, MC_PoliticalState>((item, id) => item.StateID.Equals(id))
			{
				List = GlobalCache.States.List,
				SelectedValuePath = MC_PoliticalState.Columns.StateID,
				DisplayMemberPath = MC_PoliticalState.Columns.StateName,
				CanClear = true
			});
			AddInput(PurchaseStartDate = new ValidatedInput<DateTime?>());
			AddInput(PurchaseEndDate = new ValidatedInput<DateTime?>());
			PurchaseStartDate.PropertyChanged +=new System.ComponentModel.PropertyChangedEventHandler(PurchaseDate_PropertyChanged);
			PurchaseEndDate.PropertyChanged +=new System.ComponentModel.PropertyChangedEventHandler(PurchaseDate_PropertyChanged);

			RunValidation();
			Clean();
		}
		#endregion Consturctors

		#region Methods
		void PurchaseDate_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (PurchaseStartDate.Value != null || PurchaseEndDate.Value != null)
			{
				PurchaseStartDate.Validator = MandatoryObjectValidator<DateTime?>.Create();
				PurchaseEndDate.Validator = MandatoryObjectValidator<DateTime?>.Create();
				RunValidation();
			}
			else
			{
				PurchaseStartDate.Validator = null;
				PurchaseEndDate.Validator = null;
				RunValidation();
			}
		}
		#endregion Methods
	}
}
