using SOS.Lib.Util;
using SubSonic;
using System.Collections.Generic;
using AR = SOS.Data.AuthenticationControl.AC_Action;
using ARCollection = SOS.Data.AuthenticationControl.AC_ActionCollection;
using ARController = SOS.Data.AuthenticationControl.AC_ActionController;

namespace SOS.Data.AuthenticationControl
{
	public static class AC_ActionControllerExtensions
	{
		public static ARCollection ForSession(this ARController cntrl, long sessionID)
		{
			return cntrl.LoadCollection(SosAuthControlDataStoredProcedureManager.AC_ActionsForSession(sessionID));
		}
	}
}
