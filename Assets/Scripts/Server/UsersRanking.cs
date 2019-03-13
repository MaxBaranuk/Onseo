using System.Collections.Generic;
using UnityEngine;

namespace Server
{
    [CreateAssetMenu(fileName = "New users ranking", menuName = "Users Ranking", order = 51)]
    public class UsersRanking : ScriptableObject
    {
        public List<RankUser> players;
    }
}