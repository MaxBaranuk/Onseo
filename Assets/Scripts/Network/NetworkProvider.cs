using System.Collections.Generic;
using System.Threading.Tasks;
using DataModel;

namespace Network
{
    public static class NetworkProvider
    {
        private static INetwork _network;
        private static NetworkConfig _config;

        public static void Init(NetworkConfig networkConfig)
        {           
            _config = networkConfig;
            _network = networkConfig.Network;
        }

        public static Task<ValueOrError<Credentials>> Login(Credentials credentials)
        {
            return _network.Post(_config.loginEndpoint, credentials);
        }
        
        public static Task<ValueOrError<Credentials>> CreateUser(Credentials credentials)
        {
            return _network.Post(_config.createUserEndpoint, credentials);
        }

        public static Task<ValueOrError<Resource>> GetResource(ResourceType type)
        {
            var uri = _config.GetResourceTypeUri(type);
            return _network.Get<Resource>(uri);
        }   
        
        public static Task<ValueOrError<Resource>> PostResource(Resource resource)
        {
            var uri = _config.GetResourceTypeUri(resource.Type);
            return _network.Post(uri, resource);
        }

        public static Task<ValueOrError<List<(string, float)>>> GetRanking()
        {
            return _network.Get<List<(string, float)>>(_config.rankingEndpoint);
        }
    }
}