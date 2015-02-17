namespace SOS.FunctionalServices.Contracts.Models.Reporting
{
	public interface IFnsMsSystemStatusInfo
	{
		bool InService { get; }
		bool OnTest { get; }
	}
}