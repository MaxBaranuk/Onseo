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
        private readonly IDatabase database;
        private bool isRunning;
        
        public LocalServer(NetworkConfig config, IDatabase database)
        {
            this.config = config;
            this.database = database;            
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
            return RequestGetExecutor(uri);
        } 

        private async void StartServer()
        {
            isRunning = true;

            while (isRunning)
            {
                await Task.Delay(1000);
                database.GenerateResources();
            }
        }

        private ValueOrError<string> RequestGetExecutor(string uri)
        {
            if (Random.value < 0.05f)  // simulate some unexpected error 
                return ValueOrError<string>.CreateFromError("Network error");
            
            var segments = uri.Split('/');
            if (segments[0].Equals(config.resourceEndpoint))
            {
                var id = segments[1];
                var type = segments[2];
                if (type == config.foodEndpoint)
                    return database.GetResource(ResourceType.Food, id);
                if (type == config.goldEndpoint)
                    return database.GetResource(ResourceType.Gold, id);
                if (type == config.woodEndpoint)
                    return database.GetResource(ResourceType.Wood, id);
                if (type == config.metalEndpoint)
                    return database.GetResource(ResourceType.Metal, id);
                
                return ValueOrError<string>.CreateFromError("Invalid request");
            }
            if (segments[0].Equals(config.rankingEndpoint))
                return ValueOrError<string>.CreateFromValue(database.GetRanking());
                  
            return ValueOrError<string>.CreateFromError("Invalid request");
        }

        private ValueOrError<string> RequestPostExecutor(string api, string data)
        {
            if (Random.value < 0.05f)  // simulate some unexpected error 
                return ValueOrError<string>.CreateFromError("Network error");             
            if (api.Equals(config.loginEndpoint))
                return database.Login(data);
            if (api.Equals(config.createUserEndpoint))
                return database.CreateUser(data);
            if (api.Equals(config.resourceEndpoint))
                return database.PostResource(data);
            
            return ValueOrError<string>.CreateFromError("Invalid request");
        }

        private void StopServer()
        {
            isRunning = false;
            PlayerPrefs.Save();
        }
    }
}