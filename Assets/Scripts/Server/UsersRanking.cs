using System.Collections.Generic;
using DataModel;
using UnityEngine;

namespace Server
{
    [CreateAssetMenu(fileName = "New users ranking", menuName = "Users Ranking", order = 51)]
    public class UsersRanking : ScriptableObject
    {
        public List<RankUser> players;
    }
}