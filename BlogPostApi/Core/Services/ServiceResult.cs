namespace BlogPostApi.Core.Services
{
    public class ServiceResult<T>
    {
        public bool Success { get; init; }
        public IReadOnlyList<string>? ErrorMessages { get; init; } = Array.Empty<string>();
        public T? Data { get; init; }

        public static ServiceResult<T> Ok(T data) => new()
        {
            Success = true,
            Data = data
        };


        public static ServiceResult<T> Fail(IEnumerable<string> errors) => new()
        {
            Success = false,
            ErrorMessages = errors.ToList()
        };

        public static ServiceResult<T> Fail(string error) => new()
        {
            Success = false,
            ErrorMessages = new[] { error }
        };
    }

}
