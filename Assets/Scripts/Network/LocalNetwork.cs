using System.Threading.Tasks;
using Newtonsoft.Json;
using Server;

namespace Network
{
    public class LocalNetwork : INetwork
    {
        private static LocalServer _server;
        public LocalNetwork(NetworkConfig config)
        {
            _server = new LocalServer(config, new LocalDataBase(config));
        }

        public async Task<ValueOrError<T>> Post<T>(string uri, T data)
        {
            if (string.IsNullOrEmpty(uri))
                return ValueOrError<T>.CreateFromError("Empty Uri");
            
            var json =  JsonConvert.SerializeObject(data);       
            var res = await _server.Post(uri,json);

            if (res.IsError)
                return ValueOrError<T>.CreateFromError(res.ErrorMessage);
            
            data = JsonConvert.DeserializeObject<T>(res.Value);
            return ValueOrError<T>.CreateFromValue(data);
        }

        public async Task<ValueOrError<T>> Get<T>(string uri)
        {
            if (string.IsNullOrEmpty(uri))
                return ValueOrError<T>.CreateFromError("Empty Uri");
            
            var res = await _server.Get(uri);

            if (res.IsError)
                return ValueOrError<T>.CreateFromError(res.ErrorMessage);

            var model = JsonConvert.DeserializeObject<T>(res.Value);
            
            return ValueOrError<T>.CreateFromValue(model);
        }
    }
}