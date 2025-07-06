namespace _011Global.Shared.Patterns
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? ErrorMessage { get; }

        private Result(bool isSuccess, T? value, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T? value, string? errorMessage = null) => new Result<T>(true, value, errorMessage);
        public static Result<T> Failure(string? errorMessage) => new Result<T>(false, default, errorMessage);
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public string? Message { get; }
        public string? ErrorMessage { get; }

        private Result(bool isSuccess, string? message, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Message = message;
            ErrorMessage = errorMessage;
        }

        public static Result Success(string? message = null) => new Result(true, message, null);
        public static Result Failure(string? errorMessage) => new Result(false, null, errorMessage);
    }
}
