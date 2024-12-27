namespace Easy.Core.Result
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }

        protected Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result Fail(string error)
        {
            return new Result(false, error);
        }
    }
}
