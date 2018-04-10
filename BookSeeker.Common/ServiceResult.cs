namespace BookSeeker.Common
{
    public class ServiceResult
    {
        public bool Succeeded { get; }
        public string ErrorMessage { get; }

        protected ServiceResult(bool succeeded, string errorMessage)
        {
            Succeeded = succeeded;
            ErrorMessage = errorMessage;
        }

        public static ServiceResult Success() => new ServiceResult(true, string.Empty);

        public static ServiceResult Fail(string errorMessage = null, string diagnosticMessage = null) => new ServiceResult(false, errorMessage ?? string.Empty);
    }

    public class ServiceResult<TData> : ServiceResult
    {
        public TData Data { get; }

        protected ServiceResult(bool succeeded, string errorMessage, TData data) : base(succeeded, errorMessage)
        {
            Data = data;
        }

        public static ServiceResult<TData> Success(TData result) => new ServiceResult<TData>(true, string.Empty, result);

        public static ServiceResult<TData> Fail(string errorMessage = null) => new ServiceResult<TData>(false, errorMessage ?? string.Empty, default(TData));
    }
}