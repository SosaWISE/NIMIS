using System.Data.SqlClient;
using SubSonic;

namespace SOS.Data
{
	public class NxsConnextProvider : SqlDataProvider
	{
		#region Properties

		private string _databaseName;

		public string DatabaseName
		{
			get
			{
				if (_databaseName == null)
				{
					using (var oConn = new SqlConnection(DefaultConnectionString))
					{
						oConn.Open();
						_databaseName = oConn.Database;
						oConn.Close();
					}
				}

				return _databaseName;
			}
		}

		#endregion Properties
	}
}
