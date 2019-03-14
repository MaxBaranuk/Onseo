using System.Collections.Generic;
using System.Linq;
using DataModel;
using Network;
using Newtonsoft.Json;
using UnityEngine;

namespace Server
{
    public class LocalDataBase : IDatabase
    {
        private readonly NetworkConfig config;
        private readonly List<string> logInUsers = new List<string>();

        public LocalDataBase(NetworkConfig config)
        {
            this.config = config;
        }

        public ValueOrError<string> GetResource(ResourceType type, string id)
        {
            var json = PlayerPrefs.GetString(id);
            var playerData = JsonConvert.DeserializeObject<PlayerData>(json);
            var res = new Resource {Type = type};
            switch (type)
            {
                case ResourceType.Food:
                    res.Value = playerData.food;
                    break;
                case ResourceType.Gold:
                    res.Value = playerData.gold;
                    break;
                case ResourceType.Metal:
                    res.Value = playerData.metal;
                    break;
                case ResourceType.Wood:
                    res.Value = playerData.wood;
                    break;
                default:
                    return ValueOrError<string>.CreateFromError("Invalid request");
            }

            res.userId = id;
            return ValueOrError<string>.CreateFromValue(JsonConvert.SerializeObject(res));
        }
        
        public ValueOrError<string> PostResource(string resourceData)
        {
            var resource = JsonConvert.DeserializeObject<Resource>(resourceData);
            var json = PlayerPrefs.GetString(resource.userId);
            var playerData = JsonConvert.DeserializeObject<PlayerData>(json);
            switch (resource.Type)
            {
                case ResourceType.Food:
                    playerData.food = resource.Value;
                    break;
                case ResourceType.Gold:
                    playerData.gold = resource.Value;
                    break;
                case ResourceType.Metal:
                    playerData.metal = resource.Value;
                    break;
                case ResourceType.Wood:
                    playerData.wood = resource.Value;
                    break;
                default:
                    return ValueOrError<string>.CreateFromError("Invalid request");
            }
            PlayerPrefs.SetString(resource.userId, JsonConvert.SerializeObject(playerData));
            return ValueOrError<string>.CreateFromValue(JsonConvert.SerializeObject(resource));
        }

        
        public string GetRanking()
        {
            var rankUsers = new List<RankUser>();
            rankUsers.AddRange(config.ranking.players);
            var usersJson = PlayerPrefs.GetString(config.usersEndpoint);
            var list = JsonConvert.DeserializeObject<List<string>>(usersJson);
            var savedUsers = list.Select(id =>
            {
                var userDataJson = PlayerPrefs.GetString(id);
                var userData = JsonConvert.DeserializeObject<PlayerData>(userDataJson);
                return new RankUser {Name = id, Rank = userData.GetRank()};
            }).ToList();
            rankUsers.AddRange(savedUsers);
            rankUsers.Sort((user, rankUser) =>  user.Rank > rankUser.Rank ? -1 : 1);
            return JsonConvert.SerializeObject(rankUsers);
        }

        public ValueOrError<string> CreateUser(string data)
        {
            var credentials = JsonConvert.DeserializeObject<Credentials>(data);
            if (PlayerPrefs.HasKey(credentials.userId))
            {
                credentials.Status = Status.LogInInUse;
                return ValueOrError<string>.CreateFromValue(JsonConvert.SerializeObject(credentials));
            }

            credentials.Status = Status.Ok;
            
            var json = JsonConvert.SerializeObject(credentials);
            
            var playerData = new PlayerData()
            {
                password = credentials.Password
            };

            var dataJson = JsonConvert.SerializeObject(playerData);
            PlayerPrefs.SetString(credentials.userId, dataJson);
            AddUserId(credentials.userId);
            return ValueOrError<string>.CreateFromValue(json);
        }
        
        public ValueOrError<string> Login(string data)
        {
            var credentials = JsonConvert.DeserializeObject<Credentials>(data);
            if (!PlayerPrefs.HasKey(credentials.userId))
            {
                credentials.Status = Status.UnknownLogin;
                return ValueOrError<string>.CreateFromValue(JsonConvert.SerializeObject(credentials));
            }

            var usedDataJson = PlayerPrefs.GetString(credentials.userId);
            var usedData = JsonConvert.DeserializeObject<PlayerData>(usedDataJson);

            if (credentials.Status == Status.LogOut)
            {
                logInUsers.Remove(credentials.userId);
                credentials.Status = Status.Ok;
                return ValueOrError<string>.CreateFromValue(JsonConvert.SerializeObject(credentials));
            }
            
            var allowAccess = credentials.Password.Equals(usedData.password);
            credentials.Status = allowAccess ? Status.Ok : Status.IncorrectPassword;
            
            if (allowAccess)
                logInUsers.Add(credentials.userId);
            return ValueOrError<string>.CreateFromValue(JsonConvert.SerializeObject(credentials));
        }

        private void AddUserId(string id)
        {
            var list = new List<string>();
            if (PlayerPrefs.HasKey(config.usersEndpoint))
            {
                var usersJson = PlayerPrefs.GetString(config.usersEndpoint);
                list = JsonConvert.DeserializeObject<List<string>>(usersJson);
            }

            list.Add(id);
           
            var json = JsonConvert.SerializeObject(list);
            PlayerPrefs.SetString(config.usersEndpoint, json);
        }

        public void GenerateResources()
        {
            foreach (var userId in logInUsers)
            {
                var json = PlayerPrefs.GetString(userId);
                var playerData = JsonConvert.DeserializeObject<PlayerData>(json);
                playerData.food += 2;
                playerData.gold += 0.5f;
                playerData.wood += 1;
                playerData.metal += 1.2f;
                PlayerPrefs.SetString(userId, JsonConvert.SerializeObject(playerData));
            }
        }
    }
}