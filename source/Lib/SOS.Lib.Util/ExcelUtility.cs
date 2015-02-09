using System.Data;
using System.Data.OleDb;

namespace SOS.Lib.Util
{
	public class ExcelUtility
	{
		private OleDbConnection _oleConn;

		public ExcelUtility(string file)
		{
			File = file;
		}

		private OleDbConnection oleConn
		{
			get
			{
				if (_oleConn == null)
				{
					string excelConnectionString =
						string.Format(
							@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;""",
							File);
					_oleConn = new OleDbConnection(excelConnectionString);
					_oleConn.Open();
				}
				return _oleConn;
			}
		}

		public string File { get; set; }

		public DataTable SelectData(string sqlSelect)
		{
			var oCmd = new OleDbCommand(sqlSelect, oleConn);
			var Dataadapt = new OleDbDataAdapter();
			var oDs = new DataSet();
			Dataadapt.SelectCommand = oCmd;
			Dataadapt.Fill(oDs);
			DataTable oDt = oDs.Tables[0];

			return oDt;
		}
	}
}