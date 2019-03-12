using System.Threading.Tasks;
using Network;

namespace Server
{
    public static class ServerProvider
    {
        private static LocalServer _server;
        public static void StartMockServer(NetworkConfig config)
        {
            _server = new LocalServer(config);
        }

        public static Task<ValueOrError<string>> ServerRequest(string uri, RequestType type, string data = null)
        { 
            return type == RequestType.Post
                ? _server.Post(uri, data)
                : _server.Get(uri);
        }  
    }

    public enum RequestType
    {
        Post, Get
    }
}