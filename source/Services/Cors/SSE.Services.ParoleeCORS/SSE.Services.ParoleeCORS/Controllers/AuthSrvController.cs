using System.Web.Mvc;

namespace SSE.Services.ParoleeCORS.Controllers
{
	public class AuthSrvController : Controller
	{
		#region Athentication

		public bool Authenticate(string username, string password, long sessionId, bool rememberMe)
		{

			/** Return result. */
			return true;
		}

		#endregion Athentication


	}
}