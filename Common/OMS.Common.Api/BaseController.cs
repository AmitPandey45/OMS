using Microsoft.AspNetCore.Mvc;
using OMS.Common.Api.Helpers;
using OMS.Common.Api.Validations;
using System.Net;

namespace OMS.Common.Api
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly UserContext _userContext;
        protected readonly ILogger<BaseController> _logger;

        public BaseController(IHttpContextAccessor contextAccessor, ILogger<BaseController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userContext = GetUserContext(contextAccessor.HttpContext);
        }

        private UserContext GetUserContext(HttpContext context)
        {
            if (context == null) return new UserContext();

            return new UserContext
            {
                UserId = int.TryParse(context.Request.Headers["UserId"], out var id) ? id : 0,
                UserName = context.Request.Headers["UserName"],
                ClientIp = context.Connection.RemoteIpAddress?.ToString(),
                IsTestUser = bool.TryParse(context.Request.Headers["Is-Test-Mode"], out var isTest) && isTest
            };
        }

        protected IActionResult ValidateModel<TRequest>(TRequest model)
        {
            return ValidationManager.Validate<TRequest>(model, GetCurrentMethodName());
        }

        protected IActionResult CreateResponse<T>(int code, T data)
        {
            var response = ResponseHelper.CreateResponse(code, GetCurrentMethodName(), data);
            return StatusCode(code, response);
        }

        protected IActionResult BadRequestResponse()
        {
            int badRequestCode = (int)HttpStatusCode.BadRequest;
            return DefaultStringResponse(badRequestCode);
        }

        protected IActionResult NotFoundResponse()
        {
            int notFoundCode = (int)HttpStatusCode.NotFound;
            return DefaultStringResponse(notFoundCode);
        }

        protected IActionResult NoContentResponse<T>(T results)
        {
            int noContentCode = (int)HttpStatusCode.NoContent;
            return DefaultStringResponse(noContentCode);
        }

        protected IActionResult OkResponse<T>(T results)
        {
            int okCode = (int)HttpStatusCode.OK;
            return DefaultStringResponse(okCode);
        }

        protected new IActionResult Ok(object value)
        {
            int okCode = (int)HttpStatusCode.OK;
            return DefaultStringResponse(okCode);
        }

        private IActionResult DefaultStringResponse(int code)
        {
            return CreateResponse<string>(code, null);
        }

        private string GetCurrentMethodName()
        {
            return $"{ControllerContext.ActionDescriptor.ControllerName}.{ControllerContext.ActionDescriptor.ActionName}";
        }
    }
}
