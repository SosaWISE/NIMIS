using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsAGResponseBase
	{
		int ErrorTypeNum { get; }
		bool SqlConnectionLost { get; }
		bool Success { get; }
		string UserErrorMessage { get; }
	}

	public interface IFnsAGResponseSignalBase
	{
		ExtensionDataObject ExtensionData { get; }
		int ErrorNum { get; }
		string ErrorMessage { get; }
	}
}