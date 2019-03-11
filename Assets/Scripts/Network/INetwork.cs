
using System.Threading.Tasks;

namespace Network
{
    public interface INetwork
    {
        Task<ValueOrError<T>> Post<T>(string uri, T data);
        Task<ValueOrError<T>> Get<T>(string uri);
    }
}