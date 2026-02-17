namespace Application.DTO
{
    public class Result
    {
        public bool Success { get; private set; }
        public string? Error { get; private set; }

        protected Result(bool success, string? error)
        {
            Success = success;
            Error = error;
        }

        public static Result Fail(string error)
        {
            return new Result(false, error);
        }
        public static Result Ok()
        {
            return new Result(true, null);
        }
    }
}
