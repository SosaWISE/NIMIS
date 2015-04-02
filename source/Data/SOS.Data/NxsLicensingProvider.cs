using System.Data.SqlClient;
using SubSonic;

// ReSharper disable once CheckNamespace
namespace NXS.Data
{
	public class NxsLicensingProvider : SqlDataProvider
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
