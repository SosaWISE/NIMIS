using NXS.DataServices.AuthenticationControl;
using NXS.DataServices.AuthenticationControl.Models;
using AuthApplications = NXS.Data.AuthenticationControl.AC_Application.MetaData;
using AuthActions = NXS.Data.AuthenticationControl.AC_Action.MetaData;
using Nancy.Authentication.Token;
using NXS.Lib.Web;

namespace WebModules.AuthControl
{
	public class TypesModule : BaseModule
	{
		TypesService Srv { get { return new TypesService(); } }

		public TypesModule()
			: base("/AC/Types")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/RequestReasons", true] = async (x, ct) =>
			{
				return await Srv.RequestReasons().ConfigureAwait(false);
			};
		}
	}

}
