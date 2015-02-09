using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace NXS.Lib.Web.WebSockets
{
	public class Router
	{
		WebSocketServer _server;

		public Router()
		{
			_server = new WebSocketServer(88);
			_server.AddWebSocketService<Laputa>("/laputa", () => new Laputa());
			_server.Start();
		}
	}
	public class Laputa : WebSocketBehavior
	{
		static int _n = 0;
		int count;

		public Laputa()
		{
			this.OriginValidator = OriginValidatorFunc;
			this.CookiesValidator = CookiesValidatorFunc;

			//this.Context.
			//this.Sessions.Sessions.First().ID
			//HashSet<string>
		}
		private bool OriginValidatorFunc(string val)
		{
			// Check the value of the Origin header, and return true if valid.
			//Uri origin;
			//return !val.IsNullOrEmpty() &&
			//	   Uri.TryCreate(val, UriKind.Absolute, out origin) &&
			//	   origin.Host == "example.com";
			return true;
		}
		private bool CookiesValidatorFunc(CookieCollection req, CookieCollection res)
		{
			this.count = ++_n;
			// Check the Cookies in 'req', and set the Cookies to send to the client with 'res'
			// if necessary.
			foreach (Cookie cookie in req)
			{
				cookie.Expired = true;
				res.Add(cookie);
			}

			return true; // If valid.
		}

		protected override void OnMessage(MessageEventArgs e)
		{
			var msg = e.Data == "BALUS"
					  ? "I've been balused already..."
					  : "I'm not available now.";

			Send(msg);
		}
		protected override void OnOpen()
		{
			base.OnOpen();
		}
		protected override void OnClose(CloseEventArgs e)
		{
			base.OnClose(e);
		}
		protected override void OnError(ErrorEventArgs e)
		{
			base.OnError(e);
		}
	}
}
