using OMS.Common.Messages;

namespace OMS.Common
{
    public class ApiResponse<T>
    {
        public ApiResponse(int code, string method, T data, string message = null, bool status = true, string uniqueRequestId = null)
        {
            MethodName = method;
            Status = status;
            StatusCode = code;
            Message = message ?? MessageManager.GetMessage(code, MessageManager.UserResponseProvider);
            Result = data;
            UniqueRequestId = uniqueRequestId;
        }

        public string MethodName { get; set; }
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public string UniqueRequestId { get; set; }
    }
}
