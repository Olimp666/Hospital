namespace Domain
{
    public class Result
    {
        protected Result(bool res, string message)
        {
            if (!res && message == string.Empty)
                throw new InvalidOperationException();
            Res = res;
            Message = message;
        }

        public bool Res { get; }
        public string Message { get; }
        public bool IsFailure => !Res;

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default, false, message);
        }

        public static Result Success()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }
    }

    public class Result<T> : Result
    {
        protected internal Result(T value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}