namespace SSE.Services.CmsCORS.Models.Swing
{
	public class SwingParam:JsonParamBase
	{
		public long InterimAccountId { get; set; }
		public string CustomerType { get; set; }

        public bool SwingEquipment { get; set; }
	}
}