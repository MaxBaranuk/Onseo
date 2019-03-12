using System.Threading.Tasks;
using Network;
using SceneObjects;
using UnityEngine;

namespace Server
{
    public class LocalServer
    {
        private NetworkConfig config;
        private bool isRunning;
        public LocalServer(NetworkConfig config)
        {
            this.config = config;
            SceneController.ApplicationQuit += StopServer;
            StartServer();
        }
        
        public async Task<ValueOrError<string>> Post(string uri, string data)
        {
            await Task.Delay(500);
            PlayerPrefs.SetString(uri, data);
            PlayerPrefs.Save();
            return ValueOrError<string>.CreateFromValue(data);
        }
        
        public async Task<ValueOrError<string>> Get(string uri)
        {
            await Task.Delay(500);
            var segments = uri.Split('/');
            
            var api = segments[0];
            var id = segments[1];

            if (api.Equals(config.foodEndpoint))
                return ValueOrError<string>.CreateFromValue(DataBaseProvider.GetFood(id));
            if (api.Equals(config.goldEndpoint))
                return ValueOrError<string>.CreateFromValue(DataBaseProvider.GetGold(id));
            if (api.Equals(config.woodEndpoint))
                return ValueOrError<string>.CreateFromValue(DataBaseProvider.GetWood(id));
            if (api.Equals(config.metalEndpoint))
                return ValueOrError<string>.CreateFromValue(DataBaseProvider.GetMetal(id));

            return ValueOrError<string>.CreateFromError("Invalid request");

        } 

        private async void StartServer()
        {
            isRunning = true;

            while (isRunning)
            {
                await Task.Delay(5000);
                GenerateResources();
            }
        }

        private void GenerateResources()
        {
//            var foodJson = Get(config.foodEndpoint);
//            var food = JsonUtility.FromJson<Food>(foodJson);
        }
        
        private void StopServer()
        {
            isRunning = false;
        }
    }
}