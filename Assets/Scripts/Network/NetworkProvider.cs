using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModel;
using SceneObjects;
using Server;

namespace Network
{
    public static class NetworkProvider
    {
        private static INetwork _network;
        private static NetworkConfig _config;

        public static void Init(NetworkConfig networkConfig)
        {           
            _config = networkConfig;
            _network = networkConfig.networkType == NetworkType.LocalNetwork
                ? (INetwork) new LocalNetwork(networkConfig)
                : new Network(networkConfig);
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
            string path;
            switch (type)
            {
                case ResourceType.Food:
                    path = _config.foodEndpoint;
                    break;
                case ResourceType.Gold:
                    path = _config.goldEndpoint;
                    break;
                case ResourceType.Metal:
                    path = _config.metalEndpoint;
                    break;
                case ResourceType.Wood:
                    path = _config.woodEndpoint;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            var uri = $"{_config.resourceEndpoint}/{UiController.Instance.currentUser}/{path}";
            return _network.Get<Resource>(uri);
        }   
        
        public static Task<ValueOrError<Resource>> PostResource(Resource resource)
        {
            var uri = _config.resourceEndpoint;
            return _network.Post(uri, resource);
        }

        public static Task<ValueOrError<List<RankUser>>> GetRanking()
        {
            return _network.Get<List<RankUser>>(_config.rankingEndpoint);
        }
    }
}