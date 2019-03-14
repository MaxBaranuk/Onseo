namespace DataModel
{
    public class Credentials
    {
        public string UserId;
        public string Password;
        public Status Status;
    }

    public enum Status
    {
        Ok, IncorrectPassword, UnknownLogin, LogInInUse, LogOut
    }
}