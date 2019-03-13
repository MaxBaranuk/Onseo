namespace DataModel
{
    public class Resource : Dto
    {
        public float Value;
        public ResourceType Type;
    }

    public enum ResourceType
    {
        Food, Gold, Metal, Wood 
    }
}