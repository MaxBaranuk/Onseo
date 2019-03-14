namespace DataModel
{
    public class Resource
    {
        public string UserId;
        public float Value;
        public ResourceType Type;
    }

    public enum ResourceType
    {
        Food, Gold, Metal, Wood 
    }
}