namespace NXS.Logic.MonitoringStations.Models
{
	public class CreditRequest : Base
	{
		#region .ctor
		#endregion .ctor

		#region Properties

		public string CS { get; set; }

		public string SSN { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string StreetNumber { get; set; }

		public string StreetName { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string Zip { get; set; }

		public string FICO { get; set; }

		public string TransactionID { get; set; }

		public string Token { get; set; }

		public string DealerId { get; set; }

		public string UserId { get; set; }

		public string RequestDate { get; set; }

		#endregion Properties

		#region Methods

		public string Serialize()
		{
			return Serialize<CreditRequest>();
		}
		#endregion Methods
	}
}
