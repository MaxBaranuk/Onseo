using System.Threading.Tasks;
using Network;

namespace Server
{
    public static class ServerProvider
    {
        public static void StartMockServer()
        {
            
        }

        public static async Task<ValueOrError<string>> ServerRequest(RequestType type, string data = null)
        {
            await Task.Delay(1000);
            return ValueOrError<string>.CreateFromValue("");
        }
    }

    public enum RequestType
    {
        Post, Get
    }
}