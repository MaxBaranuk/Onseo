using System;

namespace Server
{
    [Serializable]
    public class PlayerData
    {
        private string login;
        private string password;
        public float food;
        public float gold;
        public float metal;
        public float wood;

        public string Login => login;
        public int GetRank()
        {
            return (int) (food + gold + metal + wood);
        }
    }
}