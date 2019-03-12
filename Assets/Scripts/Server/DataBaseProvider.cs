using DataModel;
using Newtonsoft.Json;
using UnityEngine;

namespace Server
{
    public static class DataBaseProvider
    {
        public static string GetFood(string id)
        { 
            var json = PlayerPrefs.GetString(id);
            var playerData = JsonConvert.DeserializeObject<PlayerData>(json);
            var food = new Food()
            {
                Value = playerData.food,
                userId = playerData.login
            };
            return JsonConvert.SerializeObject(food);
        }
        
        public static string GetGold(string id)
        { 
            var json = PlayerPrefs.GetString(id);
            var playerData = JsonConvert.DeserializeObject<PlayerData>(json);
            var gold = new Gold()
            {
                Value = playerData.gold,
                userId = playerData.login
            };
            return JsonConvert.SerializeObject(gold);
        }
        
        public static string GetMetal(string id)
        { 
            var json = PlayerPrefs.GetString(id);
            var playerData = JsonConvert.DeserializeObject<PlayerData>(json);
            var metal = new Metal()
            {
                Value = playerData.metal,
                userId = playerData.login
            };
            return JsonConvert.SerializeObject(metal);
        }
        
        public static string GetWood(string id)
        { 
            var json = PlayerPrefs.GetString(id);
            var playerData = JsonConvert.DeserializeObject<PlayerData>(json);
            var wood = new Wood()
            {
                Value = playerData.wood,
                userId = playerData.login
            };
            return JsonConvert.SerializeObject(wood);
        }
       
    }
}