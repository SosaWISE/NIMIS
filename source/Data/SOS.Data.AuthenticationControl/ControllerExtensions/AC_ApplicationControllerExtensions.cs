using SOS.Lib.Util;
using SubSonic;
using System.Collections.Generic;
using AR = SOS.Data.AuthenticationControl.AC_Application;
using ARCollection = SOS.Data.AuthenticationControl.AC_ApplicationCollection;
using ARController = SOS.Data.AuthenticationControl.AC_ApplicationController;

namespace SOS.Data.AuthenticationControl
{
	public static class AC_ApplicationControllerExtensions
	{
		public static ARCollection ForSession(this ARController cntrl, long sessionID)
		{
			return cntrl.LoadCollection(SosAuthControlDataStoredProcedureManager.AC_ApplicationsForSession(sessionID));
		}
	}
}
