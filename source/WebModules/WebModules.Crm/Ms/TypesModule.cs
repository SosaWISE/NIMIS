using NXS.DataServices.Crm;
using AuthApplications = NXS.Data.AuthenticationControl.AC_Application.MetaData;
using AuthActions = NXS.Data.AuthenticationControl.AC_Action.MetaData;

namespace WebModules.Crm.ContractAdmin
{
	public class TypesModule : BaseModule
	{
		TypesService Srv { get { return new TypesService(); } }

		public TypesModule()
			: base("/Ms/Types")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/FriendsAndFamily", true] = async (x, ct) =>
			{
				return await Srv.FriendsAndFamilyTypesAsync().ConfigureAwait(false);
			};
			Get["/AccountCancelReasons", true] = async (x, ct) =>
			{
				return await Srv.AccountCancelReasonsAsync().ConfigureAwait(false);
			};
		}
	}
}
