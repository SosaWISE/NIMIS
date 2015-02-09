namespace SOS.Data.SosCrm
{
	public partial class UI_MessageAction
	{

		private UI_MessageActionParameterCollection colUI_MessageActionParameters;
		public UI_MessageActionParameterCollection UI_MessageActionParameters
		{
			get
			{
				if (colUI_MessageActionParameters == null)
					colUI_MessageActionParameters = new UI_MessageActionParameterCollection().Where(UI_MessageActionParameter.Columns.MessageActionID, MessageActionID).Load();
				return colUI_MessageActionParameters;
			}

			set { colUI_MessageActionParameters = value; }

		}
	}
}
