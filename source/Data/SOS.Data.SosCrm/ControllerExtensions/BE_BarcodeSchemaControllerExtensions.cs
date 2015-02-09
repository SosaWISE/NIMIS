﻿using SubSonic;
using AR = SOS.Data.SosCrm.BE_BarcodeSchema;
using ARCollection = SOS.Data.SosCrm.BE_BarcodeSchemaCollection;
using ARController = SOS.Data.SosCrm.BE_BarcodeSchemaController;

namespace SOS.Data.SorCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class BE_BarcodeSchemaControllerExtensions
	{
		public static AR GetByDocType(this ARController controller, int docTypeID, bool isAutoGenerated)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.DocTypeId, docTypeID)
				.AND(AR.Columns.IsAutogenerated, isAutoGenerated);

			return controller.LoadSingle(qry);
		}
	}
}
