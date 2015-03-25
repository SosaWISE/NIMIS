
namespace NXS.Data.Crm
{
	public partial class TS_Team
	{
		[IgnorePropertyAttribute(true)]
		public MC_Address Address { get; set; }
		[IgnorePropertyAttribute(true)]
		public RU_Team Team { get; set; }
	}
}
