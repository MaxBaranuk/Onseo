using System.Threading.Tasks;
using DataModel;
using UnityEngine;

namespace Network
{
    public class Network : INetwork
    {
        public Task<ValueOrError<T>> Post<T>(string uri, T data)
        {
            throw new System.NotImplementedException();
        }

        public Task<ValueOrError<T>> Get<T>(string uri)
        {
            throw new System.NotImplementedException();
        }
    }
}