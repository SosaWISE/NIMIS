// ReSharper disable once CheckNamespace
namespace SOS.Data.HumanResource
{
// ReSharper disable once InconsistentNaming
	public partial class ES_Message
	{
		public void SetReady(string username)
		{
			IsReady = true;
			Save(username);
		}
	}
}
