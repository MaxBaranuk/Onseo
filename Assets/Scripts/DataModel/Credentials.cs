namespace DataModel
{
    public class Credentials : Dto
    {
        public string Login;
        public string Password;
        public Status Status;
    }

    public enum Status
    {
        Ok, IncorrectPassword, UnknownLogin, LogInInUse, LogOut
    }
}