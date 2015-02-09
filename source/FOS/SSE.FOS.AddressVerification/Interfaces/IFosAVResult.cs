using SSE.Lib.Interfaces.FOS;

namespace SSE.FOS.AddressVerification.Interfaces
{
	public interface IFosAVResult<T> : IFosResult<T>
	{
		#region Properties
		int RemainingHits { get; }
		#endregion Properties
	}
}