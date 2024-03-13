using AuthSample.Data_Object_Models;
using AuthSample.MocksInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthSample
{
    public class AuthenticationManager
    {
        private readonly TokenValidator _tokenValidator;
        private readonly ISessionServices _sessionServices;
        private readonly Dictionary<string, DateTime> sessionCache = new();

        public AuthenticationManager(TokenValidator tokenValidator, ISessionServices sessionServices)
        {
            _tokenValidator = tokenValidator;
            _sessionServices = sessionServices;
        }

        public AuthValidationResultDto AuthenticateUserToken(string token, string publicKey)
        {
            TokenValidationResultDto tokenValidationResult = _tokenValidator.ValidateToken(token, publicKey);

            if (tokenValidationResult.Status != Enums.ValidationStatus.Success)
            {
                return new() { Status = Enums.ValidationStatus.Failed, Message = "Authentication failed \r\n" + tokenValidationResult.Message };
            }

            if (!sessionCache.ContainsKey(tokenValidationResult.Session) && !string.IsNullOrEmpty(tokenValidationResult.Session))
            {

                bool validSession = _sessionServices.VerifySessionExpiration(tokenValidationResult.SessionExpirationDate);

                if (!validSession)
                {
                    return new() { Status = Enums.ValidationStatus.Failed, Message = "Authentication failed \r\n The session is not valid" };
                }

                sessionCache.Add(tokenValidationResult.Session, DateTime.Now);
            }


            return new() { Status = Enums.ValidationStatus.Success, UserId = tokenValidationResult.UserId, Session = tokenValidationResult.Session };
        }
    }
}
