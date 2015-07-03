using Nancy;
using NXS.DataServices.HumanResource;
using System.Collections.Generic;
using System.Linq;

namespace WebModules.HumanResource
{
	public class ReportsModule : BaseModule
	{
		ReportsService Srv { get { return new ReportsService(/*this.User.GPEmployeeID*/); } }

		public ReportsModule()
			: base("/Hr/Reports")
		{
			this.RequiresPermission((string)null, null);

			Post["/{name}", true] = async (x, ct) =>
			{
				var qry = ConvertDynamicDictionary(Context.Request.Query);
				return await Srv.RunReport((string)x.name, qry);
			};
		}

		private static IDictionary<string, string> ConvertDynamicDictionary(DynamicDictionary dictionary)
		{
			return dictionary.GetDynamicMemberNames().ToDictionary(
					memberName => memberName,
					memberName => (string)dictionary[memberName]);
		}

	}
}
