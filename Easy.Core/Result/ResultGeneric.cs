namespace Easy.Core.Result
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T Value { get; private set; }
        public string Error { get; private set; }

        protected Result(bool isSuccess, T value, string error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Ok(T value)
        {
            return new Result<T>(true, value, string.Empty);
        }

        public static Result<T> Fail(string error)
        {
            return new Result<T>(false, default(T), error);
        }
    }
}
