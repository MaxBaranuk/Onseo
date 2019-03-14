using System;
using UnityEngine.Serialization;

namespace DataModel
{
    [Serializable]
    public class RankUser
    {
        [FormerlySerializedAs("Name")] public string name;
        [FormerlySerializedAs("Rank")] public int rank;
    }
}