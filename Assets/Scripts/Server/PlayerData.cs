using System;

namespace Server
{
    [Serializable]
    public class PlayerData
    {
        public string password;
        public float food;
        public float gold;
        public float metal;
        public float wood;

        public int GetRank()
        {
            return (int) (food + gold + metal + wood);
        }
    }
}