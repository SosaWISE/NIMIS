﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vtortola.WebSockets;

namespace WsModules.Chat
{
    public class ChatSession
    {
        public IObservable<dynamic> In { get; set; }
        public IObserver<dynamic> Out { get; set; }
        public string Nick { get; set; }
        public string Room { get; set; }

        readonly WebSocket _ws;

        public ChatSession(WebSocket ws)
        {
            _ws = ws;
        }
    }

}
