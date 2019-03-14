using Server;
using UnityEngine;

namespace Network
{
    [CreateAssetMenu(fileName = "New Api config", menuName = "Api config", order = 51)]
    public class NetworkConfig : ScriptableObject
    {
        [SerializeField] public NetworkType networkType; 
        [SerializeField] public string usersEndpoint;
        [SerializeField] public string loginEndpoint;
        [SerializeField] public string createUserEndpoint;
        [SerializeField] public string rankingEndpoint;
        [SerializeField] public string resourceEndpoint;
        
        
        [SerializeField] public string foodEndpoint;
        [SerializeField] public string goldEndpoint;
        [SerializeField] public string metalEndpoint;
        [SerializeField] public string woodEndpoint;

        public UsersRanking ranking;       
    }

    public enum NetworkType
    {
        LocalNetwork,
        Network
    }
}