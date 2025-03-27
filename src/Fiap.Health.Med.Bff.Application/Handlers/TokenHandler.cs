using Fiap.Health.Med.Bff.Application.DTOs.Auth.UserSearch;
using Fiap.Health.Med.Bff.Application.Interfaces.Auth;
using Fiap.Health.Med.Bff.CrossCutting.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fiap.Health.Med.Bff.Application.Handlers
{
    public class TokenHandler : ITokenHandler
    {
        private readonly SecuritySettings _jwtSettings;

        public TokenHandler(IOptions<SecuritySettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(UserSearchResponseDTO userData)
        {
            //TO-DO: Armazenar e recuperar chave e o tempo de expiração(parameter store?)
            var securityKey = Encoding.ASCII.GetBytes(_jwtSettings.JwtSecurityKey);
            var tokenExpires = _jwtSettings.JwtTokenExpiresMinutes;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenProperties = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, userData.Username),
                        new Claim(ClaimTypes.Role, userData.UserType.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenExpires),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(securityKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenProperties);

            return tokenHandler.WriteToken(token);
        }
    }
}
