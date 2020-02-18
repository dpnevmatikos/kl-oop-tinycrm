namespace TinyCrm.Core
{
    public class ApiResult<T>
    {
        public StatusCode ErrorCode { get; set; }
        public string ErrorText { get; set; }
        public T Data { get; set; }
        public bool Success => ErrorCode == StatusCode.Ok;

        public ApiResult()
        { }
        
        public ApiResult<U> ToResult<U>()
        {
            var res = new ApiResult<U>()
            {
                ErrorCode = ErrorCode,
                ErrorText = ErrorText
            };

            return res;
        }

        public ApiResult(StatusCode errorCode,
            string errorText)
        {
            ErrorCode = errorCode;
            ErrorText = errorText;
        }
    }
}
