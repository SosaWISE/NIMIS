using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WsModules.Chat
{
    public class ChatRoom : List<ChatSession>
    {
        public string Name { get; private set; }

        public ChatRoom(string name)
        {
            Name = name;
        }
    }

    public class ChatRoomManager : ConcurrentDictionary<string, ChatRoom>
    {
        public void RemoveFromRoom(ChatSession session)
        {
            if (string.IsNullOrWhiteSpace(session.Room))
                return;

            ChatRoom room;
            if (TryGetValue(session.Room, out room))
            {
                room.Remove(session);
                if (!room.Any() && TryRemove(session.Room, out room))
                    Console.WriteLine("Room " + session.Room + " removed because it was empty.");
            }
        }
    }
}
