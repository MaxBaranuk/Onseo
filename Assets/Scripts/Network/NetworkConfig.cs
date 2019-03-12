using System;
using DataModel;
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

        private INetwork network;

        public INetwork Network => network ?? (network = CreateNetwork(networkType, this));

        private static INetwork CreateNetwork(NetworkType type, NetworkConfig config)
        {
            return type == NetworkType.PlayerPrefsNetwork
                ? (INetwork) new MockNetwork(config)
                : new Network();
        }
        
        public string GetTypeUri(Type type)
        {
            if (type == typeof(Food))
                return foodEndpoint;           
            if (type == typeof(Gold))
                return goldEndpoint;  
            if (type == typeof(Metal))
                return metalEndpoint;          
            if (type == typeof(Wood))
                return woodEndpoint;            
            if (type == typeof(Credentials))
                return loginEndpoint;
            
            return "";
        }
        
        public Type GetUriType(string uri)
        {
            if (uri == foodEndpoint)
                return typeof(Food);           
            if (uri == goldEndpoint)
                return typeof(Gold);  
            if (uri == metalEndpoint)
                return typeof(Metal);          
            if (uri == woodEndpoint )
                return typeof(Wood);            
            if (uri == loginEndpoint)
                return typeof(Credentials);

            return null;
        }
    }

    public enum NetworkType
    {
        PlayerPrefsNetwork,
        Network
    }
}