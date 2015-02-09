using AR = SOS.Data.SosCrm.MC_AccountNoteCat2;
using ARCollection = SOS.Data.SosCrm.MC_AccountNoteCat2Collection;
using ARController = SOS.Data.SosCrm.MC_AccountNoteCat2Controller;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MC_AccountNoteCat2ControllerExtensions
	{
		public static ARCollection GetByCategory1Id(this ARController cntlr, int cat1Id)
		{
			var q = AR.Query().WHERE(AR.Columns.NoteCategory1Id, cat1Id);
			return cntlr.LoadCollection(q);
		}
	}
}
