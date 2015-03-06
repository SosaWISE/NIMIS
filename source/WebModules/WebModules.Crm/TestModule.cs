using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModules.Crm
{
	public class TestModule : SecureModule
	{
		public TestModule()
			: base("/test")
		{
			//this.RequiresClaims(new string[] { "Bob" });
			//this.RequiresPermission(applicationID: null, actionID: "asdf");

			Get["/", true] = async (x, ct) =>
			{
				await Task.Delay(10);
				return new Result<string>(value: "value");
				//return Respond(new Result<string>(value: "value"));
			};

			Get["/error", true] = async (x, ct) =>
			{
				await Task.Delay(10);
				throw new Exception("barf");
			};
		}
	}
}
