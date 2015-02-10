using System;
using NXS.Framework.Wpf.Mvvm;

namespace NXS.Clients.Wpf.LicensingManager.Models
{
	public class EmployeeDataModel : ModelBase
	{
		#region Properties
		public ObservableValueContainer<string> GPEmployeeID { get; private set; }
		public ObservableValueContainer<string> FirstName { get; private set; }
		public ObservableValueContainer<string> MiddleInitial { get; private set; }
		public ObservableValueContainer<string> LastName { get; private set; }
		public ObservableValueContainer<bool> Active { get; private set; }
		public ObservableValueContainer<DateTime> InactiveDate { get; private set; }
		public ObservableValueContainer<string> MaritalStatus { get; private set; }
		public ObservableValueContainer<string> SSN { get; private set; }
		public ObservableValueContainer<string> Gender { get; private set; }
		public ObservableValueContainer<string> Department { get; private set; }
		public ObservableValueContainer<DateTime> BirthDate { get; private set; }
		public ObservableValueContainer<DateTime> StartDate { get; private set; }
		public ObservableValueContainer<string> EmploymentType { get; private set; }
		public ObservableValueContainer<string> StreetAddress { get; private set; }
		public ObservableValueContainer<string> City { get; private set; }
		public ObservableValueContainer<string> State { get; private set; }
		public ObservableValueContainer<string> Zip { get; private set; }
		public ObservableValueContainer<string> Phone { get; private set; }
		#endregion Properties

		#region Constructors
		public EmployeeDataModel()
		{
			GPEmployeeID = new ObservableValueContainer<string>();
			FirstName = new ObservableValueContainer<string>();
			MiddleInitial = new ObservableValueContainer<string>();
			LastName = new ObservableValueContainer<string>();
			Active = new ObservableValueContainer<bool>();
			InactiveDate = new ObservableValueContainer<DateTime>();
			MaritalStatus = new ObservableValueContainer<string>();
			SSN = new ObservableValueContainer<string>();
			Gender = new ObservableValueContainer<string>();
			Department = new ObservableValueContainer<string>();
			BirthDate = new ObservableValueContainer<DateTime>();
			StartDate = new ObservableValueContainer<DateTime>();
			EmploymentType = new ObservableValueContainer<string>();
			StreetAddress = new ObservableValueContainer<string>();
			City = new ObservableValueContainer<string>();
			State = new ObservableValueContainer<string>();
			Zip = new ObservableValueContainer<string>();
			Phone = new ObservableValueContainer<string>();
		}
		#endregion Constructors
	}
}
