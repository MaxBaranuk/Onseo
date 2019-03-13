using System.Collections.Generic;
using DataModel;
using Network;
using Newtonsoft.Json;
using UnityEngine;

namespace Server
{
    public static class DataBaseProvider
    {
        private static readonly List<string> LogInUsers = new List<string>();
        public static ValueOrError<string> GetResource(ResourceType type, string id)
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

        public static string GetRanking(List<RankUser> players)
        {   
            return JsonConvert.SerializeObject(players);
        }

        public static string CreateUser(string data)
        {
            var credentials = JsonConvert.DeserializeObject<Credentials>(data);
            if (PlayerPrefs.HasKey(credentials.userId))
            {
                credentials.Status = Status.LogInInUse;
                return JsonConvert.SerializeObject(credentials);
            }

            credentials.Status = Status.Ok;
            
            var json = JsonConvert.SerializeObject(credentials);
            
            var playerData = new PlayerData()
            {
                password = credentials.Password
            };

            var dataJson = JsonConvert.SerializeObject(playerData);
            PlayerPrefs.SetString(credentials.userId, dataJson);
            return json;
        }
        
        public static string Login(string data)
        {
            var credentials = JsonConvert.DeserializeObject<Credentials>(data);
            if (!PlayerPrefs.HasKey(credentials.userId))
            {
                credentials.Status = Status.UnknownLogin;
                return JsonConvert.SerializeObject(credentials);
            }

            var usedDataJson = PlayerPrefs.GetString(credentials.userId);
            var usedData = JsonConvert.DeserializeObject<PlayerData>(usedDataJson);

            if (credentials.Status == Status.LogOut)
            {
                LogInUsers.Remove(credentials.userId);
                credentials.Status = Status.Ok;
                return JsonConvert.SerializeObject(credentials);
            }
            
            var allowAccess = credentials.Password.Equals(usedData.password);
            credentials.Status = allowAccess ? Status.Ok : Status.IncorrectPassword;
            
            if (allowAccess)
                LogInUsers.Add(credentials.userId);
            return JsonConvert.SerializeObject(credentials);
        }

        public static void GenerateResources()
        {
            foreach (var userId in LogInUsers)
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