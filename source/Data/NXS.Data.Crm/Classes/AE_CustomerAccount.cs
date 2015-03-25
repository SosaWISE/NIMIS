
namespace NXS.Data.Crm
{
	public partial class AE_CustomerAccount
	{
		[IgnorePropertyAttribute(true)]
		public AE_Customer Customer { get; set; }
		[IgnorePropertyAttribute(true)]
		public MC_Address Address { get; set; }
	}
}
