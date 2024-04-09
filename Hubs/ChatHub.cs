using Microsoft.AspNetCore.SignalR;
using realtime_chat.DataService;
using realtime_chat.Models;

namespace realtime_chat.Hubs
{
    public class ChatHub: Hub
    {
        private readonly SharedDb _shared;
        
        public ChatHub(SharedDb shared) => _shared = shared;
        public async Task JoinChat(UserConnection connection)
        {
            await Clients.All
                .SendAsync("ReceiveMessage", "admin", $"{connection.Username} a rejoint");
        }

        public async Task JoinSpecificChatRoom(UserConnection connection)
        {
            _shared.connections[Context.ConnectionId] = connection;

            await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);

            await Clients
                .Group(connection.ChatRoom)
                .SendAsync("JoinSpecificChatRoom", "admin", $"{connection.Username} a rejoint {connection.ChatRoom}");
        }

        public async Task SendMessage(string msg)
        {
            if(_shared.connections.TryGetValue(Context.ConnectionId, out UserConnection connection))
            {
                await Clients.Group(connection.ChatRoom)
                    .SendAsync("ReceiveSpecificMessage", connection.Username, msg);
            }
        }
    }
}
