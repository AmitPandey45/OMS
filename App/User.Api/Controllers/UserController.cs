using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.Common.Api;
using OMS.Domain.User.Dto.User;
using OMS.Domain.User.Model.User;
using OMS.Service.User.Interface;

namespace User.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IHttpContextAccessor contextAccessor, ILogger<BaseController> logger, IUserService userService)
            : base(contextAccessor, logger)
        {
            _userService = userService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestModel request)
        {
            var validationResult = ValidateModel(request);
            if (!ModelState.IsValid && validationResult != null)
            {
                return validationResult;
            }

            var userDto = Mapper.Map<CreateUserRequestDto>(request);
            var user = Mapper.Map<GetUserResponseModel>(await _userService.CreateUserAsync(userDto));
            return CreateResponse(StatusCodes.Status201Created, user);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequestModel request)
        {
            var validationResult = ValidateModel(request);
            if (validationResult != null)
            {
                return validationResult;
            }

            var userDto = Mapper.Map<UpdateUserRequestDto>(request);
            var user = Mapper.Map<GetUserResponseModel>(await _userService.UpdateUserAsync(userDto));
            return user != null ? OkResponse(user) : NotFoundResponse();
        }

        [HttpGet("get/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = Mapper.Map<GetUserResponseModel>(await _userService.GetUserByIdAsync(userId));
            return user != null ? OkResponse(user) : NotFoundResponse();
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            var responseModels = Mapper.Map<List<GetUserResponseModel>>(users);
            return OkResponse(responseModels);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkCreateOrUpdate([FromBody] BulkUserRequestModel request)
        {
            var validationResult = ValidateModel(request);
            if (validationResult != null)
            {
                return OkResponse(false);
            }

            var response = await _userService.BulkCreateOrUpdateAsync(Mapper.Map<BulkUserRequestDto>(request));
            return OkResponse(response);
        }
    }

}
