// ReSharper disable once CheckNamespace
namespace SOS.Data.SosCrm
{
// ReSharper disable once InconsistentNaming
	public partial class UI_Message
	{
		private UI_MessageActionCollection _colUIMessageActions;
		public UI_MessageActionCollection UI_MessageActions
		{
			get
			{
				return _colUIMessageActions ??
					   (_colUIMessageActions =
						new UI_MessageActionCollection().Where(UI_MessageAction.Columns.MessageID, MessageID).Load());
			}

			set { _colUIMessageActions = value; }

		}
	}
}
