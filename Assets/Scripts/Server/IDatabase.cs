using DataModel;
using Network;

namespace Server
{
    public interface IDatabase
    {
        ValueOrError<string> GetResource(ResourceType type, string id);
        ValueOrError<string> PostResource(string resourceData);
        string GetRanking();
        ValueOrError<string> Login(string data);
        ValueOrError<string> CreateUser(string data);
        void GenerateResources();
    }
}