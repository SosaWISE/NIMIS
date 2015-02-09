namespace SSE.FOS.AddressVerification.Interfaces
{
	public interface IVendor
	{
		#region Methods

		IFosAVResult<IFosAddressVerified> VerifyAddress(IFosQlAddress address, int nAreaCode, int dealerId, int seasonId, int teamLocationId, string salesRepId, string userId);

		#endregion Methods
	}
}