using System;
using NXS.Framework.Wpf.Mvvm;

namespace NXS.Clients.Wpf.LicensingManager.Models
{
	public class PurchasedAccountsModel : ModelBase
	{
		#region Properties
		public ObservableValueContainer<int> AccountID { get; private set; }
		public ObservableValueContainer<string> CustomerName { get; private set; }
		public ObservableValueContainer<string> StreetAddress { get; private set; }
		public ObservableValueContainer<string> City { get; private set; }
		public ObservableValueContainer<string> State { get; private set; }
		public ObservableValueContainer<string> Zip { get; private set; }
		public ObservableValueContainer<string> County { get; private set; }
		public ObservableValueContainer<string> PremisePhone { get; private set; }
		public ObservableValueContainer<string> HomePhone { get; private set; }
		public ObservableValueContainer<string> WorkPhone { get; private set; }
		public ObservableValueContainer<string> WorkPhoneExt { get; private set; }
		public ObservableValueContainer<string> CellPhone { get; private set; }
		public ObservableValueContainer<string> Purchaser { get; private set; }
		public ObservableValueContainer<DateTime> PurchaseDate { get; private set; }
		#endregion Properties

		#region Constructors
		public PurchasedAccountsModel()
		{
			AccountID = new ObservableValueContainer<int>();
			CustomerName = new ObservableValueContainer<string>();
			StreetAddress = new ObservableValueContainer<string>();
			City = new ObservableValueContainer<string>();
			State = new ObservableValueContainer<string>();
			Zip = new ObservableValueContainer<string>();
			County = new ObservableValueContainer<string>();
			PremisePhone = new ObservableValueContainer<string>();
			HomePhone = new ObservableValueContainer<string>();
			WorkPhone = new ObservableValueContainer<string> ();
			WorkPhoneExt = new ObservableValueContainer<string>();
			CellPhone = new ObservableValueContainer<string>();
			Purchaser = new ObservableValueContainer<string>();
			PurchaseDate = new ObservableValueContainer<DateTime>();
		}
		#endregion Constructors
	}
}
