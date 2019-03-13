using DataModel;
using Server;
using UnityEngine;

namespace Network
{
    [CreateAssetMenu(fileName = "New Api config", menuName = "Api config", order = 51)]
    public class NetworkConfig : ScriptableObject
    {
        [SerializeField] private NetworkType networkType;    
        [SerializeField] public string foodEndpoint;
        [SerializeField] public string goldEndpoint;
        [SerializeField] public string metalEndpoint;
        [SerializeField] public string woodEndpoint;
        [SerializeField] public string loginEndpoint;
        [SerializeField] public string createUserEndpoint;
        [SerializeField] public string rankingEndpoint;

        public UsersRanking ranking; 
        
        private INetwork network;

        public INetwork Network => network ?? (network = CreateNetwork(networkType, this));

        private static INetwork CreateNetwork(NetworkType type, NetworkConfig config)
        {
            return type == NetworkType.PlayerPrefsNetwork
                ? (INetwork) new MockNetwork(config)
                : new Network();
        }
        
        public string GetResourceTypeUri(ResourceType type)
        {
            string uri;
            switch (type)
            {
                case ResourceType.Food:
                    uri = foodEndpoint;
                    break;
                case ResourceType.Gold:
                    uri = foodEndpoint;
                    break;
                case ResourceType.Metal:
                    uri = foodEndpoint;
                    break;
                case ResourceType.Wood:
                    uri = foodEndpoint;
                    break;
                default:
                    uri = "";
                    break;
            }
            return uri;
        }
    }

    public enum NetworkType
    {
        PlayerPrefsNetwork,
        Network
    }
}