using AuthSample.Data_Object_Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace AuthSample
{
    public class TokenValidator
    {
        

        public TokenValidationResultDto ValidateToken(string token, string publicKey)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            RSA rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out int _);

            TokenValidationParameters tokenParameter = new()
            {
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new RsaSecurityKey(rsa),
                //ValidateIssuer = true,
                //ValidateAudience = true,
                RequireExpirationTime = true,
                ValidateLifetime = true,
            };

            try
            {
                SecurityToken validatedToken;

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenParameter, out validatedToken);

                string userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                string session = principal.FindFirst("Session")?.Value;
                string expirationDate = principal.FindFirst("exp")?.Value;

                return new() { UserId = userId, Session = session, Token = token, SessionExpirationDate = Convert.ToDateTime(expirationDate), Status = Enums.ValidationStatus.Success };
            }

            catch (SecurityTokenExpiredException)
            {
                return new() { Status = Enums.ValidationStatus.Failed, Message = "The token has expired" };
            }
            catch (SecurityTokenInvalidLifetimeException)
            {
                return new() { Status = Enums.ValidationStatus.Failed, Message = "Invalid token life time" };
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                return new() { Status = Enums.ValidationStatus.Failed, Message = "The signature of the token is invalid" };
            }
            catch (SecurityTokenValidationException)
            {
                return new() { Status = Enums.ValidationStatus.Failed, Message = "The token is invalid" };
            }
            catch (Exception)
            {
                return new() { Status = Enums.ValidationStatus.Failed, Message = "Something is wrong ..." }; 
            }
        }
    }
}
