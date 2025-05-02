using Azure.Core;
using EasyTesting.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace EasyTesting.Web.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        private readonly TokenService _tokenService;

        protected BaseApiController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        protected int? GetUserId()
        {
            return _tokenService.GetUserIdFromToken(User);
        }

        protected int? GetTeacherId()
        {
            return _tokenService.GetTeacherIdFromToken(User);
        }
    }
}
