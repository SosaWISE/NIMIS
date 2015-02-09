//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WampSharp.V2;
//using WampSharp.V2.Realm;
//using WampSharp.V2.Rpc;

//namespace NXS.Lib.Web.Wamp
//{
//	public interface IArgumentsService
//	{
//		[WampProcedure("com.arguments.ping")]
//		void Ping();

//		[WampProcedure("com.arguments.add2")]
//		int Add2(int a, int b);

//		[WampProcedure("com.arguments.stars")]
//		string Stars(string nick = "somebody", int stars = 0);
//	}

//	public class ArgumentsService : IArgumentsService
//	{
//		public void Ping()
//		{
//		}

//		public int Add2(int a, int b)
//		{
//			return a + b;
//		}

//		public string Stars(string nick = "somebody", int stars = 0)
//		{
//			return string.Format("{0} starred {1}x", nick, stars);
//		}
//	}

//	internal class TestProgram
//	{
//		public void Run()
//		{
//			const string location = "ws://127.0.0.1:8080/";

//			var container = new WampRealmContainer();
//			using (IWampHost host = new WampHost(container))
//			{
//				IArgumentsService instance = new ArgumentsService();

//				IWampHostedRealm realm = host.RealmContainer.GetRealmByName("realm1");

//				Task registrationTask = realm.Services.RegisterCallee(instance);
//				// await registrationTask;
//				registrationTask.Wait();

//				host.Open();

//				Console.WriteLine("Server is running on " + location);
//				Console.ReadLine();
//			}
//		}
//	}

//	public class Router
//	{
//		//WebSocketServer _server;

//		public Router()
//		{
//			//_server = new WebSocketServer(88);
//			//_server.AddWebSocketService<Laputa>("/laputa", () => new Laputa2());
//			//_server.Start();
//		}
//	}
//	//public class Laputa2 : WebSocketBehavior
//	//{
//	//	static int _n = 0;
//	//	int count;

//	//	public Laputa2()
//	//	{
//	//		this.OriginValidator = OriginValidatorFunc;
//	//		this.CookiesValidator = CookiesValidatorFunc;

//	//		//this.Context.
//	//		//this.Sessions.Sessions.First().ID
//	//		//HashSet<string>
//	//	}
//	//	private bool OriginValidatorFunc(string val)
//	//	{
//	//		// Check the value of the Origin header, and return true if valid.
//	//		//Uri origin;
//	//		//return !val.IsNullOrEmpty() &&
//	//		//	   Uri.TryCreate(val, UriKind.Absolute, out origin) &&
//	//		//	   origin.Host == "example.com";
//	//		return true;
//	//	}
//	//	private bool CookiesValidatorFunc(CookieCollection req, CookieCollection res)
//	//	{
//	//		this.count = ++_n;
//	//		// Check the Cookies in 'req', and set the Cookies to send to the client with 'res'
//	//		// if necessary.
//	//		foreach (Cookie cookie in req)
//	//		{
//	//			cookie.Expired = true;
//	//			res.Add(cookie);
//	//		}

//	//		return true; // If valid.
//	//	}

//	//	protected override void OnMessage(MessageEventArgs e)
//	//	{
//	//		var msg = e.Data == "BALUS"
//	//				  ? "I've been balused already..."
//	//				  : "I'm not available now.";

//	//		Send(msg);
//	//	}
//	//	protected override void OnOpen()
//	//	{
//	//		base.OnOpen();
//	//	}
//	//	protected override void OnClose(CloseEventArgs e)
//	//	{
//	//		base.OnClose(e);
//	//	}
//	//	protected override void OnError(ErrorEventArgs e)
//	//	{
//	//		base.OnError(e);
//	//	}
//	//}
//}
