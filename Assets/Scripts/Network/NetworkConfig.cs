using DataModel;
using UnityEngine;
using UnityEngine.Serialization;

namespace Network
{
    [CreateAssetMenu(fileName = "New Api config", menuName = "Api config", order = 51)]
    public class NetworkConfig : ScriptableObject
    {
        [SerializeField] private NetworkType networkType;
        [FormerlySerializedAs("Data")] [SerializeField]
        public Data [] data;

        private INetwork network;

        public INetwork Network => network ?? (network = CreateNetwork(networkType));

        private static INetwork CreateNetwork(NetworkType type)
        {
            return type == NetworkType.PlayerPrefsNetwork
                ? (INetwork) new MockNetwork()
                : new Network();
        }
    }

    public enum NetworkType
    {
        PlayerPrefsNetwork,
        Network
    }
}