using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OMS.Common.Messages;
using System.Net;

namespace OMS.Common.Api.Helpers
{
    public static class ResponseHelper
    {
        public static string CreateResponse<T>(int code, string methodName, T data, string message = null, bool status = true)
        {
            var apiResponse = new ApiResponse<T>(code, methodName, data, message ?? GetResponseMessage(code), status);
            return SerializeResponse(apiResponse);
        }

        public static string CreateErrorResponse<T>(string methodName, T data, string message = null)
        {
            int internalServerErrorCode = (int)HttpStatusCode.InternalServerError;
            return CreateResponse<T>(internalServerErrorCode, methodName, data, message: message, status: false);
        }

        public static IActionResult CreateValidationResponse<T>(string methodName, T data)
        {
            return CreateValidationResponse(methodName, data, "One or more validation errors occurred.");
        }

        public static IActionResult CreateValidationResponse<T>(string methodName, T data, string message = null)
        {
            int badRequestCode = (int)HttpStatusCode.BadRequest;
            var apiResponse = CreateResponse<T>(badRequestCode, methodName, data, message: message, status: false);

            return new BadRequestObjectResult(apiResponse);
        }

        public static string SerializeResponse<T>(T response)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver(),
                Formatting = Formatting.Indented // Optional: For pretty-printing the JSON
            };

            return JsonConvert.SerializeObject(response, settings);
        }

        public static string GetResponseMessage(int code)
        {
            return MessageManager.GetMessage(code, MessageManager.UserResponseProvider);
        }
    }
}
