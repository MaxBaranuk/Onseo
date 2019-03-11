using System.Linq;
using System.Threading.Tasks;
using DataModel;

namespace Network
{
    public static class NetworkProvider
    {             
        private static INetwork network;
        private static NetworkConfig config;

        public static void Init(NetworkConfig networkConfig)
        {           
            config = networkConfig;
            network = networkConfig.Network;
        }

        public static Task<ValueOrError<T>> Get<T>() where T : Data
        {
            return network.Get<T>(GetTypeUri(typeof(T).Name));
        }
        
        public static Task<ValueOrError<T>> Get<T>(string id) where T : Data
        {
            return network.Get<T>(GetTypeUri(typeof(T).Name + "/" + id));
        }
 
        private static string GetTypeUri(string type)
        {
            return config.data.First(data => data.dataType.Equals(type)).endpoint;
        }
    }
}