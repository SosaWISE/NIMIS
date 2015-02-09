using System;
using AR = SOS.Data.SosCrm.UI_Message;
using ARCollection = SOS.Data.SosCrm.UI_MessageCollection;
using ARController = SOS.Data.SosCrm.UI_MessageController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class UI_MessageControllerExtensions
	{
		public static UI_MessageCollection GetMessagesByRecipient(this ARController oCntlr, string recipientID, bool includeRead, bool includeDeleted)
		{
			var oQry = AR.Query().WHERE(AR.Columns.RecipeintID, recipientID);
			if (!includeRead)
			{
				oQry = oQry.AND(AR.Columns.ReadOn, SubSonic.Comparison.Is, DBNull.Value);
			}
			if (!includeDeleted)
			{
				oQry = oQry.AND(AR.Columns.IsDeleted, false);
			}
			oQry = oQry.ORDER_BY(AR.Columns.CreatedOn, "DESC");

			var oResult = oCntlr.LoadCollection(oQry);
			return oResult;
		}
	}
}
