using realtime_chat.Models;
using System.Collections.Concurrent;

namespace realtime_chat.DataService
{
    public class SharedDb
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new(); 

        public ConcurrentDictionary<string, UserConnection> connections => _connections;
    }
}
