using System;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using vtortola.WebSockets;
using vtortola.WebSockets.Deflate;

namespace WsModules.Chat
{
	public static class Listener
	{
		public static CancellationTokenSource Start(int port)
		{
			CancellationTokenSource cancellation = new CancellationTokenSource();

			var endpoint = new IPEndPoint(IPAddress.Any, port);
			var server = new WebSocketListener(endpoint, new WebSocketListenerOptions()
			{
				SubProtocols = new[] { "chat" },
			});
			var rfc6455 = new vtortola.WebSockets.Rfc6455.WebSocketFactoryRfc6455(server);
			rfc6455.MessageExtensions.RegisterExtension(new WebSocketDeflateExtension());
			server.Standards.RegisterStandard(rfc6455);
			server.Start();

			//Log("Rx Chat Server started at " + endpoint.ToString());

			var chatSessionObserver = new ChatSessionsObserver(new ChatRoomManager());

			Observable.FromAsync(server.AcceptWebSocketAsync)
				.Select(ws => new ChatSession(ws)
				{
					In = Observable.FromAsync<dynamic>(ws.ReadDynamicAsync)
							.DoWhile(() => ws.IsConnected)
							.Where(msg => msg != null),

					Out = Observer.Create<dynamic>(ws.WriteDynamic)
				})
				.DoWhile(() => server.IsStarted && !cancellation.IsCancellationRequested)
				.Subscribe(chatSessionObserver);

			return cancellation;
		}

		//static void Log(string s)
		//{
		//	Console.WriteLine(s);
		//}
	}
}
