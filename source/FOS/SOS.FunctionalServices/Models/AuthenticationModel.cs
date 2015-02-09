/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/17/11
 * Time: 11:48
 * 
 * Description:  Entity used to acquired authentication information.
 *********************************************************************************************************************/

using System;
using System.Runtime.Serialization;
using SOS.Data.AuthenticationControl;

namespace SOS.FunctionalServices.Models
{
	[DataContract]
	public class AuthenticationModel : FnsAcSessionModel
	{
		#region .ctor

		public AuthenticationModel(AC_Authentication oAuth) : base(oAuth.Session)
		{
			AuthenticationID = oAuth.AuthenticationID;
			UserId = oAuth.UserId;
			Username = oAuth.Username;
			Password = "XXXXXXX";
			CreatedDate = oAuth.CreatedOn;
		}

		#endregion .ctor

		#region Properties

		protected long AuthenticationID { get; private set; }

		protected new int? UserId { get; private set; }

		protected string Username { get; private set; }

		protected string Password { get; set; }

		protected DateTime CreatedDate { get; set; }

		#endregion Properties
	}
}
