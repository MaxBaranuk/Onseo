using System.Threading.Tasks;
using Newtonsoft.Json;
using Server;

namespace Network
{
    public class MockNetwork : INetwork
    {
        public MockNetwork(NetworkConfig config)
        {
            ServerProvider.StartMockServer(config);
        }

        public async Task<ValueOrError<T>> Post<T>(string uri, T data)
        {
            if (string.IsNullOrEmpty(uri))
                return ValueOrError<T>.CreateFromError("Empty Uri");
            
            var json =  JsonConvert.SerializeObject(data);       
            var res = await ServerProvider.ServerRequest(uri, RequestType.Post, json);
            
            data = JsonConvert.DeserializeObject<T>(res.Value);
            return res.IsError 
                ? ValueOrError<T>.CreateFromError(res.ErrorMessage)
                : ValueOrError<T>.CreateFromValue(data);
        }

        public async Task<ValueOrError<T>> Get<T>(string uri)
        {
            if (string.IsNullOrEmpty(uri))
                return ValueOrError<T>.CreateFromError("Empty Uri");
            
            var res = await ServerProvider.ServerRequest(uri, RequestType.Get);

            if (res.IsError)
                return ValueOrError<T>.CreateFromError(res.ErrorMessage);

            var model = JsonConvert.DeserializeObject<T>(res.Value);
            
            return ValueOrError<T>.CreateFromValue(model);
        }
    }
}