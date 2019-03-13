using System.Threading.Tasks;
using DataModel;
using Network;
using SceneObjects;
using UnityEngine;

namespace Server
{
    public class LocalServer
    {
        private readonly NetworkConfig config;
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
            return RequestPostExecutor(uri, data);
        }
        
        public async Task<ValueOrError<string>> Get(string uri)
        {
            await Task.Delay(500);
            var segments = uri.Split('/');
            
            var api = segments[0];
            var id = segments[1];

            return RequestGetExecutor(api, id);
        } 

        private async void StartServer()
        {
            isRunning = true;

            while (isRunning)
            {
                await Task.Delay(1000);
                DataBaseProvider.GenerateResources();
            }
        }

        private ValueOrError<string> RequestGetExecutor(string api, string id)
        {
            if (api.Equals(config.foodEndpoint))
                return DataBaseProvider.GetResource(ResourceType.Food, id);
            if (api.Equals(config.goldEndpoint))
                return DataBaseProvider.GetResource(ResourceType.Gold, id);
            if (api.Equals(config.woodEndpoint))
                return DataBaseProvider.GetResource(ResourceType.Wood, id);
            if (api.Equals(config.metalEndpoint))
                return DataBaseProvider.GetResource(ResourceType.Metal, id);
            if (api.Equals(config.rankingEndpoint))
                return ValueOrError<string>.CreateFromValue(DataBaseProvider.GetRanking(config.ranking.players));
                  
            return ValueOrError<string>.CreateFromError("Invalid request");
        }

        private ValueOrError<string> RequestPostExecutor(string api, string data)
        {
            if (api.Equals(config.loginEndpoint))
                return ValueOrError<string>.CreateFromValue(DataBaseProvider.Login(data));
            if (api.Equals(config.createUserEndpoint))
                return ValueOrError<string>.CreateFromValue(DataBaseProvider.CreateUser(data));
            
            return ValueOrError<string>.CreateFromError("Invalid request");
        }

        private void StopServer()
        {
            isRunning = false;
        }
    }
}