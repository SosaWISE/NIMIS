using SOS.Lib.Util;
using SubSonic;
using System;
using System.Collections.Generic;
using AR = SOS.Data.SosCrm.TS_TechSkills_Map;
using ARCollection = SOS.Data.SosCrm.TS_TechSkills_MapCollection;
using ARController = SOS.Data.SosCrm.TS_TechSkills_MapController;

namespace SOS.Data.SosCrm
{
	public static class TS_TechSkills_MapControllerExtensions
	{
		public static int DeleteAllForTech(this ARController cntlr, long techId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.TechId, techId);
			return DataService.ExecuteQuery(qry.BuildDeleteCommand());
		}
	}
}
