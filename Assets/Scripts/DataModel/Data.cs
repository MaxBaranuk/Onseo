using System;
using UnityEngine;

namespace DataModel
{
    [Serializable]
    public class Data
    {
        [SerializeField]
        public string dataType;
        
        [SerializeField]
        public string endpoint;

        protected Data(){}  
    }
}