using System;

namespace Server
{
    [Serializable]
    public class PlayerData
    {
        public string login;
        public string password;
        public float food;
        public float gold;
        public float metal;
        public float wood;       
    }
}