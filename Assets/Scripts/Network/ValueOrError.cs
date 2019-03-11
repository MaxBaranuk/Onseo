namespace Network
{
    public class ValueOrError<T>
    {
        public bool IsError => isError;
        public T Value => value;
        public string ErrorMessage => message;
        
        
        private bool isError;
        private T value;
        private string message;

        private ValueOrError(bool isError)
        {
            this.isError = isError;
        }

        public static ValueOrError<T> CreateFromValue(T value)
        {
            return new ValueOrError<T>(false) {value = value};;
        }
        
        public static ValueOrError<T> CreateFromError(string errorMessage)
        {
            return new ValueOrError<T>(true) {message = errorMessage};
        }
    }
}