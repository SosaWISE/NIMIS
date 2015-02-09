namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	public class AeAging : IAeAging
	{
		#region Properties
		public long CMFID { get; set; }
		public string Age { get; set; }
		public decimal Value { get; set; }
		#endregion Properties
	}

	public interface IAeAging
	{
		long CMFID { get; set; }
		string Age { get; set; }
		decimal Value { get; set; }
	}
}
