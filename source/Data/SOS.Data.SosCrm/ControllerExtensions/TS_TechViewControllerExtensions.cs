using SOS.Lib.Util;
using SubSonic;
using System;
using System.Collections.Generic;
using AR = SOS.Data.SosCrm.TS_TechView;
using ARCollection = SOS.Data.SosCrm.TS_TechViewCollection;
using ARController = SOS.Data.SosCrm.TS_TechViewController;

namespace SOS.Data.SosCrm
{
	public static class TS_TechViewControllerExtensions
	{
		public static ARCollection LoadAll(this ARController cntlr)
		{
			return cntlr.LoadCollection(AR.Query());
		}
		public static AR LoadByPrimaryKey(this ARController cntlr, long id)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.ID, id);
			return cntlr.LoadSingle(qry);
		}

		public static AR ByRecruitId(this ARController cntlr, int recruitId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.RecruitId, recruitId);
			return cntlr.LoadSingle(qry);
		}
	}
}
