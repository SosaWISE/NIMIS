using NXS.Framework.Wpf.Mvvm;

namespace NXS.Clients.Wpf.LicensingManager.Models
{
	public class GeneralModel : ModelBase
	{
		#region Properties
		public ObservableValueContainer<int> ID { get; private set; }
		public ObservableValueContainer<string> ItemName { get; private set; }
		#endregion Properties

		#region Constructors
		public GeneralModel(int id, string itemName)
		{
			ID = new ObservableValueContainer<int>();
			ItemName = new ObservableValueContainer<string>();
			ID.SetValue(id);
			ItemName.SetValue(itemName);
		}
		#endregion Constructors
	}
}
