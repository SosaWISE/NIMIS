using Nancy;
using NXS.DataServices.HumanResource;
using NXS.Lib;
using NXS.Lib.Authentication;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				var qry = ConvertDynamicDictionary(this.Context.Request.Query);
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
