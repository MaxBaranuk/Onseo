using Network;
using UnityEngine;

namespace SceneObjects
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField]
        private NetworkConfig networkConfig;

        void Start()
        {
            NetworkProvider.Init(networkConfig);
        }
    }
}
