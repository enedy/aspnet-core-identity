using AspnetCoreIdentity.Api.Controllers.Shared;
using AspnetCoreIdentity.Identity.DTOs.Request;
using AspnetCoreIdentity.Identity.DTOs.Response;
using AspnetCoreIdentity.Identity.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspnetCoreIdentity.Api.Controllers.v1
{
    [Route("api/v1/users")]
    public class UsersController : ApiControllerBase
    {
        private IIdentityService _identityService;

        public UsersController(IIdentityService identityService) =>
            _identityService = identityService;

        [HttpPost("create")]
        public async Task<ActionResult<CreateUserResponseDTO>> Create(CreateUserRequestDTO createUserDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _identityService.CreateUserAsync(createUserDTO);
            if (result.Success)
                return Ok(result);
            else if (result.Errors.Any())
                return BadRequest(result);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginReponseDTO>> Login(UserLoginRequestDTO userLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _identityService.Login(userLogin);
            if (result.Success)
                return Ok(result);

            return Unauthorized(result);
        }

        [Authorize]
        [HttpPost("refresh-login")]
        public async Task<ActionResult<UserLoginReponseDTO>> RefreshLogin()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return BadRequest();

            var result = await _identityService.LoginWithoutPwd(userId);
            if (result.Success)
                return Ok(result);

            return Unauthorized(result);
        }
    }
}
