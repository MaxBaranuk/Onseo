using System;
using Network;
using UnityEngine;

namespace SceneObjects
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField]
        private NetworkConfig networkConfig;

        public static Action ApplicationQuit;

        private void Start()
        {
            NetworkProvider.Init(networkConfig);
        }

        private void OnApplicationQuit()
        {
            ApplicationQuit?.Invoke();
        }
    }
}
