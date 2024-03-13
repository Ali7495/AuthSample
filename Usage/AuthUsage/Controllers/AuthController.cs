using AuthSample;
using AuthSample.Data_Object_Models;
using AuthUsage.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthUsage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticationManager _authManager;

        public AuthController(AuthenticationManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("")]
        public IActionResult AuthenticateToken([FromBody] AuthenticationInput authenticationInput)
        {
            AuthValidationResultDto result = _authManager.AuthenticateUserToken(authenticationInput.Token, authenticationInput.PublicKey);

            return Ok(result);
        }
    }
}
