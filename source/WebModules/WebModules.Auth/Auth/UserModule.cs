using Nancy.Authentication.Token;
using NXS.Lib;
using NXS.Lib.Authentication;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModules.Auth.Auth
{
#if DEBUG
	public class SessData
	{
		public string Token { get; set; }
		public string Username { get; set; }
		public byte[] AuthNum { get; set; }
		public string AuthKey { get; set; }
	}
#endif
	public class Credentials
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
	public class AuthResult
	{
		public string Token { get; set; }
		public UserModel User { get; set; }
	}

	public class UserModule : BaseModule
	{
		public UserModule(
			TokenAuthenticationConfiguration _tokenConfig
			)
			: base("/Auth/User")
		{
#if DEBUG
			Get["/SessionData"] = (x) =>
			{
				var result = new Result<SessData>();

				string token, username;
				AuthInformation authInfo;
				if (!GetSessionData(_tokenConfig.Tokenizer, out token, out username, out authInfo))
					return result;

				result.Value = new SessData
				{
					Token = token,
					Username = username,
					AuthNum = authInfo.AuthNum,
					AuthKey = authInfo.AuthNum == null ? null : SystemUserIdentity.AuthNumToKey(authInfo.AuthNum),
				};
				return result;
			};
#endif

			Post["/SessionStart"] = (x) =>
			{
				var result = new Result<UserModel>();
				var authService = BaseModule.AuthService;

				var identity = this.User;
				if (identity != null)
					result.Value = authService.ToUserModel(identity);
				//else //@NOTE: currently anonymous users don't have a session
				return result;
			};

			Post["/SignIn"] = (x) =>
			{
				var result = new Result<AuthResult>();
				var _authService = BaseModule.AuthService;

				var credentials = this.BindBody<Credentials>();
				if (credentials == null)
				{
					result.Code = -1;
					result.Message = "Invalid credentials";
					return result;
				}
				var authResult = _authService.Authenticate(credentials.Username, credentials.Password, this.Request.UserHostAddress);
				if (authResult.Success)
				{
					var identity = authResult.Value;
					result.Value = new AuthResult() { User = _authService.ToUserModel(identity), };
					result.Value.Token = _tokenConfig.Tokenizer.Tokenize(identity.ToTokenIdentity(), this.Context);
				}
				else
				{
					result.Code = authResult.Code;
					result.Message = authResult.Message;
				}
				return result;
			};

			Post["/SignOut"] = (x) =>
			{
				var result = new Result<bool>();
				var _authService = BaseModule.AuthService;
				string token, username;
				AuthInformation authInfo;
				if (!GetSessionData(_tokenConfig.Tokenizer, out token, out username, out authInfo))
					return result;
				if (authInfo.AuthType == SystemUserIdentity.AuthTypes.Session)
				{
					_authService.EndSession(authInfo.AuthNum);
					result.Value = true;
				}
				return result;
			};
		}

		private bool GetSessionData(ITokenizer tokenizer, out string token, out string username, out AuthInformation authInfo)
		{
			token = this.Request.ExtractTokenFromHeader();
			if (token == null)
			{
				username = null;
				authInfo = null;
				return false;
			}

			var sessionNumResolver = new SessionDataIdentityResolver();
			tokenizer.Detokenize(token, this.Context, userIdentityResolver: sessionNumResolver);
			username = sessionNumResolver.UserName;
			authInfo = sessionNumResolver.AuthInfo;
			return authInfo != null;
		}
	}
}
