using AuthSample;
using AuthSample.Data_Object_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthUsage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticationManager _authManager;
        private readonly IConfiguration _configuration;

        public AuthController(AuthenticationManager authManager, IConfiguration configuration)
        {
            _authManager = authManager;
            _configuration = configuration;
        }

        [HttpPost("")]
        public IActionResult AuthenticateToken()
        {
            string token = HttpContext.Items["Token"] as string;
            string publicKey = _configuration["PublicKey"];

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(publicKey))
            {
                return Unauthorized();
            }

            AuthValidationResultDto result = _authManager.AuthenticateUserToken(token, publicKey);

            return Ok(result);
        }
    }
}
