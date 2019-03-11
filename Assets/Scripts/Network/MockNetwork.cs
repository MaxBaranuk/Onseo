using System.Threading.Tasks;
using Server;
using UnityEngine;

namespace Network
{
    public class MockNetwork : INetwork
    {
        public MockNetwork()
        {
            ServerProvider.StartMockServer();
        }

        public async Task<ValueOrError<T>> Post<T>(string uri, T data)
        {
            if (string.IsNullOrEmpty(uri))
                return ValueOrError<T>.CreateFromError("Empty Uri");
            
            var json = JsonUtility.ToJson(data);       
            var res = await ServerProvider.ServerRequest(RequestType.Post, json);
            
            return res.IsError 
                ? ValueOrError<T>.CreateFromError(res.ErrorMessage)
                : ValueOrError<T>.CreateFromValue(data);
        }

        public async Task<ValueOrError<T>> Get<T>(string uri)
        {
            if (string.IsNullOrEmpty(uri))
                return ValueOrError<T>.CreateFromError("Empty Uri");
            
            var res = await ServerProvider.ServerRequest(RequestType.Get);

            if (res.IsError)
                return ValueOrError<T>.CreateFromError(res.ErrorMessage);
                        
            var model = JsonUtility.FromJson<T>(res.Value);
            
            return ValueOrError<T>.CreateFromValue(model);
        }
    }
}