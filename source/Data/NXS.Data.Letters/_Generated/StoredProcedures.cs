


using System;
using System.Data;
using SubSonic;
using SubSonic.Utilities;

namespace NXS.Data.Letters {
	public partial class LettersDataStoredProcedureManager {
		public static StoredProcedure LD_GetInserts(int? LetterID) {
			StoredProcedure sp = new StoredProcedure("custLD_GetInserts" ,DataService.GetInstance("NxsLettersProvider"));
			sp.Command.AddParameter("@LetterID", LetterID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure LD_TemplatesGetActive() {
			StoredProcedure sp = new StoredProcedure("custLD_TemplatesGetActive" ,DataService.GetInstance("NxsLettersProvider"));
			return sp;
		}
	}
}
 