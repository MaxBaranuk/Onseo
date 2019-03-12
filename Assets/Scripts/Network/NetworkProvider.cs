using System.Threading.Tasks;
using DataModel;

namespace Network
{
    public static class NetworkProvider
    {
//        public static NetworkConfig Config => config;
        private static INetwork network;
        private static NetworkConfig config;

        public static void Init(NetworkConfig networkConfig)
        {           
            config = networkConfig;
            network = networkConfig.Network;
        }
        
        public static Task<ValueOrError<T>> Get<T>(string id) where T : Dto
        {
            return network.Get<T>(config.GetTypeUri(typeof(T)) + "/" + id);
        }     
    }
}