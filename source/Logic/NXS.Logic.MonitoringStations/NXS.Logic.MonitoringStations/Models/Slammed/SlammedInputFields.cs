namespace NXS.Logic.MonitoringStations.Models.Slammed
{
	public class SlammedInputFields
	{
		#region Properties
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string Phone1 { get; set; }
		public string Phone2 { get; set; }
		public string ApplicationName { get; set; }

		public string ProcessNameStr;
		public ProcessNameEnum ProcessName {
			get { return ProcessNameStringToEnum(ProcessNameStr); }
			set { ProcessNameStr = ProcessNameEnumToString(value); } 
		}
		public enum ProcessNameEnum
		{
			CreditCheck,
			AccountCreation,
			SubmitFunding,
			Funding,
			WelcomeCall
		}
		public string DealerNumber { get; set; }
		public string DealerName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int CurrentSiteID { get; set; }
		#endregion Properties

		#region Methods

		private string ProcessNameEnumToString(ProcessNameEnum enumValue)
		{
			switch (enumValue)
			{
				case ProcessNameEnum.AccountCreation:
					return "AccountCreation";

				case ProcessNameEnum.CreditCheck:
					return "CreditCheck";

				case ProcessNameEnum.Funding:
					return "Funding";

				case ProcessNameEnum.SubmitFunding:
					return "SubmitFunding";

				case ProcessNameEnum.WelcomeCall:
					return "WelcomeCall";
			}

			return "CreditCheck";
		}

		private ProcessNameEnum ProcessNameStringToEnum(string value)
		{
			switch(value)
			{
				case "AccountCreation":
					return ProcessNameEnum.AccountCreation;

				case "CreditCheck":
					return ProcessNameEnum.CreditCheck;

				case "Funding":
					return ProcessNameEnum.Funding;

				case "SubmitFunding":
					return ProcessNameEnum.SubmitFunding;

				case "WelcomeCall":
					return ProcessNameEnum.WelcomeCall;
			}

			return ProcessNameEnum.CreditCheck;
		}

		#endregion Methods
	}
}
