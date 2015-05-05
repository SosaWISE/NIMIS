using System;

namespace NXS.Lib.Web.Authentication
{
	public delegate ActionRequest ActionRequestFunc(string actionKey, string gpEmployeeId);

	public class ActionRequestStore
	{
		ActionRequestFunc _use;

		public ActionRequestStore(ActionRequestFunc use)
		{
			_use = use;
		}

		public bool Access(byte[] actionNum, string gpEmployeeId, out ActionRequest ar)
		{
			ar = _use(SystemUserIdentity.AuthNumToKey(actionNum), gpEmployeeId);
			return true;
		}
	}
}
