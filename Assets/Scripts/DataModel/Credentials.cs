namespace DataModel
{
    public class Credentials : Dto
    {
        public string Password;
        public Status Status;
    }

    public enum Status
    {
        Ok, IncorrectPassword, UnknownLogin, LogInInUse, LogOut
    }
}