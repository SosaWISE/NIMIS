using NXS.DataServices.Crm;
using SOS.Lib.Util;
using System;

namespace WebModules.Crm.ContractAdmin
{
	public class EnumTypesModule : BaseModule
	{
		EnumTypesService Srv { get { return new EnumTypesService(); } }

		public EnumTypesModule()
			: base("/Ms/Types")
		{
			this.RequiresPermission(applicationID: null, actionID: null);

			Get["/FriendsAndFamily", true] = async (x, ct) =>
			{
				return await Srv.FriendsAndFamilyTypes().ConfigureAwait(false);
			};
		}
	}
}
